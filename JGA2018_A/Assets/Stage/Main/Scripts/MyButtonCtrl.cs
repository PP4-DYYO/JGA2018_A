////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/5/29～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//協力者 京都コンピュータ学院京都駅前校ゲーム学科四回生　奥田裕也
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

///<summary>
///ボタンの処理本体
///</summary>
public class MyButtonCtrl : MyBaseButtonCtrl
{
	/// <summary>
	///Mymenuのスクリプト
	/// </summary>
	MyMenu myMenu;

	/// <summary>
	/// プレイヤー
	/// </summary>
	MyPlayer m_player;

	/// <summary>
	/// uiの親オブジェクト
	/// </summary>
	public GameObject m_uiBaseObject;

	/// <summary>
	/// ボタンの選択中イメージ(紙飛行機マーク、Unity上で設定)
	/// </summary>
	public GameObject m_button1ChooseImage;
	public GameObject m_button2ChooseImage;
	public GameObject m_button3ChooseImage;
	public GameObject m_button4ChooseImage;
	//リタイア
	public GameObject m_button5ChooseImage;
	public GameObject m_button6ChooseImage;
	public GameObject m_retireText;

	/// <summary>
	/// 財宝
	/// </summary>
	[SerializeField]
	GameObject Treature;

	/// <summary>
	/// 財宝たち
	/// </summary>
	[SerializeField]
	GameObject[] Treatures;

	/// <summary>
	/// 財宝画像たち
	/// </summary>
	[SerializeField]
	Image[] TreatureImages;

	/// <summary>
	/// 操作表
	/// </summary>
	[SerializeField]
	GameObject OperationTable;

	/// <summary>
	/// ボタンオブジェクト(Unity上で設定)
	/// </summary>
	public GameObject m_button1;
	public GameObject m_button2;
	public GameObject m_button3;
	public GameObject m_button4;

	/// <summary>
	/// リタイア
	/// </summary>
	public GameObject m_button5;
	public GameObject m_button6;
	int retireNum;
	bool retire;

	/// <summary>
	///一番上のメニュー画面のボタンの数 
	/// </summary>
	const int buttonNum = 4;

	/// <summary>
	/// ボタンオブジェクトの名前
	/// </summary>
	string m_button1Name;
	string m_button2Name;
	string m_button3Name;
	string m_button4Name;

	/// <summary>
	///選択中のボタンはどれであるか 
	/// </summary>
	int m_choosingNum;

	/// <summary>
	///選択中のメニューはどれであるか 
	/// </summary>
	MenuStates menuStates;

	bool m_inputUpCrossKey;
	bool m_waitUpCrossKey;
	bool m_inputDownCrossKey;
	bool m_waitDownCrossKey;

	bool m_inputUpStick;
	bool m_waitUpStick;
	bool m_inputDownStick;
	bool m_waitDownStick;

	bool m_inputRightStick;
	bool m_waitRightStick;
	bool m_inputLeftStick;
	bool m_waitLeftStick;

	bool m_inputRightCrossKey;
	bool m_waitRightCrossKey;
	bool m_inputLeftCrossKey;
	bool m_waitLeftCrossKey;

	float inputhantei = 0.9f;

	/// <summary>
	/// メニューの開いているところ
	/// </summary>
	public enum MenuStates
	{
		menuBase, retire, treasure, description, mask
	}

	#region マスク関係
	[Header("マスク関係")]
	/// <summary>
	/// マスクの収集物
	/// </summary>
	[SerializeField]
	GameObject MaskCollection;

	/// <summary>
	/// マスクゲージ
	/// </summary>
	[SerializeField]
	Image MaskGauge;

	/// <summary>
	/// キャリーマスクゲージ
	/// </summary>
	[SerializeField]
	Image CarryMaskGauge;

	/// <summary>
	/// ウイルスマスクゲージ
	/// </summary>
	[SerializeField]
	Image VirusMaskGauge;

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
	/// マスク名
	/// </summary>
	[SerializeField]
	Text MaskName;

	/// <summary>
	/// マスク名たち
	/// </summary>
	[SerializeField]
	string[] m_maskNames;

	/// <summary>
	/// マスクの説明
	/// </summary>
	[SerializeField]
	Text DescriptionOfMask;

	/// <summary>
	/// マスクの説明たち
	/// </summary>
	[SerializeField, Multiline]
	string[] m_descriptionOfMask;

	/// <summary>
	/// マスク選択番号
	/// </summary>
	int m_maskSelectionNum;
	#endregion

	#region 点滅
	[Header("点滅")]
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
	/// カウントをスタートする時間
	/// </summary>
	float m_timeToStartCounting;

	/// <summary>
	/// 点滅
	/// </summary>
	bool m_isFlash;
	#endregion

	/// <summary>
	/// 作業用のFloat
	/// </summary>
	float m_workFloat;

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ボタンの位置の設定と名前の取得
	/// </summary>
	void Start()
	{
		myMenu = m_uiBaseObject.GetComponent<MyMenu>();
		m_player = myMenu.MainUiScript.GameScript.CharacterScript.PlayerScript;
		UiReset();
		//名前を取得
		m_button1Name = m_button1.name;
		m_button2Name = m_button2.name;
		m_button3Name = m_button3.name;
		m_button4Name = m_button4.name;

		retireNum = 0;
		m_maskSelectionNum = -1;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ui初期状態
	/// </summary>
	void UiReset()
	{
		m_choosingNum = 1;
		menuStates = MenuStates.menuBase;
		m_button1ChooseImage.GetComponent<Image>().enabled = true;
		m_button2ChooseImage.GetComponent<Image>().enabled = false;
		m_button3ChooseImage.GetComponent<Image>().enabled = false;
		m_button4ChooseImage.GetComponent<Image>().enabled = false;
		m_button5ChooseImage.GetComponent<Image>().enabled = false;
		m_button6ChooseImage.GetComponent<Image>().enabled = false;
		//仮面メニュー
		MaskCollection.SetActive(false);
		//リタイアメニュー
		m_retireText.SetActive(false);
		m_button5.SetActive(false);
		m_button6.SetActive(false);
		retireNum = 0;
		//財宝メニュー
		int multiplier2; //２の乗数
		var treature = PlayerPrefs.GetInt(PlayerPrefsKeys.IS_GET_ITEM); //財宝取得状況
		for (var i = 0; i < TreatureImages.Length; i++)
		{
			//財宝の取得状況を反映
			multiplier2 = (int)Mathf.Pow(2, i);
			TreatureImages[i].enabled = ((treature & multiplier2) == multiplier2);
		}
		for (var i = 0; i < Treatures.Length; i++)
		{
			//ステージ番号に対応した財宝を表示
			Treatures[i].SetActive(i == myMenu.MainUiScript.GameScript.StageNum);
		}
		//操作表
		OperationTable.SetActive(false);

		//コントローラー関係
		m_inputUpCrossKey = false;
		m_waitUpCrossKey = false;
		m_inputDownCrossKey = false;
		m_waitDownCrossKey = false;

		m_inputUpStick = false;
		m_waitUpStick = false;
		m_inputDownStick = false;
		m_waitDownStick = false;

		m_inputRightStick = false;
		m_waitRightStick = false;
		m_inputLeftStick = false;
		m_waitLeftStick = false;

		m_inputRightCrossKey = false;
		m_waitRightCrossKey = false;
		m_inputLeftCrossKey = false;
		m_waitLeftCrossKey = false;

		//マスクゲージの取得
		GetMaskGauge();

		//マスクの状態の取得
		GetMaskState();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// マスクゲージの取得
	/// </summary>
	void GetMaskGauge()
	{
		//キャリーマスク
		m_workFloat = CarryMaskGauge.fillAmount;
		CarryMaskGauge.fillAmount = m_player.CarryMask.countGauge / m_player.MaxCarryMaskGauge;
		MaskGauge.fillAmount = (m_workFloat > CarryMaskGauge.fillAmount) ? CarryMaskGauge.fillAmount : MaskGauge.fillAmount;

		//ウイルスマスク
		m_workFloat = VirusMaskGauge.fillAmount;
		VirusMaskGauge.fillAmount = m_player.VirusMask.countGauge / m_player.MaxVirusMaskGauge;
		MaskGauge.fillAmount = (m_workFloat > VirusMaskGauge.fillAmount) ? VirusMaskGauge.fillAmount : MaskGauge.fillAmount;

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
	/// マスクの状態の取得
	/// </summary>
	void GetMaskState()
	{
		//マスク獲得済みか
		CarryMaskCollection.SetActive(m_player.CarryMask.isObtained);
		VirusMaskCollection.SetActive(m_player.VirusMask.isObtained);
		MirrorMaskCollection.SetActive(m_player.MirrorMask.isObtained);
		MagicMaskCollection.SetActive(m_player.MagicMask.isObtained);

		//使用中マスク
		CarryMaskInUse.enabled = m_player.CarryMask.isUse;
		VirusMaskInUse.enabled = m_player.VirusMask.isUse;
		MirrorMaskInUse.enabled = m_player.MirrorMask.isUse;
		MagicMaskInUse.enabled = m_player.MagicMask.isUse;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ボタンの動作（キーボード用）
	/// </summary>
	void Update()
	{
		//メニュー表示してないとき
		if (myMenu.m_menuMove == false)
		{
			UiReset();
		}

		//メニュー表示しているとき
		if (myMenu.m_menuMove == true)
		{
			InputKey();
			DisplayMenu();
		}
		if (menuStates == MenuStates.treasure)
		{
			Treature.SetActive(true);
		}
		else
		{
			Treature.SetActive(false);
		}
		MaskCollection.SetActive(menuStates == MenuStates.mask);
		OperationTable.SetActive(menuStates == MenuStates.description);

		Flashing();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// コントローラーの長押し制御
	/// </summary>
	void InputKey()
	{
		#region 十字キー上下

		float VerticalKeyInput = Input.GetAxis("VerticalKey");
		if (VerticalKeyInput <= -inputhantei)
		{
			if (!m_waitUpCrossKey)
			{
				m_inputUpCrossKey = true;
			}
		}
		if (VerticalKeyInput > -inputhantei)
		{
			m_waitUpCrossKey = false;
		}

		if (VerticalKeyInput >= inputhantei)
		{
			if (!m_waitDownCrossKey)
			{
				m_inputDownCrossKey = true;
			}
		}
		if (VerticalKeyInput < inputhantei)
		{
			m_waitDownCrossKey = false;
		}
		#endregion
		#region　左スティック上下

		float VerticalInput = Input.GetAxis("Vertical");
		if (VerticalInput <= -inputhantei)
		{
			if (!m_waitDownStick)
			{
				m_inputDownStick = true;
			}
		}
		if (VerticalInput > -inputhantei)
		{
			m_waitDownStick = false;
		}

		if (VerticalInput >= inputhantei)
		{
			if (!m_waitUpStick)
			{
				m_inputUpStick = true;
			}
		}
		if (VerticalInput < inputhantei)
		{
			m_waitUpStick = false;
		}
		#endregion
		#region　左スティック左右
		float HorizontalInput = Input.GetAxis("Horizontal");
		if (HorizontalInput <= -inputhantei)
		{
			if (!m_waitLeftStick)
			{
				m_inputLeftStick = true;
			}
		}
		if (HorizontalInput > -inputhantei)
		{
			m_waitLeftStick = false;
		}

		if (HorizontalInput >= inputhantei)
		{
			if (!m_waitRightStick)
			{
				m_inputRightStick = true;
			}
		}
		if (HorizontalInput < inputhantei)
		{
			m_waitRightStick = false;
		}
		#endregion
		#region　十字キー左右
		float HorizontalKeyInput = Input.GetAxis("HorizontalKey");
		if (HorizontalKeyInput <= -inputhantei)
		{
			if (!m_waitLeftCrossKey)
			{
				m_inputLeftCrossKey = true;
			}
		}
		if (HorizontalKeyInput > -inputhantei)
		{
			m_waitLeftCrossKey = false;
		}

		if (HorizontalKeyInput >= inputhantei)
		{
			if (!m_waitRightCrossKey)
			{
				m_inputRightCrossKey = true;
			}
		}
		if (HorizontalKeyInput < inputhantei)
		{
			m_waitRightCrossKey = false;
		}
		#endregion
	}

	//----------------------------------------------------------------------------------------------------
	///<summary>
	///メニュー表示処理
	///</summary>
	void DisplayMenu()
	{
		//メニュー基本画面の時
		if (menuStates == MenuStates.menuBase || menuStates == MenuStates.description
			|| menuStates == MenuStates.treasure || menuStates == MenuStates.mask)
		{
			//仮面技
			if (menuStates == MenuStates.mask)
			{
				if (Input.GetKeyDown("right") || m_inputRightStick == true || m_inputRightCrossKey == true)
				{
					m_inputRightStick = false;
					m_waitRightStick = true;
					m_inputRightCrossKey = false;
					m_waitRightCrossKey = true;
					FallBackMaskSelectionNum();
				}
				if (Input.GetKeyDown("left") || m_inputLeftStick == true || m_inputLeftCrossKey == true)
				{
					m_inputLeftStick = false;
					m_waitLeftStick = true;
					m_inputLeftCrossKey = false;
					m_waitLeftCrossKey = true;
					AddvanceMaskSelectionNum();
				}
				SelectMask();
			}

			//キー入力時
			if (Input.GetKeyDown("up") || m_inputUpCrossKey == true || m_inputUpStick == true)
			{
				ChangeNum("up");
				m_inputUpCrossKey = false;
				m_waitUpCrossKey = true;
				m_inputUpStick = false;
				m_waitUpStick = true;
				if (menuStates == MenuStates.treasure || menuStates == MenuStates.mask || menuStates == MenuStates.description)
				{
					menuStates = MenuStates.menuBase;
				}
			}

			if (Input.GetKeyDown("down") || m_inputDownCrossKey == true || m_inputDownStick == true)
			{
				ChangeNum("down");
				m_inputDownCrossKey = false;
				m_waitDownCrossKey = true;
				m_inputDownStick = false;
				m_waitDownStick = true;
				if (menuStates == MenuStates.treasure || menuStates == MenuStates.mask || menuStates == MenuStates.description)
				{
					menuStates = MenuStates.menuBase;
				}
			}

			if (Input.GetKeyDown("return") || Input.GetKeyDown(KeyCode.Joystick1Button0))
			{
				PressDecideKey(m_choosingNum);

				//SEの再生
				MySoundManager.Instance.Play(SeCollection.DecideItem);
			}

			//表示関連
			switch (m_choosingNum)
			{
				case 1:
					MoveChooseImage(1);
					break;
				case 2:
					MoveChooseImage(2);
					break;
				case 3:
					MoveChooseImage(3);
					break;
				case 4:
					MoveChooseImage(4);
					break;
			}
			m_retireText.SetActive(false);
			m_button5.SetActive(false);
			m_button6.SetActive(false);
		}

		//リタイア画面の時
		else if (menuStates == MenuStates.retire)
		{
			//アイコン表示
			MoveChanegeImageRetire(retireNum);
			m_retireText.SetActive(true);
			m_button5.SetActive(true);
			m_button6.SetActive(true);
			//アイコン移動
			if (Input.GetKeyDown("right") || m_inputRightStick == true || m_inputRightCrossKey == true)
			{
				ChangeRetire();
				m_inputRightStick = false;
				m_waitRightStick = true;
				m_inputRightCrossKey = false;
				m_waitRightCrossKey = true;
			}

			if (Input.GetKeyDown("left") || m_inputLeftStick == true || m_inputLeftCrossKey == true)
			{
				ChangeRetire();
				m_inputLeftStick = false;
				m_waitLeftStick = true;
				m_inputLeftCrossKey = false;
				m_waitLeftCrossKey = true;
			}
			if (Input.GetKeyDown("return") || Input.GetKeyDown(KeyCode.Joystick1Button0))
			{
				if (retireNum == 0)
				{
					//"いいえ"で戻る
					UiReset();
				}
				else
				{
					//"はい"でタイトルへ移動
					myMenu.m_menuMove = false;
					MySceneManager.Instance.ChangeScene(MyScene.Title);
				}

				if (retireNum == 0)
				{
					m_button5ChooseImage.GetComponent<Image>().enabled = false;
					m_button6ChooseImage.GetComponent<Image>().enabled = true;
				}
				else
				{
					m_button5ChooseImage.GetComponent<Image>().enabled = true;
					m_button6ChooseImage.GetComponent<Image>().enabled = false;
				}

				//SEの再生
				MySoundManager.Instance.Play(SeCollection.DecideItem);
			}
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 選択中アイコンの移動、矢印キーかクリック時に移動
	/// </summary>
	void MoveChooseImage(int num)
	{
		switch (num)
		{
			//アイコン全てを非表示に
			case 0:
				m_button1ChooseImage.GetComponent<Image>().enabled = false;
				m_button2ChooseImage.GetComponent<Image>().enabled = false;
				m_button3ChooseImage.GetComponent<Image>().enabled = false;
				m_button4ChooseImage.GetComponent<Image>().enabled = false;
				break;
			//基本メニュー中のマークの表示を切り替え
			case 1:
				m_button1ChooseImage.GetComponent<Image>().enabled = true;
				m_button2ChooseImage.GetComponent<Image>().enabled = false;
				m_button3ChooseImage.GetComponent<Image>().enabled = false;
				m_button4ChooseImage.GetComponent<Image>().enabled = false;
				break;
			case 2:
				m_button1ChooseImage.GetComponent<Image>().enabled = false;
				m_button2ChooseImage.GetComponent<Image>().enabled = true;
				m_button3ChooseImage.GetComponent<Image>().enabled = false;
				m_button4ChooseImage.GetComponent<Image>().enabled = false;
				break;
			case 3:
				m_button1ChooseImage.GetComponent<Image>().enabled = false;
				m_button2ChooseImage.GetComponent<Image>().enabled = false;
				m_button3ChooseImage.GetComponent<Image>().enabled = true;
				m_button4ChooseImage.GetComponent<Image>().enabled = false;
				break;
			case 4:
				m_button1ChooseImage.GetComponent<Image>().enabled = false;
				m_button2ChooseImage.GetComponent<Image>().enabled = false;
				m_button3ChooseImage.GetComponent<Image>().enabled = false;
				m_button4ChooseImage.GetComponent<Image>().enabled = true;
				break;
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// マスク選択番号を後退させる
	/// </summary>
	void FallBackMaskSelectionNum()
	{
		var maskSelectionNum = m_maskSelectionNum;

		//SEの再生
		MySoundManager.Instance.Play(SeCollection.SelectItem);

		//全マスクを検索
		for (var i = 0; i < m_maskNames.Length; i++)
		{
			m_maskSelectionNum = (m_maskSelectionNum <= 0) ? m_maskNames.Length - 1 : m_maskSelectionNum - 1;

			//指定マスクが獲得済み
			if (IsObtainedMask(m_maskSelectionNum))
				return;
		}

		m_maskSelectionNum = maskSelectionNum;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// マスク選択番号を進める
	/// </summary>
	void AddvanceMaskSelectionNum()
	{
		var maskSelectionNum = m_maskSelectionNum;

		//SEの再生
		MySoundManager.Instance.Play(SeCollection.SelectItem);

		//全マスクを検索
		for (var i = 0; i < m_maskNames.Length; i++)
		{
			m_maskSelectionNum = (m_maskSelectionNum >= m_maskNames.Length - 1) ? 0 : m_maskSelectionNum + 1;

			//指定マスクが獲得済み
			if (IsObtainedMask(m_maskSelectionNum))
				return;
		}

		m_maskSelectionNum = maskSelectionNum;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// マスクが獲得済みか
	/// </summary>
	/// <param name="maskNum">マスク番号</param>
	/// <returns>獲得済み</returns>
	bool IsObtainedMask(int maskNum)
	{
		//マスク番号
		switch (maskNum)
		{
			case 0:
				return CarryMaskCollection.activeInHierarchy;
			case 1:
				return VirusMaskCollection.activeInHierarchy;
			case 2:
				return MirrorMaskCollection.activeInHierarchy;
			case 3:
				return MagicMaskCollection.activeInHierarchy;
		}

		return false;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// マスクを選択
	/// </summary>
	void SelectMask()
	{
		//注目を非表示
		CarryMaskToLetAttention.enabled = false;
		VirusMaskToLetAttention.enabled = false;
		MirrorMaskToLetAttention.enabled = false;
		MagicMaskToLetAttention.enabled = false;

		//マスク選択番号
		switch (m_maskSelectionNum)
		{
			case 0:
				CarryMaskToLetAttention.enabled = true;
				break;
			case 1:
				VirusMaskToLetAttention.enabled = true;
				break;
			case 2:
				MirrorMaskToLetAttention.enabled = true;
				break;
			case 3:
				MagicMaskToLetAttention.enabled = true;
				break;
			default:
				return;
		}

		//マスク名とマスク説明
		MaskName.text = m_maskNames[m_maskSelectionNum];
		DescriptionOfMask.text = m_descriptionOfMask[m_maskSelectionNum];
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 選択中アイコンの移動、リタイア
	/// </summary>
	void MoveChanegeImageRetire(int num)
	{
		switch (num)
		{
			case 0:
				m_button5ChooseImage.GetComponent<Image>().enabled = false;
				m_button6ChooseImage.GetComponent<Image>().enabled = true;
				break;
			case 1:
				m_button5ChooseImage.GetComponent<Image>().enabled = true;
				m_button6ChooseImage.GetComponent<Image>().enabled = false;
				break;
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 矢印キーで"はい","いいえ"の選択変更（リタイア）
	/// </summary>
	void ChangeRetire()
	{
		if (retireNum == 0)
		{
			//"はい"の状態
			retireNum = 1;
		}
		else
		{
			//"いいえ"の状態
			retireNum = 0;
		}
		MoveChanegeImageRetire(retireNum);

		//SEの再生
		MySoundManager.Instance.Play(SeCollection.SelectItem);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 矢印キーで選択中アイコンの移動
	/// </summary>
	void ChangeNum(string direction)
	{
		if (direction == "up")
		{
			//一番上なら下に
			if (m_choosingNum == 1)
			{
				m_choosingNum = buttonNum;
			}
			else
			{
				m_choosingNum--;
			}
		}
		else
		{
			//一番下なら上に
			if (m_choosingNum == buttonNum)
			{
				m_choosingNum = 1;
			}
			else
			{
				m_choosingNum++;
			}
		}
		MoveChooseImage(m_choosingNum);

		//SEの再生
		MySoundManager.Instance.Play(SeCollection.SelectItem);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 決定ボタン
	/// </summary>
	void PressDecideKey(int num)
	{
		switch (num)
		{
			case 1:
				this.Button1Click();
				break;
			case 2:
				this.Button2Click();
				break;
			case 3:
				this.Button3Click();
				break;
			case 4:
				this.Button4Click();
				break;
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// それぞれのボタンが押されたとき
	/// </summary>
	protected override void OnClick(string objectName)
	{
		if (m_button1Name.Equals(objectName))
		{
			this.Button1Click();
		}
		else if (m_button2Name.Equals(objectName))
		{
			this.Button2Click();
		}
		else if (m_button3Name.Equals(objectName))
		{
			this.Button3Click();
		}
		else if (m_button4Name.Equals(objectName))
		{
			this.Button4Click();
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ボタン１（仮面技）
	/// </summary>
	void Button1Click()
	{
		m_choosingNum = 1;
		MoveChooseImage(m_choosingNum);
		menuStates = MenuStates.mask;
		MaskCollection.SetActive(true);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ボタン２（財宝）
	/// </summary>
	void Button2Click()
	{
		if (menuStates != MenuStates.treasure)
		{
			menuStates = MenuStates.treasure;
			m_choosingNum = 2;
			MoveChooseImage(m_choosingNum);
			Treature.SetActive(true);
		}
		else
		{
			menuStates = MenuStates.menuBase;
			Treature.SetActive(false);
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ボタン３（操作）
	/// </summary>
	void Button3Click()
	{
		m_choosingNum = 3;
		MoveChooseImage(m_choosingNum);
		menuStates = MenuStates.description;
		OperationTable.SetActive(true);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ボタン４（リタイア）
	/// </summary>
	void Button4Click()
	{
		menuStates = MenuStates.retire;
		m_choosingNum = 4;
		MoveChooseImage(m_choosingNum);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 点滅
	/// </summary>
	void Flashing()
	{
		m_countFlashingTime = Time.realtimeSinceStartup - m_timeToStartCounting;

		//点滅処理
		if (m_countFlashingTime >= m_blinkingTime)
		{
			m_isFlash = !m_isFlash;
			m_timeToStartCounting = Time.realtimeSinceStartup;
		}

		//点滅対象
		switch (m_choosingNum)
		{
			case 1:
				m_button1ChooseImage.GetComponent<Image>().enabled = m_isFlash;
				break;
			case 2:
				m_button2ChooseImage.GetComponent<Image>().enabled = m_isFlash;
				break;
			case 3:
				m_button3ChooseImage.GetComponent<Image>().enabled = m_isFlash;
				break;
			case 4:
				m_button4ChooseImage.GetComponent<Image>().enabled = m_isFlash;
				if (retireNum == 0)
					m_button6ChooseImage.GetComponent<Image>().enabled = !m_isFlash;
				else
					m_button5ChooseImage.GetComponent<Image>().enabled = !m_isFlash;
				break;
		}
	}
}
