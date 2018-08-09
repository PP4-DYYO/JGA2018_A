////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/7/31～
//製作者　京都コンピュータ学院京都駅前校　ゲーム学科　四回生　中村智哉
//協力者　京都コンピュータ学院京都駅前校　ゲーム学科　四回生　奥田裕也
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//----------------------------------------------------------------------------------------------------
//クラス
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------
/// <summary>
/// エンディングクラス
/// </summary>
public class MyEnding : MonoBehaviour
{
	/// <summary>
	/// スタッフロールのテキスト
	/// </summary>
	[SerializeField]
	RectTransform StaffRoll;

	/// <summary>
	/// 指示文
	/// </summary>
	[SerializeField]
	Text Directive;

	/// <summary>
	/// 文字の速度
	/// </summary>
	[SerializeField]
	float m_speed;

	/// <summary>
	/// 追加速度
	/// </summary>
	[SerializeField]
	float m_additionalSpeed;

	/// <summary>
	/// テキストを流し終わったら
	/// </summary>
	bool m_endFlag;

	/// <summary>
	/// 終了座標
	/// </summary>
	[SerializeField]
	float m_endPosition;

	/// <summary>
	/// 点滅
	/// </summary>
	bool m_isFlash;

	/// <summary>
	/// 点滅時間を数える
	/// </summary>
	float m_countFlashTime;

	/// <summary>
	/// 点滅時間
	/// </summary>
	[SerializeField]
	float m_flashTime;

	/// <summary>
	/// OKを押しつづけている
	/// </summary>
	bool m_isKeepPressedOK;

	/// <summary>
	/// メニューボタンを押した
	/// </summary>
	bool m_isPressedMenu;

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// フレーム
	/// </summary>
	void Update()
	{
		//AボタンorEnterキー
		m_isKeepPressedOK = (Input.GetKey(KeyCode.Joystick1Button0) || Input.GetKey(KeyCode.Return));

		//メニューボタンの押下
		if (Input.GetButtonDown("Menu"))
			m_isPressedMenu = true;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 定期フレーム
	/// </summary>
	void FixedUpdate()
	{
		//点滅
		Directive.enabled = m_isFlash;
		m_countFlashTime += Time.deltaTime;
		if (m_countFlashTime >= m_flashTime)
		{
			m_isFlash = !m_isFlash;
			m_countFlashTime = 0;
		}

		//テキストを流す
		if (StaffRoll.position.y < m_endPosition)
			StaffRoll.localPosition += new Vector3(0, (m_speed + (m_isKeepPressedOK ? m_additionalSpeed : 0)) * Time.deltaTime, 0);
		else
			m_endFlag = true;

		//テキストが流れ終わった
		if (m_endFlag)
		{
			if (m_isPressedMenu)
				MySceneManager.Instance.ChangeScene(MyScene.Title);
		}

		m_isPressedMenu = false;
	}
}
