////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/8/11～
//制作者　京都コンピュータ学院京都駅前校　ゲーム学科　四回生　奥田裕也
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
/// BGM集
/// </summary>
public enum BgmCollection
{
	Title,
	Main,
	Ending,
}

//----------------------------------------------------------------------------------------------------
/// <summary>
/// SE集
/// </summary>
public enum SeCollection
{
}

//----------------------------------------------------------------------------------------------------
//クラス
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------
/// <summary>
/// サウンドマネージャークラス
/// </summary>
public class MySoundManager : MySingletonMonoBehaviour<MySoundManager>
{
	/// <summary>
	/// BGM用のオーディオソース
	/// </summary>
	[SerializeField]
	AudioSource BgmAudioSource;

	/// <summary>
	/// SE用の3Dオーディオソース
	/// </summary>
	[SerializeField]
	AudioSource Se3DAudioSource;

	/// <summary>
	/// SE用の2Dオーディオソース
	/// </summary>
	[SerializeField]
	AudioSource Se2DAudioSource;

	/// <summary>
	/// BGM達
	/// </summary>
	[SerializeField]
	AudioClip[] Bgms;

	/// <summary>
	/// SE達
	/// </summary>
	[SerializeField]
	AudioClip[] Ses;

	/// <summary>
	/// BGM鳴らすか
	/// </summary>
	bool m_isBgm = true;
	public bool IsBgm
	{
		set
		{
			m_isBgm = value;
			BgmAudioSource.mute = m_isBgm;
		}
	}

	/// <summary>
	/// SE鳴らすか
	/// </summary>
	bool m_isSe = true;
	public bool IsSe
	{
		set
		{
			m_isSe = value;
			Se3DAudioSource.mute = m_isSe;
			Se2DAudioSource.mute = m_isSe;
		}
	}

	/// <summary>
	/// 作業用のAudioSource
	/// </summary>
	AudioSource m_workAudioSource;

	/// <summary>
	/// 作業用のVector３
	/// </summary>
	Vector3 m_workVector3 = new Vector3();

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// BGMを再生
	/// </summary>
	/// <param name="Bgm">BGM</param>
	public void Play(BgmCollection Bgm)
	{
		if(m_isBgm)
		{
			//BGMを止める
			if (BgmAudioSource.isPlaying)
				BgmAudioSource.Stop();

			//BGMをセット
			BgmAudioSource.clip = Bgms[(int)Bgm];

			//再生
			BgmAudioSource.Play();
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// SEを再生
	/// </summary>
	/// <param name="Se">サウンドエフェクト</param>
	/// <param name="isSe3D">3DのSEか</param>
	/// <param name="posX">Xの位置</param>
	/// <param name="posY">Yの位置</param>
	/// <param name="posZ">Zの位置</param>
	public void Play(SeCollection Se, bool isSe3D = false, float posX = 0, float posY = 0, float posZ = 0)
	{
		if(m_isSe)
		{
			//2DのSEか3DのSEか
			var m_workAudioSource = (!isSe3D ? Se2DAudioSource : Se3DAudioSource);

			//位置
			m_workVector3.Set(posX, posY, posZ);
			m_workAudioSource.transform.position = m_workVector3;

			//SEの設定
			m_workAudioSource.clip = Ses[(int)Se];

			//再生
			m_workAudioSource.Play();
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// BGMを止める
	/// </summary>
	public void StopBGM()
	{
		if (BgmAudioSource.isPlaying)
			BgmAudioSource.Stop();
	}
}
