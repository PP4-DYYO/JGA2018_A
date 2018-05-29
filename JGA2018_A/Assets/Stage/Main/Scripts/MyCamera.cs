////////////////////////////////////////////////////////////
//
//2018/5月/8日～
//製作者 京都コンピュータ学院 ゲーム学科 四回生 中村智哉
//
////////////////////////////////////////////////////////////



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// カメラを制御するクラス（仮）
/// </summary>
public class MyCamera : MonoBehaviour {
    [SerializeField]
    Transform m_player;//対象のプレイヤー
    [SerializeField]
    float distance = 1.2f;//対象プレイヤーからカメラを離す距離
    [SerializeField]
    Quaternion vRotation;//カメラの垂直回転
    [SerializeField]
    Quaternion hRotation;//カメラの平行回転
    [SerializeField]
    float m_ypos=1.0f;//高さ
    [SerializeField]
    Vector3 velocity;

    [SerializeField]
    float turnSpeed = 10.0f;

    void Start()
    {
        //回転の初期化
        vRotation = Quaternion.Euler(0, 0, 0);//垂直回転(x軸を軸とする回転)
        hRotation = Quaternion.identity;//水平回転(y軸を軸とする回転)は、無回転
        transform.rotation = hRotation * vRotation;//最終的なカメラの回転は垂直回転してから水平回転する合成回転

        //位置の初期化
        //player位置から距離distanceだけ手前に引いた位置を設定する
        transform.position = m_player.position - transform.rotation * Vector3.forward * distance;

    }
    void LateUpdate()
    {
        //水平カメラの更新
        hRotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * turnSpeed, 0);
        //カメラの回転(transform.rotation)の更新
        //方法１：垂直回転してから水平回転する合成回転とする
        transform.rotation = vRotation * hRotation;

            //カメラの位置(transform.position)の更新
            //player位置から距離distanceだけ手前に引いた位置を設定
            transform.position = m_player.position - transform.rotation * Vector3.forward * distance;
        transform.position = new Vector3(transform.position.x, m_player.transform.position.y + m_ypos, transform.position.z);

        if (velocity.magnitude > 0)
        {
            //プレイヤーの回転の更新(transform.rotation)の更新
            //無回転状態ののプレイヤーのZ+方向(後頭部)を、移動の反対方向(-velocity)に回す回転とします
            transform.rotation = Quaternion.LookRotation(-velocity);

            // プレイヤーの位置(transform.position)の更新
            // 移動方向ベクトル(velocity)を足し込みます
            transform.position += velocity;
        }
    }

}
