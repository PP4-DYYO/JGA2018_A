////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/6/25～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----------------------------------------------------------------------------------------------------
/// <summary>
/// 大臣の行動状態
/// </summary>
public enum MinisterBehaviorStatus
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
	/// 投げる
	/// </summary>
	Throw,
	/// <summary>
	/// なし
	/// </summary>
	Non,
}

public class MyAiBoss : MonoBehaviour
{
	/// <summary>
	/// 状態
	/// </summary>
	protected MinisterBehaviorStatus m_behaviorState;

	/// <summary>
	/// 弱い敵のアニメーション
	/// </summary>
	const string MINISTER_ANIM = "MinisterAnimation";

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
	/// 攻撃遷移
	/// </summary>
	const string ANIM_ATTACK = "Attack";

	/// <summary>
	/// 投げる遷移
	/// </summary>
	const string ANIM_THROW = "Throw";

	/// <summary>
	/// 死ぬ遷移
	/// </summary>
	const string ANIM_DIE = "Die";

	protected MyPlayer myPlayer;

	protected MyAiManager myAiManager;

	/// <summary>
	/// プレイヤーオブジェクトの名前
	/// </summary>
	[SerializeField]
	protected string PLAYER_OBJECT_NAME = "DummyPlayer";
	public string PlayerObjectName
	{
		get { return PLAYER_OBJECT_NAME; }
	}

	/// <summary>
	/// MyCharacterクラス
	/// </summary>
	MyCharacter myCharacter;
	public MyCharacter CharacterScript
	{
		get { return myCharacter; }
	}


	/// <summary>
	/// MyAttackManagerクラス
	/// </summary>
	MyAttackManager myAttackManager;
	public MyAttackManager AttackManagerScript
	{
		get { return myAttackManager; }
	}

	/// <summary>
	/// 自分のタグ名
	/// </summary>
	public const string TAG_NAME = "Boss";

	/// <summary>
	/// 自分の名前"CarryMinister","VirusMinister","MirrorMinister","MagicMinister"
	/// </summary>
	[SerializeField]
	protected string m_myObjectName;

	/// <summary>
	/// ステージのスクリプト
	/// </summary>
	[SerializeField]
	protected MyStage myStage;

	/// <summary>
	/// マスクの位置用ゲームオブジェクトの名前
	/// </summary>
	[SerializeField]
	const string MASK_POSITION_OBJECT_NAME = "MaskPositionObject";
	public string MaskPositionObjectName
	{
		get { return MASK_POSITION_OBJECT_NAME; }
	}

	/// <summary>
	/// マスクの位置用ゲームオブジェクト
	/// </summary>
	[SerializeField]
	protected GameObject m_maskPositionObject;


	/// <summary>
	/// MAXHP//
	/// </summary>
	[SerializeField]
	protected int m_maxHitPoint;
	public int MaxHitPoint
	{
		get { return m_maxHitPoint; }
	}

	/// <summary>
	/// HP//
	/// </summary>
	protected int m_hitPoint;
	public int HitPoint
	{
		get { return m_hitPoint; }
	}

	/// <summary>
	/// 攻撃力
	/// </summary>
	[SerializeField]
	protected int m_attack;
	public int Attack
	{
		get { return m_attack; }
	}

	/// <summary>
	/// 攻撃番号（0:近距離、1:遠距離 2:スペシャル）
	/// </summary>
	[SerializeField]
	protected int m_attackNum;


	/// <summary>
	/// 知覚範囲
	//</summary>
	[SerializeField]
	protected int m_perceivedRange;


	/// <summary>
	/// 攻撃後か
	/// </summary>
	[SerializeField]
	protected bool m_isAttacked;

	/// <summary>
	/// 攻撃間隔//
	/// </summary>
	[SerializeField]
	protected float m_attackInterval;

	/// <summary>
	/// 一歩の移動距離//
	/// </summary>
	[SerializeField]
	protected float m_step;

	/// <summary>
	/// xへの移動はt:プラス/f:マイナス//
	/// </summary>
	[SerializeField]
	protected bool m_movingX;

	/// <summary>
	/// zへの移動はt:プラス/f:マイナス//
	/// </summary>
	[SerializeField]
	protected bool m_movingZ;

	/// <summary>
	/// xzそれぞれの移動量//
	/// </summary>
	[SerializeField]
	protected float m_moveX;
	[SerializeField]
	protected float m_moveZ;

	/// <summary>
	/// 攻撃回数カウント
	/// </summary>
	[SerializeField]
	protected int m_attackCount;

	public int AttackCount
	{
		get { return m_attackCount; }
	}

	/// <summary>
	/// 特殊技の使用制限数//
	/// </summary>
	[SerializeField]
	protected int m_specialAttackLimit;

	/// <summary>
	/// 特殊技の使用数//
	/// </summary>
	[SerializeField]
	protected int m_specialAttackCount;

	///<summary>
	///マジック用カウンターフラグ// 0:発動抽選中 1：発動中 2:カウンター攻撃待機 
	///</summary>>
	[SerializeField]
	protected int m_counterAttackFlag;

	/// <summary>
	/// プレイヤーが攻撃してきたフラグ
	/// </summary>
	[SerializeField]
	protected bool m_playerAttacked;

	/// <summary>
	/// プレイヤーとの距離
	/// </summary>
	[SerializeField]
	protected float m_distance;

	/// <summary>
	/// 仮面を捨てた
	/// </summary>
	[SerializeField]
	protected bool m_maskThrow;

	/// <summary>
	/// 毒の霧エフェクトプレファブ
	/// </summary>
	[SerializeField]
	GameObject poizonFog;

	/// <summary>
	/// 自分の状態//
	/// </summary>
	[SerializeField]
	protected AIMode m_aimode;

	/// <summary>
	/// 特殊技待機状態
	/// </summary>
	[SerializeField]
	protected bool m_specialFlag;

	/// <summary>
	/// 行動制御用(時間)
	/// </summary>
	[SerializeField]
	protected float m_gameTime;

	/// <summary>
	/// 毒計算用時間
	/// </summary>
	float m_poizonTime;

	/// <summary>
	/// 毒間隔
	/// </summary>
	[SerializeField]
	float m_poizonDamageTime = 6.0f;

	/// <summary>
	/// 自身が本物かどうか
	/// </summary>
	/// [SerializeField]
	protected bool m_isFakeBody;

	/// <summary>
	/// ドッペルゲンガーが出現中かどうか
	/// </summary>
	/// [SerializeField]
	protected bool m_isDopApper;

	/// <summary>
	/// 影武者出現中かどうか
	/// </summary>
	[SerializeField]
	protected bool m_isShadowApper;

	/// <summary>
	/// アニメーター
	/// </summary>
	[SerializeField]
	protected Animator Anim;

	/// <summary>
	/// AIの行動タイプ
	/// </summary>
	[SerializeField]
	protected enum AIMode
	{
		/// <summary>
		/// AI起動待ち
		/// </summary>
		WAIT,
		/// <summary>
		/// 待機
		/// </summary>
		IDLE,
		/// <summary>
		/// 攻撃
		/// </summary>
		ATTACK,
		/// <summary>
		/// 防御
		/// </summary>
		SKILL,
		/// <summary>
		/// 近づく
		/// </summary>
		APPROACH,
		/// <summary>
		/// 離れる
		/// </summary>
		LEAVE
	}



	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 初期状態
	/// </summary>
	protected virtual void Start()
	{
		//m_aimode = AIMode.WAIT;
		m_hitPoint = m_maxHitPoint;
		myAiManager = transform.parent.GetComponent<MyAiManager>();
		myAttackManager = myAiManager.CharacterScript.AttackManagerScript;
		myStage = myAiManager.CharacterScript.GameScript.StageScript;
		myPlayer = myAiManager.CharacterScript.PlayerScript;
		myCharacter = myAiManager.CharacterScript;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// AIの基本行動
	/// </summary>
	protected virtual void FixedUpdate()
	{
		//起動状態の時
		if (m_aimode != AIMode.WAIT)
		{
			//ステージから落下した場合中央に戻す
			if (m_distance > 30)
			{
				PositionReset();
			}

			if (m_gameTime <= m_attackInterval)
			{
				m_gameTime += Time.deltaTime;
			}

			Vector3 targetPos = myPlayer.transform.position;
			// Y座標を固定
			targetPos.y = transform.position.y;
			//プレイヤーの方を向く
			transform.LookAt(targetPos);

			//プレイヤーとの距離
			m_distance = (myPlayer.transform.position - transform.position).magnitude;

			//位置関係を確認して、移動の+-を変更する
			if (myPlayer.transform.position.x - transform.position.x > 0)
			{
				if (m_moveX != m_step)
				{
					m_moveX = m_step;
				}
			}
			else if (myPlayer.transform.position.x - transform.position.x < 0)
			{
				if (m_moveX != -m_step)
				{
					m_moveX = -m_step;
				}
			}

			if (myPlayer.transform.position.z - transform.position.z > 0)
			{
				if (m_moveZ != m_step)
				{
					m_moveZ = m_step;
				}
			}
			else if (myPlayer.transform.position.z - transform.position.z < 0)
			{
				if (m_moveZ != -m_step)
				{
					m_moveZ = -m_step;
				}
			}
		}

		//マスクを捨てる
		if (m_hitPoint < m_maxHitPoint / 2 && m_maskThrow == false)
		{
			float randx = Random.Range(1, 3);
			float randz = Random.Range(1, 3);
			switch (m_myObjectName)
			{
				case "CarryMinister(Clone)":
					transform.parent.GetComponent<MyAiManager>().
						ThrowAwayBossMask(MaskAttribute.Carry, new Vector3(m_maskPositionObject.transform.position.x, m_maskPositionObject.transform.position.y, m_maskPositionObject.transform.position.z),
						myStage.CurrentField.BossRoomCenterPos);
					break;
				case "VirusMinister(Clone)":
					transform.parent.GetComponent<MyAiManager>().
						ThrowAwayBossMask(MaskAttribute.Virus, new Vector3(m_maskPositionObject.transform.position.x, m_maskPositionObject.transform.position.y, m_maskPositionObject.transform.position.z),
						myStage.CurrentField.BossRoomCenterPos);
					break;
				case "MirrorMinister(Clone)":
					transform.parent.GetComponent<MyAiManager>().
						ThrowAwayBossMask(MaskAttribute.Mirror, new Vector3(m_maskPositionObject.transform.position.x, m_maskPositionObject.transform.position.y, m_maskPositionObject.transform.position.z),
					   myStage.CurrentField.BossRoomCenterPos);
					break;
				case "MagicMinister(Clone)":
					transform.parent.GetComponent<MyAiManager>().
						ThrowAwayBossMask(MaskAttribute.Magic, new Vector3(m_maskPositionObject.transform.position.x, m_maskPositionObject.transform.position.y, m_maskPositionObject.transform.position.z),
					   myStage.CurrentField.BossRoomCenterPos);
					break;
			}
			m_maskThrow = true;

			//SEの再生
			MySoundManager.Instance.Play(SeCollection.ThrowAwayTheMask, true, transform.position.x, transform.position.y, transform.position.z);

		}
		Animation();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// AIを起動する
	/// </summary>
	public void StartAI()
	{
		m_aimode = AIMode.IDLE;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 通常攻撃
	/// </summary>
	public void NomalAttack()
	{
		switch (m_myObjectName)
		{
			case "CarryMinister(Clone)":
				//能力技発動条件
				if (m_hitPoint < m_maxHitPoint / 2 && m_specialAttackCount == 0 ||
				   m_hitPoint < m_maxHitPoint / 4 && m_specialAttackCount == 1)
				{
					SpecialAttack();
					m_specialAttackCount++;
				}
				else
				{
					GameObject.Find("ArrowPoint").GetComponent<MyArrowShot>().Shot(m_attackNum);
				}
				break;
			case "VirusMinister(Clone)":
				//HPが一定で制限に達していないとき

				break;
			case "MirrorMinister(Clone)":
			case "MirrorMinister(Clone)(Clone)":
				//HPが一定で制限に達していないとき
				if (m_hitPoint < m_hitPoint / 4 && m_specialAttackCount < m_specialAttackLimit)
				{
					SpecialAttack();
				}
				else
				{
					MakeAttackRange();

					//SEの再生
					MySoundManager.Instance.Play(SeCollection.Punch, true, transform.position.x, transform.position.y, transform.position.z);
				}
				break;

			case "MagicMinister(Clone)":
				MakeAttackRange();
				m_isShadowApper = false;
				m_aimode = AIMode.IDLE;
				m_counterAttackFlag = 0;
				break;
		}
		m_isAttacked = true;
		m_gameTime = 0;
		m_attackCount += 1;
	}

	//----------------------------------------------------------------------------------------------------
	///<summary>
	///攻撃当たり範囲作成//
	///</summary>
	void MakeAttackRange()
	{
		Vector3 attackPoint = GameObject.Find(m_myObjectName).transform.position;

		float cubeLength = 1f;
		//頂点の位置
		Vector3 vLDB = new Vector3(attackPoint.x - cubeLength, 1 + attackPoint.y - cubeLength, attackPoint.z - cubeLength);
		Vector3 vLDF = new Vector3(attackPoint.x - cubeLength, 1 + attackPoint.y - cubeLength, attackPoint.z + cubeLength);
		Vector3 vLUB = new Vector3(attackPoint.x - cubeLength, 1 + attackPoint.y + cubeLength, attackPoint.z - cubeLength);
		Vector3 vLUF = new Vector3(attackPoint.x - cubeLength, 1 + attackPoint.y + cubeLength, attackPoint.z + cubeLength);
		Vector3 vRDB = new Vector3(attackPoint.x + cubeLength, 1 + attackPoint.y - cubeLength, attackPoint.z - cubeLength);
		Vector3 vRDF = new Vector3(attackPoint.x + cubeLength, 1 + attackPoint.y - cubeLength, attackPoint.z + cubeLength);
		Vector3 vRUB = new Vector3(attackPoint.x + cubeLength, 1 + attackPoint.y + cubeLength, attackPoint.z - cubeLength);
		Vector3 vRUF = new Vector3(attackPoint.x + cubeLength, 1 + attackPoint.y + cubeLength, attackPoint.z + cubeLength);

		//当たり判定発生
		MyCube attackRange = new MyCube(vLDB, vRDB, vLDF, vRDF, vLUB, vRUB, vLUF, vRUF);
		myAttackManager.EnemyAttack(attackRange, MaskAttribute.Non, m_attack, 1);
	}

	//----------------------------------------------------------------------------------------------------
	///<summary>
	///特殊攻撃//
	///</summary>
	public void SpecialAttack()
	{
		switch (m_myObjectName)
		{
			case "CarryMinister(Clone)":
				float warpPosX, warpPosY, warpPosZ;
				float randamX = Random.Range(-4, 4);
				float randamZ = Random.Range(-4, 4);
				warpPosX = myStage.CurrentField.BossRoomCenterPos.x + randamX;
				warpPosY = myStage.CurrentField.BossRoomCenterPos.y + 1;
				warpPosZ = myStage.CurrentField.BossRoomCenterPos.z + randamZ;
				myPlayer.transform.position = new Vector3(warpPosX, warpPosY, warpPosZ);

				//SEの再生
				MySoundManager.Instance.Play(SeCollection.Warp,
					true, myPlayer.transform.position.x, myPlayer.transform.position.y, myPlayer.transform.position.z);
				break;
			case "VirusMinister(Clone)":
				m_attack = 5;
				Vector3 attackPoint = GameObject.Find(m_myObjectName).transform.position;

				GameObject poizonfog = GameObject.Instantiate(poizonFog) as GameObject;
				poizonfog.transform.position = transform.position;

				float cubeLength = 2f;
				//頂点の位置
				Vector3 vLDB = new Vector3(attackPoint.x - cubeLength, 1 + attackPoint.y - cubeLength, attackPoint.z - cubeLength);
				Vector3 vLDF = new Vector3(attackPoint.x - cubeLength, 1 + attackPoint.y - cubeLength, attackPoint.z + cubeLength);
				Vector3 vLUB = new Vector3(attackPoint.x - cubeLength, 1 + attackPoint.y + cubeLength, attackPoint.z - cubeLength);
				Vector3 vLUF = new Vector3(attackPoint.x - cubeLength, 1 + attackPoint.y + cubeLength, attackPoint.z + cubeLength);
				Vector3 vRDB = new Vector3(attackPoint.x + cubeLength, 1 + attackPoint.y - cubeLength, attackPoint.z - cubeLength);
				Vector3 vRDF = new Vector3(attackPoint.x + cubeLength, 1 + attackPoint.y - cubeLength, attackPoint.z + cubeLength);
				Vector3 vRUB = new Vector3(attackPoint.x + cubeLength, 1 + attackPoint.y + cubeLength, attackPoint.z - cubeLength);
				Vector3 vRUF = new Vector3(attackPoint.x + cubeLength, 1 + attackPoint.y + cubeLength, attackPoint.z + cubeLength);

				////当たり判定発生
				MyCube attackRange = new MyCube(vLDB, vRDB, vLDF, vRDF, vLUB, vRUB, vLUF, vRUF);
				myAttackManager.EnemyAttack(attackRange, MaskAttribute.Virus, m_attack, 2);
				m_attack = 25;

				//SEの再生
				MySoundManager.Instance.Play(SeCollection.PoisonGas, true, transform.position.x, transform.position.y, transform.position.z);
				break;
			case "MirrorMinister(Clone)":
				break;
			case "MagicMinister(Clone)":
				break;
		}
		m_gameTime = 0;
		m_specialAttackCount += 1;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	///damage分のダメージを受ける
	/// </summary>
	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == AttackManagerTag.PLAYER_ATTACK_RANGE_TAG)
		{
			if (m_myObjectName == "ShadowMagicMinister(Clone)")
			{
				GameObject.Find("MagicMinister(Clone)").GetComponent<MyMagicMinisterAI>().m_appearReset = true;
				Destroy(gameObject);
			}
			var attack = other.GetComponent<MyAttack>();
			if (attack.Attribute == MaskAttribute.Virus)
			{
				if (!attack.IsExpansion())
				{
					ReceiveDamage(other.GetComponent<MyAttack>().Power);
				}
				else
				{
					return;
				}
			}
			else if (attack.Attribute == MaskAttribute.Carry)
			{
				PositionReset();
				ReceiveDamage(other.GetComponent<MyAttack>().Power);
			}
			else
			{
				ReceiveDamage(other.GetComponent<MyAttack>().Power);
			}
		}
	}


	//----------------------------------------------------------------------------------------------------
	/// <summary>
	///毒ダメージ用
	/// </summary>
	public void OnTriggerStay(Collider other)
	{
		if (other.tag == AttackManagerTag.PLAYER_ATTACK_RANGE_TAG)
		{
			if (other.GetComponent<MyAttack>().Attribute == MaskAttribute.Virus)
			{
				m_poizonTime += Time.deltaTime;
				if (m_poizonTime > m_poizonDamageTime)
				{
					m_poizonTime = 0;
					ReceiveDamage(other.GetComponent<MyAttack>().Power / 2);
				}
			}
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	///damage分のダメージを受ける
	/// </summary>
	public virtual void ReceiveDamage(int damage)
	{
		m_hitPoint = m_hitPoint - damage;
		ReceiveDamageAnimation();

		//キャリーは被ダメ時に特殊技
		if (m_myObjectName == "CarryMinister(Clone)" && m_hitPoint < m_maxHitPoint / 2 && m_specialAttackCount == 0 ||
		   m_myObjectName == "CarryMinister(Clone)" && m_hitPoint < m_maxHitPoint / 4 && m_specialAttackCount == 1)
		{
			if (enabled)
			{
				SpecialAttack();
				m_specialFlag = false;
			}
		}

		//マジックのカウンター攻撃
		else if (m_myObjectName == "MagicMinister(Clone)" && m_counterAttackFlag == 1)
		{
			m_counterAttackFlag = 2;
		}

		//HP0で死ぬ
		if (m_hitPoint <= 0)
		{
			if (m_isFakeBody)
			{
				Destroy(gameObject);
			}
			else
			{
				transform.parent.GetComponent<MyAiManager>().CharacterScript.GameScript.ChangeState(StageStatus.BossDestroyed);
			}

			//SEを再生
			SoundToDie();
		}
		else
		{
			//SEを再生
			SoundToBeDamaged();
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 死ぬ音
	/// </summary>
	protected virtual void SoundToDie()
	{
		//SEの再生
		MySoundManager.Instance.Play(SeCollection.WeakEnemyDied, true, transform.position.x, transform.position.y, transform.position.z);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ダメージを受ける音
	/// </summary>
	protected virtual void SoundToBeDamaged()
	{
		MySoundManager.Instance.Play(SeCollection.ReceiveDamage, true, transform.position.x, transform.position.y, transform.position.z);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// アニメーション
	/// </summary>
	void Animation()
	{
		//状態遷移済み
		if ((int)m_behaviorState == Anim.GetInteger(MINISTER_ANIM))
		{
			Anim.SetInteger(MINISTER_ANIM, (int)EnemyBehaviorStatus.Non);
			return;
		}


		//状態とアニメーションが一緒
		if (m_behaviorState == MinisterBehaviorStatus.Idle && Anim.GetCurrentAnimatorStateInfo(0).IsName(ANIM_LAYER + ANIM_IDLE)
			|| m_behaviorState == MinisterBehaviorStatus.Walk && Anim.GetCurrentAnimatorStateInfo(0).IsName(ANIM_LAYER + ANIM_WALK)
			|| m_behaviorState == MinisterBehaviorStatus.Attack && Anim.GetCurrentAnimatorStateInfo(0).IsName(ANIM_LAYER + ANIM_ATTACK)
			|| m_behaviorState == MinisterBehaviorStatus.Throw && Anim.GetCurrentAnimatorStateInfo(0).IsName(ANIM_LAYER + ANIM_THROW)
			|| m_behaviorState == MinisterBehaviorStatus.Die && Anim.GetCurrentAnimatorStateInfo(0).IsName(ANIM_LAYER + ANIM_DIE))
			return;

		//状態変更
		Anim.SetInteger(MINISTER_ANIM, (int)m_behaviorState);
	}



	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ポジションを戻す
	/// </summary>
	void PositionReset()
	{
		transform.position = myStage.CurrentField.BossRoomCenterPos;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	///被ダメアニメーション
	/// </summary>
	public void ReceiveDamageAnimation()
	{
		//Debug.Log("被ダメアニメーション");
	}
}
