////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/8/9～
//制作者　京都コンピュータ学院京都駅前校　ゲーム学科　四回生　奥田裕也
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回復の渦クラス
/// </summary>
public class MyRecoveryWhirlpool : MonoBehaviour
{
	/// <summary>
	/// 重なり判定
	/// </summary>
	/// <param name="other">重なったもの</param>
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == PlayerInfo.TAG)
		{
			other.GetComponent<MyPlayer>().RecoveryHp();
			Destroy(gameObject);
		}
	}
}
