//////////////////////////////////////////////////////////////////
//
//2018/7/3～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
//////////////////////////////////////////////////////////////////
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// トランジション制御用
/// </summary>
public class MyTransition : MonoBehaviour {

    [SerializeField]
    Sprite sp1;
    [SerializeField]
    Sprite sp2;
    [SerializeField]
    GameObject m_panel;
    public bool m_change;
    float m_time;
    Image image;
    float alfa;


    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// トランジション制御用
    /// </summary>
    void Start () {
        m_panel = this.gameObject;
        image = m_panel.GetComponent<Image>();
        alfa = GetComponent<Image>().color.a;

        sp1 = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Dummy/UI/sp1.png");
        sp2 = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Dummy/UI/sp2.png");

        image.sprite = sp1;
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 画面切り替えスタート
    /// </summary>
    public void StartTransition()
    {
        m_change = true;
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 画像を時間で切り替える
    /// </summary>
    void Update () {
        if (m_change==true)
        {
            m_time += Time.deltaTime;
            if (alfa == 0)
            {
                alfa = 1;
            }
            if (m_time > 0.5)
            {
                image.sprite = sp2;
                if (m_time > 1)
                {
                    alfa = 0;
                    m_change = false;
                    m_time = 0;
                    image.sprite = sp1;
                }
            }
            GetComponent<Image>().color = new Color(255, 255, 255, alfa);          
        }
	}
}
