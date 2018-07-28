////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/7/3～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyShadowMagicMinisterAI : MonoBehaviour {

    MyMagicMinisterAI myMagicMinisterAI;
	// Use this for initialization
	void Start () {
        myMagicMinisterAI = GameObject.Find("MagicMinister").GetComponent<MyMagicMinisterAI>();
        MyMagicMinisterAI.s_shadowCount = 1;
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 消えるとき
    /// </summary>
    void Dead()
    {
        MyMagicMinisterAI.s_shadowCount = 0;
        //解除状態へ移行させる;
    }
}
