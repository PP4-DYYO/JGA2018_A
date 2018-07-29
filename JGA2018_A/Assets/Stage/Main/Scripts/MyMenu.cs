////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/5/29～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

///<summary>
///メニュー
///</summary>
public class MyMenu : MonoBehaviour
{
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
    /// メニュー画面の４つのボタンを設定（unity上で）
    /// </summary>
    public GameObject m_Button1;
    public GameObject m_Button2;
    public GameObject m_Button3;
    public GameObject m_Button4;

    /// <summary>
    /// 画面外の位置(非表示＝画面外とする)
    /// </summary>
    float m_uiOutPosx;

    /// <summary>
    /// 画面内の位置
    /// </summary>
    float m_uiInPosx;

    /// <summary>
    /// メニューの移動判定(MyButtonCtrlで利用)
    /// </summary>
   public  bool m_menuMove;

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// メニュー位置初期設定
    /// </summary>
    void Start()
    {
        //画面外の位置
        m_uiOutPosx = -(Screen.width-Screen.width/2);

        //画面内の位置
        m_uiInPosx = this.transform.position.x;

        //移動
        this.transform.position = new Vector3(m_uiOutPosx, this.transform.position.y, 0);
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// メニューを開く処理
    /// </summary>
    void Update()
    {
        // メニューボタンを押したとき
        if (Input.GetKeyDown("m")|| Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (m_menuMove != true)
            {
                m_menuMove = true;
            }
            else
            {
                m_menuMove = false;
            }
        }

        //メニューを動かす
        if (m_menuMove)
        {
            while(m_uiInPosx > this.transform.position.x)
            {
                
                this.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y, 0);
            }
            Time.timeScale = 0f;
        }
        else
        {
            while (m_uiOutPosx < this.transform.position.x)
            {
                this.transform.position = new Vector3(this.transform.position.x-1, this.transform.position.y, 0);
            }
            Time.timeScale = 1f;
        }
    }
}
