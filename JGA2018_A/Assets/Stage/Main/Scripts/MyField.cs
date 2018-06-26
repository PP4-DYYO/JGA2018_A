﻿//----------------------------------------------------------------------------------------------------
//
//2018年6月26日～
//作成者　京都コンピュータ学院京都駅前校　ゲーム学科　4回生　奥田裕也
//編集者
//
//----------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フィールドクラス
/// </summary>
public class MyField : MonoBehaviour
{
	/// <summary>
	/// ボス部屋の中心位置
	/// </summary>
	[SerializeField]
	Vector3 CenterPosBossRoom;
	public Vector3 BossRoomCenterPos
	{
		get { return CenterPosBossRoom; }
	}

	/// <summary>
	/// 必殺技１の位置１
	/// </summary>
	[SerializeField]
	Vector3 Pos1Deathblow1;

	/// <summary>
	/// 必殺技１の位置２
	/// </summary>
	[SerializeField]
	Vector3 Pos2Deathblow1;

	/// <summary>
	/// 必殺技１の位置３
	/// </summary>
	[SerializeField]
	Vector3 Pos3Deathblow1;

	/// <summary>
	/// 必殺技１の位置４
	/// </summary>
	[SerializeField]
	Vector3 Pos4Deathblow1;

	/// <summary>
	/// 必殺技１の位置５月
	/// </summary>
	[SerializeField]
	Vector3 Pos5Deathblow1;

	/// <summary>
	/// 作業用Vector３配列
	/// </summary>
	Vector3[] m_workVector3 = new Vector3[5];

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技の複数の位置を取得
	/// </summary>
	/// <returns></returns>
	public Vector3[] GetPosDeathblow1()
	{
		m_workVector3[0] = Pos1Deathblow1;
		m_workVector3[1] = Pos2Deathblow1;
		m_workVector3[2] = Pos3Deathblow1;
		m_workVector3[3] = Pos4Deathblow1;
		m_workVector3[4] = Pos5Deathblow1;
		return m_workVector3;
	}
}
