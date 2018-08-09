////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/8/8～
//制作者　京都コンピュータ学院京都駅前校　ゲーム学科　四回生　奥田裕也
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEndingCharacter : MonoBehaviour
{
	/// <summary>
	/// 移動方向
	/// </summary>
	[SerializeField]
	Vector3 m_directionOfMovement;

	/// <summary>
	/// 移動速度
	/// </summary>
	[SerializeField]
	float m_movingSpeed;

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 定期フレーム
	/// </summary>
	void FixedUpdate()
	{
		transform.Translate(m_directionOfMovement * m_movingSpeed * Time.deltaTime);
	}
}
