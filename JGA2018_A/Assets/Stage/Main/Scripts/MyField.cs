//----------------------------------------------------------------------------------------------------
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
	/// スタートする位置
	/// </summary>
	[SerializeField]
	Vector3 m_startPos;
	public Vector3 StartPos
	{
		get { return m_startPos; }
	}

	/// <summary>
	/// カメラの相対的位置
	/// </summary>
	[SerializeField]
	Vector3 m_relativePosCamera;
	public Vector3 RelativePosCamera
	{
		get { return m_relativePosCamera; }
	}

	/// <summary>
	/// ボスのスタート位置
	/// </summary>
	[SerializeField]
	Vector3 m_bossStartPos;
	public Vector3 BossStartPos
	{
		get { return m_bossStartPos; }
	}

	/// <summary>
	/// ボスのスタート向き
	/// </summary>
	[SerializeField]
	Vector3 m_bossStartDirection;
	public Vector3 BossStartDirection
	{
		get { return m_bossStartDirection; }
	}

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
	/// 必殺技１の位置５
	/// </summary>
	[SerializeField]
	Vector3 Pos5Deathblow1;

	/// <summary>
	/// ボスイベント発生の壁
	/// </summary>
	[SerializeField]
	Collider WallOccurrenceBossEvent;
	public Collider WallOccurrenceBossEventCollider
	{
		get { return WallOccurrenceBossEvent; }
	}

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
