////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018年7月27日～
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
/// PlayerPrefsのキー
/// </summary>
public struct PlayerPrefsKeys
{
	/// <summary>
	/// Trueの数字
	/// </summary>
	public const int TRUE = 1;

	/// <summary>
	/// Falseの数字
	/// </summary>
	public const int FALSE = 0;

	/// <summary>
	/// ステージ番号
	/// </summary>
	public const string STAGE_NUM = "StageNum";

	/// <summary>
	/// アイテムを取得したか
	/// </summary>
	public const string IS_GET_ITEM = "IsGetItem";

	/// <summary>
	/// オープニングを見たか
	/// </summary>
	public const string IS_WATCH_OPENING = "IsWatchOpening";

	/// <summary>
	/// マスクを獲得したか
	/// </summary>
	public const string IS_GET_MASK = "IsGetMask";
}

//----------------------------------------------------------------------------------------------------
//クラス
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------
/// <summary>
/// ゲーム情報
/// </summary>
public class MyGameInfo : MySingleton<MyGameInfo>
{
	/// <summary>
	/// ステージ番号
	/// </summary>
	int m_stageNum;
	public int StageNum
	{
		get { return m_stageNum; }
		set { m_stageNum = value; }
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// データをリセットする
	/// </summary>
	public void ResetData()
	{
		//PlayerPrefsのリセット
		PlayerPrefs.SetInt(PlayerPrefsKeys.STAGE_NUM, 0);
		PlayerPrefs.SetInt(PlayerPrefsKeys.IS_GET_ITEM, 0);
		PlayerPrefs.SetInt(PlayerPrefsKeys.IS_WATCH_OPENING, 0);

		//変数のリセット
		m_stageNum = 0;
	}
}
