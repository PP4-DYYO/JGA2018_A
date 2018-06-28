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

	/// <summary>
	/// 頂点数
	/// </summary>
	public const int NUM_VERTICES = 8;

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
/// <summary>
/// 必殺技番号
/// </summary>
enum NumDeathblow
{
	Non,
	Dethblow1,
	Dethblow2,
	Dethblow3,
	Dethblow4,
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
	#region 外部のインスタンス
	[Header("外部のインスタンス")]
	/// <summary>
	/// キャラクター
	/// </summary>
	[SerializeField]
	MyCharacter myCharacter;

	/// <summary>
	/// ボス
	/// </summary>
	GameObject m_boss;

	/// <summary>
	/// プレイヤー
	/// </summary>
	MyPlayer m_player;

	/// <summary>
	/// ステージ
	/// </summary>
	MyStage m_stage;

	/// <summary>
	/// カメラ
	/// </summary>
	MyCamera m_camera;
	#endregion

	#region 生成されるプレファブ
	[Header("生成されるプレファブ")]
	/// <summary>
	/// プレイヤーの攻撃範囲
	/// </summary>
	[SerializeField]
	GameObject PlayerAttackRange;

	/// <summary>
	/// プレイヤーの必殺技攻撃の範囲
	/// </summary>
	[SerializeField]
	GameObject PlayerAttackDeathblowRange;

	/// <summary>
	/// 敵の攻撃範囲
	/// </summary>
	[SerializeField]
	GameObject EnemyAttackRange;
	#endregion

	#region 必殺技
	[Header("必殺技")]
	/// <summary>
	/// 必殺技の時間
	/// </summary>
	[SerializeField]
	float m_deathblowTime;

	/// <summary>
	/// 必殺技でのカメラの高さ
	/// </summary>
	[SerializeField]
	float m_cameraHeightDeathblow;

	/// <summary>
	/// 必殺技が当たった時のカメラ距離
	/// </summary>
	[SerializeField]
	float m_cameraDistanceDeathblowHit;

	/// <summary>
	/// プレイヤーの状態番号
	/// </summary>
	int m_playerStateNum;

	/// <summary>
	/// フレーム前のプレイヤー状態番号
	/// </summary>
	int m_playerStateNumPrev;

	/// <summary>
	/// ボスの状態番号
	/// </summary>
	int m_bossStateNum;

	/// <summary>
	/// フレーム前のボス状態番号
	/// </summary>
	int m_bossStateNumPrev;

	/// <summary>
	/// カメラの状態番号
	/// </summary>
	int m_cameraStateNum;

	/// <summary>
	/// フレーム前のカメラ状態番号
	/// </summary>
	int m_cameraStateNumPrev;

	/// <summary>
	/// 必殺技の時間を数える
	/// </summary>
	float m_countTimeDeathblow;

	/// <summary>
	/// 必殺技番号
	/// </summary>
	NumDeathblow m_numDeathblow;
	#endregion

	#region 必殺技１
	[Header("必殺技１")]
	/// <summary>
	/// 必殺技１の攻撃を与えるプレイヤーの距離
	/// </summary>
	[SerializeField]
	float m_distancePlayerGivingDeathblow1Attack;

	/// <summary>
	/// 必殺技１の初めのカメラ角
	/// </summary>
	[SerializeField]
	float m_cameraRotationAngleBeginningDeathblow1;

	/// <summary>
	/// 必殺技１の攻撃１の時間
	/// </summary>
	[SerializeField]
	float m_deathblow1Attack1Time;

	/// <summary>
	/// 必殺技１の攻撃２の時間
	/// </summary>
	[SerializeField]
	float m_deathblow1Attack2Time;

	/// <summary>
	/// 必殺技１の攻撃３の時間
	/// </summary>
	[SerializeField]
	float m_deathblow1Attack3Time;

	/// <summary>
	/// 必殺技１の攻撃４の時間
	/// </summary>
	[SerializeField]
	float m_deathblow1Attack4Time;

	/// <summary>
	/// 必殺技１の攻撃１で吹き飛ぶ時間
	/// </summary>
	[SerializeField]
	float m_blowingTimeDeathblow1Attack1;

	/// <summary>
	/// 必殺技１の攻撃２で吹き飛ぶ時間
	/// </summary>
	[SerializeField]
	float m_blowingTimeDeathblow1Attack2;

	/// <summary>
	/// 必殺技１の攻撃３で吹き飛ぶ時間
	/// </summary>
	[SerializeField]
	float m_blowingTimeDeathblow1Attack3;

	/// <summary>
	/// 必殺技１の攻撃４で吹き飛ぶ時間
	/// </summary>
	[SerializeField]
	float m_blowingTimeDeathblow1Attack4;
	#endregion

	#region 作業用
	/// <summary>
	/// 作業用の攻撃
	/// </summary>
	MyAttack m_workAttack;

	/// <summary>
	/// 作業のVector３
	/// </summary>
	Vector3 m_workVector3;

	/// <summary>
	/// 作業用のFloat
	/// </summary>
	float m_workFloat;
	#endregion

#if DEBUG
	#region デバッグ
	[Header("デバッグ")]
	/// <summary>
	/// デバッグモード
	/// </summary>
	[SerializeField]
	bool m_isDebug;
	#endregion
#endif

	/// <summary>
	/// 初期
	/// </summary>
	void Start()
	{
		m_player = myCharacter.PlayerScript;
		m_boss = myCharacter.Boss;
		m_stage = myCharacter.GameScript.StageScript;
		m_camera = myCharacter.GameScript.CameraScript;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// フレームメソッド
	/// </summary>
	void FixedUpdate()
	{
		//必殺技番号
		switch (m_numDeathblow)
		{
			case NumDeathblow.Non:
				//必殺技中でない
				break;
			case NumDeathblow.Dethblow1:
				Deathblow1();
				break;
		}

		DebugProcess();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技１
	/// </summary>
	void Deathblow1()
	{
		m_countTimeDeathblow += Time.deltaTime;

		//終了
		if (m_countTimeDeathblow >= m_deathblowTime)
		{
			m_numDeathblow = NumDeathblow.Non;
			SetManipulateObject(false);
			m_player.StartAnimIdle();
			return;
		}

		//必殺技１の状態を取得
		GetStateDeathblow1();

		//必殺技の実行
		Deathblow1Player();
		Deathblow1Boss();
		Deathblow1Camera();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技１の状態を取得
	/// </summary>
	void GetStateDeathblow1()
	{
		GetStateDeathblow1Player();
		GetStateDeathblow1Boss();
		GetStateDeathblow1Camera();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// プレイヤーの必殺技１の状態を取得
	/// </summary>
	void GetStateDeathblow1Player()
	{
		m_playerStateNumPrev = m_playerStateNum;

		//タイムライン
		if (m_countTimeDeathblow >= m_deathblow1Attack4Time)
			m_playerStateNum = 4;
		else if (m_countTimeDeathblow >= m_deathblow1Attack3Time)
			m_playerStateNum = 3;
		else if (m_countTimeDeathblow >= m_deathblow1Attack2Time)
			m_playerStateNum = 2;
		else if (m_countTimeDeathblow >= m_deathblow1Attack1Time)
			m_playerStateNum = 1;
		else
			m_playerStateNum = 0;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ボスの必殺技１の状態を取得
	/// </summary>
	void GetStateDeathblow1Boss()
	{
		m_bossStateNumPrev = m_bossStateNum;

		//タイムライン
		if (m_countTimeDeathblow >= m_blowingTimeDeathblow1Attack4)
			m_bossStateNum = 4;
		else if (m_countTimeDeathblow >= m_blowingTimeDeathblow1Attack3)
			m_bossStateNum = 3;
		else if (m_countTimeDeathblow >= m_blowingTimeDeathblow1Attack2)
			m_bossStateNum = 2;
		else if (m_countTimeDeathblow >= m_blowingTimeDeathblow1Attack1)
			m_bossStateNum = 1;
		else
			m_bossStateNum = 0;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// カメラの必殺技１の状態を取得
	/// </summary>
	void GetStateDeathblow1Camera()
	{
		m_cameraStateNumPrev = m_cameraStateNum;

		//タイムライン
		if (m_countTimeDeathblow >= m_deathblow1Attack4Time)
			m_cameraStateNum = 4;
		else if (m_countTimeDeathblow >= m_deathblow1Attack3Time)
			m_cameraStateNum = 3;
		else if (m_countTimeDeathblow >= m_deathblow1Attack2Time)
			m_cameraStateNum = 2;
		else if (m_countTimeDeathblow >= m_deathblow1Attack1Time)
			m_cameraStateNum = 1;
		else
			m_cameraStateNum = 0;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技１でのプレイヤー
	/// </summary>
	void Deathblow1Player()
	{
		//ステータスが初めて変わった
		if (m_playerStateNum != m_playerStateNumPrev)
		{
			//状態
			switch (m_playerStateNum)
			{
				case 0:
					//対応した位置に移動しアニメーション
					Teleportation(m_player.gameObject, m_stage.GetCenterPosBossRoomCurrentField());
					ObjectApproaches(m_player.gameObject, m_stage.GetPosDeathblow1CurrentField(0), -m_distancePlayerGivingDeathblow1Attack);
					m_player.StartAnimAttackDeathblow1A();
					break;
				case 1:
					//対応した位置に移動しアニメーション
					Teleportation(m_player.gameObject, m_stage.GetPosDeathblow1CurrentField(0));
					ObjectApproaches(m_player.gameObject, m_stage.GetCenterPosBossRoomCurrentField(), -m_distancePlayerGivingDeathblow1Attack);
					m_player.StartAnimAttackDeathblow1A();
					break;
				case 2:
					//対応した位置に移動しアニメーション
					Teleportation(m_player.gameObject, m_stage.GetPosDeathblow1CurrentField(3));
					ObjectApproaches(m_player.gameObject, m_stage.GetCenterPosBossRoomCurrentField(), -m_distancePlayerGivingDeathblow1Attack);
					m_player.StartAnimAttackDeathblow1A();
					break;
				case 3:
					//対応した位置に移動しアニメーション
					Teleportation(m_player.gameObject, m_stage.GetPosDeathblow1CurrentField(1));
					ObjectApproaches(m_player.gameObject, m_stage.GetCenterPosBossRoomCurrentField(), -m_distancePlayerGivingDeathblow1Attack);
					m_player.StartAnimAttackDeathblow1A();
					break;
				case 4:
					//対応した位置に移動しアニメーション
					Teleportation(m_player.gameObject, m_stage.GetPosDeathblow1CurrentField(4));
					ObjectApproaches(m_player.gameObject, m_stage.GetCenterPosBossRoomCurrentField(), -m_distancePlayerGivingDeathblow1Attack);
					m_player.StartAnimAttackDeathblow1A();
					break;
			}
		}

		//毎フレーム
		m_player.transform.LookAt(m_boss.transform);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 瞬間移動
	/// </summary>
	/// <param name="obj">対象オブジェクト</param>
	/// <param name="pos">位置</param>
	void Teleportation(GameObject obj, Vector3 pos)
	{
		obj.transform.position = pos;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// オブジェクトが近づく
	/// </summary>
	/// <param name="obj">対象オブジェクト</param>
	/// <param name="targetPos">ターゲット位置</param>
	/// <param name="distance">距離</param>
	void ObjectApproaches(GameObject obj, Vector3 targetPos, float distance)
	{
		m_workVector3 = targetPos - obj.transform.position;

		obj.transform.position += m_workVector3.normalized * distance;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技１でのボス
	/// </summary>
	void Deathblow1Boss()
	{
		//始めてステータスが変わった
		if(m_bossStateNum != m_bossStateNumPrev)
		{
			//状態
			switch (m_bossStateNum)
			{
				case 0:
					//中心に移動
					Teleportation(m_boss.gameObject, m_stage.GetCenterPosBossRoomCurrentField());
					break;
				case 1:
					break;
				case 2:
					break;
				case 3:
					break;
				case 4:
					break;
			}
		}

		//状態
		switch (m_bossStateNum)
		{
			case 0:
				//対応した位置から指定された位置へ移動
				Transposition(m_boss.gameObject, m_stage.GetCenterPosBossRoomCurrentField(), m_stage.GetPosDeathblow1CurrentField(0),
				m_countTimeDeathblow, m_blowingTimeDeathblow1Attack1);
				break;
			case 1:
				//対応した位置から指定された位置へ移動
				Transposition(m_boss.gameObject, m_stage.GetPosDeathblow1CurrentField(0), m_stage.GetPosDeathblow1CurrentField(3),
				(m_countTimeDeathblow - m_blowingTimeDeathblow1Attack1), (m_blowingTimeDeathblow1Attack2 - m_blowingTimeDeathblow1Attack1));
				break;
			case 2:
				//対応した位置から指定された位置へ移動
				Transposition(m_boss.gameObject, m_stage.GetPosDeathblow1CurrentField(3), m_stage.GetPosDeathblow1CurrentField(1),
				(m_countTimeDeathblow - m_blowingTimeDeathblow1Attack2), (m_blowingTimeDeathblow1Attack3 - m_blowingTimeDeathblow1Attack2));
				break;
			case 3:
				//対応した位置から指定された位置へ移動
				Transposition(m_boss.gameObject, m_stage.GetPosDeathblow1CurrentField(1), m_stage.GetPosDeathblow1CurrentField(4),
				(m_countTimeDeathblow - m_blowingTimeDeathblow1Attack3), (m_blowingTimeDeathblow1Attack4 - m_blowingTimeDeathblow1Attack3));
				break;
			case 4:
				//対応した位置から指定された位置へ移動
				Transposition(m_boss.gameObject, m_stage.GetPosDeathblow1CurrentField(4), m_stage.GetPosDeathblow1CurrentField(2),
				(m_countTimeDeathblow - m_blowingTimeDeathblow1Attack4), (m_deathblowTime - m_blowingTimeDeathblow1Attack4));
				break;
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 転移
	/// </summary>
	/// <param name="obj">対象オブジェクト</param>
	/// <param name="startPos">スタート位置</param>
	/// <param name="targetPos">目標位置</param>
	/// <param name="currentTime">現在の時間</param>
	/// <param name="travelTime">移動時間</param>
	void Transposition(GameObject obj, Vector3 startPos, Vector3 targetPos, float currentTime, float travelTime)
	{
		//必要な情報の取得
		m_workVector3 = obj.transform.position; //対象オブジェクトの位置
		m_workFloat = currentTime / travelTime; //移動割合

		m_workVector3 = startPos + (targetPos - startPos) * m_workFloat;

		//反映
		obj.transform.position = m_workVector3;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技１でのカメラ
	/// </summary>
	void Deathblow1Camera()
	{
		//初めてステータスが変わった
		if (m_cameraStateNum != m_cameraStateNumPrev)
		{
			//状態
			switch (m_cameraStateNum)
			{
				case 0:
					//初期位置と方向
					Teleportation(m_camera.gameObject, m_boss.transform.position);
					m_camera.transform.LookAt(m_player.transform);
					m_camera.transform.Rotate(Vector3.up * m_cameraRotationAngleBeginningDeathblow1);
					m_camera.transform.position += m_camera.transform.forward * -m_cameraDistanceDeathblowHit + Vector3.up * m_cameraHeightDeathblow;
					break;
				case 1:
					//中央に移動する
					Teleportation(m_camera.gameObject, m_stage.GetCenterPosBossRoomCurrentField());
					m_camera.transform.position += Vector3.up * m_cameraHeightDeathblow;
					break;
				case 2:
					break;
				case 3:
					break;
				case 4:
					break;
			}
		}
		
		//状態
		switch (m_cameraStateNum)
		{
			case 0:
				break;
			case 1:
				m_camera.transform.LookAt(m_boss.transform);
				break;
			case 2:
				m_camera.transform.LookAt(m_boss.transform);
				break;
			case 3:
				m_camera.transform.LookAt(m_boss.transform);
				break;
			case 4:
				break;
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// デバッグ処理
	/// </summary>
	void DebugProcess()
	{
#if DEBUG
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
#endif
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// プレイヤーの攻撃
	/// </summary>
	/// <param name="attackCube">攻撃用の直方体</param>
	/// <param name="attribute">攻撃属性</param>
	/// <param name="power">攻撃威力</param>
	/// <param name="time">攻撃時間</param>
	/// <param name="isDeathblow">必殺技か</param>
	public void PlayerAttack(MyCube attackCube, MaskAttribute attribute, int power = 0, float time = -1, bool isDeathblow = false)
	{
		//攻撃範囲の生成
		var attackRange = Instantiate(!isDeathblow ? PlayerAttackRange : PlayerAttackDeathblowRange, transform);
		m_workAttack = attackRange.GetComponent<MyAttack>();

		//攻撃範囲の調整
		AdjustAttackRange(attackRange, attackCube);

		//攻撃の詳細
		m_workAttack.Attribute = attribute;
		m_workAttack.Power = power;

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
	/// <param name="attribute">攻撃属性</param>
	/// <param name="power">攻撃威力</param>
	/// <param name="time">攻撃時間</param>
	public void EnemyAttack(MyCube attackCube, MaskAttribute attribute, int power = 0, float time = -1)
	{
		//攻撃範囲の生成
		var attackRange = Instantiate(EnemyAttackRange, transform);
		m_workAttack = attackRange.GetComponent<MyAttack>();

		//攻撃範囲の調整
		AdjustAttackRange(attackRange, attackCube);

		//攻撃の詳細
		m_workAttack.Attribute = attribute;
		m_workAttack.Power = power;

		//攻撃時間
		if (time >= 0)
			Destroy(attackRange, time);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技１を開始する
	/// </summary>
	public void StartDeathblow1()
	{
		//攻撃マネージャが操作する
		SetManipulateObject(true);

		//必殺技設定
		m_countTimeDeathblow = 0;
		m_numDeathblow = NumDeathblow.Dethblow1;

		//キャラクター設定
		m_playerStateNum = int.MaxValue;
		m_bossStateNum = int.MaxValue;
		m_cameraStateNum = int.MaxValue;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// オブジェクトを操る設定
	/// </summary>
	/// <param name="isManipulate">操るか</param>
	void SetManipulateObject(bool isManipulate)
	{
		//プレイヤーとボスとカメラを操る設定
		m_player.enabled = !isManipulate;
		m_boss.GetComponent<MonoBehaviour>().enabled = !isManipulate;
		myCharacter.GameScript.CameraScript.enabled = !isManipulate;
	}
}
