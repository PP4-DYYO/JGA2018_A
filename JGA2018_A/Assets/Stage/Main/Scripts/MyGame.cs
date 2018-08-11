////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018年5月8日～
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
	/// <summary>
	/// なし
	/// </summary>
	Non,
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
	/// パーティクルマネージャ
	/// </summary>
	[SerializeField]
	MyParticleManager myParticleManager;
	public MyParticleManager ParticleManagerScript
	{
		get { return myParticleManager; }
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
	/// 必殺技終了後の状態
	/// </summary>
	StageStatus m_conditionAfterEndOfDeathblow;

	/// <summary>
	/// ステージ番号
	/// </summary>
	int m_stageNum;
	public int StageNum
	{
		get { return m_stageNum; }
	}
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


#if DEBUG
	#region デバッグ
	[Header("デバッグ")]
	/// <summary>
	/// ステージを進める(デバッグ用)
	/// </summary>
	[SerializeField]
	bool m_isAddvanceStage_debug;

	/// <summary>
	/// 状態を進める（デバッグ用）
	/// </summary>
	[SerializeField]
	bool m_isAddvanceState_debug;
	#endregion
#endif

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 初期
	/// </summary>
	void Start()
	{
		//インスタンス
		m_player = myCharacter.PlayerScript;
		m_boss = myCharacter.BossScript;

		m_stageNum = MyGameInfo.Instance.StageNum - 1;

		//ステージの変更
		AddvanceStage();

		m_conditionAfterEndOfDeathblow = StageStatus.Non;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ステージを進める
	/// </summary>
	void AddvanceStage()
	{
		//ステージ変更
		if (myStage.ChangeStage(++m_stageNum))
		{
			//攻撃のリセット
			myCharacter.AttackManagerScript.ResetAttacks();

			//プレイヤーとカメラの位置と向き
			m_player.transform.position = myStage.CurrentField.StartPos;
			myCamera.SetPosition(myStage.CurrentField.RelativePosCamera);
			m_player.transform.LookAt(m_player.transform.position
				+ Vector3.Scale((m_player.transform.position - myCamera.transform.position), (Vector3.right + Vector3.forward)));

			//ボスの生成
			myCharacter.AiManagerScript.GenerateBoss(m_stageNum);
			ReproduceInstance();

			//ステージ番号の保存
			PlayerPrefs.SetInt(PlayerPrefsKeys.STAGE_NUM, m_stageNum);
			MyGameInfo.Instance.StageNum = m_stageNum;

			//BGMの切り替え
			switch (m_stageNum)
			{
				case 0:
					MySoundManager.Instance.Play(BgmCollection.Field1);
					break;
				case 1:
					MySoundManager.Instance.Play(BgmCollection.Field2);
					break;
				case 2:
					MySoundManager.Instance.Play(BgmCollection.Field3);
					break;
				case 3:
					MySoundManager.Instance.Play(BgmCollection.Field4);
					break;
			}
		}
		else
		{
			//シーン遷移
			MySceneManager.Instance.ChangeScene(MyScene.Ending);
			enabled = false;
		}

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

		//デバッグ
		DebugProcess();
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
			//設定
			SetManipulateMainObject(false);
			m_countTimeState = 0;
			myMainUi.GameScreenScript.StartDungeonInstruction();

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
		if (myStage.CurrentField.WallOccurrenceBossEventCollider.bounds.Contains(m_player.transform.position))
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

			//SEの再生
			switch (m_stageNum)
			{
				case 0:
					MySoundManager.Instance.Play(SeCollection.RoarOfTheCarryMinister,
						true, m_boss.transform.position.x, m_boss.transform.position.y, m_boss.transform.position.z);
					break;
				case 1:
					MySoundManager.Instance.Play(SeCollection.RoarOfTheVirusMinister,
						true, m_boss.transform.position.x, m_boss.transform.position.y, m_boss.transform.position.z);
					break;
				case 2:
					MySoundManager.Instance.Play(SeCollection.RoarOfTheMirrorMinister,
						true, m_boss.transform.position.x, m_boss.transform.position.y, m_boss.transform.position.z);
					break;
				case 3:
					MySoundManager.Instance.Play(SeCollection.RoarOfTheMagicMinister,
						true, m_boss.transform.position.x, m_boss.transform.position.y, m_boss.transform.position.z);
					break;
			}

			//ワープマネージャ
			if (myStage.CurrentField.WarpManagerScript)
				myStage.CurrentField.WarpManagerScript.enabled = false;

			//BGMの停止
			MySoundManager.Instance.StopBGM();

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
			m_boss.StartAI();
			myCamera.SetPosition(-(m_boss.transform.position - m_player.transform.position));

			//BGMの切り替え
			switch (m_stageNum)
			{
				case 0:
					MySoundManager.Instance.Play(BgmCollection.Boss1);
					break;
				case 1:
					MySoundManager.Instance.Play(BgmCollection.Boss2);
					break;
				case 2:
					MySoundManager.Instance.Play(BgmCollection.Boss3);
					break;
				case 3:
					MySoundManager.Instance.Play(BgmCollection.Boss4);
					break;
			}

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

			//カメラとボスの設定
			m_initPosCameraState = CameraScript.transform.position;
			m_boss.transform.LookAt(myStage.CurrentField.BossRoomCenterPos);
			Debug.Log("ボスの敗北アニメーションスタート");

			//BGMの停止
			MySoundManager.Instance.StopBGM();

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
	/// デバッグ用処理
	/// </summary>
	void DebugProcess()
	{
#if DEBUG
		//ステージを進める
		if (m_isAddvanceStage_debug)
		{
			AddvanceStage();
			m_isAddvanceStage_debug = false;
		}

		//状態を進める
		if (m_isAddvanceState_debug)
		{
			if (m_stageState != StageStatus.BossDestroyed)
				m_stageState++;
			else
				AddvanceStage();

			m_isAddvanceState_debug = false;
		}
#endif
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ステージの再構築
	/// </summary>
	public void RebuildStage()
	{
		m_stageNum--;
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
		m_player.GetComponent<Rigidbody>().velocity = Vector3.zero;
		m_boss.enabled = !isManipulate;
		myCamera.enabled = !isManipulate;

		//反転描画対策
		if (myStage.CurrentField.IsInversionField)
			myCamera.InvertDrawing(myCamera.enabled);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ステージ状態を変える
	/// </summary>
	/// <param name="stageState">ステージ状態</param>
	public void ChangeState(StageStatus stageState)
	{
		//必殺技が発動中
		if (myCharacter.AttackManagerScript.IsDeathblow)
			m_conditionAfterEndOfDeathblow = stageState;
		else
			m_stageState = stageState;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技のための待ち
	/// </summary>
	public void WaitingForDeathblow()
	{
		//待機中の状態がある
		if (m_conditionAfterEndOfDeathblow != StageStatus.Non)
		{
			m_stageState = m_conditionAfterEndOfDeathblow;
			m_conditionAfterEndOfDeathblow = StageStatus.Non;
		}
	}
}
