////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/6/25～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyAiBoss : MonoBehaviour
{

    protected MyPlayer myPlayer;

    MyAiManager myAiManager;

    /// <summary>
    /// プレイヤーオブジェクトの名前
    /// </summary>
    [SerializeField]
    protected string PLAYER_OBJECT_NAME = "DummyPlayer";
    public string PlayerObjectName
    {
        get { return PLAYER_OBJECT_NAME; }
    }

    /// <summary>
	/// MyCharacterクラス
	/// </summary>
    MyCharacter myCharacter;
    public MyCharacter CharacterScript
    {
        get { return myCharacter; }
    }
	
    /// <summary>
    /// MyAttackManagerクラス
    /// </summary>
    MyAttackManager myAttackManager;
    public MyAttackManager AttackManagerScript
    {
        get { return myAttackManager; }
    }

    /// <summary>
    /// 自分のタグ名
    /// </summary>
    public const string TAG_NAME = "Boss";

    /// <summary>
    /// 自分の名前"CarryMinister","VirusMinister","MirrorMinister","MagicMinister"
    /// </summary>
    [SerializeField]
    protected string m_myObjectName;

    /// <summary>
    /// 自分のゲームオブジェクト
    /// </summary>
    [SerializeField]
    protected GameObject m_myGameObject;

    /// <summary>
    /// ステージのオブジェクト
    /// </summary>
    [SerializeField]
    protected MyStage myStage;

    /// <summary>
    /// マスクの位置用ゲームオブジェクトの名前
    /// </summary>
    [SerializeField]
    const string MASK_POSITION_OBJECT_NAME= "MaskPositionObject";
    public string MaskPositionObjectName
    {
        get { return MASK_POSITION_OBJECT_NAME; }
    }

    /// <summary>
    /// マスクの位置用ゲームオブジェクト
    /// </summary>
    [SerializeField]
    protected GameObject m_maskPositionObject;


    /// <summary>
    /// MAXHP//
    /// </summary>
    [SerializeField]
    protected int m_maxHitPoint;
    public int MaxHitPoint
    {
        get { return m_maxHitPoint; }
    }

    /// <summary>
    /// HP//
    /// </summary>
    protected int m_hitPoint;
    public int HitPoint
    {
        get { return m_hitPoint; }
    }

    /// <summary>
    /// 攻撃力
    /// </summary>
    [SerializeField]
    protected int m_attack;
    public int Attack
    {
        get { return m_attack; }
    }

    /// <summary>
    /// 攻撃番号（0:近距離、1:遠距離 2:スペシャル）
    /// </summary>
    [SerializeField]
    protected int m_attackNum;


    /// <summary>
    /// 知覚範囲
    //</summary>
    [SerializeField]
    protected int m_perceivedRange;


    /// <summary>
    /// 攻撃後か
    /// </summary>
    [SerializeField]
    protected bool m_isAttacked;

    /// <summary>
    /// 攻撃間隔//
    /// </summary>
    [SerializeField]
    protected float m_attackInterval;

    /// <summary>
    /// 一歩の移動距離//
    /// </summary>
    [SerializeField]
    protected float m_step;

    /// <summary>
    /// xへの移動はt:プラス/f:マイナス//
    /// </summary>
    [SerializeField]
    protected bool m_movingX;

    /// <summary>
    /// zへの移動はt:プラス/f:マイナス//
    /// </summary>
    [SerializeField]
    protected bool m_movingZ;

    /// <summary>
    /// xzそれぞれの移動量//
    /// </summary>
    [SerializeField]
    protected float m_moveX;
    [SerializeField]
    protected float m_moveZ;

    /// <summary>
    /// 攻撃回数カウント
    /// </summary>
    [SerializeField]
    protected int m_attackCount;

    public int AttackCount
    {
        get { return m_attackCount; }
    }

    /// <summary>
    /// 特殊技の使用制限数//
    /// </summary>
    [SerializeField]
    protected int m_specialAttackLimit;

    /// <summary>
    /// 特殊技の使用数//
    /// </summary>
    [SerializeField]
    protected int m_specialAttackCount;

    ///<summary>
    ///マジック用カウンターフラグ
    ///</summary>>
    [SerializeField]
    protected bool m_counterAttackFlag;

    /// <summary>
    /// プレイヤーが攻撃してきたフラグ
    /// </summary>
    [SerializeField]
    protected bool m_playerAttacked;

    /// <summary>
    /// プレイヤーとの距離
    /// </summary>
    [SerializeField]
    protected float m_distance;

    /// <summary>
    /// 仮面を捨てた
    /// </summary>
    [SerializeField]
    protected bool m_maskThrow;

    /// <summary>
    /// 毒の霧エフェクトプレファブ
    /// </summary>
    [SerializeField]
    GameObject poizonFog;

    /// <summary>
    /// 自分の状態//
    /// </summary>
    [SerializeField]
    protected AIMode m_aimode;

    /// <summary>
    /// 特殊技待機状態
    /// </summary>
    [SerializeField]
    protected bool m_specialFlag;

    /// <summary>
    /// 行動制御用(時間)
    /// </summary>
    [SerializeField]
    protected float m_gameTime;

    /// <summary>
    /// AIの行動タイプ
    /// </summary>
    [SerializeField]
    protected enum AIMode
    {
        /// <summary>
        /// AI起動待ち
        /// </summary>
        WAIT,
        /// <summary>
        /// 待機
        /// </summary>
        IDLE,
        /// <summary>
        /// 攻撃
        /// </summary>
        ATTACK,
        /// <summary>
        /// 防御
        /// </summary>
        SKILL,
        /// <summary>
        /// 近づく
        /// </summary>
        APPROACH,
        /// <summary>
        /// 離れる
        /// </summary>
        LEAVE
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 初期状態
    /// </summary>
    protected virtual void Start()
    {
        //m_aimode = AIMode.WAIT;
        m_hitPoint = m_maxHitPoint;
        poizonFog = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Stage/Main/Prefab/poisonFog.prefab");
        myAiManager = transform.parent.GetComponent<MyAiManager>();
        myAttackManager = myAiManager.CharacterScript.AttackManagerScript;
        myStage = myAiManager.CharacterScript.GameScript.StageScript;
        myPlayer = myAiManager.CharacterScript.PlayerScript;
        myCharacter = myAiManager.CharacterScript;
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// AIの基本行動
    /// </summary>
    protected virtual void FixedUpdate()
    {
        //起動状態の時
        if (m_aimode != AIMode.WAIT)
        {
            //ステージから落下した場合中央に戻す
            if (m_distance > 30)
            {
                PositionReset();
            }

            if (m_gameTime <= m_attackInterval)
            {
                m_gameTime += Time.deltaTime;
            }

            Vector3 targetPos = myPlayer.transform.position;
            // Y座標を固定
            targetPos.y = m_myGameObject.transform.position.y;
            //プレイヤーの方を向く
            m_myGameObject.transform.LookAt(targetPos);

            //プレイヤーとの距離
            m_distance = (myPlayer.transform.position - m_myGameObject.transform.position).magnitude;

            //位置関係を確認して、移動の+-を変更する
            if (myPlayer.transform.position.x -m_myGameObject.transform.position.x>0)
            {
                if (m_moveX != m_step)
                {
                    m_moveX = m_step;
                }
            }
            else if (myPlayer.transform.position.x - m_myGameObject.transform.position.x<0)
            {
                if (m_moveX != -m_step)
                {
                    m_moveX = -m_step;
                }
            }

            if (myPlayer.transform.position.z - m_myGameObject.transform.position.z>0)
            {
                if (m_moveZ != m_step)
                {
                    m_moveZ = m_step;
                }
            }
            else if (myPlayer.transform.position.z - m_myGameObject.transform.position.z<0)
            {
                if (m_moveZ != -m_step)
                {
                    m_moveZ = -m_step;
                }
            }
        }

        //マスクを捨てる
        if (m_hitPoint < m_maxHitPoint / 2 && m_maskThrow == false)
        {
            float randx = Random.Range(1, 3);
            float randz = Random.Range(1, 3);
            switch (m_myObjectName)
            {
                case "CarryMinister(Clone)":
                    transform.parent.GetComponent<MyAiManager>().
                        ThrowAwayBossMask(MaskAttribute.Carry, new Vector3(m_maskPositionObject.transform.position.x, m_maskPositionObject.transform.position.y, m_maskPositionObject.transform.position.z),
                        myStage.CurrentField.BossRoomCenterPos);
                    break;
                case "VirusMinister(Clone)":
                    transform.parent.GetComponent<MyAiManager>().
                        ThrowAwayBossMask(MaskAttribute.Virus, new Vector3(m_maskPositionObject.transform.position.x, m_maskPositionObject.transform.position.y, m_maskPositionObject.transform.position.z),
                        myStage.CurrentField.BossRoomCenterPos);
                    break;
                case "MirrorMinister(Clone)":
                    transform.parent.GetComponent<MyAiManager>().
                        ThrowAwayBossMask(MaskAttribute.Mirror, new Vector3(m_maskPositionObject.transform.position.x, m_maskPositionObject.transform.position.y, m_maskPositionObject.transform.position.z),
                       myStage.CurrentField.BossRoomCenterPos);
                    break;
                case "MagicMinister(Clone)":
                    transform.parent.GetComponent<MyAiManager>().
                        ThrowAwayBossMask(MaskAttribute.Magic, new Vector3(m_maskPositionObject.transform.position.x, m_maskPositionObject.transform.position.y, m_maskPositionObject.transform.position.z),
                       myStage.CurrentField.BossRoomCenterPos);
                    break;
            }
            m_maskThrow = true;
        }
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// AIを起動する
    /// </summary>
    public void StartAI()
    {
        m_aimode = AIMode.IDLE;
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 通常攻撃
    /// </summary>
    public void NomalAttack()
    {
        Debug.Log("koko");

        switch (m_myObjectName)
        {
            case "CarryMinister(Clone)":
                //能力技発動条件
                if (m_hitPoint < m_maxHitPoint / 2 && m_specialAttackCount == 0 ||
                   m_hitPoint < m_maxHitPoint / 4 && m_specialAttackCount == 1)
                {
                    SpecialAttack();
                    m_specialAttackCount++;
                }
                else
                {
                    GameObject.Find("ArrowPoint").GetComponent<MyArrowShot>().Shot(m_attackNum);
                }
                m_isAttacked = true;
                break;
            case "VirusMinister":
            case "VirusMinister(Clone)":
                Debug.Log(m_hitPoint);
                Debug.Log(m_maxHitPoint / 4);
                //HPが一定で制限に達していないとき
                if (m_hitPoint < m_maxHitPoint / 2 && m_specialAttackCount ==0||
                    m_hitPoint < m_maxHitPoint / 4 && m_specialAttackCount == 1||
                      m_specialAttackCount>1&&m_distance<5)
                {
                    SpecialAttack();
                }
                GameObject.Find("BombPoint").GetComponent<MyBombShot>().Shot(m_attackNum);
                m_isAttacked = true;
                break;
            case "MirrorMinister(Clone)":
            case "MirrorMinister(Clone)(Clone)":
                Debug.Log("攻撃");
                //HPが一定で制限に達していないとき
                if (m_hitPoint < m_hitPoint / 4 && m_specialAttackCount < m_specialAttackLimit)
                {
                    SpecialAttack();
                }
                else
                {
                    //当たり判定Cube
                    Vector3 attackPoint = GameObject.Find(m_myObjectName).transform.position;

                    float cubeLength = 0.6f;
                    //頂点の位置
                    Vector3 vLDB = new Vector3(attackPoint.x - cubeLength, 1 + attackPoint.y - cubeLength, attackPoint.z - cubeLength);
                    Vector3 vLDF = new Vector3(attackPoint.x - cubeLength, 1 + attackPoint.y - cubeLength, attackPoint.z + cubeLength);
                    Vector3 vLUB = new Vector3(attackPoint.x - cubeLength, 1 + attackPoint.y + cubeLength, attackPoint.z - cubeLength);
                    Vector3 vLUF = new Vector3(attackPoint.x - cubeLength, 1 + attackPoint.y + cubeLength, attackPoint.z + cubeLength);
                    Vector3 vRDB = new Vector3(attackPoint.x + cubeLength, 1 + attackPoint.y - cubeLength, attackPoint.z - cubeLength);
                    Vector3 vRDF = new Vector3(attackPoint.x + cubeLength, 1 + attackPoint.y - cubeLength, attackPoint.z + cubeLength);
                    Vector3 vRUB = new Vector3(attackPoint.x + cubeLength, 1 + attackPoint.y + cubeLength, attackPoint.z - cubeLength);
                    Vector3 vRUF = new Vector3(attackPoint.x + cubeLength, 1 + attackPoint.y + cubeLength, attackPoint.z + cubeLength);

                    //当たり判定発生
                    MyCube attackRange = new MyCube(vLDB, vRDB, vLDF, vRDF, vLUB, vRUB, vLUF, vRUF);
                    myAttackManager.EnemyAttack(attackRange, MaskAttribute.Non, m_attack, 1);
                    Debug.Log("攻撃");
                }
                m_isAttacked = true;
                break;
            case "MagicMinister":
            case "MagicMinister(Clone)":
                //HPが一定で制限に達していないとき
                if (m_hitPoint < m_hitPoint / 4 && m_specialAttackCount < m_specialAttackLimit)
                {
                    SpecialAttack();
                }
                m_isAttacked = true;
                break;
        }
        m_gameTime = 0;
        m_attackCount += 1;
    }

    //----------------------------------------------------------------------------------------------------
    ///<summary>
    ///特殊攻撃//
    ///</summary>
    public void SpecialAttack()
    {
        switch (m_myObjectName)
        {
            case "CarryMinister(Clone)":
                float warpPosX, warpPosY, warpPosZ;
                float randamX = Random.Range(-4,4);
                float randamZ = Random.Range(-4, 4);
                warpPosX = myStage.CurrentField.BossRoomCenterPos.x+randamX;
                warpPosY = myStage.CurrentField.BossRoomCenterPos.y+1;
                warpPosZ = myStage.CurrentField.BossRoomCenterPos.z+ randamZ;
                myPlayer.transform.position = new Vector3(warpPosX, warpPosY, warpPosZ);
                break;
            case "VirusMinister(Clone)":
                m_attack = 5;
                Vector3 attackPoint = GameObject.Find(m_myObjectName).transform.position;

                GameObject poizonfog = GameObject.Instantiate(poizonFog) as GameObject;
                poizonfog.transform.position = m_myGameObject.transform.position;

                float cubeLength = 2f;
                //頂点の位置
                Vector3 vLDB = new Vector3(attackPoint.x - cubeLength, 1 + attackPoint.y - cubeLength, attackPoint.z - cubeLength);
                Vector3 vLDF = new Vector3(attackPoint.x - cubeLength, 1 + attackPoint.y - cubeLength, attackPoint.z + cubeLength);
                Vector3 vLUB = new Vector3(attackPoint.x - cubeLength, 1 + attackPoint.y + cubeLength, attackPoint.z - cubeLength);
                Vector3 vLUF = new Vector3(attackPoint.x - cubeLength, 1 + attackPoint.y + cubeLength, attackPoint.z + cubeLength);
                Vector3 vRDB = new Vector3(attackPoint.x + cubeLength, 1 + attackPoint.y - cubeLength, attackPoint.z - cubeLength);
                Vector3 vRDF = new Vector3(attackPoint.x + cubeLength, 1 + attackPoint.y - cubeLength, attackPoint.z + cubeLength);
                Vector3 vRUB = new Vector3(attackPoint.x + cubeLength, 1 + attackPoint.y + cubeLength, attackPoint.z - cubeLength);
                Vector3 vRUF = new Vector3(attackPoint.x + cubeLength, 1 + attackPoint.y + cubeLength, attackPoint.z + cubeLength);

                ////当たり判定発生
                MyCube attackRange = new MyCube(vLDB, vRDB, vLDF, vRDF, vLUB, vRUB, vLUF, vRUF);
                myAttackManager.EnemyAttack(attackRange, MaskAttribute.Virus, m_attack, 2);
                Debug.Log("特殊技！！！");
                m_attack = 25;
                break;
            case "MirrorMinister(Clone)":
                break;
            case "MagicMinister(Clone)":
                Debug.Log("特殊技！！！");
                break;
        }
        m_gameTime = 0;
        m_specialAttackCount += 1;
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    ///damage分のダメージを受ける
    /// </summary>
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == AttackManagerTag.PLAYER_ATTACK_RANGE_TAG)
        {
            ReceiveDamage(other.GetComponent<MyAttack>().Power);
        }
    }


    //----------------------------------------------------------------------------------------------------
    /// <summary>
    ///damage分のダメージを受ける
    /// </summary>
    public void ReceiveDamage(int damage)
    {
        m_hitPoint = m_hitPoint - damage;
        ReceiveDamageAnimation();
        Debug.Log("プレイヤーは" +m_myObjectName+"に"+ damage + "を与えた");

        //キャリーは被ダメ時に特殊技
        if (m_myObjectName == "CarryMinister(Clone)" && m_hitPoint < m_maxHitPoint/2 &&m_specialAttackCount==0||
           m_myObjectName == "CarryMinister(Clone)" && m_hitPoint < m_maxHitPoint / 4 && m_specialAttackCount == 1)
        {
            SpecialAttack();
            m_specialFlag = false;
        }
        //マジックのカウンター攻撃
        else if (m_myObjectName == "MagicMinister(Clone)"&&m_counterAttackFlag==true)
        {
            float m_length = 0.6f;

            Vector3 vLDB = new Vector3(m_myGameObject.transform.position.x - m_length, m_myGameObject.transform.position.y - m_length, m_myGameObject.transform.position.z - m_length);
            Vector3 vLDF = new Vector3(m_myGameObject.transform.position.x - m_length, m_myGameObject.transform.position.y - m_length, m_myGameObject.transform.position.z + m_length);
            Vector3 vLUB = new Vector3(m_myGameObject.transform.position.x - m_length, m_myGameObject.transform.position.y + m_length, m_myGameObject.transform.position.z - m_length);
            Vector3 vLUF = new Vector3(m_myGameObject.transform.position.x - m_length, m_myGameObject.transform.position.y + m_length, m_myGameObject.transform.position.z + m_length);
            Vector3 vRDB = new Vector3(m_myGameObject.transform.position.x + m_length, m_myGameObject.transform.position.y - m_length, m_myGameObject.transform.position.z - m_length);
            Vector3 vRDF = new Vector3(m_myGameObject.transform.position.x + m_length, m_myGameObject.transform.position.y - m_length, m_myGameObject.transform.position.z + m_length);
            Vector3 vRUB = new Vector3(m_myGameObject.transform.position.x + m_length, m_myGameObject.transform.position.y + m_length, m_myGameObject.transform.position.z - m_length);
            Vector3 vRUF = new Vector3(m_myGameObject.transform.position.x + m_length, m_myGameObject.transform.position.y + m_length, m_myGameObject.transform.position.z + m_length);

            //当たり判定発生と攻撃
            MyCube attackRange = new MyCube(vLDB, vRDB, vLDF, vRDF, vLUB, vRUB, vLUF, vRUF);
            myAttackManager.EnemyAttack(attackRange, MaskAttribute.Non, 0, 0.1f);
            m_counterAttackFlag = false;
        }

        //HP0で死ぬ
        if (m_hitPoint <= 0)
        {
            transform.parent.GetComponent<MyAiManager>().CharacterScript.GameScript.ChangeState(StageStatus.BossDestroyed);
        }
    }

     void PositionReset()
    {
       m_myGameObject.transform.position= myStage.CurrentField.BossRoomCenterPos;
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    ///被ダメアニメーション
    /// </summary>
    public void ReceiveDamageAnimation()
    {
        Debug.Log("被ダメアニメーション");
    }
}
