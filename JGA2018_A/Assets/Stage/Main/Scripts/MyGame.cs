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
	#region 外部のインスタンス
	[Header("外部のインスタンス")]
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
	MyCamera MainCamera;
	public MyCamera CameraScript
	{
		get { return MainCamera; }
	}
	#endregion

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 初期
	/// </summary>
	void Start()
	{
		myStage.ChangeStage(0);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// フレーム
	/// </summary>
	void Update()
	{
		//ボスイベント発生管理
		BossEventOccurrenceManagement();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ボスイベント発生管理
	/// </summary>
	void BossEventOccurrenceManagement()
	{
		//現在のフィールドで、ボスイベントの位置にプレイヤーがいる
		if (myStage.CurrentField.WallOccurrenceBossEventCollider.bounds.Contains(myCharacter.PlayerScript.transform.position))
			Debug.Log("ボス部屋に到着しました");
	}
}
