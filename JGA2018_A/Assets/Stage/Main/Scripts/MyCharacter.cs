﻿////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018年5月8日～
//作成者　京都コンピュータ学院京都駅前校　ゲーム学科　4回生　奥田裕也
//編集者
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----------------------------------------------------------------------------------------------------
//Enum
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------
/// <summary>
/// マスクの属性
/// </summary>
public enum MaskAttribute
{
	/// <summary>
	/// なし
	/// </summary>
	Non,
	/// <summary>
	/// 配達
	/// </summary>
	Carry,
	/// <summary>
	/// ウイルス
	/// </summary>
	Virus,
	/// <summary>
	/// 鏡
	/// </summary>
	Mirror,
	/// <summary>
	/// マジック
	/// </summary>
	Magic,
}

//----------------------------------------------------------------------------------------------------
//クラス
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------
/// <summary>
/// キャラクタークラス
/// </summary>
public class MyCharacter : MonoBehaviour
{
	/// <summary>
	/// ゲーム
	/// </summary>
	[SerializeField]
	MyGame myGame;
	public MyGame GameScript
	{
		get { return myGame; }
	}

	/// <summary>
	/// AIマネージャ
	/// </summary>
	[SerializeField]
	MyAiManager myAiManager;
	public MyAiManager AiManagerScript
	{
		get { return myAiManager; }
	}

	/// <summary>
	/// プレイヤー
	/// </summary>
	[SerializeField]
	MyPlayer myPlayer;
	public MyPlayer PlayerScript
	{
		get { return myPlayer; }
	}

	/// <summary>
	/// 攻撃マネージャ
	/// </summary>
	[SerializeField]
	MyAttackManager myAttackManager;
	public MyAttackManager AttackManagerScript
	{
		get { return myAttackManager; }
	}

	/// <summary>
	/// ボス
	/// </summary>
	MyAiBoss m_boss;
	public MyAiBoss BossScript
	{
		get { return m_boss; }
		set { m_boss = value; }
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ボスの位置を得る
	/// </summary>
	/// <returns>ボスの位置</returns>
	public Vector3 GetPosBoss()
	{
		//ボス不在
		if (!m_boss)
			return Vector3.down * byte.MaxValue;

		return m_boss.transform.position;
	}
}
