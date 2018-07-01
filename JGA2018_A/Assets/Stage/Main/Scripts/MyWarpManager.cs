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
    /// ワープのオブジェクト「unity上で設定すること」
    /// </summary>
    public GameObject warpA;
    public GameObject warpA_;
    public GameObject warpB;
    public GameObject warpB_;
    public GameObject warpC;
    public GameObject warpC_;
    public GameObject warpD;
    public GameObject warpD_;
    public GameObject warpE1;
    public GameObject warpE2;
    public GameObject warpE3;
    public GameObject warpF;
    public GameObject warpF_;
    public GameObject warpG1;
    public GameObject warpG2;
    public GameObject warpG3;
    public GameObject warpH1;
    public GameObject warpH2;

    /// <summary>
    /// 時間計測用
    /// </summary>
    int m_time =0;


    /// <summary>
    /// ワープしよう可能状態
    /// </summary>
    bool m_canUseWarp;


    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 時間を測るだけ
    /// </summary>
    public void Update()
    {
        if (m_canUseWarp == false)
        {
            m_time++;
            if (m_time > 120)
            {
                m_time = 0;
                m_canUseWarp = true;
            }
        }
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// ワープ処理
    /// </summary>
    public void Warp(string warppointName,GameObject player)
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
        }
    }
}
