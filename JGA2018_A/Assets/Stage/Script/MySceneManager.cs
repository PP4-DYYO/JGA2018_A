////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018年7月17日～
//作成者　京都コンピュータ学院京都駅前校　ゲーム学科　4回生　奥田裕也
//編集者
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//----------------------------------------------------------------------------------------------------
//Enum・Struct
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------
/// <summary>
/// シーン
/// </summary>
public enum MyScene
{
	Title,
	Main,
	Ending,
	Non,
}

//----------------------------------------------------------------------------------------------------
/// <summary>
/// シーン状態
/// </summary>
enum SceneStatus
{
	/// <summary>
	/// 過去シーン
	/// </summary>
	PastScene,
	/// <summary>
	/// シーン変更中
	/// </summary>
	SceneChangeInProgress,
	/// <summary>
	/// 現在シーン
	/// </summary>
	CurrentScene,
}

//----------------------------------------------------------------------------------------------------
//クラス
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------
/// <summary>
/// シーンマネージャ
/// </summary>
public class MySceneManager : MySingletonMonoBehaviour<MySceneManager>
{
	/// <summary>
	/// マスクイメージ
	/// </summary>
	[SerializeField]
	Image MaskImage;

	/// <summary>
	/// 現在シーン
	/// </summary>
	MyScene m_currentScene;

	/// <summary>
	/// 過去シーン
	/// </summary>
	MyScene m_pastScene;

	/// <summary>
	/// シーン状態
	/// </summary>
	SceneStatus m_sceneState;

	/// <summary>
	/// マスクの時間
	/// </summary>
	[SerializeField]
	float m_maskTime;

	/// <summary>
	/// マスク時間を数える
	/// </summary>
	float m_countMaskTime;

	/// <summary>
	/// 作業用の色
	/// </summary>
	Color m_workColor;

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	///	初期
	/// </summary>
	void Start()
	{
		m_currentScene = MyScene.Non;
		m_pastScene = m_currentScene;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	///	定期フレーム
	/// </summary>
	void FixedUpdate()
	{
		//シーンが変わる
		if (m_currentScene != m_pastScene)
		{
			//シーン状態
			switch (m_sceneState)
			{
				case SceneStatus.PastScene:
					PastSceneProcess();
					break;
				case SceneStatus.SceneChangeInProgress:
					SceneChangeInProgressProcess();
					break;
				case SceneStatus.CurrentScene:
					CurrentSceneProcess();
					break;
			}
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 過去シーンの処理
	/// </summary>
	void PastSceneProcess()
	{
		m_countMaskTime += Time.deltaTime;

		m_workColor = MaskImage.color;
		m_workColor.a = m_countMaskTime / m_maskTime;
		MaskImage.color = m_workColor;

		//マスク処理完了
		if (MaskImage.color.a >= 1f)
			m_sceneState = SceneStatus.SceneChangeInProgress;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// シーン変更中の処理
	/// </summary>
	void SceneChangeInProgressProcess()
	{
		//シーンのチェンジ
		if (SceneManager.GetActiveScene().name != m_currentScene.ToString())
		{
			//ロードシーン
			SceneManager.LoadScene(m_currentScene.ToString());
			m_sceneState = SceneStatus.CurrentScene;
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 現在シーンの処理
	/// </summary>
	void CurrentSceneProcess()
	{
		m_countMaskTime -= Time.deltaTime;

		m_workColor = MaskImage.color;
		m_workColor.a = m_countMaskTime / m_maskTime;
		MaskImage.color = m_workColor;

		//マスク処理完了
		if (MaskImage.color.a <= 0f)
			m_pastScene = m_currentScene;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// シーンのチェンジ
	/// </summary>
	/// <param name="scene">シーン</param>
	public void ChangeScene(MyScene scene)
	{
		m_currentScene = scene;
		m_pastScene = (m_pastScene == m_currentScene) ? MyScene.Non : m_pastScene;
		m_sceneState = SceneStatus.PastScene;
		m_countMaskTime = 0;
	}
}
