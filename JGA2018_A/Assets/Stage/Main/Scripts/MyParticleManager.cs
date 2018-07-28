////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018年7月24日～
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
/// パーティクルの種類
/// </summary>
public enum ParticleKind
{
	/// <summary>
	/// 毒の必殺技
	/// </summary>
	VirusDethblow,
	/// <summary>
	/// 拾うマスク
	/// </summary>
	PickUpMask,
}

//----------------------------------------------------------------------------------------------------
//Enum・Struct
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------
/// <summary>
/// パーティクルマネージャ
/// </summary>
public class MyParticleManager : MonoBehaviour
{
	/// <summary>
	/// ゲーム
	/// </summary>
	[SerializeField]
	MyGame myGame;

	/// <summary>
	/// 毒の必殺技パーティクル
	/// </summary>
	[SerializeField]
	ParticleSystem VirusDethblowParticle;

	/// <summary>
	/// 拾うマスクのパーティクル
	/// </summary>
	[SerializeField]
	ParticleSystem PickUpMaskParticle;

	/// <summary>
	/// 作業用のパーティクルシステム
	/// </summary>
	ParticleSystem m_workParticleSystem;

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// パーティクルの生成
	/// </summary>
	/// <param name="particleKind">パーティクル種類</param>
	/// <returns>生成されたパーティクル</returns>
	public ParticleSystem CreateParticle(ParticleKind particleKind)
	{
		m_workParticleSystem = null;

		//パーティクル種類
		switch(particleKind)
		{
			case ParticleKind.VirusDethblow:
				m_workParticleSystem = Instantiate(VirusDethblowParticle.gameObject, transform).GetComponent<ParticleSystem>();
				break;
			case ParticleKind.PickUpMask:
				m_workParticleSystem = Instantiate(PickUpMaskParticle.gameObject, transform).GetComponent<ParticleSystem>();
				break;
		}

		return m_workParticleSystem;
	}
}
