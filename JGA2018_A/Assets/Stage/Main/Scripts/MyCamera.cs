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
[RequireComponent(typeof(Camera))]
public class MyCamera : MonoBehaviour
{
    /// <summary>
    /// ゲーム
    /// </summary>
    [SerializeField]
    MyGame myGame;
    public MyGame GameScript
    {
        get { return myGame; }
    }

    /// <summary>
    /// カメラの対象
    /// </summary>
    MyPlayer m_player;

    [Header("カメラとプレイヤーの距離")]
    /// <summary>
    /// カメラとプレイヤーとの距離[m]
    /// </summary>
    [SerializeField]
    float m_distanceToPlayerM = 2f;

    [Header("注視点の高さ")]
    /// <summary>
    /// 注視点の高さ[m]
    /// </summary>
    [SerializeField]
    float m_heightM;

    [Header("カメラ感度")]
    /// <summary>
    /// 感度
    /// </summary>
    [SerializeField]
    float m_rotationSensitivity;

    /// <summary>
    /// Xの回転量
    /// </summary>
    float m_rotX;

    /// <summary>
    /// Yの回転量
    /// </summary>
    float m_rotY;

    /// <summary>
    /// プレイヤーの中心位置
    /// </summary>
    Vector3 m_lookAt;

    /// <summary>
    /// Rayの方向
    /// </summary>
    Vector3 m_rayDirection;

    /// <summary>
    /// Rayの作成
    /// </summary>
    Ray m_ray;

    /// <summary>
    /// Rayが衝突したコライダーの情報を得る
    /// </summary>
    RaycastHit m_hit;

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 初期
    /// </summary>
    void Start()
    {
        //対象の設定
        m_player = myGame.CharacterScript.PlayerScript;
        if (m_player == null)
        {
            Debug.LogError("ターゲットが設定されていない");
            Application.Quit();
        }

    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 定期フレーム
    /// </summary>
    void FixedUpdate()
    {
        m_rotX = Input.GetAxis("HorizontalR") * Time.deltaTime * m_rotationSensitivity;
        m_rotY = Input.GetAxis("VerticalR") * Time.deltaTime * m_rotationSensitivity;

        m_lookAt = m_player.transform.position + Vector3.up * m_heightM;

        // 回転
        transform.RotateAround(m_lookAt, Vector3.up, m_rotX);
        // カメラがプレイヤーの真上や真下にあるときにそれ以上回転させないようにする
        if (transform.forward.y > 0.9f && m_rotY < 0)
        {
            m_rotY = 0;
        }
        if (transform.forward.y < -0.9f && m_rotY > 0)
        {
            m_rotY = 0;
        }
        transform.RotateAround(m_lookAt, transform.right, m_rotY);

        // 視点の設定
        transform.LookAt(m_lookAt);

        // カメラとプレイヤーとの間の距離を調整
        transform.position = m_lookAt - transform.forward * m_distanceToPlayerM;

        CheckWall();
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 敵とプレイヤーとの間に壁があるかを確認する
    /// </summary>
    void CheckWall()
    {
        // 自分の位置とプレイヤーの位置から向きベクトルを作成しRayに渡す
        m_rayDirection = transform.position - m_lookAt;
        m_ray = new Ray(m_lookAt, m_rayDirection);

        // Rayが衝突したかどうか
        if (Physics.Raycast(m_ray, out m_hit, m_distanceToPlayerM))
        {
            transform.position = m_hit.point;
        }
        transform.position -= m_ray.GetPoint(Camera.main.nearClipPlane) - m_lookAt;
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// カメラの位置を設定
    /// </summary>
    /// <param name="relativeLocations">相対的位置</param>
    public void SetPosition(Vector3 relativeLocations)
    {
        transform.position = m_player.transform.position + relativeLocations;
    }
}
