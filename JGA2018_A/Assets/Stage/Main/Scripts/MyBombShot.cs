//////////////////////////////////////////////////////////////////
//
//2018/5/14～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
//////////////////////////////////////////////////////////////////

using System;
using UnityEngine;

/// <summary>
/// 爆弾発射用
/// </summary>
public class MyBombShot : MonoBehaviour
{

    /// <summary>
    // 爆弾prefab//
    /// </summary>
    public GameObject m_bomb;

    /// <summary>
    // 発射点//
    /// </summary>
    Transform m_throwPoint;

    /// <summary>
    // 弾の速度//
    /// </summary>
    const float BOMBSPEED = 500;

    GameObject m_playerObject;


    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 初期状態設定
    /// </summary>
    void Start()
    {
        m_throwPoint = this.gameObject.transform;
        m_playerObject = GameObject.Find("DummyPlayer");
    }


    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 0が近距離、1が遠距離MyVirusMinisterAIから実行、爆弾の発射
    /// </summary>
    public void Shot(int num)
    {
        //発射点を少しプレイヤー側へ前に出る
        transform.Translate(new Vector3(0, 0, 0.5f));

        if (num == 0)
        {
            GameObject bombs = GameObject.Instantiate(m_bomb) as GameObject;
            Vector3 force;
            //力は横だけ、ややランダム性あり
            int m_random = UnityEngine.Random.Range(0, 100);
            force = this.gameObject.transform.forward * ((2 * BOMBSPEED + m_random) / 5);
            bombs.GetComponent<Rigidbody>().AddForce(force);
            bombs.transform.position = m_throwPoint.position;
            Debug.Log("爆弾転がし!");
        }

        else if (num == 1)
        {
            GameObject bombs = GameObject.Instantiate(m_bomb) as GameObject;
            Vector3 force;
            //力は斜め上に、ややランダム性あり
            int m_randomX = UnityEngine.Random.Range(0, 500);
            int m_randomY = UnityEngine.Random.Range(0, 100);
            force = this.gameObject.transform.forward * ((2 * BOMBSPEED + m_randomX) / 5) + this.gameObject.transform.up * ((BOMBSPEED + m_randomY) / 2);
            bombs.GetComponent<Rigidbody>().AddForce(force);
            bombs.transform.position = m_throwPoint.position;
            Debug.Log("爆弾投げ!");
        }
        else if (num == 2)
        {
            GameObject bombs = GameObject.Instantiate(m_bomb) as GameObject;
            GameObject bombsTwo = GameObject.Instantiate(m_bomb) as GameObject;
            GameObject bombsThree = GameObject.Instantiate(m_bomb) as GameObject;
            Vector3 force;
            //力は斜め上に、ややランダム性あり
            int m_random = UnityEngine.Random.Range(0, 100);
            force = this.gameObject.transform.forward * ((2 * BOMBSPEED + m_random) / 5); bombs.GetComponent<Rigidbody>().AddForce(force);
            bombsTwo.GetComponent<Rigidbody>().AddForce(force);
            bombsThree.GetComponent<Rigidbody>().AddForce(force);
            bombs.transform.position = m_throwPoint.position;
            bombsTwo.transform.position = m_throwPoint.position;
            bombsThree.transform.position = m_throwPoint.position;
        }
        //発射点を戻す
        this.transform.Translate(new Vector3(0, 0, -0.5f));
    }
}
