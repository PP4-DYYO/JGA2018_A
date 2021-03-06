﻿////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/5/22～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;

using UnityEngine;

///<summary>
///ミラー大臣のAI
///</summary>
public class MyMirrorMinisterAI : MyAiBoss
{

	/// <summary>
	/// ドッペルゲンガーのゲームオブジェクト
	/// </summary>
	GameObject Doppelganger;

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 初期状態設定
	/// </summary>
	protected override void Start()
	{
		m_attackNum = 0;
		m_myObjectName = this.gameObject.name;
		m_maskPositionObject = transform.FindChild(MaskPositionObjectName).gameObject;
		m_maxHitPoint = 400;
		if (m_myObjectName == "MirrorMinister(Clone)(Clone)")
		{
			m_maxHitPoint = (m_maxHitPoint * 2) / 3;
		}
		m_attack = 10;
		if (m_myObjectName == "MirrorMinister(Clone)(Clone)")
		{
			m_attack = (m_attack * 2) / 3;
		}
		m_perceivedRange = 5;
		m_distance = 30;
		m_isAttacked = false;
		m_attackInterval = 0.5f;
		m_step = 0.06f;
		m_moveX = 0;
		m_moveZ = 0;
		m_movingX = false;
		m_movingZ = false;
		m_specialAttackLimit = 1;
		if (m_myObjectName == "MirrorMinister(Clone)(Clone)")
		{
			m_specialAttackLimit = 0;
		}
		m_specialAttackCount = 0;
		m_playerAttacked = false;
		m_aimode = AIMode.WAIT;
		if (m_myObjectName == "MirrorMinister(Clone)(Clone)")
		{
			m_aimode = AIMode.IDLE;
		}
		if (m_myObjectName == "MirrorMinister(Clone)(Clone)")
		{
			m_isFakeBody = true;
		}

		base.Start();
	}


	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 移動、行動
	/// </summary>
	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		if (m_aimode != AIMode.WAIT)
		{
			//距離が0.5より小さければ離れる
			if (m_distance < 0.5)
			{

				//ATTACK_INTERVALまで到達していれば攻撃する
				if (m_gameTime >= m_attackInterval)
				{
					m_aimode = AIMode.ATTACK;
				}
				else
				{
					m_aimode = AIMode.LEAVE;
				}
			}
			//距離が2より小さければ攻撃継続
			else if (m_distance < 1)
			{
				//ATTACK_INTERVALまで到達していれば攻撃する
				if (m_gameTime >= m_attackInterval)
				{
					m_aimode = AIMode.ATTACK;
				}
				else
				{
					m_aimode = AIMode.IDLE;
				}
			}
			else
			{
				//それ以上離れると近づく
				m_aimode = AIMode.APPROACH;
			}
		}

		//状態によって行動を切り替える
		switch (m_aimode)
		{
			case AIMode.IDLE:
				break;
			case AIMode.ATTACK:
				//一定時間毎に攻撃をする
				NomalAttack();
				break;
			case AIMode.APPROACH:
				//近づく(y座標は固定)            
				this.transform.position = Vector3.MoveTowards(
				   new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z),
				   new Vector3(myPlayer.transform.position.x, this.transform.position.y, myPlayer.transform.position.z),
				   m_step);
				break;
			case AIMode.LEAVE:
				//離れる            
				this.transform.position = Vector3.MoveTowards(
								  new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z),
								  new Vector3(myPlayer.transform.position.x, this.transform.position.y, myPlayer.transform.position.z),
								  -m_step); break;
		}
	}
	public override void ReceiveDamage(int damage)
	{
		base.ReceiveDamage(damage);

		//特殊技
		if (m_hitPoint < (m_maxHitPoint * 3) / 4 && m_specialAttackCount < m_specialAttackLimit)
		{
			Doppelganger = Instantiate(gameObject) as GameObject;
			Doppelganger.transform.parent = myAiManager.transform;
			Doppelganger.transform.position = new Vector3(gameObject.transform.position.x + 3f, gameObject.transform.position.y, gameObject.transform.position.z + 3f);

			m_specialAttackCount += 1;

			//SEの再生
			MySoundManager.Instance.Play(SeCollection.Divide, true, transform.position.x, transform.position.y, transform.position.z);
		}

		if (m_hitPoint <= 0)
		{
			Destroy(Doppelganger);
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ダメージを受ける音
	/// </summary>
	protected override void SoundToBeDamaged()
	{
		MySoundManager.Instance.Play(SeCollection.MirrorMinisterIsDamaged, true, transform.position.x, transform.position.y, transform.position.z);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 死ぬ音
	/// </summary>
	protected override void SoundToDie()
	{
		//SEの再生
		MySoundManager.Instance.Play(SeCollection.MirrorMinisterDied, true, transform.position.x, transform.position.y, transform.position.z);
	}
}
