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

//カメラ属性を付ける
[RequireComponent(typeof(Camera))]
public class MyCamera : MonoBehaviour {
    /// <summary>
    /// ゲーム
    /// </summary>
    [SerializeField]
    MyGame myGame;
    public MyGame GameScript
    {
        get { return myGame; }
    }

    //カメラの対象
    [SerializeField]
    Transform Target;
    // カメラとプレイヤーとの距離[m]
    [SerializeField]
    float DistanceToPlayerM = 2f;
    // カメラを横にスライドさせる；プラスの時右へ，マイナスの時左へ[m]
    [SerializeField]
    float slideDistanceM = 0f;  
    // 注視点の高さ[m]
    [SerializeField]
    float HeightM = 1.2f;  
    // 感度
    [SerializeField]
    float rotationSensitivity = 100f;
    float rotX = 0.0f;
    float rotY = 0.0f;

    float duration = 3;
    Transform rayPosition;

    void Start()
    {
        if (Target == null)
        {
            Debug.LogError("ターゲットが設定されていない");
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        rotX = Input.GetAxis("HorizontalR") * Time.deltaTime * rotationSensitivity;
        rotY = Input.GetAxis("VerticalR") * Time.deltaTime * rotationSensitivity;


        var lookAt = Target.position + Vector3.up * HeightM;

        // 回転
        transform.RotateAround(lookAt, Vector3.up, rotX);
        // カメラがプレイヤーの真上や真下にあるときにそれ以上回転させないようにする
        if (transform.forward.y > 0.9f && rotY < 0)
        {
            rotY = 0;
        }
        if (transform.forward.y < -0.9f && rotY > 0)
        {
            rotY = 0;
        }
        transform.RotateAround(lookAt, transform.right, rotY);


        // カメラとプレイヤーとの間の距離を調整
        transform.position = lookAt - transform.forward * DistanceToPlayerM;

        // 視点の設定
        transform.LookAt(lookAt);

        // カメラを横にずらして中央を開ける
        transform.position = transform.position + transform.right * slideDistanceM;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        Debug.DrawRay(ray.origin, ray.direction * DistanceToPlayerM, Color.red, duration, false);

        RaycastHit hit = new RaycastHit();


    }
    private void Update()
    {
        //位置からRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Target.position);
        RaycastHit hit;

        if (Physics.Raycast(transform.position,Vector3.back, out hit)){
            //Rayが当たるオブジェクトがあった場合はそのオブジェクト名をログに表示

            Debug.Log(hit.collider.gameObject.name);
         }  
    }
}
