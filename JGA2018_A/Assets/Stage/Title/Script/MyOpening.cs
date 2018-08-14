////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018年7月13日～
//作成者　京都コンピュータ学院京都駅前校　ゲーム学科　4回生　奥田裕也
//編集者
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//----------------------------------------------------------------------------------------------------
//Enum・Struct
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------
/// <summary>
/// オープニング状態
/// </summary>
enum OpeningStatus
{
	/// <summary>
	/// オープニングでない
	/// </summary>
	NotOpening,
	/// <summary>
	/// ユーザの入力を待つ
	/// </summary>
	WaitForUserInput,
	/// <summary>
	/// 動いている
	/// </summary>
	Moving,
}

/// <summary>
/// ボス画像の出現状態
/// </summary>
enum BossImageAppearanceStatus
{
	/// <summary>
	/// 出現
	/// </summary>
	Emergence,
	/// <summary>
	/// アピール
	/// </summary>
	Appeal,
	/// <summary>
	/// 並ぶ
	/// </summary>
	LineUp,
}

//----------------------------------------------------------------------------------------------------
//クラス
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------
/// <summary>
/// オープニングクラス
/// </summary>
public class MyOpening : MonoBehaviour
{
	#region 外部のインスタンス
	[Header("外部のインスタンス")]
	/// <summary>
	/// キャンバス
	/// </summary>
	[SerializeField]
	RectTransform Canvas;

	/// <summary>
	/// 了解画像
	/// </summary>
	[SerializeField]
	Image OkImage;

	/// <summary>
	/// 標識
	/// </summary>
	[SerializeField]
	GameObject Sign;

	/// <summary>
	/// 王
	/// </summary>
	[SerializeField]
	GameObject King;

	/// <summary>
	/// セリフテキスト
	/// </summary>
	[SerializeField]
	Text SerifText;

	/// <summary>
	/// ボス
	/// </summary>
	[SerializeField]
	GameObject Boss;

	/// <summary>
	/// ボス画像達
	/// </summary>
	[SerializeField]
	Image[] BossImage;

	/// <summary>
	/// アピールテキスト
	/// </summary>
	[SerializeField]
	Text AppealText;
	#endregion

	#region データ
	[Header("データ")]
	/// <summary>
	/// データクラス
	/// </summary>
	[SerializeField]
	MyOpeningData myOpeningData;
	#endregion

	#region 状態
	/// <summary>
	/// オープニング状態
	/// </summary>
	OpeningStatus m_openingState;

	/// <summary>
	/// フレーム前のオープニング状態
	/// </summary>
	OpeningStatus m_openingStatePrev;

	/// <summary>
	/// 状態番号
	/// </summary>
	int m_stateNum;

	/// <summary>
	/// 終了フラグ
	/// </summary>
	bool m_isEnd;
	public bool IsEnd
	{
		get { return m_isEnd; }
	}
	#endregion

	#region 全体
	[Header("全体")]
	/// <summary>
	/// 点滅時間
	/// </summary>
	[SerializeField]
	float m_blinkingTime;

	/// <summary>
	///	点滅時間を数える
	/// </summary>
	float m_countFlashingTime;

	/// <summary>
	/// 状態の時間を数える
	/// </summary>
	float m_countTimeState;

	/// <summary>
	/// スクリーンサイズ
	/// </summary>
	Vector3 m_screenSize = new Vector3();
	#endregion

	#region Sign
	[Header("Sign")]
	/// <summary>
	/// 看板に注目する時間
	/// </summary>
	[SerializeField]
	float m_timeToPayAttentionToSignboard;
	#endregion

	#region King
	[Header("King")]
	/// <summary>
	/// セリフの早さ
	/// </summary>
	[SerializeField]
	float m_speedSpeech;

	/// <summary>
	/// セリフ番号
	/// </summary>
	int m_serifNum;

	/// <summary>
	/// セリフ文の番号
	/// </summary>
	int m_serifStatementNum;

	/// <summary>
	/// セリフ文
	/// </summary>
	string m_serifStatement;
	#endregion

	#region Boss
	[Header("Boss")]
	/// <summary>
	/// ボス画像の出現時間
	/// </summary>
	[SerializeField]
	float m_bossImageAppearanceTime;

	/// <summary>
	/// フレーム前のアピールテキスト文字
	/// </summary>
	string m_appealTextPrev;

	/// <summary>
	/// ボス画像がアピールする時間
	/// </summary>
	[SerializeField]
	float m_bossImageAppealTime;

	/// <summary>
	/// ボス画像の並ぶ時間
	/// </summary>
	[SerializeField]
	float m_bossImageLineUpTime;

	/// <summary>
	/// ボス画像の回転速度
	/// </summary>
	[SerializeField]
	float m_rotationSpeedOfBossImage;

	/// <summary>
	/// 回転音の間隔
	/// </summary>
	[SerializeField]
	float m_rotationSoundInterval;

	/// <summary>
	/// ボス画像の倍率
	/// </summary>
	[SerializeField]
	float m_bossImageMagnification;

	/// <summary>
	/// ボス画像の名前
	/// </summary>
	[SerializeField]
	string[] m_bossImageNames;

	/// <summary>
	/// ボス画像の出現状態
	/// </summary>
	BossImageAppearanceStatus m_bossImageAppearanceState;

	/// <summary>
	/// ボス画像の初期位置達
	/// </summary>
	Vector3[] m_initPosBossImage;

	/// <summary>
	/// 出現画像数
	/// </summary>
	int m_numAppearingImages;

	/// <summary>
	/// 回転音の間隔を数える
	/// </summary>
	float m_countRotationSoundInterval;
	#endregion

	#region キーボード関係
	[Header("キーボード関係")]
	/// <summary>
	/// Aボタン
	/// </summary>
	const string A_BUTTON = "Jump";

	/// <summary>
	/// 左クリック
	/// </summary>
	const string LEFT_CLICK = "Attack1";

	/// <summary>
	/// メニュー
	/// </summary>
	const string MENU_BUTTON = "Menu";

	/// <summary>
	/// 了解ボタンを押された
	/// </summary>
	bool m_isPressedOk = false;

	/// <summary>
	/// メニューボタンを押された
	/// </summary>
	bool m_isPressedMenu = false;
	#endregion

	#region 作業用
	/// <summary>
	/// 作業用Vector３
	/// </summary>
	Vector3 m_workVector3;

	/// <summary>
	/// 作業用のFloat
	/// </summary>
	float m_workFloat;
	#endregion

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 初期
	/// </summary>
	void Start()
	{
		//スクリーンサイズ
		m_screenSize.x = Screen.width / Canvas.localScale.x;
		m_screenSize.y = Screen.height / Canvas.localScale.y;

		//ボス画像の初期位置
		m_initPosBossImage = new Vector3[BossImage.Length];
		m_initPosBossImage[0] = new Vector3(-m_screenSize.x, m_screenSize.y) / 2;
		m_initPosBossImage[1] = m_screenSize / 2;
		m_initPosBossImage[2] = -m_screenSize / 2;
		m_initPosBossImage[3] = new Vector3(m_screenSize.x, -m_screenSize.y) / 2;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// フレーム
	/// </summary>
	void Update()
	{
		//キーの状態を調べる
		CheckKeyStatus();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// キーの状態を調べる
	/// </summary>
	void CheckKeyStatus()
	{
		//Aボタンの押下or左クリックの押下
		if (Input.GetButtonDown(A_BUTTON) || Input.GetButtonDown(LEFT_CLICK))
			m_isPressedOk = true;

		//メニューボタンの押下
		if (Input.GetButtonDown(MENU_BUTTON))
			m_isPressedMenu = true;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 定期フレーム
	/// </summary>
	void FixedUpdate()
	{
		//オープニング状態
		switch (m_openingState)
		{
			case OpeningStatus.WaitForUserInput:
				WaitForUserInputProcess();
				break;
			case OpeningStatus.Moving:
				MovingProcess();
				break;
		}

		//スキップ
		if (m_isPressedMenu)
			EndOfMovingProcess();

		//SEの再生
		if (m_isPressedOk || m_isPressedMenu)
			MySoundManager.Instance.Play(SeCollection.DecideItem);

		//キー状態のリセット
		ResetKeyStatus();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ユーザ入力を待つ処理
	/// </summary>
	void WaitForUserInputProcess()
	{
		//初めて状態が変わった
		if (m_openingState != m_openingStatePrev)
		{
			OkImage.enabled = true;
			m_openingStatePrev = m_openingState;
		}

		m_countFlashingTime += Time.deltaTime;

		//点滅処理
		if (m_countFlashingTime >= m_blinkingTime)
		{
			OkImage.enabled = !OkImage.enabled;
			m_countFlashingTime = 0;
		}

		//了解ボタンの押下
		if (m_isPressedOk)
			m_openingState = OpeningStatus.Moving;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 動作処理
	/// </summary>
	void MovingProcess()
	{
		//初めて状態が変わった(全体設定)
		if (m_openingState != m_openingStatePrev)
		{
			m_countTimeState = 0;
			OkImage.enabled = false;
		}

		//状態番号
		switch (m_stateNum)
		{
			case 0:
				MovingProcess1();
				break;
			case 1:
				MovingProcess2();
				break;
			case 2:
				MovingProcess3();
				break;
			case 3:
				MovingProcess4();
				break;
			case 4:
				EndOfMovingProcess();
				break;
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 動作処理１
	/// </summary>
	void MovingProcess1()
	{
		//初めて状態が変わった(個別設定)
		if (m_openingState != m_openingStatePrev)
		{
			Sign.SetActive(true);
			King.SetActive(false);
			Boss.SetActive(false);
			m_openingStatePrev = m_openingState;
		}

		m_countTimeState += Time.deltaTime;

		//看板に注目する時間を超えるor了解ボタンの押下
		if (m_countTimeState >= m_timeToPayAttentionToSignboard || m_isPressedOk)
		{
			m_stateNum++;
			m_openingState = OpeningStatus.WaitForUserInput;
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 動作処理２
	/// </summary>
	void MovingProcess2()
	{
		//初めて状態が変わった(個別設定)
		if (m_openingState != m_openingStatePrev)
		{
			Sign.SetActive(false);
			King.SetActive(true);
			Boss.SetActive(false);
			m_serifStatement = myOpeningData.sheets[m_serifNum].list[m_serifStatementNum++].sentence;
			m_openingStatePrev = m_openingState;
		}

		//セリフ処理
		SerifProcess();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// セリフ処理
	/// </summary>
	void SerifProcess()
	{
		SerifText.text = m_serifStatement.Substring(0, (int)(m_countTimeState * m_speedSpeech));

		m_countTimeState += Time.deltaTime;

		//セリフの文が終了or了解ボタンの押下
		if (SerifText.text.Length >= m_serifStatement.Length || m_isPressedOk)
		{
			SerifText.text = m_serifStatement;
			m_openingState = OpeningStatus.WaitForUserInput;

			//セリフの終了
			if (m_serifStatementNum + 1 > myOpeningData.sheets[m_serifNum].list.Count)
			{
				//状態の変更
				m_stateNum++;
				m_serifNum++;
				m_serifStatementNum = 0;
			}
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 動作処理３
	/// </summary>
	void MovingProcess3()
	{
		//初めて状態が変わった(個別設定)
		if (m_openingState != m_openingStatePrev)
		{
			Sign.SetActive(false);
			King.SetActive(false);
			Boss.SetActive(true);
			m_bossImageAppearanceState = BossImageAppearanceStatus.Emergence;
			for (var i = m_numAppearingImages; i < BossImage.Length; i++)
			{
				//ボス画像を指定の位置へ
				BossImage[i].transform.localPosition = m_initPosBossImage[i] * (1 + (1.0f / (m_screenSize.x / BossImage[i].rectTransform.sizeDelta.x)));
			}

			m_openingStatePrev = m_openingState;
		}

		m_countTimeState += Time.deltaTime;

		//了解ボタンの押下
		if (m_isPressedOk)
			m_bossImageAppearanceState = BossImageAppearanceStatus.LineUp;

		//ボス画像が出現
		BossImageAppear();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ボス画像が出現
	/// </summary>
	void BossImageAppear()
	{
		switch (m_bossImageAppearanceState)
		{
			case BossImageAppearanceStatus.Emergence:
				BossImageAppearance();
				break;
			case BossImageAppearanceStatus.Appeal:
				BossImageAppeal();
				break;
			case BossImageAppearanceStatus.LineUp:
				BossImageLineUp();
				break;
		}
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
		m_workVector3 = obj.transform.localPosition; //対象オブジェクトの位置
		m_workFloat = currentTime / travelTime; //移動割合

		m_workVector3 = startPos + (targetPos - startPos) * m_workFloat;

		//反映
		obj.transform.localPosition = m_workVector3;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ボス画像の出現
	/// </summary>
	void BossImageAppearance()
	{
		//移動
		Transposition(BossImage[m_numAppearingImages].gameObject,
			m_initPosBossImage[m_numAppearingImages], Vector3.zero, m_countTimeState, m_bossImageAppearanceTime);

		//倍率
		BossImage[m_numAppearingImages].transform.localScale
			= Vector3.one * (Mathf.Min((m_countTimeState / m_bossImageAppearanceTime) + 1, m_bossImageMagnification));

		//回転
		BossImage[m_numAppearingImages].transform.Rotate(Vector3.forward * (m_rotationSpeedOfBossImage * Time.deltaTime));

		//SEの再生
		if (m_countRotationSoundInterval >= m_rotationSoundInterval)
		{
			MySoundManager.Instance.Play(SeCollection.SwordAttack,
				true, Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
			m_countRotationSoundInterval = 0;
		}
		m_countRotationSoundInterval += Time.deltaTime;

		//出現時間を超えた
		if (m_countTimeState >= m_bossImageAppearanceTime)
		{
			BossImage[m_numAppearingImages].transform.localPosition = Vector3.zero;
			BossImage[m_numAppearingImages].transform.eulerAngles = Vector3.zero;
			m_bossImageAppearanceState = BossImageAppearanceStatus.Appeal;
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ボス画像のアピール
	/// </summary>
	void BossImageAppeal()
	{
		//アピール文字
		m_appealTextPrev = AppealText.text;
		AppealText.text	= m_bossImageNames[m_numAppearingImages]
			.Substring(0, (int)(m_bossImageNames[m_numAppearingImages].Length * ((m_countTimeState - m_bossImageAppearanceTime) / m_bossImageAppealTime)));

		//アピール文字が増えた
		if (m_appealTextPrev != AppealText.text)
			MySoundManager.Instance.Play(SeCollection.Explosion,
				true, Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);

		//アピールタイムが終わった
		if (m_countTimeState - m_bossImageAppearanceTime >= m_bossImageAppealTime)
		{
			m_bossImageAppearanceState = BossImageAppearanceStatus.LineUp;
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ボス画像が並ぶ
	/// </summary>
	void BossImageLineUp()
	{
		//移動
		Transposition(BossImage[m_numAppearingImages].gameObject, Vector3.zero,
			m_initPosBossImage[m_numAppearingImages] / 2, m_countTimeState - (m_bossImageAppealTime + m_bossImageAppearanceTime), m_bossImageLineUpTime);

		//倍率
		BossImage[m_numAppearingImages].transform.localScale = Vector3.one
			* (Mathf.Min(((m_bossImageLineUpTime - (m_countTimeState - (m_bossImageAppealTime + m_bossImageAppearanceTime))) + 1), m_bossImageMagnification));

		//並ぶ時間を超えたor了解ボタンの押下
		if (m_countTimeState - (m_bossImageAppealTime + m_bossImageAppearanceTime) >= m_bossImageLineUpTime || m_isPressedOk)
		{
			BossImage[m_numAppearingImages].transform.localPosition = m_initPosBossImage[m_numAppearingImages] / 2;
			BossImage[m_numAppearingImages].transform.eulerAngles = Vector3.zero;
			BossImage[m_numAppearingImages++].transform.localScale = Vector3.one;
			AppealText.text = "";
			m_openingState = OpeningStatus.WaitForUserInput;

			//ボス画像が全て並んだ
			if(m_numAppearingImages >= BossImage.Length)
			{
				m_stateNum++;
			}
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 動作処理４
	/// </summary>
	void MovingProcess4()
	{
		//初めて状態が変わった(個別設定)
		if (m_openingState != m_openingStatePrev)
		{
			Sign.SetActive(false);
			King.SetActive(true);
			Boss.SetActive(false);
			m_serifStatement = myOpeningData.sheets[m_serifNum].list[m_serifStatementNum++].sentence;
			m_openingStatePrev = m_openingState;
		}

		//セリフ処理
		SerifProcess();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 動作処理の終了
	/// </summary>
	void EndOfMovingProcess()
	{
		gameObject.SetActive(false);
		m_isEnd = true;
		PlayerPrefs.SetInt(PlayerPrefsKeys.IS_WATCH_OPENING, PlayerPrefsKeys.TRUE);

		//BGMをタイトルに戻す
		MySoundManager.Instance.Play(BgmCollection.Title);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// キー状態のリセット
	/// </summary>
	void ResetKeyStatus()
	{
		m_isPressedOk = false;
		m_isPressedMenu = false;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// オープニングを開始
	/// </summary>
	public void StartOpening()
	{
		//視聴済み
		if (PlayerPrefs.GetInt(PlayerPrefsKeys.IS_WATCH_OPENING) == PlayerPrefsKeys.TRUE)
		{
			m_isEnd = true;
			return;
		}

		gameObject.SetActive(true);
		Sign.SetActive(false);
		King.SetActive(false);
		Boss.SetActive(false);
		m_stateNum = 0;
		m_serifNum = 0;
		m_serifStatementNum = 0;
		m_numAppearingImages = 0;
		AppealText.text = "";
		m_openingState = OpeningStatus.Moving;
		m_openingStatePrev = OpeningStatus.NotOpening;
		m_isEnd = false;

		//BGMの再生
		MySoundManager.Instance.Play(BgmCollection.Openning);
	}
}
