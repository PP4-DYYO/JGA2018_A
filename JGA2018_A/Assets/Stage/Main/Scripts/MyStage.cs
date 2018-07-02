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

/// <summary>
/// ステージクラス
/// </summary>
public class MyStage : MonoBehaviour
{
	/// <summary>
	/// ゲーム
	/// </summary>
	[SerializeField]
	MyGame myGame;

	/// <summary>
	/// 現在のフィールド
	/// </summary>
	MyField m_currentField;
	public MyField CurrentField
	{
		get { return m_currentField; }
		set { m_currentField = value; }
	}

	/// <summary>
	/// 現在のフィールドのボス部屋の中心位置を取得
	/// </summary>
	/// <returns></returns>
	public Vector3 GetCenterPosBossRoomCurrentField()
	{
		return m_currentField.BossRoomCenterPos;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 現在のフィールドの必殺技１の位置を取得
	/// </summary>
	/// <param name="num"></param>
	/// <returns></returns>
	public Vector3 GetPosDeathblow1CurrentField(int num)
	{
		num = (num < 0) ? 0 : (num > 4) ? 4 : num;
		return m_currentField.GetPosDeathblow1()[num];
	}
}
