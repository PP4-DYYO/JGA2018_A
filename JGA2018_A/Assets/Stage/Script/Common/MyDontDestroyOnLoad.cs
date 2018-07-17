﻿////////////////////////////////////////////////////////////
//
//2017/4/19～
//製作者　京都コンピュータ学院　ゲーム学科　３回生　奥田裕也
//
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シーン間を跨ぐオブジェクトのクラス
/// </summary>
public class MyDontDestroyOnLoad : MonoBehaviour
{
	/// <summary>
	/// 起動時メソッド
	/// </summary>
	void Awake()
	{
		//シーン間で削除されない
		DontDestroyOnLoad(gameObject);
	}
}
