////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/6/24～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class MyWarpPoint : MonoBehaviour
{
    /// <summary>
    /// スクリプト
    /// </summary>
    MyWarpManager myWarpManager;

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 当たり判定
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        myWarpManager = GameObject.Find("WARP").GetComponent<MyWarpManager>();
        if (other.name == "DummyPlayer")
        {
            myWarpManager.Warp(this.gameObject.name, other.gameObject);
        }
    }

}
