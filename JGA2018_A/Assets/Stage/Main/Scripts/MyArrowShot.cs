//////////////////////////////////////////////////////////////////
//
//2018/5/21～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
//////////////////////////////////////////////////////////////////

using System;
using UnityEngine;

/// <summary>
/// 爆弾発射用
/// </summary>
public class MyArrowShot : MonoBehaviour
{

    /// <summary>
    // 矢prefab//
    /// </summary>
    public GameObject m_arrow;

    /// <summary>
    // 発射点//
    /// </summary>
    Transform m_throwPoint;

    /// <summary>
    // 弾の速度//
    /// </summary>
    const float ARROWSPEED = 500;

    /// <summary>
    // スクリプト
    /// </summary>
    MyAiBoss myAiBoss;

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 初期状態設定
    /// </summary>
    void Start()
    {
        myAiBoss = GameObject.Find("CarryMinister").GetComponent<MyAiBoss>();
        m_throwPoint = this.gameObject.transform;
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 向きの変更のみ
    /// </summary>
    void Update()
    {
        //常にプレイヤーの方向を向く//
        this.transform.LookAt(new Vector3(myAiBoss.PlayerObject.transform.position.x, 0, myAiBoss.PlayerObject.transform.position.z));
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// MyCarryMinisterAIから実行、矢の発射
    /// </summary>
    public void Shot(int num)
    {
        if (num == 1)
        {
            GameObject arrows = GameObject.Instantiate(m_arrow) as GameObject;
            Vector3 force;
            //力は斜め上に,ランダム性を持たせる
            float m_random = UnityEngine.Random.Range(15, 30) / 10;
            force = this.gameObject.transform.forward * (3*ARROWSPEED) + this.gameObject.transform.up *ARROWSPEED/2;
            arrows.GetComponent<Rigidbody>().AddForce(force);
            arrows.transform.position = m_throwPoint.position;
            Debug.Log("矢遠距離!");
        }
        else
        {
            GameObject arrows = GameObject.Instantiate(m_arrow) as GameObject;
            Vector3 force;
            //力は横だけ
            force = this.gameObject.transform.forward * ((2 * ARROWSPEED) / 5);
            arrows.GetComponent<Rigidbody>().AddForce(force);
            arrows.transform.position = m_throwPoint.position;
            Debug.Log("矢近距離!");
        }
    }
}
