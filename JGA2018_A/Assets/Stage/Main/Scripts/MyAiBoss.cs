//////////////////////////////////////////////////////////////////
//
//2018/6/25～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
//////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAiBoss : MonoBehaviour
{
    /// <summary>
    /// プレイヤーのオブジェクト
    /// </summary>
    [SerializeField]
    protected GameObject m_playerObject;
    public GameObject PlayerObject
    {
        get { return m_playerObject; }
    }

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

    /// <summary>
	/// myBombShotクラス（ウイルス大臣）
	/// </summary>
    MyBombShot myBombShot;

    /// <summary>
    /// MyArrowShotクラス（キャリー大臣）
    /// </summary>
    MyArrowShot myArrowShot;

    /// <summary>
    /// MyAttackManagerクラス
    /// </summary>
    MyAttackManager myAttackManager;

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
    /// 特殊技の使用制限数//
    /// </summary>
    [SerializeField]
    protected int m_specialAttackLimit;

    /// <summary>
    /// 特殊技の使用数//
    /// </summary>
    [SerializeField]
    protected int m_specialAttackCount;

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
        DEFENSE,
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
        myAttackManager = GameObject.Find("AttackManager").GetComponent<MyAttackManager>();
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// AIの基本行動
    /// </summary>
    protected virtual void Update()
    {
        //起動状態の時
        if (m_aimode != AIMode.WAIT)
        {
            if (m_gameTime < m_attackInterval)
            {
                m_gameTime += Time.deltaTime;
            }
            //プレイヤーとの距離
            m_distance = (m_playerObject.transform.position - m_myGameObject.transform.position).magnitude;

            //位置関係を確認して、移動の+-を変更する
            if (m_playerObject.transform.position.x > m_myGameObject.transform.position.x)
            {
                m_movingX = true;
            }
            else
            {
                m_movingX = false;
            }

            if (m_playerObject.transform.position.z > m_myGameObject.transform.position.z)
            {
                m_movingZ = true;
            }
            else
            {
                m_movingZ = false;
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
                        new Vector3(transform.position.x + randx, 0, transform.position.z + randz));
                    break;
                case "VirusMinister(Clone)":
                    transform.parent.GetComponent<MyAiManager>().
                        ThrowAwayBossMask(MaskAttribute.Virus, new Vector3(m_maskPositionObject.transform.position.x, m_maskPositionObject.transform.position.y, m_maskPositionObject.transform.position.z),
                        new Vector3(transform.position.x + randx, 0, transform.position.z + randz));
                    break;
                case "MirrorMinister(Clone)":
                    transform.parent.GetComponent<MyAiManager>().
                        ThrowAwayBossMask(MaskAttribute.Mirror, new Vector3(m_maskPositionObject.transform.position.x, m_maskPositionObject.transform.position.y, m_maskPositionObject.transform.position.z),
                        new Vector3(transform.position.x + randx, 0, transform.position.z + randz));
                    break;
                case "MagicMinister(Clone)":
                    transform.parent.GetComponent<MyAiManager>().
                        ThrowAwayBossMask(MaskAttribute.Magic, new Vector3(m_maskPositionObject.transform.position.x, m_maskPositionObject.transform.position.y, m_maskPositionObject.transform.position.z),
                        new Vector3(transform.position.x + randx, 0, transform.position.z + randz));
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
        switch (m_myObjectName)
        {
            case "CarryMinister(Clone)":
                //能力技発動条件
                if (m_hitPoint < m_maxHitPoint / 2 && m_specialAttackCount == 0 ||
                   m_hitPoint < m_maxHitPoint / 4 && m_specialAttackCount == 1 ||
                   m_hitPoint < m_maxHitPoint / 4 && m_attackNum == 1)
                {
                    SpecialAttack();
                    if (m_specialAttackCount < m_specialAttackLimit)
                    {
                        m_specialAttackCount++;
                    }
                }
                else
                {
                    GameObject.Find("BombPoint").GetComponent<MyBombShot>().Shot(m_attackNum);
                }
                m_isAttacked = true;
                break;

            case "VirusMinister(Clone)":
                //HPが一定で制限に達していないとき
                if (m_hitPoint < m_hitPoint / 4 && m_specialAttackCount < m_specialAttackLimit)
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

                    float m_length = 0.6f;
                    //頂点の位置
                    Vector3 vLDB = new Vector3(attackPoint.x - m_length, 1 + attackPoint.y - m_length, attackPoint.z - m_length);
                    Vector3 vLDF = new Vector3(attackPoint.x - m_length, 1 + attackPoint.y - m_length, attackPoint.z + m_length);
                    Vector3 vLUB = new Vector3(attackPoint.x - m_length, 1 + attackPoint.y + m_length, attackPoint.z - m_length);
                    Vector3 vLUF = new Vector3(attackPoint.x - m_length, 1 + attackPoint.y + m_length, attackPoint.z + m_length);
                    Vector3 vRDB = new Vector3(attackPoint.x + m_length, 1 + attackPoint.y - m_length, attackPoint.z - m_length);
                    Vector3 vRDF = new Vector3(attackPoint.x + m_length, 1 + attackPoint.y - m_length, attackPoint.z + m_length);
                    Vector3 vRUB = new Vector3(attackPoint.x + m_length, 1 + attackPoint.y + m_length, attackPoint.z - m_length);
                    Vector3 vRUF = new Vector3(attackPoint.x + m_length, 1 + attackPoint.y + m_length, attackPoint.z + m_length);

                    //当たり判定発生
                    MyCube attackRange = new MyCube(vLDB, vRDB, vLDF, vRDF, vLUB, vRUB, vLUF, vRUF);
                    myAttackManager.EnemyAttack(attackRange, MaskAttribute.Non, m_attack, 1);
                    Debug.Log("攻撃");
                }
                m_isAttacked = true;
                break;
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
                m_specialAttackCount += 1;
                Debug.Log("特殊技！！！");
                break;
            case "VirusMinister(Clone)":
                m_attack = 30;
                GameObject.Find("BombPoint").GetComponent<MyBombShot>().Shot(2);
                Debug.Log("特殊技！！！");
                break;
            case "MirrorMinister(Clone)":
                m_specialAttackCount += 1;
                Debug.Log("特殊技！！！");
                break;
            case "MagicMinister(Clone)":
                m_specialAttackCount += 1;
                Debug.Log("特殊技！！！");
                break;
        }
        m_gameTime = 0;
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
        Debug.Log(damage + "うけた");

        //キャリーは被ダメ時に特殊技
        if (m_myObjectName == "CarryMinister(Clone)" && m_specialFlag == true)
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

            //当たり判定発生
            MyCube attackRange = new MyCube(vLDB, vRDB, vLDF, vRDF, vLUB, vRUB, vLUF, vRUF);
            //プレイヤーを飛ばす特殊技
            myAttackManager.EnemyAttack(attackRange, MaskAttribute.Carry, 0, 0.1f);
            m_specialFlag = false;
            m_specialAttackCount += 1;
        }
        //マジックのカウンター攻撃
        else if (m_myObjectName == "MagicMinister(Clone)")
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
        }

        //HP0で死ぬ
        if (m_hitPoint <= 0)
        {
            transform.parent.GetComponent<MyAiManager>().CharacterScript.GameScript.ChangeState(StageStatus.BossDestroyed);
        }
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
