//----------------------------------------------------------------------------------------------------
//
//2018年5月8日～
//作成者　京都コンピュータ学院京都駅前校　ゲーム学科　4回生　奥田裕也
//編集者
//
//----------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤークラス
/// </summary>
public class MyPlayer : MonoBehaviour
{
	/// <summary>
	/// キャラクター
	/// </summary>
	[SerializeField]
	MyCharactor myCharactor;

	/// <summary>
	/// カメラ
	/// </summary>
	Camera m_camera;

	/// <summary>
	/// アニメータ
	/// </summary>
	[SerializeField]
	Animator m_anim;

	#region アニメーション遷移
	/// <summary>
	/// 無操作遷移
	/// </summary>
	const string TRANS_IDLE = "Idle";

	/// <summary>
	/// 歩く遷移
	/// </summary>
	const string TRANS_WALK = "Walk";
	#endregion

	#region 状態
	/// <summary>
	/// 状態
	/// </summary>
	enum Status
	{
		/// <summary>
		/// 無操作
		/// </summary>
		Idle,
		/// <summary>
		/// 徒歩
		/// </summary>
		Walk,
	}

	/// <summary>
	/// 現在の状態
	/// </summary>
	Status m_state;

	/// <summary>
	/// フレーム前の状態
	/// </summary>
	Status m_statePrev;
	#endregion

	/// <summary>
	/// フレーム前の位置
	/// </summary>
	Vector3 m_posPrev;

	/// <summary>
	/// 移動量
	/// </summary>
	Vector3 m_direction;

	/// <summary>
	/// 角度
	/// </summary>
	float m_angle;

	/// <summary>
	/// １周の角度
	/// </summary>
	const int ONE_TURNING_ANGLE = 360;

	/// <summary>
	/// 半周の角度
	/// </summary>
	const int HALF_CIRCUMFERENCE_ANGLE = 180;

	/// <summary>
	/// 補正角度
	/// </summary>
	const int CORRECTION_ANGLE = 5;

	/// <summary>
	/// 速度
	/// </summary>
	float m_speed;

	/// <summary>
	/// 徒歩速度
	/// </summary>
	[SerializeField]
	float m_walkSpeed;

	/// <summary>
	/// 回転スピード
	/// </summary>
	[SerializeField]
	float m_rotationSpeed;

	/// <summary>
	/// 作業用のVector３
	/// </summary>
	Vector3 m_workVector3;

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 初期
	/// </summary>
	void Start()
	{
		//アクセスしやすいように
		m_camera = myCharactor.GameScript.CameraScript;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 定期フレーム
	/// </summary>
	void FixedUpdate()
	{
		//速度の決定
		Speed();

		//移動
		Move();

		//アニメーション
		Animation();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 速度
	/// </summary>
	void Speed()
	{
		m_speed = m_walkSpeed;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 移動
	/// </summary>
	void Move()
	{
		m_posPrev = transform.position;

		//カメラの向きに対応した移動
		transform.position +=
			(Vector3.Scale(m_camera.transform.forward, (Vector3.right + Vector3.forward)) * Input.GetAxis("Vertical")
			+ m_camera.transform.right * Input.GetAxis("Horizontal")).normalized * m_speed * Time.deltaTime;

		//移動に伴う回転
		Rotation();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 回転
	/// </summary>
	void Rotation()
	{
		//移動量
		m_direction = Vector3.Scale((transform.position - m_posPrev), (Vector3.right + Vector3.forward)).normalized;

		//移動なし
		if (m_direction == Vector3.zero)
			return;

		//移動量から向くべき角度を算出
		m_angle = Mathf.Asin(m_direction.x) / Mathf.PI * HALF_CIRCUMFERENCE_ANGLE;
		if (m_direction.z < 0)
			m_angle = HALF_CIRCUMFERENCE_ANGLE - m_angle;

		//マイナス角度の修正
		if (m_angle < 0)
			m_angle += ONE_TURNING_ANGLE;

		m_workVector3 = transform.eulerAngles;

		//回転したい角度が小さい
		if (Mathf.Abs(m_workVector3.y - m_angle) <= CORRECTION_ANGLE)
		{
			//向きたい方向に向く
			m_workVector3.y = m_angle;
		}
		else
		{
			//徐々に向きたい方向に向く
			m_angle -= m_workVector3.y;
			m_angle *= (Mathf.Abs(m_angle) < HALF_CIRCUMFERENCE_ANGLE) ? Time.deltaTime * m_rotationSpeed : -Time.deltaTime * m_rotationSpeed;
			m_workVector3.y += m_angle;
		}

		transform.eulerAngles = m_workVector3;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// アニメーション関係
	/// </summary>
	void Animation()
	{
		//状態を調べる
		CheckState();

		//状態変化なし
		if (m_state == m_statePrev)
			return;

		//アニメーションを変化
		switch (m_state)
		{
			case Status.Walk:
				m_anim.SetTrigger(TRANS_WALK);
				break;
			case Status.Idle:
				m_anim.SetTrigger(TRANS_IDLE);
				break;
		}

		m_statePrev = m_state;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 状態を調べる
	/// </summary>
	void CheckState()
	{
		//移動量あり
		if (m_direction.sqrMagnitude > 0)
			m_state = Status.Walk;
		else
			m_state = Status.Idle;
	}
}
