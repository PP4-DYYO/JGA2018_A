////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/7月/17日～
//製作者 京都コンピュータ学院 ゲーム学科 四回生 中村智哉
//協力者 京都コンピュータ学院 ゲーム学科 四回生 奥田裕也
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyTitle : MonoBehaviour
{
	/// <summary>
	/// オープニング
	/// </summary>
	[SerializeField]
	MyOpening myOpening;

	/// <summary>
	/// 二つ目のタイトル
	/// </summary>
	[SerializeField]
	GameObject SecondTitle;

	/// <summary>
	/// 何か押してください
	/// </summary>
	[SerializeField]
	Text PleaseAnyKey;

	[SerializeField]
	Image select1;

	[SerializeField]
	Image select2;

	int m_selectNum;

	/// <summary>
	/// オープニングフラグ
	/// </summary>
	bool m_isOpening;

	bool m_changeFlag;

	float m_count;

	[SerializeField]
	float m_second;

	/// <summary>
	/// ロードフラグ
	/// </summary>
	bool m_isLoad;

	/// <summary>
	/// ロードオブジェト
	/// </summary>
	[SerializeField]
	GameObject LoadObj;

	/// <summary>
	/// ロードの選択番号
	/// </summary>
	int m_loadSelectNum;

	/// <summary>
	/// ロード数
	/// </summary>
	[SerializeField]
	int m_loadNum;

	/// <summary>
	/// 部屋のオブジェクト
	/// </summary>
	[SerializeField]
	GameObject[] RoomObjs;

	/// <summary>
	/// アイテム
	/// </summary>
	[SerializeField]
	GameObject[] Items;

	/// <summary>
	/// ロードの選択
	/// </summary>
	[SerializeField]
	Image[] LoadSelects;

	/// <summary>
	/// エンターガード
	/// </summary>
	bool m_isEnterGuard;

	/// <summary>
	/// 点滅フラグ
	/// </summary>
	bool m_isFlash;

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 初期
	/// </summary>
	void Start()
	{
		m_selectNum = 1;

		//部屋場音号を読み込む
		ReadRoomNum();

		//取得アイテム
		ReadGetItem();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 部屋番号を読み込む
	/// </summary>
	void ReadRoomNum()
	{
		//保存された部屋番号
		switch (PlayerPrefs.GetInt(PlayerPrefsKeys.STAGE_NUM))
		{
			case 0:
				m_loadNum = 1;
				break;
			case 1:
				m_loadNum = 2;
				break;
			case 2:
				m_loadNum = 3;
				break;
			case 3:
				m_loadNum = 4;
				break;
		}

		//部屋の非表示
		for (var i = m_loadNum; i < RoomObjs.Length; i++)
		{
			RoomObjs[i].SetActive(false);
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 取得アイテムの読み込み
	/// </summary>
	void ReadGetItem()
	{
		var isGetItem = PlayerPrefs.GetInt(PlayerPrefsKeys.IS_GET_ITEM);

		//非表示
		foreach (var item in Items)
		{
			item.SetActive(false);
		}

		//２進数で保存された取得アイテムの表示
		var bit = 1;
		for (var i = 0; i < Items.Length; i++)
		{
			if ((isGetItem & bit) == bit)
				Items[i].SetActive(true);
			bit *= 2;
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// フレーム
	/// </summary>
	void Update()
	{
		//点滅
		m_count = m_count + Time.deltaTime;
		if (m_count >= m_second)
		{
			m_isFlash = !m_isFlash;
			m_count = 0;
		}

		if (!m_isOpening)
			FirstTitleProcess();

		//エンターガード
		if (m_isEnterGuard)
		{
			if (!Input.anyKey)
				m_isEnterGuard = false;
			return;
		}

		if (myOpening.IsEnd)
			if (!m_isLoad)
				SecondTitleProcess();
			else
				LoadProcess();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 一つ目のタイトル処理
	/// </summary>
	void FirstTitleProcess()
	{
		PleaseAnyKey.enabled = m_isFlash;

		if (!m_isOpening)
		{
			if (Input.anyKey)
			{
				m_isOpening = true;
				SecondTitle.SetActive(true);
				myOpening.StartOpening();
				m_isEnterGuard = true;
			}
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 二つ目のタイトル処理
	/// </summary>
	void SecondTitleProcess()
	{
		if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Return))
		{
			if (m_selectNum == 1)
			{
				OnClickStartButton();
			}
			else
			{
				OnClickLoadButton();
			}
		}
		float HorizontalKeyInput = Input.GetAxis("HorizontalKey") + Input.GetAxis("Horizontal") + Input.GetAxis("CrossKeyLeft") + Input.GetAxis("CrossKeyRight");

		if (HorizontalKeyInput <= -0.1f && m_changeFlag == false)
		{
			if (m_selectNum == 1)
			{
				m_selectNum = 2;
			}
			else
			{
				m_selectNum = 1;
			}
			m_changeFlag = true;
		}
		else if (HorizontalKeyInput >= 0.1f && m_changeFlag == false)
		{
			if (m_selectNum == 1)
			{
				m_selectNum = 2;
			}
			else
			{
				m_selectNum = 1;
			}
			m_changeFlag = true;
		}
		if (m_selectNum == 1)
		{
			select1.enabled = m_isFlash;
			select2.enabled = false;
		}
		else if (m_selectNum == 2)
		{
			select1.enabled = false;
			select2.enabled = m_isFlash;
		}
		if (HorizontalKeyInput <= 0.1 && HorizontalKeyInput >= -0.1)
		{
			m_changeFlag = false;
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ロード処理
	/// </summary>
	void LoadProcess()
	{
		//決定
		if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Return))
		{
			//選択番号
			switch (m_loadSelectNum)
			{
				case 0:
					OnClickStartButton();
					MyGameInfo.Instance.StageNum = 0;
					break;
				case 1:
					OnClickStartButton();
					MyGameInfo.Instance.StageNum = 1;
					break;
				case 2:
					OnClickStartButton();
					MyGameInfo.Instance.StageNum = 2;
					break;
				case 3:
					OnClickStartButton();
					MyGameInfo.Instance.StageNum = 3;
					break;
			}
		}

		//選択
		var HorizontalKeyInput = Input.GetAxis("HorizontalKey") + Input.GetAxis("Horizontal") + Input.GetAxis("CrossKeyLeft") + Input.GetAxis("CrossKeyRight");

		if (HorizontalKeyInput <= -0.1f && m_changeFlag == false)
		{
			m_loadSelectNum = (m_loadSelectNum == 0) ? m_loadNum - 1 : m_loadSelectNum - 1;
			m_changeFlag = true;
		}
		else if (HorizontalKeyInput >= 0.1f && m_changeFlag == false)
		{
			m_loadSelectNum = (m_loadSelectNum + 1) % m_loadNum;
			m_changeFlag = true;
		}

		//表示非表示
		foreach (var select in LoadSelects)
		{
			if (!m_isFlash)
				select.enabled = false;
			else
				select.enabled = (select == LoadSelects[m_loadSelectNum]);
		}

		//選択不可
		if (HorizontalKeyInput <= 0.1 && HorizontalKeyInput >= -0.1)
		{
			m_changeFlag = false;
		}
	}

	public void OnClickStartButton()
	{
		MySceneManager.Instance.ChangeScene(MyScene.Main);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ロードボタンをクリック
	/// </summary>
	public void OnClickLoadButton()
	{
		LoadObj.SetActive(true);
		m_isLoad = true;
	}
}
