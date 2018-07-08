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

//----------------------------------------------------------------------------------------------------
//Enum・Struct
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------
/// <summary>
/// ステージ状態
/// </summary>
public enum StageStatus
{
	/// <summary>
	/// ダンジョン
	/// </summary>
	Dungeon,
	/// <summary>
	/// ボスの登場
	/// </summary>
	BossAppearance,
	/// <summary>
	/// ボス戦
	/// </summary>
	BossGame,
	/// <summary>
	/// ボス撃破
	/// </summary>
	BossDestroyed,
}

//----------------------------------------------------------------------------------------------------
//クラス
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------
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
	MyCamera myCamera;
	public MyCamera CameraScript
	{
		get { return myCamera; }
	}

	/// <summary>
	/// メインUI
	/// </summary>
	[SerializeField]
	MyMainUI myMainUi;
	public MyMainUI MainUiScript
	{
		get { return myMainUi; }
	}

	/// <summary>
	/// プレイヤー
	/// </summary>
	MyPlayer m_player;

	/// <summary>
	/// ボス
	/// </summary>
	MyAiBoss m_boss;
	#endregion

	#region ステージ
	[Header("ステージ")]
	/// <summary>
	/// ステージ状態
	/// </summary>
	StageStatus m_stageState;
	public StageStatus StageState
	{
		get { return m_stageState; }
	}

	/// <summary>
	/// フレーム前のステージ状態
	/// </summary>
	StageStatus m_stageStatePrev;

	/// <summary>
	/// ステージ番号
	/// </summary>
	int m_stageNum;
	#endregion

	#region オブジェクトの操り
	[Header("オブジェクトの操り")]
	/// <summary>
	/// ターゲットへのカメラ距離
	/// </summary>
	[SerializeField]
	float m_cameraDistanceTarget;

	/// <summary>
	/// カメラの高さ
	/// </summary>
	[SerializeField]
	float m_cameraHeight;

	/// <summary>
	/// 状況認識時間
	/// </summary>
	[SerializeField]
	float m_situationRecognitionTime;

	/// <summary>
	/// カメラがボスに移動する時間
	/// </summary>
	[SerializeField]
	float m_timeCameraMovesBoss;

	/// <summary>
	/// 状態の時間を数える
	/// </summary>
	float m_countTimeState;

	/// <summary>
	/// 状態のカメラ初期位置
	/// </summary>
	Vector3 m_initPosCameraState;
	#endregion

	#region ボスの登場状態
	[Header("ボスの登場状態")]
	/// <summary>
	/// ボスの登場時間
	/// </summary>
	[SerializeField]
	float m_appearanceTimeBoss;
	#endregion

	#region ボス撃破状態
	[Header("ボス撃破状態")]
	/// <summary>
	/// ボスの断末魔時間
	/// </summary>
	[SerializeField]
	float m_bossDevastationTime;
	#endregion

	#region 作業用
	/// <summary>
	/// 作業用Vector３
	/// </summary>
	Vector3 m_workVector3;

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
		//インスタンス
		m_player = myCharacter.PlayerScript;
		m_boss = myCharacter.BossScript;

		m_stageNum = 0 - 1;

		//ステージの変更
		AddvanceStage();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ステージを進める
	/// </summary>
	void AddvanceStage()
	{
		if (!myStage.ChangeStage(++m_stageNum))
			Debug.Log("全クリ処理");
		m_stageState = StageStatus.Dungeon;
		m_stageStatePrev = StageStatus.BossDestroyed;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 定期フレーム
	/// </summary>
	void FixedUpdate()
	{
		//ステージ状態
		switch (m_stageState)
		{
			case StageStatus.Dungeon:
				DungeonState();
				break;
			case StageStatus.BossAppearance:
				BossAppearanceState();
				break;
			case StageStatus.BossGame:
				BossGameState();
				break;
			case StageStatus.BossDestroyed:
				BossDestroyedState();
				break;
		}

		//時間の経過
		m_countTimeState += Time.deltaTime;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 転移
	/// </summary>
	/// <param name="obj">対象オブジェクト</param>
	/// <param name="startPos">スタート位置</param>
	/// <param name="targetPos">目標位置</param>
	/// <param name="currentTime">現在の時間</param>
	/// <param name="travelTime">移動時間</param>
	void Transposition(GameObject obj, Vector3 startPos, Vector3 targetPos, float currentTime, float travelTime)
	{
		//必要な情報の取得
		m_workVector3 = obj.transform.position; //対象オブジェクトの位置
		m_workFloat = currentTime / travelTime; //移動割合

		m_workVector3 = startPos + (targetPos - startPos) * m_workFloat;

		//反映
		obj.transform.position = m_workVector3;
	}

	//----------------------------------------------------------------------------------------------------
	//インスタンスの再コピー
	void ReproduceInstance()
	{
		m_boss = myCharacter.BossScript;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ダンジョン状態
	/// </summary>
	void DungeonState()
	{
		//初めてこの状態になった
		if (m_stageState != m_stageStatePrev)
		{
			//ボスの生成とその他の設定
			myCharacter.AiManagerScript.GenerateBoss(m_stageNum);
			ReproduceInstance();
			SetManipulateMainObject(false);
			m_countTimeState = 0;
			Debug.Log("UIに「ダンジョン先のボスを倒せ」と表示");

			m_stageStatePrev = m_stageState;
		}

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
			m_stageState = StageStatus.BossAppearance;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ボスの登場状態
	/// </summary>
	void BossAppearanceState()
	{
		//初めてこの状態になった
		if (m_stageState != m_stageStatePrev)
		{
			//初期設定
			SetManipulateMainObject(true);
			m_countTimeState = 0;

			//カメラの設定とプレイヤー&ボスのアニメーション
			m_initPosCameraState = CameraScript.transform.position;
			m_player.StartAnimIdle();
			Debug.Log("ボスの威嚇アニメーションスタート");

			m_stageStatePrev = m_stageState;
		}

		//状況認識した時間～カメラが動き終わるまで
		if (m_countTimeState >= m_situationRecognitionTime && m_countTimeState <= m_situationRecognitionTime + m_timeCameraMovesBoss)
		{
			//ボスにカメラがズーム
			Transposition(CameraScript.gameObject, m_initPosCameraState,
				m_boss.transform.position + m_boss.transform.forward * m_cameraDistanceTarget + Vector3.up * m_cameraHeight,
				(m_countTimeState - m_situationRecognitionTime), m_timeCameraMovesBoss);
		}

		//カメラの見る場所
		CameraScript.transform.LookAt(m_boss.transform.position + Vector3.up * m_cameraHeight);

		//状態の終了
		if (m_countTimeState >= m_appearanceTimeBoss)
			m_stageState = StageStatus.BossGame;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ボス戦状態
	/// </summary>
	void BossGameState()
	{
		//初めてこの状態になった
		if (m_stageState != m_stageStatePrev)
		{
			//初期設定
			SetManipulateMainObject(false);
			m_countTimeState = 0;

			//戦闘位置へ移動
			m_player.transform.position = myStage.CurrentField.BossRoomCenterPos;
			m_boss.transform.position = myStage.CurrentField.GetPosDeathblow1()[0];
			m_player.transform.LookAt(m_boss.transform);
			m_boss.transform.LookAt(m_player.transform);
			Debug.Log("カメラがボスを捕らえる設定");

			m_stageStatePrev = m_stageState;
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ボス撃破状態
	/// </summary>
	void BossDestroyedState()
	{
		//初めてこの状態になった
		if (m_stageState != m_stageStatePrev)
		{
			//初期設定
			SetManipulateMainObject(true);
			m_countTimeState = 0;

			//カメラの設定とボスのアニメーション
			m_initPosCameraState = CameraScript.transform.position;
			Debug.Log("ボスの敗北アニメーションスタート");

			m_stageStatePrev = m_stageState;
		}

		//ボスにカメラがズーム
		if (m_countTimeState <= m_timeCameraMovesBoss)
			Transposition(CameraScript.gameObject, m_initPosCameraState,
				m_boss.transform.position + m_boss.transform.forward * m_cameraDistanceTarget + Vector3.up * m_cameraHeight,
				m_countTimeState, m_timeCameraMovesBoss);

		//カメラの見る場所
		CameraScript.transform.LookAt(m_boss.transform.position + Vector3.up * m_cameraHeight);

		//状態の終了
		if (m_countTimeState >= m_bossDevastationTime)
			AddvanceStage();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 主要オブジェクトを操る設定
	/// </summary>
	/// <param name="isManipulate">操るか</param>
	public void SetManipulateMainObject(bool isManipulate)
	{
		//プレイヤーとボスとカメラを操る設定
		m_player.enabled = !isManipulate;
		m_player.GetComponent<Collider>().enabled = !isManipulate;
		m_player.GetComponent<Rigidbody>().useGravity = !isManipulate;
		m_boss.enabled = !isManipulate;
		myCamera.enabled = !isManipulate;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ステージ状態を変える
	/// </summary>
	/// <param name="stageState">ステージ状態</param>
	public void ChangeState(StageStatus stageState)
	{
		m_stageState = stageState;
	}
}
