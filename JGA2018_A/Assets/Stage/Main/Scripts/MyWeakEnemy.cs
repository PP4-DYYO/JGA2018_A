//----------------------------------------------------------------------------------------------------
//
//2018年7月19日～
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
/// 敵の行動状態
/// </summary>
enum EnemyBehaviorStatus
{
	/// <summary>
	/// 待機
	/// </summary>
	Idle,
	/// <summary>
	/// 徒歩
	/// </summary>
	Walk,
	/// <summary>
	/// 走る
	/// </summary>
	Run,
	/// <summary>
	/// 攻撃前
	/// </summary>
	BeforeAttack,
	/// <summary>
	/// 攻撃
	/// </summary>
	Attack,
	/// <summary>
	/// 死ぬ
	/// </summary>
	Die,
	/// <summary>
	/// なし
	/// </summary>
	Non,
}

/// <summary>
/// 弱い敵のアニメーション番号
/// </summary>
enum WeakEnemyAnimIdx
{
	Idle,
	Walk,
	Run,
	BeforeAttack,
	Attack,
	Die,
}

//----------------------------------------------------------------------------------------------------
//クラス
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------
/// <summary>
/// 弱い敵クラス
/// </summary>
public class MyWeakEnemy : MonoBehaviour
{
	#region 外部のインスタンス
	[Header("外部のインスタンス")]
	/// <summary>
	/// AIマネージャ
	/// </summary>
	MyAiManager myAiManager;
	public MyAiManager AiManagerScript
	{
		set { myAiManager = value; }
	}
	#endregion

	#region コンポーネント
	[Header("コンポーネント")]
	/// <summary>
	/// アニメータ
	/// </summary>
	[SerializeField]
	Animator Anim;
	#endregion

	#region 状態
	[Header("状態")]
	/// <summary>
	/// 状態
	/// </summary>
	EnemyBehaviorStatus m_behaviorState;
	#endregion

	#region 移動
	[Header("移動")]
	/// <summary>
	/// 移動位置
	/// </summary>
	[SerializeField]
	Vector3[] m_movingPos;

	/// <summary>
	/// 循環するか
	/// </summary>
	[SerializeField]
	bool m_isCirculation;

	/// <summary>
	/// 移動位置の番号
	/// </summary>
	int m_numMovingPos;

	/// <summary>
	/// 移動位置の番号の符号
	/// </summary>
	int m_signOfNumMovingPos;

	/// <summary>
	/// 進む方向
	/// </summary>
	Vector3 m_directionTravel;

	/// <summary>
	/// フレーム前の進む方向
	/// </summary>
	Vector3 m_directionTravelPrev;
	#endregion

	#region 探索
	/// <summary>
	/// 顔のTransform
	/// </summary>
	[SerializeField]
	Transform FaceTrans;

	/// <summary>
	/// 視野距離
	/// </summary>
	[SerializeField]
	float m_viewingDistance;

	/// <summary>
	/// 視線
	/// </summary>
	Ray m_lineSight;

	/// <summary>
	/// 視線の情報
	/// </summary>
	RaycastHit m_lineSightInfo;
	#endregion

	#region 速度
	[Header("速度")]
	/// <summary>
	/// 徒歩速度
	/// </summary>
	[SerializeField]
	float m_walkSpeed;

	/// <summary>
	/// 走り速度
	/// </summary>
	[SerializeField]
	float m_runSpeed;

	/// <summary>
	/// 速度
	/// </summary>
	float m_speed;
	#endregion

	#region 攻撃
	[Header("攻撃")]
	/// <summary>
	/// 攻撃可能な距離
	/// </summary>
	[SerializeField]
	float m_attackableDistance;

	/// <summary>
	/// 攻撃威力
	/// </summary>
	[SerializeField]
	int m_powerAttack;

	/// <summary>
	/// 攻撃有効時間
	/// </summary>
	[SerializeField]
	float m_effectiveAttackTime;

	/// <summary>
	/// 攻撃前の時間
	/// </summary>
	[SerializeField]
	float m_timeBeforeAttack;

	/// <summary>
	/// 攻撃時間
	/// </summary>
	[SerializeField]
	float m_attackTime;

	/// <summary>
	/// 攻撃時間を数える
	/// </summary>
	float m_countAttackTime;

	/// <summary>
	/// 攻撃モーション
	/// </summary>
	bool m_isAttackMotion;

	/// <summary>
	/// 攻撃の頂点
	/// </summary>
	static readonly Vector3[] ATTACK_VERTECES =
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

	#region 生命
	[Header("生命")]
	/// <summary>
	/// 最大HP
	/// </summary>
	[SerializeField]
	int m_maxHp;

	/// <summary>
	/// 死ぬ時間
	/// </summary>
	[SerializeField]
	float m_timeToDie;

	/// <summary>
	/// 体力
	/// </summary>
	int m_hp;
	#endregion

	#region アニメーション
	[Header("アニメーション")]
	/// <summary>
	/// 弱い敵のアニメーション
	/// </summary>
	const string WEAK_ENEMY_ANIM = "WeakEnemyAnimIdx";

	/// <summary>
	/// アニメーションのレイヤー
	/// </summary>
	const string ANIM_LAYER = "Base Layer.";

	/// <summary>
	/// 待機状態
	/// </summary>
	const string ANIM_IDLE = "Idle";

	/// <summary>
	/// 歩く遷移
	/// </summary>
	const string ANIM_WALK = "Walk";

	/// <summary>
	/// 走る遷移
	/// </summary>
	const string ANIM_RUN = "Run";

	/// <summary>
	/// 攻撃前遷移
	/// </summary>
	const string ANIM_BEFORE_ATTACK = "BeforeAttack";

	/// <summary>
	/// 攻撃遷移
	/// </summary>
	const string ANIM_ATTACK = "Attack";

	/// <summary>
	/// 死ぬ遷移
	/// </summary>
	const string ANIM_DIE = "Die";
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
	/// 定期フレーム
	/// </summary>
	void FixedUpdate()
	{
		//死んでいない
		if (m_behaviorState != EnemyBehaviorStatus.Die)
		{
			//攻撃モーションでない
			if (!m_isAttackMotion)
				Move();
			else
				Attack();
		}

		//アニメーション
		Animation();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 移動
	/// </summary>
	void Move()
	{
		//探索
		Search();

		//視線にプレイヤー
		if (m_lineSightInfo.transform && m_lineSightInfo.transform.tag.Equals(PlayerInfo.TAG))
		{
			//プレイヤーの追跡
			TrackingPlayer();
		}
		else
		{
			//徘徊
			WanderingAround();
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 探索
	/// </summary>
	void Search()
	{
		//視線の設定
		m_lineSight.origin = FaceTrans.position;
		m_lineSight.direction = FaceTrans.forward;

		//視線で探索
		Physics.Raycast(m_lineSight, out m_lineSightInfo, m_viewingDistance);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// プレイヤーを追跡
	/// </summary>
	void TrackingPlayer()
	{
		//移動方向
		m_directionTravelPrev = m_directionTravel;
		m_directionTravel = (m_lineSightInfo.transform.position - transform.position).normalized;

		//攻撃範囲にプレイヤー
		if (m_lineSightInfo.distance <= m_attackableDistance)
		{
			m_behaviorState = EnemyBehaviorStatus.BeforeAttack;
			m_speed = 0;
			m_isAttackMotion = true;
			transform.LookAt(
				Vector3.Scale(m_lineSightInfo.transform.position, (Vector3.right + Vector3.forward)) + Vector3.Scale(transform.position, (Vector3.up)));
			m_countAttackTime = 0;
			return;
		}

		//移動
		m_behaviorState = EnemyBehaviorStatus.Run;
		m_speed = m_runSpeed;
		transform.LookAt(
			Vector3.Scale(m_lineSightInfo.transform.position, (Vector3.right + Vector3.forward)) + Vector3.Scale(transform.position, (Vector3.up)));
		transform.position += m_directionTravel * m_speed * Time.deltaTime;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 徘徊
	/// </summary>
	void WanderingAround()
	{
		//移動箇所なし
		if (m_movingPos.Length <= 1)
		{
			m_behaviorState = EnemyBehaviorStatus.Idle;
			return;
		}

		//移動方向
		m_directionTravelPrev = m_directionTravel;
		m_directionTravel = (m_movingPos[m_numMovingPos] - transform.position).normalized;

		//移動
		m_behaviorState = EnemyBehaviorStatus.Walk;
		m_speed = m_walkSpeed;
		transform.LookAt(Vector3.Scale(m_movingPos[m_numMovingPos], (Vector3.right + Vector3.forward)) + Vector3.Scale(transform.position, (Vector3.up)));
		transform.position += m_directionTravel * m_speed * Time.deltaTime;

		//目的地到着
		if (Vector3.Cross(m_directionTravelPrev, transform.right).y <= 0)
		{
			//目的地を循環するか
			if (m_isCirculation)
			{
				//循環する位置
				m_numMovingPos = ++m_numMovingPos % m_movingPos.Length;
			}
			else
			{
				//来た道を戻る位置
				m_signOfNumMovingPos = (m_numMovingPos <= 0) ? +1 : (m_numMovingPos >= m_movingPos.Length - 1) ? -1 : m_signOfNumMovingPos;
				m_numMovingPos += m_signOfNumMovingPos;
			}

			//方向転換
			transform.LookAt(Vector3.Scale(m_movingPos[m_numMovingPos], (Vector3.right + Vector3.forward)) + Vector3.Scale(transform.position, (Vector3.up)));
			m_directionTravel = transform.forward;
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 攻撃
	/// </summary>
	void Attack()
	{
		m_countAttackTime += Time.deltaTime;

		//攻撃時間
		if (m_countAttackTime <= m_timeBeforeAttack)
			m_behaviorState = EnemyBehaviorStatus.BeforeAttack;
		else if (m_countAttackTime <= m_timeBeforeAttack + m_attackTime)
			m_behaviorState = EnemyBehaviorStatus.Attack;
		else
			m_isAttackMotion = false;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// アニメーション
	/// </summary>
	void Animation()
	{
		//状態遷移済み
		if ((int)m_behaviorState == Anim.GetInteger(WEAK_ENEMY_ANIM))
		{
			Anim.SetInteger(WEAK_ENEMY_ANIM, (int)EnemyBehaviorStatus.Non);
			return;
		}

		//状態とアニメーションが一緒
		if (m_behaviorState == EnemyBehaviorStatus.Idle && Anim.GetCurrentAnimatorStateInfo(0).IsName(ANIM_LAYER + ANIM_IDLE)
			|| m_behaviorState == EnemyBehaviorStatus.Walk && Anim.GetCurrentAnimatorStateInfo(0).IsName(ANIM_LAYER + ANIM_WALK)
			|| m_behaviorState == EnemyBehaviorStatus.Run && Anim.GetCurrentAnimatorStateInfo(0).IsName(ANIM_LAYER + ANIM_RUN)
			|| m_behaviorState == EnemyBehaviorStatus.BeforeAttack && Anim.GetCurrentAnimatorStateInfo(0).IsName(ANIM_LAYER + ANIM_BEFORE_ATTACK)
			|| m_behaviorState == EnemyBehaviorStatus.Attack && Anim.GetCurrentAnimatorStateInfo(0).IsName(ANIM_LAYER + ANIM_ATTACK)
			|| m_behaviorState == EnemyBehaviorStatus.Die && Anim.GetCurrentAnimatorStateInfo(0).IsName(ANIM_LAYER + ANIM_DIE))
			return;

		//状態変更
		Anim.SetInteger(WEAK_ENEMY_ANIM, (int)m_behaviorState);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 重なり始め判定
	/// </summary>
	/// <param name="other">重なったもの</param>
	void OnTriggerEnter(Collider other)
	{
		//敵からの攻撃を受ける
		if (other.tag.Equals(AttackManagerTag.PLAYER_ATTACK_RANGE_TAG) || other.tag.Equals(AttackManagerTag.PLAYER_ATTACK_DEATHBLOW_RANGE_TAG))
			Damage(other.GetComponent<MyAttack>());
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ダメージ
	/// </summary>
	/// <param name="attack">攻撃</param>
	void Damage(MyAttack attack)
	{
		m_hp -= attack.Power;

		//死亡
		if (m_hp <= 0)
		{
			Destroy(gameObject, m_timeToDie);
			m_behaviorState = EnemyBehaviorStatus.Die;
		}
	}

	//----------------------------------------------------------------------------------------------------
	//アニメーションイベント
	//----------------------------------------------------------------------------------------------------

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 攻撃の攻撃
	/// </summary>
	void AttackEvent()
	{
		//ワールド座標でプレイヤーの原点と方向
		m_workMatrix.SetTRS(transform.position, transform.rotation, Vector3.one);

		//攻撃範囲頂点の決定
		for (var i = 0; i < MyCube.NUM_VERTICES; i++)
		{
			//絶対的な攻撃範囲の決定
			m_workVector3Array[i] = m_workMatrix.MultiplyPoint(ATTACK_VERTECES[i]);
		}

		//攻撃範囲の直方体の構築
		m_workMyCube.SetCube(m_workVector3Array[0], m_workVector3Array[1], m_workVector3Array[2], m_workVector3Array[3],
			m_workVector3Array[4], m_workVector3Array[5], m_workVector3Array[6], m_workVector3Array[7]);

		//攻撃範囲の生成
		myAiManager.CharacterScript.AttackManagerScript.EnemyAttack(m_workMyCube, MaskAttribute.Non, m_powerAttack, m_effectiveAttackTime);
	}
}
