////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/5月/8日～
//製作者　京都コンピュータ学院京都駅前校　ゲーム学科　四回生　中村智哉
//協力者　京都コンピュータ学院京都駅前校　ゲーム学科　四回生　奥田裕也
//
////////////////////////////////////////////////////////////////////////////////////////////////////

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
	MyPlayer m_target;

	[Header("カメラとプレイヤーの距離")]
	/// <summary>
	/// カメラとプレイヤーとの距離[m]
	/// </summary>
	[SerializeField]
	float m_distanceToPlayer;

	[Header("注視点の高さ")]
	/// <summary>
	/// 注視点の高さ[m]
	/// </summary>
	[SerializeField]
	float m_heightToWatch;

	[Header("回転感度")]
	/// <summary>
	/// 回転感度
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
	Vector3 m_playerCenterPos;

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

	/// <summary>
	/// 反転描画
	/// </summary>
	bool m_isInvertedDrawing;
	public bool IsInvertedDrawing
	{
		get { return m_isInvertedDrawing; }
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 初期
	/// </summary>
	void Start()
	{
		//対象の設定
		m_target = myGame.CharacterScript.PlayerScript;
		if (m_target == null)
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
		//カメラの位置リセット
		if (Input.GetKey(KeyCode.Joystick1Button9))
			SetPosition(-m_target.transform.forward + Vector3.up * 1.75f);

		//カメラの回転量
		m_rotX = Input.GetAxis("HorizontalR") * Time.deltaTime * m_rotationSensitivity;
		m_rotY = Input.GetAxis("VerticalR") * Time.deltaTime * m_rotationSensitivity;

		//プレイヤーの中心位置
		m_playerCenterPos = m_target.transform.position + Vector3.up * m_heightToWatch;

		//Y回転
		transform.RotateAround(m_playerCenterPos, Vector3.up, m_rotX);

		//X回転
		//カメラがプレイヤーの真上や真下にあるときにそれ以上回転させないようにする
		if (transform.forward.y > 0.9f && m_rotY < 0)
		{
			m_rotY = 0;
		}
		if (transform.forward.y < -0.9f && m_rotY > 0)
		{
			m_rotY = 0;
		}
		transform.RotateAround(m_playerCenterPos, transform.right, m_rotY);

		// カメラとプレイヤーとの間の距離を調整
		transform.position = m_playerCenterPos - transform.forward * m_distanceToPlayer;

		// 視点の設定
		transform.LookAt(m_playerCenterPos);

		//壁のチェック
		CheckWall();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 敵とプレイヤーとの間に壁があるかを確認する
	/// </summary>
	void CheckWall()
	{
		// 自分の位置とプレイヤーの位置から向きベクトルを作成しRayに渡す
		m_rayDirection = transform.position - m_playerCenterPos;
		m_ray = new Ray(m_playerCenterPos, m_rayDirection);

		// Rayが衝突したかどうか
		if (Physics.Raycast(m_ray, out m_hit, m_distanceToPlayer))
		{
			//触れることが可
			if (!m_hit.collider.isTrigger)
				transform.position = m_hit.point;
		}
		transform.position -= m_ray.GetPoint(Camera.main.nearClipPlane) - m_playerCenterPos;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 描画前
	/// </summary>
	void OnPreRender()
	{
		//反転描画対策
		GL.invertCulling = m_isInvertedDrawing;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 描画後
	/// </summary>
	void OnPostRender()
	{
		//反転処理を元に戻す
		GL.invertCulling = false;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// カメラの位置を設定
	/// </summary>
	/// <param name="relativeLocations">相対的位置</param>
	public void SetPosition(Vector3 relativeLocations)
	{
		transform.position = m_target.transform.position + relativeLocations;
		transform.LookAt(m_target.transform.position + Vector3.up * m_heightToWatch);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 反転描画する
	/// </summary>
	/// <param name="isFlip">反転するか</param>
	public void InvertDrawing(bool isFlip)
	{
		//既に変更済み
		if (m_isInvertedDrawing == isFlip)
			return;

		m_isInvertedDrawing = isFlip;

		//反転
		if (!m_isInvertedDrawing)
			RestoreCameraDisplay();
		else
			InvertCameraDisplay();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// カメラ表示を元に戻す
	/// </summary>
	void RestoreCameraDisplay()
	{
		var mat = Camera.main.projectionMatrix * Matrix4x4.Scale(new Vector3(-1, 1, 1));
		Camera.main.ResetWorldToCameraMatrix();
		Camera.main.ResetProjectionMatrix();
		Camera.main.projectionMatrix = mat;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// カメラ表示を反転にする
	/// </summary>
	void InvertCameraDisplay()
	{
		var mat = Camera.main.projectionMatrix * Matrix4x4.Scale(new Vector3(-1, 1, 1));
		Camera.main.ResetWorldToCameraMatrix();
		Camera.main.ResetProjectionMatrix();
		Camera.main.projectionMatrix = mat;
	}
}
