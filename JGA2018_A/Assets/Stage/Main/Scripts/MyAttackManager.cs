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

//----------------------------------------------------------------------------------------------------
//構造体
//----------------------------------------------------------------------------------------------------

/// <summary>
/// 直方体
/// </summary>
public struct MyCube
{
	/// <summary>
	/// 左下後方の頂点
	/// </summary>
	public Vector3 vLDB;

	/// <summary>
	/// 右下後方の頂点
	/// </summary>
	public Vector3 vRDB;

	/// <summary>
	/// 左下前方の頂点
	/// </summary>
	public Vector3 vLDF;

	/// <summary>
	/// 右下前方の頂点
	/// </summary>
	public Vector3 vRDF;

	/// <summary>
	/// 左上後方の頂点
	/// </summary>
	public Vector3 vLUB;

	/// <summary>
	/// 右上後方の頂点
	/// </summary>
	public Vector3 vRUB;

	/// <summary>
	/// 左上前方の頂点
	/// </summary>
	public Vector3 vLUF;

	/// <summary>
	/// 右上前方の頂点
	/// </summary>
	public Vector3 vRUF;

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 直方体の設定
	/// </summary>
	/// <param name="vertexLDB">左下後方の頂点</param>
	/// <param name="vertexRDB">右下後方の頂点</param>
	/// <param name="vertexLDF">左下前方の頂点</param>
	/// <param name="vertexRDF">右下前方の頂点</param>
	/// <param name="vertexLUB">左上後方の頂点</param>
	/// <param name="vertexRUB">右上後方の頂点</param>
	/// <param name="vertexLUF">左上前方の頂点</param>
	/// <param name="vertexRUF">右上前方の頂点</param>
	public MyCube(Vector3 vertexLDB, Vector3 vertexRDB, Vector3 vertexLDF, Vector3 vertexRDF,
		Vector3 vertexLUB, Vector3 vertexRUB, Vector3 vertexLUF, Vector3 vertexRUF)
	{
		vLDB = vertexLDB;
		vRDB = vertexRDB;
		vLDF = vertexLDF;
		vRDF = vertexRDF;
		vLUB = vertexLUB;
		vRUB = vertexRUB;
		vLUF = vertexLUF;
		vRUF = vertexRUF;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 直方体の設定
	/// </summary>
	/// <param name="vertexLDB">左下後方の頂点</param>
	/// <param name="vertexRDB">右下後方の頂点</param>
	/// <param name="vertexLDF">左下前方の頂点</param>
	/// <param name="vertexRDF">右下前方の頂点</param>
	/// <param name="vertexLUB">左上後方の頂点</param>
	/// <param name="vertexRUB">右上後方の頂点</param>
	/// <param name="vertexLUF">左上前方の頂点</param>
	/// <param name="vertexRUF">右上前方の頂点</param>
	public void SetCube(Vector3 vertexLDB, Vector3 vertexRDB, Vector3 vertexLDF, Vector3 vertexRDF,
		Vector3 vertexLUB, Vector3 vertexRUB, Vector3 vertexLUF, Vector3 vertexRUF)
	{
		vLDB = vertexLDB;
		vRDB = vertexRDB;
		vLDF = vertexLDF;
		vRDF = vertexRDF;
		vLUB = vertexLUB;
		vRUB = vertexRUB;
		vLUF = vertexLUF;
		vRUF = vertexRUF;
	}
}

//----------------------------------------------------------------------------------------------------
//クラス
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------
/// <summary>
/// 攻撃マネージャ
/// </summary>
public class MyAttackManager : MonoBehaviour
{
	/// <summary>
	/// キャラクター
	/// </summary>
	[SerializeField]
	MyCharactor myCharactor;

	/// <summary>
	/// プレイヤーの攻撃範囲
	/// </summary>
	[SerializeField]
	GameObject PlayerAttackRange;

	/// <summary>
	/// 敵の攻撃範囲
	/// </summary>
	[SerializeField]
	GameObject EnemyAttackRange;

	/// <summary>
	/// デバッグモード
	/// </summary>
	[SerializeField]
	bool m_isDebug;

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// フレームメソッド
	/// </summary>
	void Update()
	{
		//デバッグモードで表示する
		if (m_isDebug)
		{
			//子供にアクセス
			foreach (Transform child in transform)
			{
				child.GetComponent<MeshRenderer>().enabled = true;
			}
		}
		else
		{
			//子供にアクセス
			foreach (Transform child in transform)
			{
				child.GetComponent<MeshRenderer>().enabled = false;
			}
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// プレイヤーの攻撃
	/// </summary>
	/// <param name="attackCube">攻撃用の直方体</param>
	/// <param name="time">攻撃時間</param>
	public void PlayerAttack(MyCube attackCube, float time = -1)
	{
		//攻撃範囲の生成
		var attackRange = Instantiate(PlayerAttackRange, transform);

		//攻撃範囲の調整
		AdjustAttackRange(attackRange, attackCube);

		//攻撃時間
		if (time >= 0)
			Destroy(attackRange, time);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 攻撃範囲の調整
	/// </summary>
	/// <param name="attackRange">攻撃範囲</param>
	/// <param name="attackCube">攻撃用の直方体</param>
	void AdjustAttackRange(GameObject attackRange, MyCube attackCube)
	{
		var mf = attackRange.GetComponent<MeshFilter>();

		//頂点の変更
		var vertex = mf.mesh.vertices;
		for (int i = 0; i < vertex.Length; i++)
		{
			//頂点番号に対応する直方体の頂点を代入
			switch (i)
			{
				case 0:
				case 13:
				case 23:
					//右下前方
					vertex[i] = attackCube.vRDF;
					break;
				case 1:
				case 14:
				case 16:
					//左下前方
					vertex[i] = attackCube.vLDF;
					break;
				case 2:
				case 8:
				case 22:
					//右上前方
					vertex[i] = attackCube.vRUF;
					break;
				case 3:
				case 9:
				case 17:
					//左上前方
					vertex[i] = attackCube.vLUF;
					break;
				case 4:
				case 10:
				case 21:
					//右上後方
					vertex[i] = attackCube.vRUB;
					break;
				case 5:
				case 11:
				case 18:
					//左上後方
					vertex[i] = attackCube.vLUB;
					break;
				case 6:
				case 12:
				case 20:
					//右下後方
					vertex[i] = attackCube.vRDB;
					break;
				case 7:
				case 15:
				case 19:
					//左下後方
					vertex[i] = attackCube.vLDB;
					break;
			}
		}

		//変更内容の反映
		mf.mesh.vertices = vertex;
		mf.mesh.RecalculateBounds(); //メッシュコンポーネントのプロパティboundsを再計算する

		//編集した頂点で当たり判定を作る
		var mc = attackRange.AddComponent<MeshCollider>();
		mc.convex = true;
		mc.isTrigger = true;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 敵の攻撃
	/// </summary>
	/// <param name="attackCube">攻撃用の直方体</param>
	/// <param name="time">攻撃時間</param>
	public void EnemyAttack(MyCube attackCube, float time = -1)
	{
		//攻撃範囲の生成
		var attackRange = Instantiate(EnemyAttackRange, transform);

		//攻撃範囲の調整
		AdjustAttackRange(attackRange, attackCube);

		//攻撃時間
		if (time >= 0)
			Destroy(attackRange, time);
	}
}
