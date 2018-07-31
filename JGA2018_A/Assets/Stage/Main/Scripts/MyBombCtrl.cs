////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/5/14～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using UnityEngine;

/// <summary>
/// エフェクト&ダメージ発生用
/// </summary>
public class MyBombCtrl : MonoBehaviour
{
	/// <summary>
	// 爆発エフェクト//
	/// </summary>
	public GameObject m_bombEffect;

	/// <summary>
	// 発生点//
	/// </summary>
	Transform m_effectPoint;

	/// <summary>
	/// 爆発点
	/// </summary>
	const string m_BombPoint = "BombPoint";
	public Vector3 explosionPoint;

	/// <summary>
	/// それぞれのスクリプト
	/// </summary>
	MyBombShot myBombShot;
	MyAttackManager myAttackManager;
	MyCharacter myCharacter;
	MyAiBoss myAiBoss;


	/// <summary>
	/// 爆弾の力（当たり判定調整用）
	/// </summary>
	int m_bombPower = 1;

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// スクリプト取得
	/// </summary>
	private void Start()
	{
		myAiBoss = GameObject.Find("VirusMinister(Clone)").GetComponent<MyAiBoss>();
        if (myAiBoss == null)
        {
            myAiBoss = GameObject.Find("VirusMinister").GetComponent<MyAiBoss>();
        }
		myBombShot = GameObject.Find(m_BombPoint).GetComponent<MyBombShot>();
		myAttackManager = GameObject.Find("AttackManager").GetComponent<MyAttackManager>();
		myCharacter = myAiBoss.PlayerObject.GetComponent<MyCharacter>();
		m_effectPoint = this.gameObject.transform;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 着弾点がエフェクトの発生点
	/// </summary>
	void Update()
	{
		m_effectPoint = this.gameObject.transform;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 当たった時に爆発
	/// </summary>
	private void OnCollisionEnter(Collision other)
	{
		if (!myAiBoss)
			return;

		if (other.gameObject.name == myAiBoss.PlayerObjectName)
		{
			Explosion();
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 爆発本体
	/// </summary>
	public void Explosion()
	{
		//エフェクトプレファブ
		GameObject bombEffect = GameObject.Instantiate(m_bombEffect) as GameObject;
		Debug.Log(m_effectPoint);
		bombEffect.transform.position = m_effectPoint.position;

		//当たり判定Cube
		explosionPoint = this.gameObject.transform.position;

		//頂点の位置
		Vector3 vLDB = new Vector3(explosionPoint.x - m_bombPower, explosionPoint.y - m_bombPower, explosionPoint.z - m_bombPower);
		Vector3 vLDF = new Vector3(explosionPoint.x - m_bombPower, explosionPoint.y - m_bombPower, explosionPoint.z + m_bombPower);
		Vector3 vLUB = new Vector3(explosionPoint.x - m_bombPower, explosionPoint.y + m_bombPower, explosionPoint.z - m_bombPower);
		Vector3 vLUF = new Vector3(explosionPoint.x - m_bombPower, explosionPoint.y + m_bombPower, explosionPoint.z + m_bombPower);
		Vector3 vRDB = new Vector3(explosionPoint.x + m_bombPower, explosionPoint.y - m_bombPower, explosionPoint.z - m_bombPower);
		Vector3 vRDF = new Vector3(explosionPoint.x + m_bombPower, explosionPoint.y - m_bombPower, explosionPoint.z + m_bombPower);
		Vector3 vRUB = new Vector3(explosionPoint.x + m_bombPower, explosionPoint.y + m_bombPower, explosionPoint.z - m_bombPower);
		Vector3 vRUF = new Vector3(explosionPoint.x + m_bombPower, explosionPoint.y + m_bombPower, explosionPoint.z + m_bombPower);

		//当たり判定発生
		MyCube explosionRange = new MyCube(vLDB, vRDB, vLDF, vRDF, vLUB, vRUB, vLUF, vRUF);
		myAttackManager.EnemyAttack(explosionRange, MaskAttribute.Non, myAiBoss.Attack, 1);

		//爆弾削除
		Destroy(this.gameObject);
	}
}
