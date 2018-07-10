//----------------------------------------------------------------------------------------------------
//
//2018年7月8日～
//作成者　京都コンピュータ学院京都駅前校　ゲーム学科　4回生　奥田裕也
//編集者
//
//----------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メインシーンのUIクラス
/// </summary>
public class MyMainUI : MonoBehaviour
{
	/// <summary>
	/// ゲームクラス
	/// </summary>
	[SerializeField]
	MyGame myGame;
	public MyGame GameScript
	{
		get { return myGame; }
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
