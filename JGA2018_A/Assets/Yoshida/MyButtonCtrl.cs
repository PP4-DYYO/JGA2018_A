//////////////////////////////////////////////////////////////////
//
//2018/5/29～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
//////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;

///<summary>
///ボタンの処理本体
///</summary>
public class MyButtonCtrl : MyBaseButtonCtrl
{
    /// <summary>
    /// ボタンの選択中イメージ(Unity上で設定)
    /// </summary>
    public GameObject m_button1ChooseImage;
    public GameObject m_button2ChooseImage;
    public GameObject m_button3ChooseImage;
    public GameObject m_button4ChooseImage;

    /// <summary>
    /// ボタンオブジェクト(Unity上で設定)
    /// </summary>
    public GameObject m_button1;
    public GameObject m_button2;
    public GameObject m_button3;
    public GameObject m_button4;

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

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// ボタンの位置の設定と名前の取得
    /// </summary>
    void Start()
    {
        //初期状態としてボタン1を選択状態にする
        m_button1ChooseImage.GetComponent<Image>().enabled = true;
        m_button2ChooseImage.GetComponent<Image>().enabled = false;
        m_button3ChooseImage.GetComponent<Image>().enabled = false;
        m_button4ChooseImage.GetComponent<Image>().enabled = false;
        m_choosingNum = 1;

        //名前を取得
        m_button1Name = m_button1.name;
        m_button2Name = m_button2.name;
        m_button3Name = m_button3.name;
        m_button4Name = m_button4.name;
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// ボタンの動作（キーボード用）
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown("up"))
        {
            ChangeNum("up");
        }

        if (Input.GetKeyDown("down"))
        {
            ChangeNum("down");
        }

        if (Input.GetKeyDown("return"))
        {
            PressReturnKey(m_choosingNum);
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
    /// 矢印キーで選択中アイコンの移動
    /// </summary>
    void ChangeNum(string way)
    {
        if (way == "up")
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
            MoveChooseImage(m_choosingNum);
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
            MoveChooseImage(m_choosingNum);
        }
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// エンターキーを押したときにクリックと同じ処理
    /// </summary>
    void PressReturnKey(int num)
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


    /// <summary>
    /// ボタン１（仮面技）
    /// </summary>
    void Button1Click()
    {
        Debug.Log("仮面技選択");
        m_choosingNum = 1;
        MoveChooseImage(m_choosingNum);
    }

    /// <summary>
    /// ボタン２（財宝）
    /// </summary>
    void Button2Click()
    {
        Debug.Log("財宝選択");
        m_choosingNum = 2;
        MoveChooseImage(m_choosingNum);
    }

    /// <summary>
    /// ボタン３（操作）
    /// </summary>
    void Button3Click()
    {
        Debug.Log("操作選択");
        m_choosingNum = 3;
        MoveChooseImage(m_choosingNum);
    }

    /// <summary>
    /// ボタン４（リタイア）
    /// </summary>
    void Button4Click()
    {
        Debug.Log("リタイア選択");
        m_choosingNum = 4;
        MoveChooseImage(m_choosingNum);
    }
}
