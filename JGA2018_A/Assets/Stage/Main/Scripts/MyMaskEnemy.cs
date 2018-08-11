////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018年7月28日～
//作成者　京都コンピュータ学院京都駅前校　ゲーム学科　4回生　奥田裕也
//編集者
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----------------------------------------------------------------------------------------------------
//Enum・Struct
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------
/// <summary>
/// マスクの敵の状態
/// </summary>
enum MaskEnemyStatus
{
	/// <summary>
	/// 待機
	/// </summary>
	Idle,
	/// <summary>
	/// 移動
	/// </summary>
	Move,
}

//----------------------------------------------------------------------------------------------------
//クラス
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------
/// <summary>
/// マスクの敵クラス
/// </summary>
public class MyMaskEnemy : MonoBehaviour
{
	#region 外部のインスタンス
	[Header("外部のインスタンス")]
	/// <summary>
	/// 攻撃マネージャ
	/// </summary>
	MyAttackManager m_attackManager;

	/// <summary>
	/// フィールド
	/// </summary>
	[SerializeField]
	MyField myField;
	#endregion

	#region 状態
	[Header("状態")]
	/// <summary>
	/// 状態
	/// </summary>
	MaskEnemyStatus m_state;

	/// <summary>
	/// フレーム前の状態
	/// </summary>
	MaskEnemyStatus m_statePrev;
	#endregion

	#region 移動
	[Header("移動")]
	/// <summary>
	/// 初期位置
	/// </summary>
	[SerializeField]
	Vector3 m_startPos;

	/// <summary>
	/// 目的位置
	/// </summary>
	[SerializeField]
	Vector3 m_targetPos;

	/// <summary>
	/// 速度
	/// </summary>
	[SerializeField]
	float m_speed;

	/// <summary>
	/// 揺れ幅
	/// </summary>
	[SerializeField]
	float m_shakeWidth;

	/// <summary>
	/// 揺れ速度
	/// </summary>
	[SerializeField]
	float m_shakeSpeed;

	/// <summary>
	/// １フレーム当たりの出現率
	/// </summary>
	[SerializeField]
	float m_appearanceRatePerFrame;

	/// <summary>
	/// 進行方向
	/// </summary>
	Vector3 m_progressiongDirection;

	/// <summary>
	/// 移動時間を数える
	/// </summary>
	float m_countMovingTime;
	#endregion

	#region 攻撃
	[Header("攻撃")]
	/// <summary>
	/// 威力
	/// </summary>
	[SerializeField]
	int m_power;

	/// <summary>
	/// 攻撃の有効時間
	/// </summary>
	[SerializeField]
	float m_effectiveAttackTime;

	/// <summary>
	/// 攻撃範囲
	/// </summary>
	MyCube m_attackRange;

	/// <summary>
	/// マトリクス
	/// </summary>
	Matrix4x4 m_matrix;

	/// <summary>
	/// 攻撃の頂点達（相対的頂点）
	/// </summary>
	static readonly Vector3[] ATTACK_VERTECES =
	{
		new Vector3(-0.125f, -0.125f, -0.125f),
		new Vector3(0.125f, -0.125f, -0.125f),
		new Vector3(-0.125f, -0.125f, 0.125f),
		new Vector3(0.125f, -0.125f, 0.125f),
		new Vector3(-0.125f, 0.125f, -0.125f),
		new Vector3(0.125f, 0.125f, -0.125f),
		new Vector3(-0.125f, 0.125f, 0.125f),
		new Vector3(0.125f, 0.125f, 0.125f),
	};

	/// <summary>
	/// 攻撃の頂点達
	/// </summary>
	Vector3[] m_attackVerteces = new Vector3[ATTACK_VERTECES.Length];
	#endregion

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 初期
	/// </summary>
	void Start()
	{
		//進行方向
		m_progressiongDirection = (m_targetPos - m_startPos).normalized;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 定期フレーム
	/// </summary>
	void FixedUpdate()
	{
		//状態
		switch (m_state)
		{
			case MaskEnemyStatus.Idle:
				IdleProcess();
				break;
			case MaskEnemyStatus.Move:
				MoveProcess();
				break;
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 待機処理
	/// </summary>
	void IdleProcess()
	{
		//初期設定
		if (m_state != m_statePrev)
		{
			m_statePrev = m_state;
		}

		//出現確立
		if (Random.Range(0f, 1f) <= m_appearanceRatePerFrame)
			m_state = MaskEnemyStatus.Move;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 移動処理
	/// </summary>
	void MoveProcess()
	{
		//初期設定
		if (m_state != m_statePrev)
		{
			transform.position = m_startPos;
			transform.LookAt(m_targetPos);
			m_statePrev = m_state;
		}

		//移動
		transform.position += m_progressiongDirection * m_speed * Time.deltaTime;
		m_countMovingTime += Time.deltaTime;
		transform.position += Vector3.up * Mathf.Sin(m_countMovingTime * m_shakeSpeed) * m_shakeWidth;

		//目的地到着
		if (Vector3.Cross((m_targetPos - transform.position), transform.right).y <= 0)
			m_state = MaskEnemyStatus.Idle;

		//SEの再生
		if (Mathf.Abs(Mathf.Sin(m_countMovingTime * m_shakeSpeed)) <= m_shakeWidth)
			MySoundManager.Instance.Play(SeCollection.StrangeVoiceOfTheMask,
				true, transform.position.x, transform.position.y, transform.position.z);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 重なり判定
	/// </summary>
	/// <param name="other">重なったもの</param>
	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals(PlayerInfo.TAG))
		{
			if (!m_attackManager)
				m_attackManager = myField.transform.parent.GetComponent<MyStage>().GameScript.CharacterScript.AttackManagerScript;

			Attack();
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 攻撃
	/// </summary>
	void Attack()
	{
		//ワールド座標で自分の原点と方向
		m_matrix.SetTRS(transform.position, transform.rotation, Vector3.one);

		//攻撃範囲頂点の決定
		for (var i = 0; i < MyCube.NUM_VERTICES; i++)
		{
			//絶対的な攻撃範囲の決定
			m_attackVerteces[i] = m_matrix.MultiplyPoint(ATTACK_VERTECES[i]);
		}

		//攻撃範囲の直方体の構築
		m_attackRange.SetCube(m_attackVerteces[0], m_attackVerteces[1], m_attackVerteces[2], m_attackVerteces[3],
			m_attackVerteces[4], m_attackVerteces[5], m_attackVerteces[6], m_attackVerteces[7]);

		m_attackManager.EnemyAttack(m_attackRange, MaskAttribute.Non, m_power, m_effectiveAttackTime);
	}
}
