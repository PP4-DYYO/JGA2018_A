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
/// キャラクタークラス
/// </summary>
public class MyCharactor : MonoBehaviour
{
	/// <summary>
	/// ゲーム
	/// </summary>
	[SerializeField]
	MyGame myGame;

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

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
