﻿//////////////////////////////////////////////////////////////////
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

    /// <summary>
    //プレイヤーのオブジェクト//
    /// </summary>
    GameObject m_PlayerObjct;

    /// <summary>
    //プレイヤーの名前//
    /// </summary>
    const string PLAYER_OBJECT_NAME = "DummyPlayer";

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 初期状態設定
    /// </summary>
    void Start ()
    {
        m_PlayerObjct = GameObject.Find(PLAYER_OBJECT_NAME);
        m_throwPoint = this.gameObject.transform;
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 向きの変更のみ
    /// </summary>
    void Update ()
    {
        //常にプレイヤーの方向を向く//
        this.transform.LookAt(new Vector3(m_PlayerObjct.transform.position.x, 0, m_PlayerObjct.transform.position.z));
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// MyVirusMinisterAIから実行、爆弾の発射
    /// </summary>
    public void Shot(int num)
    {
        if (num == 1)
        {
            GameObject bombs = GameObject.Instantiate(m_bomb) as GameObject;
            Vector3 force;
            //力は斜め上に、ややランダム性あり
            int m_randomX = UnityEngine.Random.Range(0, 500);
            int m_randomY = UnityEngine.Random.Range(0, 100);
            Debug.Log(m_randomX);
            force = this.gameObject.transform.forward * ((2*BOMBSPEED+m_randomX) / 5) + this.gameObject.transform.up *((BOMBSPEED+m_randomY)/ 2);
            bombs.GetComponent<Rigidbody>().AddForce(force);
            bombs.transform.position = m_throwPoint.position;
            Debug.Log("爆弾投げ!");
        }
        else
        {
            GameObject bombs = GameObject.Instantiate(m_bomb) as GameObject;
            Vector3 force;
            //力は横だけ、ややランダム性あり
            int m_random = UnityEngine.Random.Range(0, 100);
            force = this.gameObject.transform.forward * ((2*BOMBSPEED+m_random)/5)  ;
            bombs.GetComponent<Rigidbody>().AddForce(force);
            bombs.transform.position = m_throwPoint.position;
            Debug.Log("爆弾転がし!");
        }
    }
}
