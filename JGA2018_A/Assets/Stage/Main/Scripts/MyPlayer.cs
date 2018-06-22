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

//----------------------------------------------------------------------------------------------------
//Enum・Struct
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------
/// <summary>
/// 行動状態
/// </summary>
enum BehaviorStatus
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

//----------------------------------------------------------------------------------------------------
/// <summary>
/// プレイヤーのマスク
/// </summary>
struct PlayerMask
{
	/// <summary>
	/// 属性
	/// </summary>
	public MaskAttribute attribute;
	/// <summary>
	/// 獲得済み
	/// </summary>
	public bool isObtained;
	/// <summary>
	/// 使用可能
	/// </summary>
	public bool isAvailable;
	/// <summary>
	/// 使用中
	/// </summary>
	public bool isUse;
	/// <summary>
	/// ゲージを数える
	/// </summary>
	public float countGauge;
}

//----------------------------------------------------------------------------------------------------
//クラス
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------
/// <summary>
/// プレイヤー
/// </summary>
public class MyPlayer : MonoBehaviour
{
	#region 外部のインスタンス
	[Header("外部のインスタンス")]
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
	[Header("コンポーネント")]
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
	[Header("アニメーション")]
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
	/// マスクチェンジ遷移
	/// </summary>
	const string TRANS_CHANGE_MASK = "ChangeMask";
	#endregion

	#region 状態
	[Header("状態")]
	/// <summary>
	/// 現在の行動状態
	/// </summary>
	BehaviorStatus m_behaviorState;

	/// <summary>
	/// フレーム前の状態
	/// </summary>
	BehaviorStatus m_behaviorStatePrev;

	/// <summary>
	/// 現在のマスク状態
	/// </summary>
	MaskAttribute m_maskState;

	/// <summary>
	/// フレーム前のマスク状態
	/// </summary>
	MaskAttribute m_maskStatePrev;
	#endregion

	#region トランスポート
	[Header("トランスポート")]
	/// <summary>
	/// 回転スピード
	/// </summary>
	[SerializeField]
	float m_rotationSpeed;

	/// <summary>
	/// 補正角度
	/// </summary>
	[SerializeField]
	int m_correctionAngle;

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
	#endregion

	#region 移動速度
	[Header("移動速度")]
	/// <summary>
	/// 徒歩速度
	/// </summary>
	[SerializeField]
	float m_walkSpeed;

	/// <summary>
	/// 速度
	/// </summary>
	float m_speed;
	#endregion

	#region ジャンプ
	[Header("ジャンプ")]
	/// <summary>
	/// ジャンプ力
	/// </summary>
	[SerializeField]
	float m_jumpingPower;

	/// <summary>
	/// ジャンプで力を入れる時間
	/// </summary>
	[SerializeField]
	float m_jumpForceTime;

	/// <summary>
	/// ジャンプしている
	/// </summary>
	bool m_isJump;

	/// <summary>
	/// ジャンプで力を入れている時間
	/// </summary>
	float m_jumpForceCountTime;
	#endregion

	#region 攻撃
	[Header("攻撃")]
	/// <summary>
	/// 攻撃テンポ時間
	/// </summary>
	[SerializeField]
	float m_attackTempoTime;

	/// <summary>
	/// 攻撃の有効時間
	/// </summary>
	[SerializeField]
	float m_effectiveAttackTime;

	/// <summary>
	/// コンボ間の時間
	/// </summary>
	[SerializeField]
	float m_comboTime;

	/// <summary>
	/// 攻撃の後休憩する時間
	/// </summary>
	[SerializeField]
	float m_attackBreakTime;

	/// <summary>
	/// 攻撃１の威力
	/// </summary>
	[SerializeField]
	int m_powerAttack1;

	/// <summary>
	/// 攻撃２の威力
	/// </summary>
	[SerializeField]
	int m_powerAttack2;

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
	/// 攻撃２のコンボ数
	/// </summary>
	int m_numAttack2Combo;

	/// <summary>
	/// 現在の攻撃休憩時間
	/// </summary>
	float m_currentAttackBreakTime;
	#endregion

	#region 攻撃範囲
	[Header("攻撃範囲")]
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

	/// <summary>
	/// 攻撃２の１種目の頂点
	/// </summary>
	static readonly Vector3[] ATTACK2_KIND1_VERTECES =
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

	/// <summary>
	/// 攻撃２の２種目の頂点
	/// </summary>
	static readonly Vector3[] ATTACK2_KIND2_VERTECES =
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

	/// <summary>
	/// 攻撃２の３種目の頂点
	/// </summary>
	static readonly Vector3[] ATTACK2_KIND3_VERTECES =
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

	/// <summary>
	/// 攻撃２の４種目のAパターンの頂点
	/// </summary>
	static readonly Vector3[] ATTACK2_KIND4_A_VERTECES =
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
	/// 攻撃２の４種目のBパターンの頂点
	/// </summary>
	static readonly Vector3[] ATTACK2_KIND4_B_VERTECES =
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
	/// 攻撃２の４種目のCパターンの頂点
	/// </summary>
	static readonly Vector3[] ATTACK2_KIND4_C_VERTECES =
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
	#endregion

	#region 仮面関係
	[Header("仮面関係")]
	/// <summary>
	/// マスクオブジェクト
	/// </summary>
	[SerializeField]
	GameObject MaskObj;

	/// <summary>
	/// 配達マスクゲージの最大値
	/// </summary>
	[SerializeField]
	float m_maxCarryMaskGauge;

	/// <summary>
	/// ウイルスマスクゲージの最大値
	/// </summary>
	[SerializeField]
	float m_maxVirusMaskGauge;

	/// <summary>
	/// 鏡マスクゲージの最大値
	/// </summary>
	[SerializeField]
	float m_maxMirrorMaskGauge;

	/// <summary>
	/// マジックマスクゲージの最大値
	/// </summary>
	[SerializeField]
	float m_maxMagicMaskGauge;

	/// <summary>
	/// 能力使用時間の最大値
	/// </summary>
	[SerializeField]
	float m_maxAbilityUseTime;

	/// <summary>
	/// 配達マスク
	/// </summary>
	PlayerMask m_carryMask;

	/// <summary>
	/// ウイルスマスク
	/// </summary>
	PlayerMask m_virusMask;

	/// <summary>
	/// 鏡マスク
	/// </summary>
	PlayerMask m_mirrorMask;

	/// <summary>
	/// マジックマスク
	/// </summary>
	PlayerMask m_magicMask;
	#endregion

	#region キーボード関係
	[Header("キーボード関係")]
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
	/// 十字キー上
	/// </summary>
	const string CROSS_KEY_UP = "CrossKeyUp";

	/// <summary>
	/// 十字キー下
	/// </summary>
	const string CROSS_KEY_DOWN = "CrossKeyDown";

	/// <summary>
	/// 十字キー左
	/// </summary>
	const string CROSS_KEY_LEFT = "CrossKeyLeft";

	/// <summary>
	/// 十字キー右
	/// </summary>
	const string CROSS_KEY_RIGHT = "CrossKeyRight";

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

	/// <summary>
	/// 十字キー上が押された
	/// </summary>
	bool m_isPressedCrossKeyUp = false;

	/// <summary>
	/// 十字キー下が押された
	/// </summary>
	bool m_isPressedCrossKeyDown = false;

	/// <summary>
	/// 十字キー左が押された
	/// </summary>
	bool m_isPressedCrossKeyLeft = false;

	/// <summary>
	/// 十字キー右が押された
	/// </summary>
	bool m_isPressedCrossKeyRight = false;
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

		m_maskState = MaskAttribute.Non;
		m_attackCount = 0;
		m_numAttack2Combo = 1;

		//マスク関係
		m_carryMask.attribute = MaskAttribute.Carry;
		m_carryMask.isObtained = false;
		m_carryMask.isAvailable = false;
		m_carryMask.isUse = false;
		m_carryMask.countGauge = 0;
		m_virusMask.attribute = MaskAttribute.Virus;
		m_virusMask.isObtained = false;
		m_virusMask.isAvailable = false;
		m_virusMask.isUse = false;
		m_virusMask.countGauge = 0;
		m_mirrorMask.attribute = MaskAttribute.Mirror;
		m_mirrorMask.isObtained = false;
		m_mirrorMask.isAvailable = false;
		m_mirrorMask.isUse = false;
		m_mirrorMask.countGauge = 0;
		m_magicMask.attribute = MaskAttribute.Magic;
		m_magicMask.isObtained = false;
		m_magicMask.isAvailable = false;
		m_magicMask.isUse = false;
		m_magicMask.countGauge = 0;
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

		//十字キー上の押下
		if (Input.GetButtonDown(CROSS_KEY_UP))
			m_isPressedCrossKeyUp = true;

		//十字キー下の押下
		if (Input.GetButtonDown(CROSS_KEY_DOWN))
			m_isPressedCrossKeyDown = true;

		//十字キー左の押下
		if (Input.GetButtonDown(CROSS_KEY_LEFT))
			m_isPressedCrossKeyLeft = true;

		//十字キー右の押下
		if (Input.GetButtonDown(CROSS_KEY_RIGHT))
			m_isPressedCrossKeyRight = true;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 定期フレーム
	/// </summary>
	void FixedUpdate()
	{
		//攻撃休憩時間
		if (m_currentAttackBreakTime > 0)
		{
			m_currentAttackBreakTime -= Time.deltaTime;

			//攻撃休憩時間の終了
			if (m_currentAttackBreakTime <= 0)
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

		//マスク関係
		Mask();

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
			else if (m_attackTime > m_attackTempoTime && m_attackCount <= CONSECUTIVE_ATTACK_LIMIT_NUM) //攻撃が終わったand攻撃２を繰り出していない
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
				m_currentAttackBreakTime = m_attackTempoTime + m_attackBreakTime;
				m_attackTime = m_attackTempoTime;
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
			else if (m_attackTime > m_attackTempoTime && m_attackCount > CONSECUTIVE_ATTACK_LIMIT_NUM) //攻撃が終わったand攻撃１でない
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
				m_currentAttackBreakTime = m_attackTempoTime + m_attackBreakTime;
				m_attackTime = m_attackTempoTime;
				return;
			}
		}

		//攻撃中
		if (m_attackTime >= 0)
			m_attackTime += Time.deltaTime;

		//コンボキャンセル
		if (m_attackTime >= m_attackTempoTime + m_comboTime)
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
		if (Mathf.Abs(m_workVector3.y - m_angle) <= m_correctionAngle)
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
		if (m_behaviorState == m_behaviorStatePrev)
		{
			//アイドル状態中andマスクの切り替え
			if (m_behaviorState == BehaviorStatus.Idle && m_maskState != m_maskStatePrev)
				Anim.SetTrigger(TRANS_CHANGE_MASK);
			return;
		}

		//アニメーションを変化
		switch (m_behaviorState)
		{
			case BehaviorStatus.Walk:
				Anim.SetTrigger(TRANS_WALK);
				break;
			case BehaviorStatus.Idle:
				Anim.SetTrigger(TRANS_IDLE);
				break;
			case BehaviorStatus.Attack1A:
				Anim.SetTrigger(TRANS_ATTACK1A);
				break;
			case BehaviorStatus.Attack1B:
				Anim.SetTrigger(TRANS_ATTACK1B);
				break;
			case BehaviorStatus.Attack1C:
				Anim.SetTrigger(TRANS_ATTACK1C);
				break;
			case BehaviorStatus.Attack2:
				Anim.SetTrigger(TRANS_ATTACK2);
				break;
		}

		m_behaviorStatePrev = m_behaviorState;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 状態を調べる
	/// </summary>
	void CheckState()
	{
		//動きがあるか
		if (m_direction.sqrMagnitude > 0)
			m_behaviorState = BehaviorStatus.Walk;
		else
			m_behaviorState = BehaviorStatus.Idle;

		//攻撃が終了しているor攻撃していない
		if (m_attackTime > m_attackTempoTime || m_attackTime == -1)
			return;

		//攻撃
		switch (m_attackCount)
		{
			case 1:
				m_behaviorState = BehaviorStatus.Attack1A;
				break;
			case 2:
				m_behaviorState = BehaviorStatus.Attack1B;
				break;
			case 3:
				m_behaviorState = BehaviorStatus.Attack1C;
				break;
			case CONSECUTIVE_ATTACK_LIMIT_NUM + 1:
			case CONSECUTIVE_ATTACK_LIMIT_NUM + 2:
			case CONSECUTIVE_ATTACK_LIMIT_NUM + 3:
				m_behaviorState = BehaviorStatus.Attack2;
				break;
			default:
				return;
		}
	}

	//----------------------------------------------------------------------------------------------------
	//マスク関係
	void Mask()
	{
		//マスク状態の前回を更新
		m_maskStatePrev = m_maskState;
		MaskObj.SetActive(m_maskState != MaskAttribute.Non);

		//配達マスクが使用可能
		if (m_carryMask.isObtained)
		{
			AttributeMask(ref m_carryMask);
		}

		//ウイルスマスクが使用可能
		if (m_virusMask.isObtained)
		{
			AttributeMask(ref m_virusMask);
		}

		//鏡マスクが使用可能
		if (m_mirrorMask.isObtained)
		{
			AttributeMask(ref m_mirrorMask);
		}

		//マジックマスクが使用可能
		if (m_magicMask.isObtained)
		{
			AttributeMask(ref m_magicMask);
		}

		//装着したマスク
		MountingMask();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// １つの属性のマスク関係
	/// </summary>
	/// <param name="mask">属性のマスク</param>
	void AttributeMask(ref PlayerMask mask)
	{
		//利用可能
		if (mask.isAvailable)
		{
			//使用中
			if (mask.isUse)
			{
				//ゲージが減る
				switch (mask.attribute)
				{
					case MaskAttribute.Carry:
						mask.countGauge -= (m_maxCarryMaskGauge / m_maxAbilityUseTime) * Time.deltaTime;
						break;
					case MaskAttribute.Virus:
						mask.countGauge -= (m_maxVirusMaskGauge / m_maxAbilityUseTime) * Time.deltaTime;
						break;
					case MaskAttribute.Mirror:
						mask.countGauge -= (m_maxMirrorMaskGauge / m_maxAbilityUseTime) * Time.deltaTime;
						break;
					case MaskAttribute.Magic:
						mask.countGauge -= (m_maxMagicMaskGauge / m_maxAbilityUseTime) * Time.deltaTime;
						break;
				}
			}

			//利用不可にする
			mask.isAvailable = !(mask.countGauge <= 0);
			if (!mask.isAvailable)
			{
				mask.isUse = false;
				m_maskState = MaskAttribute.Non;
			}
		}
		else
		{
			mask.countGauge += Time.deltaTime;

			//利用可能にする
			switch (mask.attribute)
			{
				case MaskAttribute.Carry:
					mask.isAvailable = (mask.countGauge >= m_maxCarryMaskGauge);
					break;
				case MaskAttribute.Virus:
					mask.isAvailable = (mask.countGauge >= m_maxVirusMaskGauge);
					break;
				case MaskAttribute.Mirror:
					mask.isAvailable = (mask.countGauge >= m_maxMirrorMaskGauge);
					break;
				case MaskAttribute.Magic:
					mask.isAvailable = (mask.countGauge >= m_maxMagicMaskGauge);
					break;
			}
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 装着したマスク
	/// </summary>
	void MountingMask()
	{
		//仮面を被っていない
		if (m_maskState == MaskAttribute.Non)
		{
			//十字キーの押下and仮面の使用可能か
			if (m_isPressedCrossKeyUp && m_carryMask.isAvailable)
			{
				//配達マスク
				m_maskState = MaskAttribute.Carry;
				m_carryMask.isUse = true;
			}
			else if (m_isPressedCrossKeyLeft && m_virusMask.isAvailable)
			{
				//ウイルスマスク
				m_maskState = MaskAttribute.Virus;
				m_virusMask.isUse = true;
			}
			else if (m_isPressedCrossKeyDown && m_mirrorMask.isAvailable)
			{
				//鏡マスク
				m_maskState = MaskAttribute.Mirror;
				m_mirrorMask.isUse = true;
			}
			else if (m_isPressedCrossKeyRight && m_magicMask.isAvailable)
			{
				//マジックマスク
				m_maskState = MaskAttribute.Magic;
				m_magicMask.isUse = true;
			}
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
		m_isPressedCrossKeyUp = false;
		m_isPressedCrossKeyDown = false;
		m_isPressedCrossKeyLeft = false;
		m_isPressedCrossKeyRight = false;
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
		myCharacter.AttackManagerScript.PlayerAttack(m_workMyCube, MaskAttribute.Non, m_powerAttack1, m_effectiveAttackTime);
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
		myCharacter.AttackManagerScript.PlayerAttack(m_workMyCube, MaskAttribute.Non, m_powerAttack1, m_effectiveAttackTime);
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
		myCharacter.AttackManagerScript.PlayerAttack(m_workMyCube, MaskAttribute.Non, m_powerAttack1, m_effectiveAttackTime);
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
		myCharacter.AttackManagerScript.PlayerAttack(m_workMyCube, m_maskState, m_powerAttack2, m_effectiveAttackTime);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 攻撃2の１種目の攻撃
	/// </summary>
	void Attack2Kind1Event()
	{
		//ワールド座標でプレイヤーの原点と方向
		m_workMatrix.SetTRS(transform.position, transform.rotation, Vector3.one);

		//攻撃範囲頂点の決定
		for (var i = 0; i < MyCube.NUM_VERTICES; i++)
		{
			//絶対的な攻撃範囲の決定
			m_workVector3Array[i] = m_workMatrix.MultiplyPoint(ATTACK2_KIND1_VERTECES[i]);
		}

		//攻撃範囲の直方体の構築
		m_workMyCube.SetCube(m_workVector3Array[0], m_workVector3Array[1], m_workVector3Array[2], m_workVector3Array[3],
			m_workVector3Array[4], m_workVector3Array[5], m_workVector3Array[6], m_workVector3Array[6]);

		//攻撃範囲の生成
		myCharacter.AttackManagerScript.PlayerAttack(m_workMyCube, m_maskState, m_powerAttack2, m_effectiveAttackTime);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 攻撃2の２種目の攻撃
	/// </summary>
	void Attack2Kind2Event()
	{
		//ワールド座標でプレイヤーの原点と方向
		m_workMatrix.SetTRS(transform.position, transform.rotation, Vector3.one);

		//攻撃範囲頂点の決定
		for (var i = 0; i < MyCube.NUM_VERTICES; i++)
		{
			//絶対的な攻撃範囲の決定
			m_workVector3Array[i] = m_workMatrix.MultiplyPoint(ATTACK2_KIND2_VERTECES[i]);
		}

		//攻撃範囲の直方体の構築
		m_workMyCube.SetCube(m_workVector3Array[0], m_workVector3Array[1], m_workVector3Array[2], m_workVector3Array[3],
			m_workVector3Array[4], m_workVector3Array[5], m_workVector3Array[6], m_workVector3Array[6]);

		//攻撃範囲の生成
		myCharacter.AttackManagerScript.PlayerAttack(m_workMyCube, m_maskState, m_powerAttack2, m_effectiveAttackTime);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 攻撃2の３種目１撃目の攻撃
	/// </summary>
	void Attack2Kind3Hook1Event()
	{
		//ワールド座標でプレイヤーの原点と方向
		m_workMatrix.SetTRS(transform.position, transform.rotation, Vector3.one);

		//攻撃範囲頂点の決定
		for (var i = 0; i < MyCube.NUM_VERTICES; i++)
		{
			//絶対的な攻撃範囲の決定
			m_workVector3Array[i] = m_workMatrix.MultiplyPoint(ATTACK2_KIND3_VERTECES[i]);
		}

		//攻撃範囲の直方体の構築
		m_workMyCube.SetCube(m_workVector3Array[0], m_workVector3Array[1], m_workVector3Array[2], m_workVector3Array[3],
			m_workVector3Array[4], m_workVector3Array[5], m_workVector3Array[6], m_workVector3Array[6]);

		//攻撃範囲の生成
		myCharacter.AttackManagerScript.PlayerAttack(m_workMyCube, m_maskState, m_powerAttack2, m_effectiveAttackTime);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 攻撃2の３種目２撃目の攻撃
	/// </summary>
	void Attack2Kind3Hook2Event()
	{
		//ワールド座標でプレイヤーの原点と方向
		m_workMatrix.SetTRS(transform.position, transform.rotation, Vector3.one);

		//攻撃範囲頂点の決定
		for (var i = 0; i < MyCube.NUM_VERTICES; i++)
		{
			//絶対的な攻撃範囲の決定
			m_workVector3Array[i] = m_workMatrix.MultiplyPoint(ATTACK2_KIND3_VERTECES[i]);
		}

		//攻撃範囲の直方体の構築
		m_workMyCube.SetCube(m_workVector3Array[0], m_workVector3Array[1], m_workVector3Array[2], m_workVector3Array[3],
			m_workVector3Array[4], m_workVector3Array[5], m_workVector3Array[6], m_workVector3Array[6]);

		//攻撃範囲の生成
		myCharacter.AttackManagerScript.PlayerAttack(m_workMyCube, m_maskState, m_powerAttack2, m_effectiveAttackTime);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 攻撃2の4種目Aの攻撃
	/// </summary>
	void Attack2Kind4AEvent()
	{
		//ワールド座標でプレイヤーの原点と方向
		m_workMatrix.SetTRS(transform.position, transform.rotation, Vector3.one);

		//攻撃範囲頂点の決定
		for (var i = 0; i < MyCube.NUM_VERTICES; i++)
		{
			//絶対的な攻撃範囲の決定
			m_workVector3Array[i] = m_workMatrix.MultiplyPoint(ATTACK2_KIND4_A_VERTECES[i]);
		}

		//攻撃範囲の直方体の構築
		m_workMyCube.SetCube(m_workVector3Array[0], m_workVector3Array[1], m_workVector3Array[2], m_workVector3Array[3],
			m_workVector3Array[4], m_workVector3Array[5], m_workVector3Array[6], m_workVector3Array[6]);

		//攻撃範囲の生成
		myCharacter.AttackManagerScript.PlayerAttack(m_workMyCube, m_maskState, m_powerAttack2, m_effectiveAttackTime);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 攻撃2の4種目Bの攻撃
	/// </summary>
	void Attack2Kind4BEvent()
	{
		//ワールド座標でプレイヤーの原点と方向
		m_workMatrix.SetTRS(transform.position, transform.rotation, Vector3.one);

		//攻撃範囲頂点の決定
		for (var i = 0; i < MyCube.NUM_VERTICES; i++)
		{
			//絶対的な攻撃範囲の決定
			m_workVector3Array[i] = m_workMatrix.MultiplyPoint(ATTACK2_KIND4_B_VERTECES[i]);
		}

		//攻撃範囲の直方体の構築
		m_workMyCube.SetCube(m_workVector3Array[0], m_workVector3Array[1], m_workVector3Array[2], m_workVector3Array[3],
			m_workVector3Array[4], m_workVector3Array[5], m_workVector3Array[6], m_workVector3Array[6]);

		//攻撃範囲の生成
		myCharacter.AttackManagerScript.PlayerAttack(m_workMyCube, m_maskState, m_powerAttack2, m_effectiveAttackTime);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 攻撃2の4種目Cの攻撃
	/// </summary>
	void Attack2Kind4CEvent()
	{
		//ワールド座標でプレイヤーの原点と方向
		m_workMatrix.SetTRS(transform.position, transform.rotation, Vector3.one);

		//攻撃範囲頂点の決定
		for (var i = 0; i < MyCube.NUM_VERTICES; i++)
		{
			//絶対的な攻撃範囲の決定
			m_workVector3Array[i] = m_workMatrix.MultiplyPoint(ATTACK2_KIND4_C_VERTECES[i]);
		}

		//攻撃範囲の直方体の構築
		m_workMyCube.SetCube(m_workVector3Array[0], m_workVector3Array[1], m_workVector3Array[2], m_workVector3Array[3],
			m_workVector3Array[4], m_workVector3Array[5], m_workVector3Array[6], m_workVector3Array[6]);

		//攻撃範囲の生成
		myCharacter.AttackManagerScript.PlayerAttack(m_workMyCube, m_maskState, m_powerAttack2, m_effectiveAttackTime);
	}
}
