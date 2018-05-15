//////////////////////////////////////////////////////////////////
//
//2018/5/14～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
//////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エフェクト発生用
/// </summary>
public class MyBombCtrl : MonoBehaviour {

    // 爆発エフェクト//
    public GameObject BombEffect;
    // 発生点//
    private Transform effectPoint;
	
	// Update is called once per frame
	void Update () {
        effectPoint = this.gameObject.transform;
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject bombeffect = GameObject.Instantiate(BombEffect) as GameObject;
        bombeffect.transform.position = effectPoint.position;
        Destroy(this.gameObject);

    }
}
