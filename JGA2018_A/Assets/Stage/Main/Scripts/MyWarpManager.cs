//////////////////////////////////////////////////////////////////
//
//2018/6/24～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
//////////////////////////////////////////////////////////////////

using UnityEngine;

public class MyWarpManager : MonoBehaviour
{
    /// <summary>
    /// ワープのゲームオブジェクト
    /// </summary>
    GameObject warpA;
    GameObject warpA_;
    GameObject warpB;
    GameObject warpB_;
    GameObject warpC;
    GameObject warpC_;
    GameObject warpD;
    GameObject warpD_;
    GameObject warpE1;
    GameObject warpE2;
    GameObject warpE3;
    GameObject warpF;
    GameObject warpF_;
    GameObject warpG1;
    GameObject warpG2;
    GameObject warpG3;
    GameObject warpH1;
    GameObject warpH2;

    /// <summary>
    /// プレイヤーのゲームオブジェクト
    /// </summary>
    GameObject m_playerObject;


    /// <summary>
    /// 時間計測用
    /// </summary>
    float m_time = 0;


    /// <summary>
    /// ワープ使用可能状態
    /// </summary>
    bool m_canUseWarp;


    /// <summary>
    /// スクリプト
    /// </summary>
    MyTransition myTransition;

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 最初に代入
    /// </summary>
    void Start()
    {
        myTransition = GameObject.Find("TransitionPanel").GetComponent<MyTransition>();
        warpA = GameObject.Find("WarpA");
        warpA_ = GameObject.Find("WarpA_");
        warpB = GameObject.Find("WarpB");
        warpB_ = GameObject.Find("WarpB_");
        warpC = GameObject.Find("WarpC");
        warpC_ = GameObject.Find("WarpC_");
        warpD = GameObject.Find("WarpD");
        warpD_ = GameObject.Find("WarpD_");
        warpF = GameObject.Find("WarpF");
        warpF_ = GameObject.Find("WarpF_");

        warpE1 = GameObject.Find("WarpE1");
        warpE2 = GameObject.Find("WarpE2");
        warpE3 = GameObject.Find("WarpE3");
        warpG1 = GameObject.Find("WarpG1");
        warpG2 = GameObject.Find("WarpG2");
        warpG3 = GameObject.Find("WarpG3");
        warpH1 = GameObject.Find("WarpH1");
        warpH2 = GameObject.Find("WarpH2");

        m_playerObject = GameObject.Find("DummyPlayer");
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 時間を測るだけ
    /// </summary>
    public void Update()
    {
        if (m_canUseWarp == false)
        {
            m_time += Time.deltaTime;
            if (m_time > 2)
            {
                m_time = 0;
                m_canUseWarp = true;
                m_playerObject.GetComponent<MyPlayer>().enabled = true;
            }
        }
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// ワープ処理
    /// </summary>
    public void Warp(string warppointName, GameObject player)
    {
        if (m_canUseWarp == true)
        {
            if (warppointName == warpA.name)
            {
                player.transform.position = warpA_.transform.position;
            }
            else if (warppointName == warpA_.name)
            {
                player.transform.position = warpA.transform.position;
            }
            else if (warppointName == warpB.name)
            {
                player.transform.position = warpB_.transform.position;
            }
            else if (warppointName == warpB_.name)
            {
                player.transform.position = warpB.transform.position;
            }
            else if (warppointName == warpC.name)
            {
                player.transform.position = warpC_.transform.position;
            }
            else if (warppointName == warpC_.name)
            {
                player.transform.position = warpC.transform.position;
            }
            else if (warppointName == warpD.name)
            {
                player.transform.position = warpD_.transform.position;
            }
            else if (warppointName == warpD_.name)
            {
                player.transform.position = warpD.transform.position;
            }
            else if (warppointName == warpF.name)
            {
                player.transform.position = warpF_.transform.position;
            }
            else if (warppointName == warpF_.name)
            {
                player.transform.position = warpF.transform.position;
            }
            else if (warppointName == warpE1.name)
            {
                player.transform.position = warpE2.transform.position;
                warpE2.SetActive(false);
            }
            else if (warppointName == warpE2.name)
            {
                player.transform.position = warpE3.transform.position;
            }
            else if (warppointName == warpE3.name)
            {
                player.transform.position = warpE1.transform.position;
            }
            else if (warppointName == warpG1.name)
            {
                player.transform.position = warpG2.transform.position;
                warpG2.SetActive(false);
            }
            else if (warppointName == warpG2.name)
            {
                player.transform.position = warpG3.transform.position;
            }
            else if (warppointName == warpG3.name)
            {
                player.transform.position = warpG1.transform.position;
            }
            else if (warppointName == warpH1.name)
            {
                player.transform.position = warpH2.transform.position;
                warpH2.SetActive(false);
            }
            else if (warppointName == warpH2.name)
            {
                player.transform.position = warpH1.transform.position;
            }
            m_canUseWarp = false;
            myTransition.StartTransition();
            player.GetComponent<MyPlayer>().enabled = false;
        }
    }
}
