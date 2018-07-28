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

/// <summary>
/// アイテムクラス
/// </summary>
public class MyItem : MonoBehaviour
{
	/// <summary>
	/// アイテム番号
	/// </summary>
	[SerializeField]
	int m_itemNum;

	/// <summary>
	/// 回転角度
	/// </summary>
	[SerializeField]
	int m_rotateAngle;

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 定期フレーム
	/// </summary>
	void FixedUpdate()
	{
		transform.Rotate(Vector3.up, m_rotateAngle * Time.deltaTime);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 重なり判定
	/// </summary>
	/// <param name="other">重なったもの</param>
	void OnTriggerEnter(Collider other)
	{
		//プレイヤー
		if(other.tag.Equals(PlayerInfo.TAG))
		{
			//アイテムを2進数で保存
			PlayerPrefs.SetInt(PlayerPrefsKeys.IS_GET_ITEM, PlayerPrefs.GetInt(PlayerPrefsKeys.IS_GET_ITEM) | (int)Mathf.Pow(2, m_itemNum));
			Destroy(gameObject);
		}
	}
}
