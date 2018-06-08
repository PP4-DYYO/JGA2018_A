//----------------------------------------------------------------------------------------------------
//
//2018年5月8日～
//作成者　京都コンピュータ学院京都駅前校　ゲーム学科　4回生　奥田裕也
//編集者
//
//----------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤークラス
/// </summary>
public class MyPlayer : MonoBehaviour
{
	#region 外部のインスタンス
	/// <summary>
	/// キャラクター
	/// </summary>
	[SerializeField]
	MyCharacter myCharacter;

	/// <summary>
	/// カメラ
	/// </summary>
	Camera m_camera;
	#endregion

	#region コンポーネント
	/// <summary>
	/// リジッドボディ
	/// </summary>
	[SerializeField]
	Rigidbody RB;

	/// <summary>
	/// アニメータ
	/// </summary>
	[SerializeField]
	Animator Anim;
	#endregion

	#region アニメーション
	/// <summary>
	/// 無操作遷移
	/// </summary>
	const string TRANS_IDLE = "Idle";

	/// <summary>
	/// 歩く遷移
	/// </summary>
	const string TRANS_WALK = "Walk";

	/// <summary>
	/// 攻撃１のパターンA遷移
	/// </summary>
	const string TRANS_ATTACK1A = "Attack1A";

	/// <summary>
	/// 攻撃１のパターンB遷移
	/// </summary>
	const string TRANS_ATTACK1B = "Attack1B";

	/// <summary>
	/// 攻撃１のパターンC遷移
	/// </summary>
	const string TRANS_ATTACK1C = "Attack1C";

	/// <summary>
	/// 攻撃２遷移
	/// </summary>
	const string TRANS_ATTACK2 = "Attack2";

	/// <summary>
	/// 攻撃時間
	/// </summary>
	const float ATTACK_TIME = 1.125f;
	#endregion

	#region 状態
	/// <summary>
	/// 状態
	/// </summary>
	enum Status
	{
		/// <summary>
		/// 無操作
		/// </summary>
		Idle,
		/// <summary>
		/// 徒歩
		/// </summary>
		Walk,
		/// <summary>
		/// 攻撃１のAパターン
		/// </summary>
		Attack1A,
		/// <summary>
		/// 攻撃１のBパターン
		/// </summary>
		Attack1B,
		/// <summary>
		/// 攻撃１のCパターン
		/// </summary>
		Attack1C,
		/// <summary>
		/// 攻撃２
		/// </summary>
		Attack2,
	}

	/// <summary>
	/// 現在の状態
	/// </summary>
	Status m_state;

	/// <summary>
	/// フレーム前の状態
	/// </summary>
	Status m_statePrev;
	#endregion

	#region トランスポート
	/// <summary>
	/// フレーム前の位置
	/// </summary>
	Vector3 m_posPrev;

	/// <summary>
	/// 移動量
	/// </summary>
	Vector3 m_direction;

	/// <summary>
	/// 角度
	/// </summary>
	float m_angle;

	/// <summary>
	/// 回転スピード
	/// </summary>
	[SerializeField]
	float m_rotationSpeed;

	/// <summary>
	/// １周の角度
	/// </summary>
	const int ONE_TURNING_ANGLE = 360;

	/// <summary>
	/// 半周の角度
	/// </summary>
	const int HALF_CIRCUMFERENCE_ANGLE = 180;

	/// <summary>
	/// 補正角度
	/// </summary>
	const int CORRECTION_ANGLE = 5;
	#endregion

	#region 移動速度
	/// <summary>
	/// 速度
	/// </summary>
	float m_speed;

	/// <summary>
	/// 徒歩速度
	/// </summary>
	[SerializeField]
	float m_walkSpeed;
	#endregion

	#region ジャンプ
	/// <summary>
	/// ジャンプしている
	/// </summary>
	bool m_isJump;

	/// <summary>
	/// ジャンプ力
	/// </summary>
	[SerializeField]
	float m_jumpingPower;

	/// <summary>
	/// ジャンプで力を入れている時間
	/// </summary>
	float m_jumpForceCountTime;

	/// <summary>
	/// ジャンプで力を入れる時間
	/// </summary>
	[SerializeField]
	float m_jumpForceTime;
	#endregion

	#region 攻撃
	/// <summary>
	/// 攻撃の連続回数
	/// </summary>
	int m_attackCount;

	/// <summary>
	/// 連続攻撃の上限数
	/// </summary>
	const int CONSECUTIVE_ATTACK_LIMIT_NUM = 3;

	/// <summary>
	/// 攻撃の時間
	/// </summary>
	float m_attackTime;

	/// <summary>
	/// 攻撃の有効時間
	/// </summary>
	[SerializeField]
	float m_effectiveAttackTime;

	/// <summary>
	/// 攻撃２のコンボ数
	/// </summary>
	int m_numAttack2Combo;

	/// <summary>
	/// コンボ間の時間
	/// </summary>
	const float COMBO_TIME = 0.75f;

	/// <summary>
	/// 攻撃休憩時間
	/// </summary>
	float m_attackBreakTime;

	/// <summary>
	/// 攻撃の後休憩する時間
	/// </summary>
	const float ATTACK_BREAK_TIME = 1.0f;
	#endregion

	#region 攻撃範囲
	/// <summary>
	/// 攻撃１のAパターンの頂点
	/// </summary>
	static readonly Vector3[] ATTACK1_A_VERTECES =
	{
		new Vector3(0.08698074f, 2.237455f, 1.532429f),
		new Vector3(0.1659906f, 1.311585f, 0.6939639f),
		new Vector3(-0.6301885f, 0.4820206f, 1.491178f),
		new Vector3(-0.04720514f, 1.017675f, 0.5212768f),
		new Vector3(-0.04720514f, 1.017675f, 0.5212768f),
		new Vector3(-0.04720514f, 1.017675f, 0.5212768f),
		new Vector3(-0.04720514f, 1.017675f, 0.5212768f),
		new Vector3(-0.04720514f, 1.017675f, 0.5212768f),
	};

	/// <summary>
	/// 攻撃１のBパターンの頂点
	/// </summary>
	static readonly Vector3[] ATTACK1_B_VERTECES =
	{
		new Vector3(-1.086515f, 0.9399486f, 0.6289284f),
		new Vector3(0.1558482f, 0.9843702f,0.519088f),
		new Vector3(1.140949f,1.735734f,1.729904f),
		new Vector3(0.6208854f,1.634966f,0.604331f),
		new Vector3(0.6208854f,1.634966f,0.604331f),
		new Vector3(0.6208854f,1.634966f,0.604331f),
		new Vector3(0.6208854f,1.634966f,0.604331f),
		new Vector3(0.6208854f,1.634966f,0.604331f),
	};

	/// <summary>
	/// 攻撃１のCパターンの頂点
	/// </summary>
	static readonly Vector3[] ATTACK1_C_VERTECES =
	{
		new Vector3(-1.76715f,0.6671497f,0.5825509f),
		new Vector3(-0.658033f,1.108374f,0.2252046f),
		new Vector3(-0.5207606f,0.5439724f,-1.696099f),
		new Vector3(-0.2424484f,1.081343f,-0.624122f),
		new Vector3(1.695093f,0.4451257f,-0.3083975f),
		new Vector3(0.6111093f,1.063734f,-0.1974359f),
		new Vector3(0.0841179f,0.4025702f,1.660222f),
		new Vector3(0.1402777f,1.054813f,0.6047654f),
	};

	/// <summary>
	/// 攻撃２の頂点
	/// </summary>
	static readonly Vector3[] ATTACK2_VERTECES =
	{
		new Vector3(0.3140665f,0.9749715f,1.035166f),
		new Vector3(0.3246632f,1.034966f,-0.009343892f),
		new Vector3(0.25f,1.330896f,1.93709f),
		new Vector3(0.25f,1.284438f,0.6949587f),
		new Vector3(0.25f,1.284438f,0.6949587f),
		new Vector3(0.25f,1.284438f,0.6949587f),
		new Vector3(0.25f,1.284438f,0.6949587f),
		new Vector3(0.25f,1.284438f,0.6949587f),
	};
	#endregion

	#region キーボード関係
	/// <summary>
	/// 水平移動軸
	/// </summary>
	const string HORIZONTAL = "Horizontal";

	/// <summary>
	/// 垂直移動軸
	/// </summary>
	const string VERTICAL = "Vertical";

	/// <summary>
	/// ジャンプ軸
	/// </summary>
	const string JUMP = "Jump";

	/// <summary>
	/// 攻撃１
	/// </summary>
	const string ATTACK1 = "Attack1";

	/// <summary>
	/// 攻撃２
	/// </summary>
	const string ATTACK2 = "Attack2";

	/// <summary>
	/// スペースキーを押された
	/// </summary>
	bool m_isPressedSpace = false;

	/// <summary>
	/// 左クリックを押された
	/// </summary>
	bool m_isPressedLeftClick = false;

	/// <summary>
	/// 右クリックを押された
	/// </summary>
	bool m_isPressedRightClick = false;
	#endregion

	#region 作業用
	/// <summary>
	/// 作業用のVector３
	/// </summary>
	Vector3 m_workVector3;

	/// <summary>
	/// 作業用のVector３配列
	/// </summary>
	Vector3[] m_workVector3Array =
	{
		Vector3.zero,
		Vector3.zero,
		Vector3.zero,
		Vector3.zero,
		Vector3.zero,
		Vector3.zero,
		Vector3.zero,
		Vector3.zero,
	};

	/// <summary>
	/// 作業用のMatrix
	/// </summary>
	Matrix4x4 m_workMatrix = new Matrix4x4();

	/// <summary>
	/// 作業用の直方体
	/// </summary>
	MyCube m_workMyCube = new MyCube();
	#endregion

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 初期
	/// </summary>
	void Start()
	{
		//アクセスしやすいように
		m_camera = myCharacter.GameScript.CameraScript;

		m_attackCount = 0;
		m_numAttack2Combo = 1;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// フレーム
	/// </summary>
	void Update()
	{
		//キーの状態を調べる
		CheckKeyStatus();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// キーの状態を調べる
	/// </summary>
	void CheckKeyStatus()
	{
		//スペースキーの押下
		if (Input.GetButtonDown(JUMP))
			m_isPressedSpace = true;

		//左クリックの押下
		if (Input.GetButtonDown(ATTACK1))
			m_isPressedLeftClick = true;

		//右クリックの押下
		if (Input.GetButtonDown(ATTACK2))
			m_isPressedRightClick = true;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 定期フレーム
	/// </summary>
	void FixedUpdate()
	{
		//攻撃休憩時間
		if (m_attackBreakTime > 0)
		{
			m_attackBreakTime -= Time.deltaTime;

			//攻撃休憩時間の終了
			if(m_attackBreakTime <= 0)
				m_attackTime = -1;
		}
		else
		{
			//攻撃
			Attack();

			//速度の決定
			Speed();

			//移動
			Move();

			//ジャンプ
			Jump();

			//アニメーション
			Animation();
		}

		//キー状態のリセット
		ResetKeyStatus();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 攻撃
	/// </summary>
	void Attack()
	{
		//左クリック（攻撃１）
		if (m_isPressedLeftClick)
		{
			//攻撃していない
			if (m_attackTime == -1)
			{
				//初めの攻撃１設定
				m_attackTime = 0;
				m_attackCount = 1;
			}
			else if (m_attackTime > ATTACK_TIME && m_attackCount <= CONSECUTIVE_ATTACK_LIMIT_NUM) //攻撃が終わったand攻撃２を繰り出していない
			{
				//次の攻撃１設定
				m_attackTime = 0;
				m_attackCount++;
			}

			//攻撃１のコンボ終了
			if (m_attackCount == CONSECUTIVE_ATTACK_LIMIT_NUM)
			{
				//攻撃１の最後のパターンにする
				m_attackCount = CONSECUTIVE_ATTACK_LIMIT_NUM;
				m_attackBreakTime = ATTACK_TIME + ATTACK_BREAK_TIME;
				m_attackTime = ATTACK_TIME;
				return;
			}
		}

		//右クリック（攻撃２）
		if (m_isPressedRightClick)
		{
			//攻撃していないor攻撃１のコンボが終わっていない
			if (m_attackTime == -1 || m_attackCount < CONSECUTIVE_ATTACK_LIMIT_NUM)
			{
				//初めの攻撃２設定
				m_attackTime = 0;
				m_attackCount = CONSECUTIVE_ATTACK_LIMIT_NUM + 1;
			}
			else if (m_attackTime > ATTACK_TIME && m_attackCount > CONSECUTIVE_ATTACK_LIMIT_NUM) //攻撃が終わったand攻撃１でない
			{
				//次の攻撃２設定
				m_attackTime = 0;
				m_attackCount++;
			}

			//攻撃２のコンボ終了
			if (m_attackCount >= CONSECUTIVE_ATTACK_LIMIT_NUM + m_numAttack2Combo)
			{
				//攻撃２を最後のパターンにする
				m_attackCount = CONSECUTIVE_ATTACK_LIMIT_NUM + m_numAttack2Combo;
				m_attackBreakTime = ATTACK_TIME + ATTACK_BREAK_TIME;
				m_attackTime = ATTACK_TIME;
				return;
			}
		}

		//攻撃中
		if (m_attackTime >= 0)
			m_attackTime += Time.deltaTime;

		//コンボキャンセル
		if (m_attackTime >= ATTACK_TIME + COMBO_TIME)
			m_attackTime = -1;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 速度
	/// </summary>
	void Speed()
	{
		m_speed = m_walkSpeed;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 移動
	/// </summary>
	void Move()
	{
		m_posPrev = transform.position;

		//カメラの向きに対応した移動
		transform.position +=
			(Vector3.Scale(m_camera.transform.forward, (Vector3.right + Vector3.forward)) * Input.GetAxis(VERTICAL)
			+ m_camera.transform.right * Input.GetAxis(HORIZONTAL)).normalized * m_speed * Time.deltaTime;

		//移動に伴う回転
		Rotation();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 回転
	/// </summary>
	void Rotation()
	{
		//移動量
		m_direction = Vector3.Scale((transform.position - m_posPrev), (Vector3.right + Vector3.forward)).normalized;

		//移動なし
		if (m_direction == Vector3.zero)
			return;

		//移動量から向くべき角度を算出
		m_angle = Mathf.Asin(m_direction.x) / Mathf.PI * HALF_CIRCUMFERENCE_ANGLE;
		if (m_direction.z < 0)
			m_angle = HALF_CIRCUMFERENCE_ANGLE - m_angle;

		//マイナス角度の修正
		if (m_angle < 0)
			m_angle += ONE_TURNING_ANGLE;

		m_workVector3 = transform.eulerAngles;

		//回転したい角度が小さい
		if (Mathf.Abs(m_workVector3.y - m_angle) <= CORRECTION_ANGLE)
		{
			//向きたい方向に向く
			m_workVector3.y = m_angle;
		}
		else
		{
			//徐々に向きたい方向に向く
			m_angle -= m_workVector3.y;
			m_angle *= (Mathf.Abs(m_angle) < HALF_CIRCUMFERENCE_ANGLE) ? Time.deltaTime * m_rotationSpeed : -Time.deltaTime * m_rotationSpeed;
			m_workVector3.y += m_angle;
		}

		transform.eulerAngles = m_workVector3;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ジャンプ
	/// </summary>
	void Jump()
	{
		//スペースキーを押したandジャンプしていない
		if (m_isPressedSpace && m_jumpForceCountTime == -1)
			m_jumpForceCountTime = 0;

		//ジャンプしない
		if (m_jumpForceCountTime == -1)
			return;

		//スペースキーを押しているandジャンプする力を入れている間
		if (Input.GetButton(JUMP) && m_jumpForceCountTime < m_jumpForceTime)
			RB.AddForce(m_jumpingPower * Vector3.up * (1.0f - m_jumpForceCountTime / m_jumpForceTime));

		//ジャンプ中
		if (m_jumpForceCountTime >= 0)
			m_jumpForceCountTime += Time.deltaTime;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 当たり判定（くっついている）
	/// </summary>
	/// <param name="other">当たっているもの</param>
	void OnCollisionStay(Collision other)
	{
		//当たった位置
		foreach (var point in other.contacts)
		{
			//足で乗っている
			if (point.point.y <= transform.position.y)
			{
				//ジャンプしきった時、ジャンプ終了を許可
				if (m_jumpForceCountTime >= m_jumpForceTime)
					m_jumpForceCountTime = -1;
				return;
			}
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// アニメーション関係
	/// </summary>
	void Animation()
	{
		//状態を調べる
		CheckState();

		//状態変化なし
		if (m_state == m_statePrev)
			return;

		//アニメーションを変化
		switch (m_state)
		{
			case Status.Walk:
				Anim.SetTrigger(TRANS_WALK);
				break;
			case Status.Idle:
				Anim.SetTrigger(TRANS_IDLE);
				break;
			case Status.Attack1A:
				Anim.SetTrigger(TRANS_ATTACK1A);
				break;
			case Status.Attack1B:
				Anim.SetTrigger(TRANS_ATTACK1B);
				break;
			case Status.Attack1C:
				Anim.SetTrigger(TRANS_ATTACK1C);
				break;
			case Status.Attack2:
				Anim.SetTrigger(TRANS_ATTACK2);
				break;
		}

		m_statePrev = m_state;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 状態を調べる
	/// </summary>
	void CheckState()
	{
		//動きがあるか
		if (m_direction.sqrMagnitude > 0)
			m_state = Status.Walk;
		else
			m_state = Status.Idle;

		//攻撃が終了しているor攻撃していない
		if (m_attackTime > ATTACK_TIME || m_attackTime == -1)
			return;

		//攻撃
		switch (m_attackCount)
		{
			case 1:
				m_state = Status.Attack1A;
				break;
			case 2:
				m_state = Status.Attack1B;
				break;
			case 3:
				m_state = Status.Attack1C;
				break;
			case CONSECUTIVE_ATTACK_LIMIT_NUM + 1:
			case CONSECUTIVE_ATTACK_LIMIT_NUM + 2:
			case CONSECUTIVE_ATTACK_LIMIT_NUM + 3:
				m_state = Status.Attack2;
				break;
			default:
				return;
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// キー状態のリセット
	/// </summary>
	void ResetKeyStatus()
	{
		m_isPressedSpace = false;
		m_isPressedLeftClick = false;
		m_isPressedRightClick = false;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 重なり判定
	/// </summary>
	/// <param name="other">重なったもの</param>
	void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.tag);
	}

	//----------------------------------------------------------------------------------------------------
	//アニメーションイベント
	//----------------------------------------------------------------------------------------------------

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 攻撃１のAパターンの攻撃
	/// </summary>
	void Attack1AEvent()
	{
		//ワールド座標でプレイヤーの原点と方向
		m_workMatrix.SetTRS(transform.position, transform.rotation, Vector3.one);

		//攻撃範囲頂点の決定
		for (var i = 0; i < MyCube.NUM_VERTICES; i++)
		{
			//絶対的な攻撃範囲の決定
			m_workVector3Array[i] = m_workMatrix.MultiplyPoint(ATTACK1_A_VERTECES[i]);
		}

		//攻撃範囲の直方体の構築
		m_workMyCube.SetCube(m_workVector3Array[0], m_workVector3Array[1], m_workVector3Array[2], m_workVector3Array[3],
			m_workVector3Array[4], m_workVector3Array[5], m_workVector3Array[6], m_workVector3Array[6]);

		//攻撃範囲の生成
		myCharacter.AttackManagerScript.PlayerAttack(m_workMyCube, m_effectiveAttackTime);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 攻撃１のBパターンの攻撃
	/// </summary>
	void Attack1BEvent()
	{
		//ワールド座標でプレイヤーの原点と方向
		m_workMatrix.SetTRS(transform.position, transform.rotation, Vector3.one);

		//攻撃範囲頂点の決定
		for (var i = 0; i < MyCube.NUM_VERTICES; i++)
		{
			//絶対的な攻撃範囲の決定
			m_workVector3Array[i] = m_workMatrix.MultiplyPoint(ATTACK1_B_VERTECES[i]);
		}

		//攻撃範囲の直方体の構築
		m_workMyCube.SetCube(m_workVector3Array[0], m_workVector3Array[1], m_workVector3Array[2], m_workVector3Array[3],
			m_workVector3Array[4], m_workVector3Array[5], m_workVector3Array[6], m_workVector3Array[6]);

		//攻撃範囲の生成
		myCharacter.AttackManagerScript.PlayerAttack(m_workMyCube, m_effectiveAttackTime);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 攻撃１のCパターンの攻撃
	/// </summary>
	void Attack1CEvent()
	{
		//ワールド座標でプレイヤーの原点と方向
		m_workMatrix.SetTRS(transform.position, transform.rotation, Vector3.one);

		//攻撃範囲頂点の決定
		for (var i = 0; i < MyCube.NUM_VERTICES; i++)
		{
			//絶対的な攻撃範囲の決定
			m_workVector3Array[i] = m_workMatrix.MultiplyPoint(ATTACK1_C_VERTECES[i]);
		}

		//攻撃範囲の直方体の構築
		m_workMyCube.SetCube(m_workVector3Array[0], m_workVector3Array[1], m_workVector3Array[2], m_workVector3Array[3],
			m_workVector3Array[4], m_workVector3Array[5], m_workVector3Array[6], m_workVector3Array[6]);

		//攻撃範囲の生成
		myCharacter.AttackManagerScript.PlayerAttack(m_workMyCube, m_effectiveAttackTime);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 攻撃2の攻撃
	/// </summary>
	void Attack2Event()
	{
		//ワールド座標でプレイヤーの原点と方向
		m_workMatrix.SetTRS(transform.position, transform.rotation, Vector3.one);

		//攻撃範囲頂点の決定
		for (var i = 0; i < MyCube.NUM_VERTICES; i++)
		{
			//絶対的な攻撃範囲の決定
			m_workVector3Array[i] = m_workMatrix.MultiplyPoint(ATTACK2_VERTECES[i]);
		}

		//攻撃範囲の直方体の構築
		m_workMyCube.SetCube(m_workVector3Array[0], m_workVector3Array[1], m_workVector3Array[2], m_workVector3Array[3],
			m_workVector3Array[4], m_workVector3Array[5], m_workVector3Array[6], m_workVector3Array[6]);

		//攻撃範囲の生成
		myCharacter.AttackManagerScript.PlayerAttack(m_workMyCube, m_effectiveAttackTime);
	}
}
