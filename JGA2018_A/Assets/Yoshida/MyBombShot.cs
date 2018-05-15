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
/// 爆弾発射用
/// </summary>
public class MyBombShot : MonoBehaviour {

    // 爆弾prefab//
    public GameObject bomb;
    // 発射点//
    private Transform throwPoint;
    // 弾の速度//
    public float bombSpeed = 500;
    //プレイヤーのオブジェクト//
    private GameObject playerObjct;
    //プレイヤーの名前//
    private string playerObjctName = "DummyPlayer";

    // Use this for initialization
    void Start () {
        playerObjct = GameObject.Find(playerObjctName);
        throwPoint = this.gameObject.transform;
    }
	
	// Update is called once per frame
	void Update () {
        //常にプレイヤーの方向を向く//
        this.transform.LookAt(new Vector3(playerObjct.transform.position.x, 0, playerObjct.transform.position.z));
    }

    /// <summary>
    /// MyVirusMinisterAIから実行
    /// </summary>
    public void Shot()
    {
        GameObject bombs = GameObject.Instantiate(bomb) as GameObject;
        Vector3 force;
        force = this.gameObject.transform.forward * bombSpeed+ this.gameObject.transform.up * bombSpeed/2;
        bombs.GetComponent<Rigidbody>().AddForce(force);
        bombs.transform.position = throwPoint.position;
        Debug.Log("爆弾投げ!");
    }
}
