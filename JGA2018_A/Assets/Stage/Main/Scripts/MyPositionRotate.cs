﻿////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/7/3～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPositionRotate : MonoBehaviour
{
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
    /// 初期状態
    /// </summary>
    void Start()
    {
        m_PlayerObjct = GameObject.Find(PLAYER_OBJECT_NAME);
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 向きの変更のみ
    /// </summary>
    void Update()
    {
        //常にプレイヤーの方向を向く//
        this.transform.LookAt(new Vector3(m_PlayerObjct.transform.position.x, 0, m_PlayerObjct.transform.position.z));
    }
}
