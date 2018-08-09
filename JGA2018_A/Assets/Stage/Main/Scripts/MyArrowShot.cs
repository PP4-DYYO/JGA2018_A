////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/5/21～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
////////////////////////////////////////////////////////////////////////////////////////////////////

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

    [SerializeField]
    MyCarryMinisterAI myCarryMinisterAI;

    /// <summary>
    // 発射点//
    /// </summary>
    Transform m_throwPoint;

    /// <summary>
    // 弾の速度//
    /// </summary>
    const float ARROWSPEED = 250;


    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 初期状態設定
    /// </summary>
    void Start()
    {
        m_throwPoint = this.gameObject.transform;
    }


    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// MyCarryMinisterAIから実行、矢の発射
    /// </summary>
    public void Shot(int num)
    {
        transform.Translate(new Vector3(0, 0, 0.5f));
        //遠距離
        if (num == 1)
        {
            Debug.Log(myCarryMinisterAI);

            GameObject arrows = Instantiate(m_arrow) as GameObject;
            arrows.transform.parent = myCarryMinisterAI.AttackManagerScript.transform;
            
            arrows.transform.position = gameObject.transform.position;
            arrows.transform.rotation = gameObject.transform.rotation;
            Vector3 force;
            //力は斜め上に,ランダム性を持たせる
            float m_random = UnityEngine.Random.Range(15, 30) / 10;
            force = this.gameObject.transform.forward * (3*ARROWSPEED) + this.gameObject.transform.up *ARROWSPEED/2;
            arrows.GetComponent<Rigidbody>().AddForce(force);
        }
        //近距離
        else
        {
            GameObject arrows = Instantiate(m_arrow) as GameObject;
            arrows.transform.parent = myCarryMinisterAI.AttackManagerScript.transform;
            arrows.transform.position = gameObject.transform.position;
            arrows.transform.rotation = gameObject.transform.rotation;
            Vector3 force;
            //力は横だけ
            force = this.gameObject.transform.forward * ((2 * ARROWSPEED) / 5);
            arrows.GetComponent<Rigidbody>().AddForce(force);
        }
        transform.Translate(new Vector3(0, 0, -0.5f));
    }
}
