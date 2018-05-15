//////////////////////////////////////////////////////////////////
//
//2018/5/14～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
//////////////////////////////////////////////////////////////////

using System;
using UnityEngine;

/// <summary>
/// エフェクト発生用
/// </summary>
public class MyBombCtrl : MonoBehaviour
{

    /// <summary>
    // 爆発エフェクト//
    /// </summary>
    public GameObject m_bombEffect;

    /// <summary>
    // 発生点//
    /// </summary>
    Transform m_effectPoint;

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 着弾点がエフェクトの発生点
    /// </summary>
    void Update ()
    {
        m_effectPoint = this.gameObject.transform;
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 当たった時に爆発
    /// </summary>
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("VirusMinister"))
        {
            //なにかするかも
        }
        else if(other.gameObject.CompareTag("Player"))
        {
            Explosion();
        }
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 爆発本体
    /// </summary>
   public void Explosion()
    {
        GameObject bombeffect = GameObject.Instantiate(m_bombEffect) as GameObject;
        bombeffect.transform.position = m_effectPoint.position;
        Destroy(this.gameObject);
    }
}
