﻿//////////////////////////////////////////////////////////////////
//
//2018/5/29～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
//////////////////////////////////////////////////////////////////

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
    //財宝
    public GameObject m_MaskAImage;
    public GameObject m_MaskBImage;
    public GameObject m_MaskCImage;

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
        menuBase, retire,treasure, description
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// ボタンの位置の設定と名前の取得
    /// </summary>
    void Start()
    {
        myMenu = m_uiBaseObject.GetComponent<MyMenu>();
        UiReset();
        //名前を取得
        m_button1Name = m_button1.name;
        m_button2Name = m_button2.name;
        m_button3Name = m_button3.name;
        m_button4Name = m_button4.name;

        retireNum = 0;
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
        //リタイアメニュー
        m_retireText.SetActive(false);
        m_button5.SetActive(false);
        m_button6.SetActive(false);
        retireNum = 0;
        //財宝メニュー
        m_MaskAImage.GetComponent<Image>().enabled = false;
        m_MaskBImage.GetComponent<Image>().enabled = false;
        m_MaskCImage.GetComponent<Image>().enabled = false;

        //コントローラー関係
         m_inputUpCrossKey=false;
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
            //コントローラーの長押し制御
            #region
            //十字キー上下
            float VerticalKeyInput = Input.GetAxis("VerticalKey");
            if (VerticalKeyInput <= -inputhantei)
            {
                if (m_waitUpCrossKey != true)
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
                if (m_waitDownCrossKey != true)
                {
                    m_inputDownCrossKey = true;
                }
            }
            if (VerticalKeyInput < inputhantei)
            {
                m_waitDownCrossKey = false;
            }
            #endregion
            #region
            //左スティック上下
            float VerticalInput = Input.GetAxis("Vertical");
            if (VerticalInput <= -inputhantei)
            {
                if (m_waitDownStick != true)
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
                if (m_waitUpStick != true)
                {
                    m_inputUpStick = true;
                }
            }
            if (VerticalInput < inputhantei)
            {
                m_waitUpStick = false;
            }
            #endregion
            #region
            //左スティック左右
            float HorizontalInput = Input.GetAxis("Horizontal");
            if (HorizontalInput <= -inputhantei)
            {
                if (m_waitLeftStick != true)
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
                if (m_waitRightStick != true)
                {
                    m_inputRightStick = true;
                }
            }
            if (HorizontalInput < inputhantei)
            {
                m_waitRightStick = false;
            }
            #endregion
            #region
            //十字キー左右
            float HorizontalKeyInput = Input.GetAxis("HorizontalKey");
            if (HorizontalKeyInput <= -inputhantei)
            {
                if (m_waitLeftCrossKey != true)
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
                if (m_waitRightCrossKey != true)
                {
                    m_inputRightCrossKey = true;
                }
            }
            if (HorizontalKeyInput < inputhantei)
            {
                m_waitRightCrossKey = false;
            }
            #endregion
            //メニュー基本画面の時
            if (menuStates == MenuStates.menuBase|| menuStates == MenuStates.description || menuStates == MenuStates.treasure)
            {
                Debug.Log(menuStates);
                //キー入力時
                if (Input.GetKeyDown("up")||m_inputUpCrossKey==true|| m_inputUpStick == true)
                {
                    ChangeNum("up");
                    m_inputUpCrossKey = false;
                    m_waitUpCrossKey = true;
                    m_inputUpStick = false;
                    m_waitUpStick = true;
                    if(menuStates == MenuStates.treasure)
                    {
                        menuStates= MenuStates.menuBase;
                    }
                }

                if (Input.GetKeyDown("down") || m_inputDownCrossKey == true|| m_inputDownStick == true)
                {
                    ChangeNum("down");
                    m_inputDownCrossKey = false;
                    m_waitDownCrossKey = true;
                    m_inputDownStick = false;
                    m_waitDownStick = true;
                    if (menuStates == MenuStates.treasure)
                    {
                        menuStates = MenuStates.menuBase;
                    }
                }

                if (Input.GetKeyDown("return")|| Input.GetKeyDown(KeyCode.Joystick1Button0))
                {
                    PressDecideKey(m_choosingNum);
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
           else  if (menuStates == MenuStates.retire)
            {
                //アイコン表示
                MoveChanegeImageRetire(retireNum);
                m_retireText.SetActive(true);
                m_button5.SetActive(true);
                m_button6.SetActive(true);
                //アイコン移動
                if (Input.GetKeyDown("right")|| m_inputRightStick==true || m_inputRightCrossKey == true)
                {
                    ChangeRetire();
                    m_inputRightStick = false;
                    m_waitRightStick = true;
                    m_inputRightCrossKey = false;
                    m_waitRightCrossKey = true;
                }

                if (Input.GetKeyDown("left")||m_inputLeftStick==true || m_inputLeftCrossKey == true)
                {
                    ChangeRetire();
                    m_inputLeftStick = false;
                    m_waitLeftStick = true;
                    m_inputLeftCrossKey = false;
                    m_waitLeftCrossKey = true;
                }
                if (Input.GetKeyDown("return")|| Input.GetKeyDown(KeyCode.Joystick1Button0))
                {
                    if (retireNum == 0)
                    {
                        //"いいえ"で戻る
                        UiReset();
                    }
                    else
                    {
                        //"はい"でタイトルへ移動
                        SceneManager.LoadScene("Title");
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
                }
            }
        }
        if (menuStates == MenuStates.treasure)
        {
            m_MaskAImage.GetComponent<Image>().enabled = true;
            m_MaskBImage.GetComponent<Image>().enabled = true;
            m_MaskCImage.GetComponent<Image>().enabled = true;
        }
        else
        {
            m_MaskAImage.GetComponent<Image>().enabled = false;
            m_MaskBImage.GetComponent<Image>().enabled = false;
            m_MaskCImage.GetComponent<Image>().enabled = false;
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
        Debug.Log("仮面技選択");
        m_choosingNum = 1;
        MoveChooseImage(m_choosingNum);
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// ボタン２（財宝）
    /// </summary>
    void Button2Click()
    {
        Debug.Log("財宝選択");
        if (menuStates != MenuStates.treasure)
        {
            menuStates = MenuStates.treasure;
            m_choosingNum = 2;
            MoveChooseImage(m_choosingNum);
            m_MaskAImage.GetComponent<Image>().enabled = true;
            m_MaskBImage.GetComponent<Image>().enabled = true;
            m_MaskCImage.GetComponent<Image>().enabled = true;
        }
        else
        {
            menuStates = MenuStates.menuBase;
            m_MaskAImage.GetComponent<Image>().enabled = false;
            m_MaskBImage.GetComponent<Image>().enabled = false;
            m_MaskCImage.GetComponent<Image>().enabled = false;
        }
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// ボタン３（操作）
    /// </summary>
    void Button3Click()
    {
        Debug.Log("操作選択");
        m_choosingNum = 3;
        MoveChooseImage(m_choosingNum);
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// ボタン４（リタイア）
    /// </summary>
    void Button4Click()
    {
        Debug.Log("リタイア選択");
        menuStates = MenuStates.retire;
        m_choosingNum = 4;
        MoveChooseImage(m_choosingNum);
    }
}