using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBombPointCtrl : MonoBehaviour
{
    /// <summary>
    // スクリプト
    /// </summary>
    MyAiBoss myAiBoss;

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 設定
    /// </summary>
    void Start()
    {
        myAiBoss = GameObject.Find("VirusMinister").GetComponent<MyAiBoss>();
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
}
