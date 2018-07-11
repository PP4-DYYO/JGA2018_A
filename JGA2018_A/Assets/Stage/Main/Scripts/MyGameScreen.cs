﻿//----------------------------------------------------------------------------------------------------
//
//2018年7月8日～
//作成者　京都コンピュータ学院京都駅前校　ゲーム学科　4回生　奥田裕也
//編集者
//
//----------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームスクリーンクラス
/// </summary>
public class MyGameScreen : MonoBehaviour
{
	#region 外部のインスタンス
	[Header("外部のインスタンス")]
	/// <summary>
	/// メインUI
	/// </summary>
	[SerializeField]
	MyMainUI myMainUi;

	/// <summary>
	/// プレイヤーHP
	/// </summary>
	[SerializeField]
	Image PlayerHp;

	/// <summary>
	/// プレイヤーのアシスタンスHp
	/// </summary>
	[SerializeField]
	Image PlayerAssistanceHp;

	/// <summary>
	/// ボスのHPを集めたもの
	/// </summary>
	[SerializeField]
	GameObject BossHpCollection;

	/// <summary>
	/// ボスのHP
	/// </summary>
	[SerializeField]
	Image BossHp;

	/// <summary>
	/// ボスのアシスタンスHp
	/// </summary>
	[SerializeField]
	Image BossAssistanceHp;

	/// <summary>
	/// マスクゲージ
	/// </summary>
	[SerializeField]
	Image MaskGauge;

	/// <summary>
	/// ウイルスマスクゲージ
	/// </summary>
	[SerializeField]
	Image VirusMaskGauge;

	/// <summary>
	/// キャリーマスクゲージ
	/// </summary>
	[SerializeField]
	Image CarryMaskGauge;

	/// <summary>
	/// ミラーマスクゲージ
	/// </summary>
	[SerializeField]
	Image MirrorMaskGauge;

	/// <summary>
	/// マジックマスクゲージ
	/// </summary>
	[SerializeField]
	Image MagicMaskGauge;

	/// <summary>
	/// ウイルスマスクの収集物
	/// </summary>
	[SerializeField]
	GameObject VirusMaskCollection;

	/// <summary>
	/// 注目させるウイルスマスク
	/// </summary>
	[SerializeField]
	Image VirusMaskToLetAttention;

	/// <summary>
	/// 使用中のウイルスマスク
	/// </summary>
	[SerializeField]
	Image VirusMaskInUse;

	/// <summary>
	/// キャリーマスクの収集物
	/// </summary>
	[SerializeField]
	GameObject CarryMaskCollection;

	/// <summary>
	/// 注目させるキャリーマスク
	/// </summary>
	[SerializeField]
	Image CarryMaskToLetAttention;

	/// <summary>
	/// 使用中のキャリーマスク
	/// </summary>
	[SerializeField]
	Image CarryMaskInUse;

	/// <summary>
	/// ミラーマスクの収集物
	/// </summary>
	[SerializeField]
	GameObject MirrorMaskCollection;

	/// <summary>
	/// 注目させるミラーマスク
	/// </summary>
	[SerializeField]
	Image MirrorMaskToLetAttention;

	/// <summary>
	/// 使用中のミラーマスク
	/// </summary>
	[SerializeField]
	Image MirrorMaskInUse;

	/// <summary>
	/// マジックマスクの収集物
	/// </summary>
	[SerializeField]
	GameObject MagicMaskCollection;

	/// <summary>
	/// 注目させるマジックマスク
	/// </summary>
	[SerializeField]
	Image MagicMaskToLetAttention;

	/// <summary>
	/// 使用中のマジックマスク
	/// </summary>
	[SerializeField]
	Image MagicMaskInUse;

	/// <summary>
	/// キャラクター
	/// </summary>
	MyCharacter m_character;

	/// <summary>
	/// プレイヤー
	/// </summary>
	MyPlayer m_player;
	#endregion

	#region HP
	[Header("HP")]
	/// <summary>
	/// HP減少速度
	/// </summary>
	[SerializeField]
	float m_hpDecreaseSpeed;

	/// <summary>
	/// プレイヤーHp
	/// </summary>
	int m_playerHp;

	/// <summary>
	/// ボスHp
	/// </summary>
	int m_bossHp;
	#endregion

	#region 点滅
	[Header("点滅")]
	/// <summary>
	/// 点滅時間
	/// </summary>
	[SerializeField]
	float m_blinkingTime;

	/// <summary>
	/// 点滅時間を数える
	/// </summary>
	float m_countTimeFlashing;

	/// <summary>
	/// 点灯中
	/// </summary>
	bool m_isLightingUp;
	#endregion

	#region 作業用
	/// <summary>
	/// 作業用Float
	/// </summary>
	float m_workFloat;
	#endregion

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 初期
	/// </summary>
	void Start()
	{
		//外部のインスタンス
		m_character = myMainUi.GameScript.CharacterScript;
		m_player = m_character.PlayerScript;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 定期フレーム
	/// </summary>
	void FixedUpdate()
	{
		//HP処理
		HpProcess();

		//マスクゲージ処理
		MaskGaugeProcess();

		//マスクの状態処理
		MaskStateProcess();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// HP処理
	/// </summary>
	void HpProcess()
	{
		//プレイヤー
		PlayerHpProcess();

		//ボス
		BossHpProcess();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// プレイヤーHPの処理
	/// </summary>
	void PlayerHpProcess()
	{
		//プレイヤーのHPが変わった
		if (m_playerHp != m_player.Hp)
		{
			//アシスタンスHPとHPの表示切替
			PlayerAssistanceHp.fillAmount =
				(PlayerAssistanceHp.fillAmount > PlayerHp.fillAmount) ? PlayerAssistanceHp.fillAmount : (float)m_playerHp / m_player.MaxHp;
			m_playerHp = m_player.Hp;
			PlayerHp.fillAmount = (float)m_playerHp / m_player.MaxHp;
		}

		//アシスタンスHPの減り
		if (PlayerAssistanceHp.fillAmount > PlayerHp.fillAmount)
			PlayerAssistanceHp.fillAmount -= m_hpDecreaseSpeed * Time.deltaTime;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ボスHPの処理
	/// </summary>
	void BossHpProcess()
	{
		//ボスHP表示非表示
		BossHpCollection.SetActive(myMainUi.GameScript.StageState == StageStatus.BossGame);

		//ボスのHPが変わった
		if (m_bossHp != m_character.BossScript.HitPoint)
		{
			//アシスタンスHPとHPの表示切替
			BossAssistanceHp.fillAmount =
				(BossAssistanceHp.fillAmount > BossHp.fillAmount) ? BossAssistanceHp.fillAmount : (float)m_bossHp / m_character.BossScript.MaxHitPoint;
			m_bossHp = m_character.BossScript.HitPoint;
			BossHp.fillAmount = (float)m_bossHp / m_character.BossScript.MaxHitPoint;
		}

		//アシスタンスHPの減り
		if (BossAssistanceHp.fillAmount > BossHp.fillAmount)
			BossAssistanceHp.fillAmount -= m_hpDecreaseSpeed * Time.deltaTime;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// マスクゲージ処理
	/// </summary>
	void MaskGaugeProcess()
	{
		//ウイルスマスク
		m_workFloat = VirusMaskGauge.fillAmount;
		VirusMaskGauge.fillAmount = m_player.VirusMask.countGauge / m_player.MaxVirusMaskGauge;
		MaskGauge.fillAmount = (m_workFloat > VirusMaskGauge.fillAmount) ? VirusMaskGauge.fillAmount : MaskGauge.fillAmount;

		//キャリーマスク
		m_workFloat = CarryMaskGauge.fillAmount;
		CarryMaskGauge.fillAmount = m_player.CarryMask.countGauge / m_player.MaxCarryMaskGauge;
		MaskGauge.fillAmount = (m_workFloat > CarryMaskGauge.fillAmount) ? CarryMaskGauge.fillAmount : MaskGauge.fillAmount;

		//ミラーマスク
		m_workFloat = MirrorMaskGauge.fillAmount;
		MirrorMaskGauge.fillAmount = m_player.MirrorMask.countGauge / m_player.MaxMirrorMaskGauge;
		MaskGauge.fillAmount = (m_workFloat > MirrorMaskGauge.fillAmount) ? MirrorMaskGauge.fillAmount : MaskGauge.fillAmount;

		//マジックマスク
		m_workFloat = MagicMaskGauge.fillAmount;
		MagicMaskGauge.fillAmount = m_player.MagicMask.countGauge / m_player.MaxMagicMaskGauge;
		MaskGauge.fillAmount = (m_workFloat > MagicMaskGauge.fillAmount) ? MagicMaskGauge.fillAmount : MaskGauge.fillAmount;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// マスクの状態処理
	/// </summary>
	void MaskStateProcess()
	{
		m_countTimeFlashing += Time.deltaTime;

		//点滅のタイミング
		if (m_countTimeFlashing >= m_blinkingTime)
		{
			m_countTimeFlashing = 0;
			m_isLightingUp = !m_isLightingUp;
		}

		//マスク獲得済みか
		VirusMaskCollection.SetActive(m_player.VirusMask.isObtained);
		CarryMaskCollection.SetActive(m_player.CarryMask.isObtained);
		MirrorMaskCollection.SetActive(m_player.MirrorMask.isObtained);
		MagicMaskCollection.SetActive(m_player.MagicMask.isObtained);

		//マスク使用中
		if(MaskGauge.fillAmount > 0)
		{
			//使用中マスクで必殺技が使われていないと点滅
			VirusMaskToLetAttention.enabled = (!m_player.WasUseDeathblow && m_player.VirusMask.isUse) && m_isLightingUp;
			CarryMaskToLetAttention.enabled = (!m_player.WasUseDeathblow && m_player.CarryMask.isUse) && m_isLightingUp;
			MirrorMaskToLetAttention.enabled = (!m_player.WasUseDeathblow && m_player.MirrorMask.isUse) && m_isLightingUp;
			MagicMaskToLetAttention.enabled = (!m_player.WasUseDeathblow && m_player.MagicMask.isUse) && m_isLightingUp;
		}
		else
		{
			//使用可能であれば点滅
			VirusMaskToLetAttention.enabled = (VirusMaskGauge.fillAmount >= 1) ? m_isLightingUp : false;
			CarryMaskToLetAttention.enabled = (CarryMaskGauge.fillAmount >= 1) ? m_isLightingUp : false;
			MirrorMaskToLetAttention.enabled = (MirrorMaskGauge.fillAmount >= 1) ? m_isLightingUp : false;
			MagicMaskToLetAttention.enabled = (MagicMaskGauge.fillAmount >= 1) ? m_isLightingUp : false;
		}

		//使用中マスク
		VirusMaskInUse.enabled = m_player.VirusMask.isUse;
		CarryMaskInUse.enabled = m_player.CarryMask.isUse;
		MirrorMaskInUse.enabled = m_player.MirrorMask.isUse;
		MagicMaskInUse.enabled = m_player.MagicMask.isUse;
	}
}
