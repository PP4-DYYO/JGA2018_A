////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/8/8～
//制作者　京都コンピュータ学院京都駅前校　ゲーム学科　四回生　奥田裕也
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エンディングカメラクラス
/// </summary>
public class MyEndingCamera : MonoBehaviour
{
	/// <summary>
	/// 目標オブジェクト
	/// </summary>
	[SerializeField]
	GameObject TargetObj;

	/// <summary>
	/// 目標の身長
	/// </summary>
	[SerializeField]
	float m_heightOfTarget;

	/// <summary>
	/// 初期位置たち
	/// </summary>
	[SerializeField]
	Vector3[] m_startPoss;

	/// <summary>
	/// 目標位置たち
	/// </summary>
	[SerializeField]
	Vector3[] m_targetPoss;

	/// <summary>
	/// 位置番号
	/// </summary>
	int m_posNum;

	/// <summary>
	/// 移動時間たち
	/// </summary>
	[SerializeField]
	float[] m_movingTimes;

	/// <summary>
	/// 移動時間を数える
	/// </summary>
	float m_countMovingTime;
	
	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 定期フレーム
	/// </summary>
	void FixedUpdate()
	{
		//移動時間
		if (m_countMovingTime <= m_movingTimes[m_posNum])
		{
			//カメラの位置
			transform.position = m_startPoss[m_posNum] + (m_targetPoss[m_posNum] - m_startPoss[m_posNum]) * (m_countMovingTime / m_movingTimes[m_posNum]);

			//プレイヤー情報の反映
			ReflectingPlayerInfo();

			m_countMovingTime += Time.deltaTime;
		}
		else if(m_posNum < m_startPoss.Length)
		{
			//移動番号の繰り上げ
			m_posNum++;
			m_countMovingTime = 0;

			//カメラの位置
			transform.position = m_startPoss[m_posNum];

			//プレイヤー情報の反映
			ReflectingPlayerInfo();
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// プレイヤー情報の反映
	/// </summary>
	void ReflectingPlayerInfo()
	{
		transform.position += TargetObj.transform.position;
		transform.LookAt(TargetObj.transform.position + Vector3.up * (m_heightOfTarget * 0.75f));
	}
}
