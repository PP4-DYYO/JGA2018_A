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
/// 攻撃マネージャタグ
/// </summary>
public struct AttackManagerTag
{
	/// <summary>
	/// プレイヤー攻撃範囲
	/// </summary>
	public const string PLAYER_ATTACK_RANGE_TAG = "PlayerAttackRange";

	/// <summary>
	/// プレイヤー攻撃必殺技範囲
	/// </summary>
	public const string PLAYER_ATTACK_DEATHBLOW_RANGE_TAG = "PlayerAttackDeathblowRange";

	/// <summary>
	/// 敵攻撃範囲
	/// </summary>
	public const string ENEMY_ATTACK_RANGE_TAG = "EnemyAttackRange";
}

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

	/// <summary>
	/// 中心位置を取得
	/// </summary>
	/// <returns></returns>
	public Vector3 GetCenter()
	{
		return new Vector3((vLDB.x + vRDB.x + vLDF.x + vRDF.x + vLUB.x + vRUB.x + vLUF.x + vRUF.x) / NUM_VERTICES,
			(vLDB.y + vRDB.y + vLDF.y + vRDF.y + vLUB.y + vRUB.y + vLUF.y + vRUF.y) / NUM_VERTICES);
	}
}

//----------------------------------------------------------------------------------------------------
/// <summary>
/// １２角柱
/// </summary>
public struct MyPrism12
{
	/// <summary>
	/// 上00時位置の頂点
	/// </summary>
	Vector3 vU00;

	/// <summary>
	/// 上01時位置の頂点
	/// </summary>
	Vector3 vU01;

	/// <summary>
	/// 上02時位置の頂点
	/// </summary>
	Vector3 vU02;

	/// <summary>
	/// 上03時位置の頂点
	/// </summary>
	Vector3 vU03;

	/// <summary>
	/// 上04時位置の頂点
	/// </summary>
	Vector3 vU04;

	/// <summary>
	/// 上05時位置の頂点
	/// </summary>
	Vector3 vU05;

	/// <summary>
	/// 上06時位置の頂点
	/// </summary>
	Vector3 vU06;

	/// <summary>
	/// 上07時位置の頂点
	/// </summary>
	Vector3 vU07;

	/// <summary>
	/// 上08時位置の頂点
	/// </summary>
	Vector3 vU08;

	/// <summary>
	/// 上09時位置の頂点
	/// </summary>
	Vector3 vU09;

	/// <summary>
	/// 上10時位置の頂点
	/// </summary>
	Vector3 vU10;

	/// <summary>
	/// 上11時位置の頂点
	/// </summary>
	Vector3 vU11;

	/// <summary>
	/// 下00時位置の頂点
	/// </summary>
	Vector3 vD00;

	/// <summary>
	/// 下01時位置の頂点
	/// </summary>
	Vector3 vD01;

	/// <summary>
	/// 下02時位置の頂点
	/// </summary>
	Vector3 vD02;

	/// <summary>
	/// 下03時位置の頂点
	/// </summary>
	Vector3 vD03;

	/// <summary>
	/// 下04時位置の頂点
	/// </summary>
	Vector3 vD04;

	/// <summary>
	/// 下05時位置の頂点
	/// </summary>
	Vector3 vD05;

	/// <summary>
	/// 下06時位置の頂点
	/// </summary>
	Vector3 vD06;

	/// <summary>
	/// 下07時位置の頂点
	/// </summary>
	Vector3 vD07;

	/// <summary>
	/// 下08時位置の頂点
	/// </summary>
	Vector3 vD08;

	/// <summary>
	/// 下09時位置の頂点
	/// </summary>
	Vector3 vD09;

	/// <summary>
	/// 下10時位置の頂点
	/// </summary>
	Vector3 vD10;

	/// <summary>
	/// 下11時位置の頂点
	/// </summary>
	Vector3 vD11;

	/// <summary>
	/// 高さ
	/// </summary>
	float height;

	/// <summary>
	/// 頂点数
	/// </summary>
	const int NUM_VERTICES = 24;

	/// <summary>
	/// 頂点の組み合わせ
	/// </summary>
	public static int[] COMBINATION_VERTICES =
	{
		//上辺
		0, 1, 11, 1, 10, 11,
		1, 2, 10, 2, 9, 10,
		2, 3, 9, 3, 8, 9,
		3, 4, 8, 4, 7, 8,
		4, 5, 7, 5, 6, 7,
		//側面
		0, 12, 1, 1, 12, 13,
		1, 13, 2, 2, 13, 14,
		2, 14, 3, 3, 14, 15,
		3, 15, 4, 4, 15, 16,
		4, 16, 5, 5, 16, 17,
		5, 17, 6, 6, 17, 18,
		6, 18, 7, 7, 18, 19,
		7, 19, 8, 8, 19, 20,
		8, 20, 9, 9, 20, 21,
		9, 21, 10, 10, 21, 22,
		10, 22, 11, 11, 22, 23,
		11, 23, 0, 0, 23, 12,
		//下辺
		12, 23, 13, 13, 23, 22,
		13, 22, 14, 14, 22, 21,
		14, 21, 15, 15, 21, 20,
		15, 20, 16, 16, 20, 19,
		16, 19, 17, 17, 19, 18,
	};

	/// <summary>
	/// 作業用のVector３
	/// </summary>
	Vector3[] workVector3;

	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="height">高さ</param>
	/// <param name="radius">半径</param>
	public MyPrism12(float height = 0, float radius = 1)
	{
		vU00 = new Vector3();
		vU01 = new Vector3();
		vU02 = new Vector3();
		vU03 = new Vector3();
		vU04 = new Vector3();
		vU05 = new Vector3();
		vU06 = new Vector3();
		vU07 = new Vector3();
		vU08 = new Vector3();
		vU09 = new Vector3();
		vU10 = new Vector3();
		vU11 = new Vector3();
		vD00 = new Vector3();
		vD01 = new Vector3();
		vD02 = new Vector3();
		vD03 = new Vector3();
		vD04 = new Vector3();
		vD05 = new Vector3();
		vD06 = new Vector3();
		vD07 = new Vector3();
		vD08 = new Vector3();
		vD09 = new Vector3();
		vD10 = new Vector3();
		vD11 = new Vector3();
		this.height = height;
		workVector3 = new Vector3[NUM_VERTICES];
		SetRadius(radius);
	}

	/// <summary>
	/// 頂点を取得
	/// </summary>
	/// <returns></returns>
	public Vector3[] GetVertices()
	{
		workVector3[0] = vU00;
		workVector3[1] = vU01;
		workVector3[2] = vU02;
		workVector3[3] = vU03;
		workVector3[4] = vU04;
		workVector3[5] = vU05;
		workVector3[6] = vU06;
		workVector3[7] = vU07;
		workVector3[8] = vU08;
		workVector3[9] = vU09;
		workVector3[10] = vU10;
		workVector3[11] = vU11;
		workVector3[12] = vD00;
		workVector3[13] = vD01;
		workVector3[14] = vD02;
		workVector3[15] = vD03;
		workVector3[16] = vD04;
		workVector3[17] = vD05;
		workVector3[18] = vD06;
		workVector3[19] = vD07;
		workVector3[20] = vD08;
		workVector3[21] = vD09;
		workVector3[22] = vD10;
		workVector3[23] = vD11;
		return workVector3;
	}

	/// <summary>
	/// MyPrism１２の設定
	/// </summary>
	/// <param name="height"></param>
	/// <param name="radius"></param>
	public void SetMyPrism12(float height, float radius = 1)
	{
		this.height = height;
		SetRadius(radius);
	}

	/// <summary>
	/// 半径の設定
	/// </summary>
	/// <param name="radius">半径</param>
	public void SetRadius(float radius)
	{
		//半径１の時の座標
		ResetPrism12();

		//半径を考慮した座標
		vU00 *= radius;
		vU01 *= radius;
		vU02 *= radius;
		vU03 *= radius;
		vU04 *= radius;
		vU05 *= radius;
		vU06 *= radius;
		vU07 *= radius;
		vU08 *= radius;
		vU09 *= radius;
		vU10 *= radius;
		vU11 *= radius;
		vD00 = vU00;
		vD01 = vU01;
		vD02 = vU02;
		vD03 = vU03;
		vD04 = vU04;
		vD05 = vU05;
		vD06 = vU06;
		vD07 = vU07;
		vD08 = vU08;
		vD09 = vU09;
		vD10 = vU10;
		vD11 = vU11;

		//高さを元に戻す
		SetHeight(height);
	}

	/// <summary>
	/// リセット（半径１の状態にする）
	/// </summary>
	void ResetPrism12()
	{
		vU00 = new Vector3(0, 0, 1);
		vU01 = new Vector3(0.5f, 0, 0.8660254037844386467637f);
		vU02 = new Vector3(0.8660254037844386467637f, 0, 0.5f);
		vU03 = new Vector3(1, 0, 0);
		vU04 = new Vector3(0.8660254037844386467637f, 0, -0.5f);
		vU05 = new Vector3(0.5f, 0, -0.8660254037844386467637f);
		vU06 = new Vector3(0, 0, -1);
		vU07 = new Vector3(-0.5f, 0, -0.8660254037844386467637f);
		vU08 = new Vector3(-0.8660254037844386467637f, 0, -0.5f);
		vU09 = new Vector3(-1, 0, 0);
		vU10 = new Vector3(-0.8660254037844386467637f, 0, 0.5f);
		vU11 = new Vector3(-0.5f, 0, 0.8660254037844386467637f);
		vD00 = vU00;
		vD01 = vU01;
		vD02 = vU02;
		vD03 = vU03;
		vD04 = vU04;
		vD05 = vU05;
		vD06 = vU06;
		vD07 = vU07;
		vD08 = vU08;
		vD09 = vU09;
		vD10 = vU10;
		vD11 = vU11;
	}

	/// <summary>
	/// 高さの設定
	/// </summary>
	/// <param name="height">高さ</param>
	void SetHeight(float height)
	{
		vU00.y = height;
		vU01.y = height;
		vU02.y = height;
		vU03.y = height;
		vU04.y = height;
		vU05.y = height;
		vU06.y = height;
		vU07.y = height;
		vU08.y = height;
		vU09.y = height;
		vU10.y = height;
		vU11.y = height;
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
	/// ゲーム
	/// </summary>
	MyGame m_game;

	/// <summary>
	/// ボス
	/// </summary>
	MyAiBoss m_boss;

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

	#region 必殺技２
	[Header("必殺技２")]
	/// <summary>
	/// 必殺技2の時間
	/// </summary>
	[SerializeField]
	float m_deathblow2Time;

	/// <summary>
	/// 壁にめり込まないための距離
	/// </summary>
	[SerializeField]
	float m_distanceNotToGetDrownedInWall;
	#endregion

	#region 必殺技３
	[Header("必殺技３")]
	/// <summary>
	/// 必殺技３のプレイヤー位置
	/// </summary>
	[SerializeField]
	Vector3 m_playerPosDeathblow3;

	/// <summary>
	/// 一回目の必殺技３時間
	/// </summary>
	[SerializeField]
	float m_firstDeathblow3Time;

	/// <summary>
	/// 二回目の必殺技３時間
	/// </summary>
	[SerializeField]
	float m_secondDeathblow3Time;

	/// <summary>
	/// 三回目の必殺技３時間
	/// </summary>
	[SerializeField]
	float m_thirdDeathblow3Time;

	/// <summary>
	/// 四回目の必殺技３時間
	/// </summary>
	[SerializeField]
	float m_fourthDeathblow3Time;

	/// <summary>
	/// 五回目の必殺技３時間
	/// </summary>
	[SerializeField]
	float m_fifthDeathblow3Time;

	/// <summary>
	/// 六回目の必殺技３時間
	/// </summary>
	[SerializeField]
	float m_sixthDeathblow3Time;

	/// <summary>
	/// 七回目の必殺技３時間
	/// </summary>
	[SerializeField]
	float m_seventhDeathblow3Time;

	/// <summary>
	/// 八回目の必殺技３時間
	/// </summary>
	[SerializeField]
	float m_eighthDeathblow3Time;

	/// <summary>
	/// 九回目の必殺技３時間
	/// </summary>
	[SerializeField]
	float m_ninthDeathblow3Time;

	/// <summary>
	/// 分身する開始時間
	/// </summary>
	[SerializeField]
	float m_startTimeDivide;

	/// <summary>
	/// 分身する終了時間
	/// </summary>
	[SerializeField]
	float m_endTimeDivide;

	/// <summary>
	/// 影武者
	/// </summary>
	GameObject m_shadowWarrior;
	#endregion

	#region 必殺技４
	[Header("必殺技４")]
	/// <summary>
	/// 必殺技４を当てるカメラ角度
	/// </summary>
	[SerializeField]
	float m_cameraAngleHitDeathblow4;
	
	/// <summary>
	/// 必殺技４の時間
	/// </summary>
	[SerializeField]
	float m_deathblow4Time;

	/// <summary>
	/// 背後を取る時間
	/// </summary>
	[SerializeField]
	float m_timeTakeBehind;

	/// <summary>
	/// 必殺技４を当てる時間
	/// </summary>
	[SerializeField]
	float m_timeHitDeathblow4;

	/// <summary>
	/// 必殺技４で吹き飛ぶ時間
	/// </summary>
	[SerializeField]
	float m_blowingTimeDeathblow4;

	/// <summary>
	/// プレイヤーの初期位置の保存用
	/// </summary>
	Vector3 m_playerInitPos;
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

	/// <summary>
	/// 作業用のRay
	/// </summary>
	Ray m_workRay;

	/// <summary>
	/// 作業用のRaycastHit
	/// </summary>
	RaycastHit m_workRaycastHit;
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

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 初期
	/// </summary>
	void Start()
	{
		//インスタンスの取得
		m_game = myCharacter.GameScript;
		m_player = myCharacter.PlayerScript;
		m_boss = myCharacter.BossScript;
		m_stage = m_game.StageScript;
		m_camera = m_game.CameraScript;
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
			case NumDeathblow.Dethblow2:
				Deathblow2();
				break;
			case NumDeathblow.Dethblow3:
				Deathblow3();
				break;
			case NumDeathblow.Dethblow4:
				Deathblow4();
				break;
		}

		DebugProcess();
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
	/// 分身する
	/// </summary>
	/// <param name="target">ターゲット</param>
	/// <param name="survivalTime">生存時間</param>
	void Divide(GameObject target, float survivalTime)
	{
		//影武者生成
		m_shadowWarrior = Instantiate(target);

		//親・位置・回転のコピーと当たり判定の無効化
		m_shadowWarrior.transform.parent = target.transform.parent;
		m_shadowWarrior.transform.position = target.transform.position;
		m_shadowWarrior.transform.rotation = target.transform.rotation;

		//影武者の生存時間
		Destroy(m_shadowWarrior, survivalTime);
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
			m_game.SetManipulateMainObject(false);
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
	/// 必殺技１でのボス
	/// </summary>
	void Deathblow1Boss()
	{
		//始めてステータスが変わった
		if (m_bossStateNum != m_bossStateNumPrev)
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
	/// 必殺技２
	/// </summary>
	void Deathblow2()
	{
		m_countTimeDeathblow += Time.deltaTime;

		//終了
		if (m_countTimeDeathblow >= m_deathblow2Time)
		{
			m_numDeathblow = NumDeathblow.Non;
			m_game.SetManipulateMainObject(false);
			return;
		}

		//必殺技２の状態を取得
		GetStateDeathblow2();

		//必殺技の実行
		Deathblow2Player();
		Deathblow2Boss();
		Deathblow2Camera();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技２の状態を取得
	/// </summary>
	void GetStateDeathblow2()
	{
		GetStateDeathblow2Player();
		GetStateDeathblow2Boss();
		GetStateDeathblow2Camera();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// プレイヤーの必殺技２の状態を取得
	/// </summary>
	void GetStateDeathblow2Player()
	{
		m_playerStateNumPrev = m_playerStateNum;

		//タイムライン
		m_playerStateNum = 0;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ボスの必殺技２の状態を取得
	/// </summary>
	void GetStateDeathblow2Boss()
	{
		m_bossStateNumPrev = m_bossStateNum;

		//タイムライン
		m_bossStateNum = 0;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// カメラの必殺技２の状態を取得
	/// </summary>
	void GetStateDeathblow2Camera()
	{
		m_cameraStateNumPrev = m_cameraStateNum;

		//タイムライン
		m_cameraStateNum = 0;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技２でのプレイヤー
	/// </summary>
	void Deathblow2Player()
	{
		//ステータスが初めて変わった
		if (m_playerStateNum != m_playerStateNumPrev)
		{
			//状態
			switch (m_playerStateNum)
			{
				case 0:
					//アニメーション再生
					m_player.StartAnimAttackDeathblow2A();
					break;
			}
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技２でのボス
	/// </summary>
	void Deathblow2Boss()
	{
		//始めてステータスが変わった
		if (m_bossStateNum != m_bossStateNumPrev)
		{
			//状態
			switch (m_bossStateNum)
			{
				case 0:
					break;
			}
		}

	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技２でのカメラ
	/// </summary>
	void Deathblow2Camera()
	{
		//初めてステータスが変わった
		if (m_cameraStateNum != m_cameraStateNumPrev)
		{
			//状態
			switch (m_cameraStateNum)
			{
				case 0:
					//初期位置と方向
					Teleportation(m_camera.gameObject, m_player.transform.position);
					m_camera.transform.position += m_player.transform.forward * m_cameraDistanceDeathblowHit + Vector3.up * m_cameraHeightDeathblow;
					m_camera.transform.LookAt(m_player.transform.position + (Vector3.up * m_cameraHeightDeathblow));
					//壁めり込み考慮
					m_workRay.origin = m_player.transform.position + Vector3.up * m_cameraHeightDeathblow;
					m_workRay.direction = m_camera.transform.position - (m_player.transform.position + Vector3.up * m_cameraHeightDeathblow);
					if (Physics.Raycast(m_workRay, out m_workRaycastHit, m_cameraDistanceDeathblowHit))
						m_camera.transform.position = m_workRaycastHit.point
							- (m_workRay.GetPoint(m_distanceNotToGetDrownedInWall) - (m_player.transform.position + Vector3.up * m_cameraHeightDeathblow));
					break;
			}
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技３
	/// </summary>
	void Deathblow3()
	{
		m_countTimeDeathblow += Time.deltaTime;

		//終了
		if (m_countTimeDeathblow >= m_deathblowTime)
		{
			m_numDeathblow = NumDeathblow.Non;
			m_game.SetManipulateMainObject(false);
			m_player.StartAnimIdle();
			return;
		}

		//必殺技３の状態を取得
		GetStateDeathblow3();

		//必殺技の実行
		Deathblow3Player();
		Deathblow3Boss();
		Deathblow3Camera();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技３の状態を取得
	/// </summary>
	void GetStateDeathblow3()
	{
		GetStateDeathblow3Player();
		GetStateDeathblow3Boss();
		GetStateDeathblow3Camera();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// プレイヤーの必殺技３の状態を取得
	/// </summary>
	void GetStateDeathblow3Player()
	{
		m_playerStateNumPrev = m_playerStateNum;

		//タイムライン
		if (m_countTimeDeathblow >= m_endTimeDivide)
			m_playerStateNum = 11;
		else if (m_countTimeDeathblow >= m_ninthDeathblow3Time)
			m_playerStateNum = 10;
		else if (m_countTimeDeathblow >= m_eighthDeathblow3Time)
			m_playerStateNum = 9;
		else if (m_countTimeDeathblow >= m_seventhDeathblow3Time)
			m_playerStateNum = 8;
		else if (m_countTimeDeathblow >= m_sixthDeathblow3Time)
			m_playerStateNum = 7;
		else if (m_countTimeDeathblow >= m_fifthDeathblow3Time)
			m_playerStateNum = 6;
		else if (m_countTimeDeathblow >= m_fourthDeathblow3Time)
			m_playerStateNum = 5;
		else if (m_countTimeDeathblow >= m_thirdDeathblow3Time)
			m_playerStateNum = 4;
		else if (m_countTimeDeathblow >= m_secondDeathblow3Time)
			m_playerStateNum = 3;
		else if (m_countTimeDeathblow >= m_firstDeathblow3Time)
			m_playerStateNum = 2;
		else if (m_countTimeDeathblow >= m_startTimeDivide)
			m_playerStateNum = 1;
		else
			m_playerStateNum = 0;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ボスの必殺技３の状態を取得
	/// </summary>
	void GetStateDeathblow3Boss()
	{
		m_bossStateNumPrev = m_bossStateNum;

		//タイムライン
		m_bossStateNum = 0;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// カメラの必殺技３の状態を取得
	/// </summary>
	void GetStateDeathblow3Camera()
	{
		m_cameraStateNumPrev = m_cameraStateNum;

		//タイムライン
		if (m_countTimeDeathblow >= m_startTimeDivide)
			m_cameraStateNum = 1;
		else
			m_cameraStateNum = 0;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技３でのプレイヤー
	/// </summary>
	void Deathblow3Player()
	{
		//ステータスが初めて変わった
		if (m_playerStateNum != m_playerStateNumPrev)
		{
			//状態
			switch (m_playerStateNum)
			{
				case 0:
					//初期位置と方向
					Teleportation(m_player.gameObject, m_stage.GetCenterPosBossRoomCurrentField() + m_playerPosDeathblow3);
					m_player.transform.LookAt(m_stage.GetCenterPosBossRoomCurrentField());
					break;
				case 1:
					//分身
					Divide(m_player.gameObject, m_deathblowTime - m_startTimeDivide);
					break;
				case 2:
					//分身の攻撃アニメーション
					m_shadowWarrior.transform.LookAt(m_stage.GetCenterPosBossRoomCurrentField());
					m_shadowWarrior.GetComponent<MyPlayer>().StartAnimAttackDethblow3A();
					break;
				case 3:
					//攻撃アニメーション
					m_player.transform.LookAt(m_stage.GetCenterPosBossRoomCurrentField());
					m_player.StartAnimAttackDethblow3A();
					break;
				case 4:
					//分身の攻撃アニメーション
					m_shadowWarrior.transform.LookAt(m_stage.GetCenterPosBossRoomCurrentField());
					m_shadowWarrior.GetComponent<MyPlayer>().StartAnimAttackDethblow3A();
					break;
				case 5:
					//攻撃アニメーション
					m_player.transform.LookAt(m_stage.GetCenterPosBossRoomCurrentField());
					m_player.StartAnimAttackDethblow3A();
					break;
				case 6:
					//分身の攻撃アニメーション
					m_shadowWarrior.transform.LookAt(m_stage.GetCenterPosBossRoomCurrentField());
					m_shadowWarrior.GetComponent<MyPlayer>().StartAnimAttackDethblow3A();
					break;
				case 7:
					//攻撃アニメーション
					m_player.transform.LookAt(m_stage.GetCenterPosBossRoomCurrentField());
					m_player.StartAnimAttackDethblow3A();
					break;
				case 8:
					//攻撃アニメーション
					m_player.transform.LookAt(m_stage.GetCenterPosBossRoomCurrentField());
					m_player.StartAnimAttackDethblow3A();
					//分身の攻撃アニメーション
					m_shadowWarrior.transform.LookAt(m_stage.GetCenterPosBossRoomCurrentField());
					m_shadowWarrior.GetComponent<MyPlayer>().StartAnimAttackDethblow3A();
					break;
				case 9:
					//攻撃アニメーション
					m_player.transform.LookAt(m_stage.GetCenterPosBossRoomCurrentField());
					m_player.StartAnimAttackDethblow3A();
					//分身の攻撃アニメーション
					m_shadowWarrior.transform.LookAt(m_stage.GetCenterPosBossRoomCurrentField());
					m_shadowWarrior.GetComponent<MyPlayer>().StartAnimAttackDethblow3A();
					break;
				case 10:
					//攻撃アニメーション
					m_player.transform.LookAt(m_stage.GetCenterPosBossRoomCurrentField());
					m_player.StartAnimAttackDethblow3A();
					//分身の攻撃アニメーション
					m_shadowWarrior.transform.LookAt(m_stage.GetCenterPosBossRoomCurrentField());
					m_shadowWarrior.GetComponent<MyPlayer>().StartAnimAttackDethblow3A();
					break;
				case 11:
					break;
			}
		}

		//状態
		switch (m_playerStateNum)
		{
			case 0:
				//移動
				Transposition(m_player.gameObject, (m_stage.GetCenterPosBossRoomCurrentField() + m_playerPosDeathblow3),
					(m_stage.GetCenterPosBossRoomCurrentField() + -m_playerPosDeathblow3), m_countTimeDeathblow, m_startTimeDivide);
				break;
			case 1:
				//分身の移動
				Transposition(m_shadowWarrior,
					(m_stage.GetCenterPosBossRoomCurrentField() + -m_playerPosDeathblow3),
					(m_stage.GetCenterPosBossRoomCurrentField() + Vector3.Scale(m_playerPosDeathblow3, (Vector3.back * 2 + Vector3.one))),
					(m_countTimeDeathblow - m_startTimeDivide), (m_firstDeathblow3Time - m_startTimeDivide));
				break;
			case 2:
				//分身の攻撃のための移動
				Transposition(m_shadowWarrior,
					(m_stage.GetCenterPosBossRoomCurrentField() + Vector3.Scale(m_playerPosDeathblow3, (Vector3.back * 2 + Vector3.one))),
					(m_stage.GetCenterPosBossRoomCurrentField() + Vector3.Scale(m_playerPosDeathblow3, (Vector3.left * 2 + Vector3.one))),
					(m_countTimeDeathblow - m_firstDeathblow3Time), (m_secondDeathblow3Time - m_firstDeathblow3Time));
				break;
			case 3:
				//攻撃のための移動
				Transposition(m_player.gameObject,
					(m_stage.GetCenterPosBossRoomCurrentField() + -m_playerPosDeathblow3),
					(m_stage.GetCenterPosBossRoomCurrentField() + m_playerPosDeathblow3),
					(m_countTimeDeathblow - m_secondDeathblow3Time), (m_thirdDeathblow3Time - m_secondDeathblow3Time));
				break;
			case 4:
				//分身の攻撃のための移動
				Transposition(m_shadowWarrior,
					(m_stage.GetCenterPosBossRoomCurrentField() + Vector3.Scale(m_playerPosDeathblow3, (Vector3.left * 2 + Vector3.one))),
					(m_stage.GetCenterPosBossRoomCurrentField() + Vector3.Scale(m_playerPosDeathblow3, (Vector3.back * 2 + Vector3.one))),
					(m_countTimeDeathblow - m_thirdDeathblow3Time), (m_fourthDeathblow3Time - m_thirdDeathblow3Time));
				break;
			case 5:
				//攻撃のための移動
				Transposition(m_player.gameObject, (m_stage.GetCenterPosBossRoomCurrentField() + m_playerPosDeathblow3),
					(m_stage.GetCenterPosBossRoomCurrentField() + -m_playerPosDeathblow3),
					(m_countTimeDeathblow - m_fourthDeathblow3Time), (m_fifthDeathblow3Time - m_fourthDeathblow3Time));
				break;
			case 6:
				//分身の攻撃のための移動
				Transposition(m_shadowWarrior,
					(m_stage.GetCenterPosBossRoomCurrentField() + Vector3.Scale(m_playerPosDeathblow3, (Vector3.back * 2 + Vector3.one))),
					(m_stage.GetCenterPosBossRoomCurrentField() + Vector3.Scale(m_playerPosDeathblow3, (Vector3.left * 2 + Vector3.one))),
					(m_countTimeDeathblow - m_fifthDeathblow3Time), (m_sixthDeathblow3Time - m_fifthDeathblow3Time));
				break;
			case 7:
				//攻撃のための移動
				Transposition(m_player.gameObject,
					(m_stage.GetCenterPosBossRoomCurrentField() + -m_playerPosDeathblow3),
					(m_stage.GetCenterPosBossRoomCurrentField() + m_playerPosDeathblow3),
					(m_countTimeDeathblow - m_sixthDeathblow3Time), (m_seventhDeathblow3Time - m_sixthDeathblow3Time));
				break;
			case 8:
				//攻撃のための移動
				Transposition(m_player.gameObject, (m_stage.GetCenterPosBossRoomCurrentField() + m_playerPosDeathblow3),
					(m_stage.GetCenterPosBossRoomCurrentField() + -m_playerPosDeathblow3),
					(m_countTimeDeathblow - m_seventhDeathblow3Time), (m_eighthDeathblow3Time - m_seventhDeathblow3Time));
				//分身の攻撃のための移動
				Transposition(m_shadowWarrior,
					(m_stage.GetCenterPosBossRoomCurrentField() + Vector3.Scale(m_playerPosDeathblow3, (Vector3.left * 2 + Vector3.one))),
					(m_stage.GetCenterPosBossRoomCurrentField() + Vector3.Scale(m_playerPosDeathblow3, (Vector3.back * 2 + Vector3.one))),
					(m_countTimeDeathblow - m_seventhDeathblow3Time), (m_eighthDeathblow3Time - m_seventhDeathblow3Time));
				break;
			case 9:
				//攻撃のための移動
				Transposition(m_player.gameObject,
					(m_stage.GetCenterPosBossRoomCurrentField() + -m_playerPosDeathblow3),
					(m_stage.GetCenterPosBossRoomCurrentField() + m_playerPosDeathblow3),
					(m_countTimeDeathblow - m_eighthDeathblow3Time), (m_ninthDeathblow3Time - m_eighthDeathblow3Time));
				//分身の攻撃のための移動
				Transposition(m_shadowWarrior,
					(m_stage.GetCenterPosBossRoomCurrentField() + Vector3.Scale(m_playerPosDeathblow3, (Vector3.back * 2 + Vector3.one))),
					(m_stage.GetCenterPosBossRoomCurrentField() + Vector3.Scale(m_playerPosDeathblow3, (Vector3.left * 2 + Vector3.one))),
					(m_countTimeDeathblow - m_eighthDeathblow3Time), (m_ninthDeathblow3Time - m_eighthDeathblow3Time));
				break;
			case 10:
				//攻撃のための移動
				Transposition(m_player.gameObject, (m_stage.GetCenterPosBossRoomCurrentField() + m_playerPosDeathblow3),
					(m_stage.GetCenterPosBossRoomCurrentField() + -m_playerPosDeathblow3),
					(m_countTimeDeathblow - m_ninthDeathblow3Time), (m_endTimeDivide - m_ninthDeathblow3Time));
				//分身の攻撃のための移動
				Transposition(m_shadowWarrior,
					(m_stage.GetCenterPosBossRoomCurrentField() + Vector3.Scale(m_playerPosDeathblow3, (Vector3.left * 2 + Vector3.one))),
					(m_stage.GetCenterPosBossRoomCurrentField() + Vector3.Scale(m_playerPosDeathblow3, (Vector3.back * 2 + Vector3.one))),
					(m_countTimeDeathblow - m_ninthDeathblow3Time), (m_endTimeDivide - m_ninthDeathblow3Time));
				break;
			case 11:
				//分身の移動と方向
				Transposition(m_shadowWarrior,
					(m_stage.GetCenterPosBossRoomCurrentField() + Vector3.Scale(m_playerPosDeathblow3, (Vector3.back * 2 + Vector3.one))),
					(m_stage.GetCenterPosBossRoomCurrentField() + -m_playerPosDeathblow3),
					(m_countTimeDeathblow - m_endTimeDivide), (m_deathblowTime - m_endTimeDivide));
				m_shadowWarrior.transform.LookAt(m_player.transform.position + m_player.transform.forward);
				break;
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技３でのボス
	/// </summary>
	void Deathblow3Boss()
	{
		//始めてステータスが変わった
		if (m_bossStateNum != m_bossStateNumPrev)
		{
			//状態
			switch (m_bossStateNum)
			{
				case 0:
					//ステージ中央に移動
					Teleportation(m_boss.gameObject, m_stage.GetCenterPosBossRoomCurrentField());
					break;
			}
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技３でのカメラ
	/// </summary>
	void Deathblow3Camera()
	{
		//初めてステータスが変わった
		if (m_cameraStateNum != m_cameraStateNumPrev)
		{
			//状態
			switch (m_cameraStateNum)
			{
				case 0:
					//初期位置と方向
					Teleportation(m_camera.gameObject, m_player.transform.position);
					m_camera.transform.position += m_player.transform.right * m_cameraDistanceDeathblowHit + Vector3.up * m_cameraHeightDeathblow;
					m_camera.transform.LookAt(m_boss.transform.position + (Vector3.up * m_cameraHeightDeathblow));
					break;
				case 1:
					//分身の位置
					Teleportation(m_camera.gameObject, m_player.transform.position);
					m_camera.transform.position += m_player.transform.forward * m_cameraDistanceDeathblowHit + Vector3.up * m_cameraHeightDeathblow;
					m_camera.transform.LookAt(m_boss.transform.position + (Vector3.up * m_cameraHeightDeathblow));
					break;
			}
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技４
	/// </summary>
	void Deathblow4()
	{
		m_countTimeDeathblow += Time.deltaTime;

		//終了
		if (m_countTimeDeathblow >= m_deathblow4Time)
		{
			m_numDeathblow = NumDeathblow.Non;
			m_game.SetManipulateMainObject(false);
			m_player.StartAnimIdle();
			return;
		}

		//必殺技４の状態を取得
		GetStateDeathblow4();

		//必殺技の実行
		Deathblow4Player();
		Deathblow4Boss();
		Deathblow4Camera();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技４の状態を取得
	/// </summary>
	void GetStateDeathblow4()
	{
		GetStateDeathblow4Player();
		GetStateDeathblow4Boss();
		GetStateDeathblow4Camera();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// プレイヤーの必殺技４の状態を取得
	/// </summary>
	void GetStateDeathblow4Player()
	{
		m_playerStateNumPrev = m_playerStateNum;

		//タイムライン
		if (m_countTimeDeathblow >= m_timeHitDeathblow4)
			m_playerStateNum = 2;
		else if (m_countTimeDeathblow >= m_timeTakeBehind)
			m_playerStateNum = 1;
		else
			m_playerStateNum = 0;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ボスの必殺技４の状態を取得
	/// </summary>
	void GetStateDeathblow4Boss()
	{
		m_bossStateNumPrev = m_bossStateNum;

		//タイムライン
		if (m_countTimeDeathblow >= m_blowingTimeDeathblow4)
			m_bossStateNum = 2;
		else if (m_countTimeDeathblow >= m_timeTakeBehind)
			m_bossStateNum = 1;
		else
			m_bossStateNum = 0;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// カメラの必殺技４の状態を取得
	/// </summary>
	void GetStateDeathblow4Camera()
	{
		m_cameraStateNumPrev = m_cameraStateNum;

		//タイムライン
		if (m_countTimeDeathblow >= m_timeHitDeathblow4)
			m_cameraStateNum = 2;
		else if (m_countTimeDeathblow >= m_timeTakeBehind)
			m_cameraStateNum = 1;
		else
			m_cameraStateNum = 0;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技４でのプレイヤー
	/// </summary>
	void Deathblow4Player()
	{
		//ステータスが初めて変わった
		if (m_playerStateNum != m_playerStateNumPrev)
		{
			//状態
			switch (m_playerStateNum)
			{
				case 0:
					//プレイヤー初期位置の保存と移動アニメーション
					m_playerInitPos = m_player.transform.position;
					m_player.StartAnimRun();
					break;
				case 1:
					//指定位置の移動と構えるアニメーション
					Teleportation(m_player.gameObject, m_stage.GetCenterPosBossRoomCurrentField());
					ObjectApproaches(m_player.gameObject, m_stage.GetPosDeathblow1CurrentField(0), -m_distancePlayerGivingDeathblow1Attack);
					m_player.transform.LookAt(m_stage.GetPosDeathblow1CurrentField(0));
					m_player.StartAnimIdle();
					break;
				case 2:
					//必殺技４アニメーション
					m_player.StartAnimAttackDethblow4A();
					break;
			}
		}

		//状態
		switch (m_playerStateNum)
		{
			case 0:
				//ボスの位置へ移動
				Transposition(m_player.gameObject, m_playerInitPos, m_boss.transform.position, m_countTimeDeathblow, m_timeTakeBehind);
				m_player.transform.LookAt(m_boss.transform);
				break;
			case 1:
				break;
			case 2:
				break;
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技４でのボス
	/// </summary>
	void Deathblow4Boss()
	{
		//始めてステータスが変わった
		if (m_bossStateNum != m_bossStateNumPrev)
		{
			//状態
			switch (m_bossStateNum)
			{
				case 0:
					break;
				case 1:
					//中心に移動と指定方向を向く
					Teleportation(m_boss.gameObject, m_stage.GetCenterPosBossRoomCurrentField());
					m_boss.transform.LookAt(m_stage.GetPosDeathblow1CurrentField(0));
					break;
				case 2:
					break;
			}
		}

		//状態
		switch (m_bossStateNum)
		{
			case 0:
				break;
			case 1:
				break;
			case 2:
				//対応した位置から指定された位置へ移動
				Transposition(m_boss.gameObject, m_stage.GetCenterPosBossRoomCurrentField(), m_stage.GetPosDeathblow1CurrentField(0),
					(m_countTimeDeathblow - m_blowingTimeDeathblow4), (m_deathblow4Time - m_blowingTimeDeathblow4));
				break;
		}
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技４でのカメラ
	/// </summary>
	void Deathblow4Camera()
	{
		//初めてステータスが変わった
		if (m_cameraStateNum != m_cameraStateNumPrev)
		{
			//状態
			switch (m_cameraStateNum)
			{
				case 0:
					//方向
					m_camera.transform.LookAt(m_boss.transform);
					break;
				case 1:
					//位置と向き
					Teleportation(m_camera.gameObject, m_stage.GetCenterPosBossRoomCurrentField());
					ObjectApproaches(m_camera.gameObject, m_stage.GetPosDeathblow1CurrentField(0), -m_cameraDistanceDeathblowHit);
					m_camera.transform.position += Vector3.up * m_cameraHeightDeathblow;
					m_camera.transform.RotateAround(m_stage.GetCenterPosBossRoomCurrentField(), Vector3.up, m_cameraAngleHitDeathblow4);
					m_camera.transform.LookAt(m_stage.GetCenterPosBossRoomCurrentField() + Vector3.up * m_cameraHeightDeathblow);
					break;
				case 2:
					break;
			}
		}

		//状態
		switch (m_cameraStateNum)
		{
			case 0:
				break;
			case 1:
				break;
			case 2:
				m_camera.transform.LookAt(m_boss.transform.position + Vector3.up * m_cameraHeightDeathblow);
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
		m_workAttack.CenterPosVertices = attackCube.GetCenter();
		m_workAttack.Attribute = attribute;
		m_workAttack.Power = power;

		//攻撃時間
		if (time >= 0)
			Destroy(attackRange, time);
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// プレイヤーの攻撃
	/// </summary>
	/// <param name="attacattackPrism12kCube">攻撃用の12角柱</param>
	/// <param name="pos">位置</param>
	/// <param name="attribute">攻撃属性</param>
	/// <param name="power">攻撃威力</param>
	/// <param name="time">攻撃時間</param>
	/// <param name="time">攻撃時間</param>
	/// <param name="isDeathblowTrriger">必殺技トリガーか</param>
	/// <param name="expansionTime">拡張する時間</param>
	/// <param name="expansionDistance">拡張した距離</param>
	public void PlayerAttack(MyPrism12 attackPrism12, Vector3 pos, MaskAttribute attribute, int power = 0, float time = -1, bool isDeathblowTrriger = false, float expansionTime = 0, float expansionDistance = 0)
	{
		//攻撃範囲の生成
		var attackRange = Instantiate(!isDeathblowTrriger ? PlayerAttackRange : PlayerAttackDeathblowRange, transform);
		m_workAttack = attackRange.GetComponent<MyAttack>();

		//攻撃範囲の調整
		AdjustAttackRange(attackRange, attackPrism12);

		//攻撃の詳細
		m_workAttack.Pos = pos;
		m_workAttack.Attribute = attribute;
		m_workAttack.Power = power;
		m_workAttack.ExpansionTime = expansionTime;
		m_workAttack.ExpansionDistance = expansionDistance;
		m_workAttack.Kind = AttackKind.MyPrism12;
		m_workAttack.Prism12 = attackPrism12;

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
	/// 攻撃範囲の調整
	/// </summary>
	/// <param name="attackRange">攻撃範囲</param>
	/// <param name="attackPrism12">攻撃用の12角柱</param>
	void AdjustAttackRange(GameObject attackRange, MyPrism12 attackPrism12)
	{
		var mf = attackRange.GetComponent<MeshFilter>();

		//頂点の設定
		mf.mesh.vertices = attackPrism12.GetVertices();
		mf.mesh.triangles = MyPrism12.COMBINATION_VERTICES;
		mf.mesh.RecalculateBounds(); //メッシュコンポーネントのプロパティboundsを再計算する
		mf.mesh.RecalculateNormals();

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
	//インスタンスの再コピー
	void ReproduceInstance()
	{
		m_boss = myCharacter.BossScript;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技１を開始する
	/// </summary>
	public void StartDeathblow1()
	{
		//インスタンスの再コピー
		ReproduceInstance();

		//攻撃マネージャが操作する
		m_game.SetManipulateMainObject(true);

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
	/// 必殺技２を開始する
	/// </summary>
	public void StartDeathblow2()
	{
		//インスタンスの再コピー
		ReproduceInstance();

		//攻撃マネージャが操作する
		m_game.SetManipulateMainObject(true);

		//必殺技設定
		m_countTimeDeathblow = 0;
		m_numDeathblow = NumDeathblow.Dethblow2;

		//キャラクター設定
		m_playerStateNum = int.MaxValue;
		m_bossStateNum = int.MaxValue;
		m_cameraStateNum = int.MaxValue;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技３を開始する
	/// </summary>
	public void StartDeathblow3()
	{
		//インスタンスの再コピー
		ReproduceInstance();

		//攻撃マネージャが操作する
		m_game.SetManipulateMainObject(true);

		//必殺技設定
		m_countTimeDeathblow = 0;
		m_numDeathblow = NumDeathblow.Dethblow3;

		//キャラクター設定
		m_playerStateNum = int.MaxValue;
		m_bossStateNum = int.MaxValue;
		m_cameraStateNum = int.MaxValue;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 必殺技４を開始する
	/// </summary>
	public void StartDeathblow4()
	{
		//インスタンスの再コピー
		ReproduceInstance();

		//攻撃マネージャが操作する
		m_game.SetManipulateMainObject(true);

		//必殺技設定
		m_countTimeDeathblow = 0;
		m_numDeathblow = NumDeathblow.Dethblow4;

		//キャラクター設定
		m_playerStateNum = int.MaxValue;
		m_bossStateNum = int.MaxValue;
		m_cameraStateNum = int.MaxValue;
	}
}
