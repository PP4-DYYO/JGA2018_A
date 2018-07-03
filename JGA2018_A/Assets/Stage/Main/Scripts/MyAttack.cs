//----------------------------------------------------------------------------------------------------
//
//2018年6月8日～
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
/// 攻撃の種類
/// </summary>
public enum AttackKind
{
	MyCube,
	MyPrism12,
}

//----------------------------------------------------------------------------------------------------
//クラス
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------
/// <summary>
/// 攻撃
/// </summary>
public class MyAttack : MonoBehaviour
{
	/// <summary>
	/// メッシュフィッター
	/// </summary>
	MeshFilter m_mf;

	/// <summary>
	/// メッシュコライダー
	/// </summary>
	MeshCollider m_mc;

	/// <summary>
	/// 種類
	/// </summary>
	AttackKind m_kind;
	public AttackKind Kind
	{
		set { m_kind = value; }
	}

	/// <summary>
	/// 位置
	/// </summary>
	Vector3 m_pos;
	public Vector3 Pos
	{
		set { m_pos = value; }
	}

	/// <summary>
	/// 頂点の中心位置
	/// </summary>
	Vector3 m_centerPosVertices;
	public Vector3 CenterPosVertices
	{
		get { return m_centerPosVertices; }
		set { m_centerPosVertices = value; }
	}

	/// <summary>
	/// 属性
	/// </summary>
	MaskAttribute m_attribute;
	public MaskAttribute Attribute
	{
		get { return m_attribute; }
		set { m_attribute = value; }
	}

	/// <summary>
	/// 威力
	/// </summary>
	int m_power;
	public int Power
	{
		get { return m_power; }
		set { m_power = value; }
	}

	/// <summary>
	/// 拡張する時間を数える
	/// </summary>
	float m_countTimeExpansion;

	/// <summary>
	/// 拡張時間
	/// </summary>
	float m_expansionTime;
	public float ExpansionTime
	{
		set { m_expansionTime = value; }
	}

	/// <summary>
	/// 拡張する距離
	/// </summary>
	float m_expansionDistance;
	public float ExpansionDistance
	{
		set { m_expansionDistance = value; }
	}

	/// <summary>
	/// １２角柱
	/// </summary>
	MyPrism12 m_prism12;
	public MyPrism12 Prism12
	{
		set { m_prism12 = value; }
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 初期
	/// </summary>
	void Start()
	{
		m_mf = GetComponent<MeshFilter>();
		m_mc = GetComponent<MeshCollider>();

		if (m_pos != Vector3.zero)
		{
			transform.position = m_pos;
			m_centerPosVertices += m_pos;
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 定期フレーム
	/// </summary>
	void FixedUpdate()
	{
		//拡張しないもの
		if (m_expansionTime <= 0)
			return;

		//拡張
		Expansion();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 拡張
	/// </summary>
	void Expansion()
	{
		//拡張中
		if (m_countTimeExpansion <= m_expansionTime)
		{
			m_countTimeExpansion += Time.deltaTime;
			switch(m_kind)
			{
				case AttackKind.MyCube:
					break;
				case AttackKind.MyPrism12:
					ExpansionMyPrism12();
					break;
			}
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 拡張する１２角柱
	/// </summary>
	void ExpansionMyPrism12()
	{
		m_prism12.SetRadius((m_countTimeExpansion / m_expansionTime) * m_expansionDistance);

		//頂点の設定
		m_mf.mesh.vertices = m_prism12.GetVertices();
		m_mf.mesh.triangles = MyPrism12.COMBINATION_VERTICES;
		m_mf.mesh.RecalculateBounds(); //メッシュコンポーネントのプロパティboundsを再計算する
		m_mf.mesh.RecalculateNormals();
		m_mc.sharedMesh = m_mf.mesh;
	}
}
