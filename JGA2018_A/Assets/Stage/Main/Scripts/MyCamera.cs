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

    /// <summary>
    /// カメラの対象
    /// </summary>
    [SerializeField]
    Transform m_target = null;
    /// <summary>
    /// 座標
    /// </summary>
    [SerializeField]
    float m_speed = 0.0f;

    float m_posx = 0.0f;
    float m_posy = 0.0f;
    float m_posz = 0.0f;

    public Transform Target
    {
        get { return m_target; }
    }

    Transform m_cameraTransform = null;
    Transform m_pivot = null;

    void Awake()
    {
        Camera camera = GetComponentInChildren<Camera>();
        Debug.AssertFormat(camera != null, "カメラがありません");
        if (camera == null)
        {
            return;
        }

        m_cameraTransform = camera.transform;
        m_pivot = m_cameraTransform.parent;
    }

    void LateUpdate()
    {
        UpdateCamera();
    }

    void UpdateCamera()
    {
        if (Target == null)
        {
            return;
        }
        Vector3 targetPos = Target.position;
        targetPos.y += 2.0f;
        targetPos.z -= 2.5f;
        transform.position = targetPos;
        float deltaSpeed = m_speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, m_speed);
    }

    [ContextMenu("ApplyTarget")]
    void ApplyForceTarget()
    {
        if (Target == null)
        {
            return;
        }
        transform.position = Target.position;
        SetCameraTransform();

        if (m_cameraTransform == null)
        {
            return;
        }
        m_cameraTransform.transform.LookAt(Target);
    }

    void SetCameraTransform()
    {
        Camera camera = GetComponentInChildren<Camera>();
        Debug.AssertFormat(camera != null, "カメラがありません2");
        if (camera == null)
        {
            return;
        }
        m_cameraTransform = camera.transform;
        m_pivot = m_cameraTransform.parent;
    }
}
