////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/5/14～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using UnityEngine;



/// <summary>
/// 自動消滅用
/// </summary>
public class MyAutoDestroyer : MonoBehaviour
{
    MyBombCtrl mybombctrl;
    float m_time;

    void Start()
    {
        mybombctrl = this.GetComponent<MyBombCtrl>();
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 時間経過で削除
    /// </summary>
    void Update ()
    {
        m_time += Time.deltaTime;
        
        if (m_time > 2)
        {
            Destroy(this.gameObject);
        }

        //爆弾は１.5秒
        if (this.gameObject.CompareTag("Bomb"))
        {
            if (m_time > 1.5)
            {
                mybombctrl.Explosion();
            }
        }
	}
}
