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
/// エフェクト自動消滅用
/// </summary>
public class MyAutoDestroyer : MonoBehaviour {

    int time;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time++;
        if (time > 180)
        {
            Destroy(this.gameObject);
        }
	}
}
