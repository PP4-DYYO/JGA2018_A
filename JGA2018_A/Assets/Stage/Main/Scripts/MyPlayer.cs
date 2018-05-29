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
	/// <summary>
	/// キャラクター
	/// </summary>
	[SerializeField]
	MyCharactor myCharactor;

	/// <summary>
	/// カメラ
	/// </summary>
	Camera m_camera;

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

	/// <summary>
	/// 速度
	/// </summary>
	float m_speed;

	/// <summary>
	/// 徒歩速度
	/// </summary>
	[SerializeField]
	float m_walkSpeed;

	/// <summary>
	/// 回転スピード
	/// </summary>
	[SerializeField]
	float m_rotationSpeed;

	/// <summary>
	/// ジャンプ力
	/// </summary>
	[SerializeField]
	float m_jumpingPower;

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
	/// コンボ間の時間
	/// </summary>
	const float COMBO_TIME = 1.5f;

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

	/// <summary>
	/// 作業用のVector３
	/// </summary>
	Vector3 m_workVector3;

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 初期
	/// </summary>
	void Start()
	{
		//アクセスしやすいように
		m_camera = myCharactor.GameScript.CameraScript;

		m_attackCount = 0;
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

		//キー状態のリセット
		ResetKeyStatus();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 攻撃
	/// </summary>
	void Attack()
	{
		//左クリック
		if(m_isPressedLeftClick)
		{
			//コンボの種類
			if (m_attackTime == -1) //攻撃していない
			{
				//初めの攻撃設定
				m_attackTime = 0;
				m_attackCount = 1;
			}
			else if (m_attackTime > ATTACK_TIME && m_attackCount != int.MaxValue) //攻撃が終わったand攻撃２を繰り出していない
			{
				//次の攻撃設定
				m_attackTime = 0;
				m_attackCount = Mathf.Min(++m_attackCount, CONSECUTIVE_ATTACK_LIMIT_NUM);
			}
		}

		//右クリック
		if (m_isPressedRightClick)
		{
			//攻撃していない
			if (m_attackTime == -1 || m_attackCount <= CONSECUTIVE_ATTACK_LIMIT_NUM)
			{
				//攻撃２
				m_attackTime = 0;
				m_attackCount = int.MaxValue;
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
		if (m_isPressedSpace)
			RB.AddForce(m_jumpingPower * Vector3.up);
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

		//攻撃が終了している
		if (m_attackTime >= ATTACK_TIME || m_attackTime == -1)
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
			case int.MaxValue:
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
}
