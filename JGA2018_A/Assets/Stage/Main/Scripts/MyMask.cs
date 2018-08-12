////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018年7月17日～
//作成者　京都コンピュータ学院京都駅前校　ゲーム学科　4回生　奥田裕也
//編集者
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マスククラス
/// </summary>
public class MyMask : MonoBehaviour
{
	/// <summary>
	/// 重さ
	/// </summary>
	[SerializeField]
	float m_weight;

	/// <summary>
	/// 抵抗
	/// </summary>
	[SerializeField]
	float m_drag;

	/// <summary>
	/// リジッドボディ
	/// </summary>
	[SerializeField]
	Rigidbody RB;

	/// <summary>
	/// 高さ
	/// </summary>
	[SerializeField]
	float m_height;

	/// <summary>
	/// マスク属性
	/// </summary>
	MaskAttribute m_attribute;
	public MaskAttribute Attribute
	{
		get { return m_attribute; }
	}

	/// <summary>
	/// 初期位置
	/// </summary>
	Vector3 m_startPos;

	/// <summary>
	/// 目的位置
	/// </summary>
	Vector3 m_targetPos;

	/// <summary>
	/// 移動フラグ
	/// </summary>
	bool m_isMove;

	/// <summary>
	/// マスクのタグ
	/// </summary>
	public const string MASK_TAG = "Mask";

	/// <summary>
	/// パーティカル
	/// </summary>
	ParticleSystem m_particle;

	/// <summary>
	/// 作業用のFloat
	/// </summary>
	float m_workFloat;

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 初期
	/// </summary>
	void Start()
	{
		transform.position = m_startPos;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 定期フレーム
	/// </summary>
	void FixedUpdate()
	{
		//目的地についていないand移動している
		if (transform.position != m_targetPos && m_isMove)
		{
			//落ちている割合
			m_workFloat = (transform.position.y - m_startPos.y) / (m_targetPos.y - m_startPos.y);

			//移動割合が１以下
			if (m_workFloat < 1)
			{
				//捨てている
				transform.position = m_startPos + (m_targetPos - m_startPos) * m_workFloat;
			}
			else
			{
				//落ちた
				m_isMove = false;
				transform.position = m_targetPos + Vector3.up * m_height;
				transform.rotation = Quaternion.identity;
				GetComponent<SphereCollider>().enabled = true;
			}
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// マスクを捨てる
	/// </summary>
	/// <param name="maskAttribute">マスク属性</param>
	/// <param name="startPos">開始位置</param>
	/// <param name="targetPos">目的位置</param>
	public void ThrowAwayMask(MaskAttribute maskAttribute, Vector3 startPos, Vector3 targetPos)
	{
		m_attribute = maskAttribute;
		m_startPos = startPos;
		transform.position = m_startPos;
		m_targetPos = targetPos;
		m_isMove = true;
		RB.mass = m_weight;
		RB.drag = m_drag;

		m_particle = transform.parent.GetComponent<MyAiManager>().CharacterScript
			.GameScript.ParticleManagerScript.CreateParticle(ParticleKind.PickUpMask);
		m_particle.transform.position = transform.position;
		m_particle.transform.SetParent(transform);
	}
}
