//----------------------------------------------------------------------------------------------------
//
//2018年5月8日～
//作成者　京都コンピュータ学院京都駅前校　ゲーム学科　4回生　奥田裕也
//編集者
//
//----------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームクラス
/// </summary>
public class MyGame : MonoBehaviour
{
	/// <summary>
	/// キャラクター
	/// </summary>
	[SerializeField]
	MyCharacter myCharacter;
	public MyCharacter CharacterScript
	{
		get { return myCharacter; }
	}

	/// <summary>
	/// ステージ
	/// </summary>
	[SerializeField]
	MyStage myStage;
	public MyStage StageScript
	{
		get { return myStage; }
	}

	/// <summary>
	/// メインカメラ
	/// </summary>
	[SerializeField]
	Camera MainCamera;
	public Camera CameraScript
	{
		get { return MainCamera; }
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
