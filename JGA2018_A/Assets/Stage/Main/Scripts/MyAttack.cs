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

/// <summary>
/// 攻撃
/// </summary>
public class MyAttack : MonoBehaviour
{
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
}
