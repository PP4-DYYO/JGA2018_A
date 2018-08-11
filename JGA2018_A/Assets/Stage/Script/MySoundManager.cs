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
	/// <summary>
	/// タイトルシーン
	/// </summary>
	Title,
	/// <summary>
	/// オープニング
	/// </summary>
	Openning,
	/// <summary>
	/// フィールド１
	/// </summary>
	Field1,
	/// <summary>
	/// フィールド２
	/// </summary>
	Field2,
	/// <summary>
	/// フィールド３
	/// </summary>
	Field3,
	/// <summary>
	/// フィールド４
	/// </summary>
	Field4,
	/// <summary>
	/// ボス１
	/// </summary>
	Boss1,
	/// <summary>
	/// ボス２
	/// </summary>
	Boss2,
	/// <summary>
	/// ボス３
	/// </summary>
	Boss3,
	/// <summary>
	/// ボス４
	/// </summary>
	Boss4,
	/// <summary>
	/// エンディング
	/// </summary>
	Ending,
}

//----------------------------------------------------------------------------------------------------
/// <summary>
/// SE集
/// </summary>
public enum SeCollection
{
	/// <summary>
	/// アイテムの選択
	/// </summary>
	SelectItem,
	/// <summary>
	/// アイテムの決定
	/// </summary>
	DecideItem,
	/// <summary>
	/// 足音
	/// </summary>
	Footsteps,
	/// <summary>
	/// 剣の攻撃
	/// </summary>
	SwordAttack,
	/// <summary>
	/// キャリー剣の攻撃
	/// </summary>
	CarrySwordAttack,
	/// <summary>
	/// ウイルス剣の攻撃
	/// </summary>
	VirusSwordAttack,
	/// <summary>
	/// ミラー剣の攻撃
	/// </summary>
	MirrorSwordAttack,
	/// <summary>
	/// マジック剣の攻撃
	/// </summary>
	MagicSwordAttack,
	/// <summary>
	/// 力を集める
	/// </summary>
	CollectPower,
	/// <summary>
	/// マスクを変える
	/// </summary>
	ChangeMask,
	/// <summary>
	/// ダメージを受ける
	/// </summary>
	ReceiveDamage,
	/// <summary>
	/// ジャンプ
	/// </summary>
	Jump,
	/// <summary>
	/// ガード
	/// </summary>
	Guard,
	/// <summary>
	/// 物をつかむ
	/// </summary>
	GrabThings,
	/// <summary>
	/// 死ぬ
	/// </summary>
	Die,
	/// <summary>
	/// 弱い敵が死んだ
	/// </summary>
	WeakEnemyDied,
	/// <summary>
	/// ワープ
	/// </summary>
	Warp,
	/// <summary>
	/// 毒に侵される
	/// </summary>
	InvadedByPoison,
	/// <summary>
	/// 仮面の奇声
	/// </summary>
	StrangeVoiceOfTheMask,
	/// <summary>
	/// コインの獲得
	/// </summary>
	AcquisitionOfCoins,
	/// <summary>
	/// HPの回復
	/// </summary>
	HpRecovery,
	/// <summary>
	/// キャリー大臣の咆哮
	/// </summary>
	RoarOfTheCarryMinister,
	/// <summary>
	/// ウイルス大臣の咆哮
	/// </summary>
	RoarOfTheVirusMinister,
	/// <summary>
	/// ミラー大臣の咆哮
	/// </summary>
	RoarOfTheMirrorMinister,
	/// <summary>
	/// マジック大臣の咆哮
	/// </summary>
	RoarOfTheMagicMinister,
	/// <summary>
	/// 矢を射る
	/// </summary>
	ShootAnArrow,
	/// <summary>
	/// 爆弾を投げる
	/// </summary>
	ThrowBomb,
	/// <summary>
	/// 爆発
	/// </summary>
	Explosion,
	/// <summary>
	/// パンチ
	/// </summary>
	Punch,
	/// <summary>
	/// キャリー大臣がダメージを受ける
	/// </summary>
	CarryMinisterIsDamaged,
	/// <summary>
	/// ウイルス大臣がダメージを受ける
	/// </summary>
	VirusMinisterIsDamaged,
	/// <summary>
	/// ミラー大臣がダメージを受ける
	/// </summary>
	MirrorMinisterIsDamaged,
	/// <summary>
	/// マジック大臣がダメージを受ける
	/// </summary>
	MagicMinisterIsDamaged,
	/// <summary>
	/// マスクを捨てる
	/// </summary>
	ThrowAwayTheMask,
	/// <summary>
	/// 毒ガス
	/// </summary>
	PoisonGas,
	/// <summary>
	/// 分身
	/// </summary>
	Divide,
	/// <summary>
	/// キャリー大臣が死ぬ
	/// </summary>
	CarryMinisterDied,
	/// <summary>
	/// ウイルス大臣が死ぬ
	/// </summary>
	VirusMinisterDied,
	/// <summary>
	/// ミラー大臣が死ぬ
	/// </summary>
	MirrorMinisterDied,
	/// <summary>
	/// マジック大臣が死ぬ
	/// </summary>
	MagicMinisterDied,
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
	#region オーディオソース
	[Header("オーディオソース")]
	/// <summary>
	/// BGM用のオーディオソース
	/// </summary>
	[SerializeField]
	AudioSource BgmAudioSource;

	/// <summary>
	/// SE用の3Dオーディオソース
	/// </summary>
	[SerializeField]
	AudioSource[] Se3DAudioSources;

	/// <summary>
	/// SE用の2Dオーディオソース
	/// </summary>
	[SerializeField]
	AudioSource Se2DAudioSource;
	#endregion

	#region BGM
	[Header("BGM")]
	/// <summary>
	/// BGM達
	/// </summary>
	[SerializeField]
	AudioClip[] Bgms;

	/// <summary>
	/// BGMの小音量
	/// </summary>
	[SerializeField]
	float m_bgmSmallVolume;

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
	/// BGMの音量
	/// </summary>
	float m_bgmVolume;
	public float BgmVolume
	{
		set
		{
			m_bgmVolume = value;
			BgmAudioSource.volume = m_bgmVolume;
		}
	}

	/// <summary>
	/// BGMのピッチ
	/// </summary>
	float m_bgmPitch;
	public float BgmPitch
	{
		set
		{
			m_bgmPitch = value;
			BgmAudioSource.pitch = m_bgmPitch;
		}
	}
	#endregion

	#region SE
	[Header("SE")]
	/// <summary>
	/// SE達
	/// </summary>
	[SerializeField]
	AudioClip[] Ses;

	/// <summary>
	/// SE鳴らすか
	/// </summary>
	bool m_isSe = true;
	public bool IsSe
	{
		set
		{
			m_isSe = value;
			foreach(var audioSource in Se3DAudioSources)
			{
				audioSource.mute = m_isSe;
			}
			Se2DAudioSource.mute = m_isSe;
		}
	}

	/// <summary>
	/// 3DのSEの番号
	/// </summary>
	int m_se3DAudioSourceNum;
	#endregion

	#region 作業用
	[Header("作業用")]
	/// <summary>
	/// 作業用のAudioSource
	/// </summary>
	AudioSource m_workAudioSource;

	/// <summary>
	/// 作業用のVector３
	/// </summary>
	Vector3 m_workVector3 = new Vector3();
	#endregion

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// BGMを再生
	/// </summary>
	/// <param name="Bgm">BGM</param>
	public void Play(BgmCollection Bgm)
	{
		if (m_isBgm)
		{
			//BGMを止める
			StopBGM();

			//BGMをセット
			BgmAudioSource.clip = Bgms[(int)Bgm];

			//再生
			BgmAudioSource.Play();
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// BGMを止める
	/// </summary>
	public void StopBGM()
	{
		if (BgmAudioSource.isPlaying)
		{
			BgmAudioSource.Stop();
			BgmAudioSource.pitch = 1f;
			BgmAudioSource.volume = 1f;
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// BGMを小音量にする
	/// </summary>
	public void MakeBgmSmallVolume()
	{
		m_bgmVolume = m_bgmSmallVolume;
		BgmAudioSource.volume = m_bgmVolume;
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
		if (m_isSe)
		{
			//2DのSEか3DのSEか
			if(!isSe3D)
			{
				m_workAudioSource = Se2DAudioSource;
			}
			else
			{
				//音切れ防止
				m_workAudioSource = Se3DAudioSources[m_se3DAudioSourceNum];
				m_se3DAudioSourceNum = (m_se3DAudioSourceNum + 1) % Se3DAudioSources.Length;
			}

			//位置
			m_workVector3.Set(posX, posY, posZ);
			m_workAudioSource.transform.position = m_workVector3;

			//SEの設定
			m_workAudioSource.clip = Ses[(int)Se];

			//再生
			m_workAudioSource.Play();
		}
	}
}
