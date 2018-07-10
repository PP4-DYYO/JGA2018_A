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
    protected string m_playerObjectName = "DummyPlayer";

    public string PlayerObjectName
    {
        get { return m_playerObjectName; }
    }

    MyCharacter myCharacter;
    MyBombShot myBombShot;
    MyArrowShot myArrowShot;

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
    /// HP//
    /// </summary>
    [SerializeField] 
     protected int m_hitPoint;

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
    /// 攻撃番号（近距離、遠距離など）
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
    /// 自分の状態//
    /// </summary>
    [SerializeField]
    protected AIMode m_aimode;

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
        switch (m_myObjectName)
        {
            case "VirusMinister":
                myBombShot = GameObject.Find("BombPoint").GetComponent<MyBombShot>();
                break;
            case "CarryMinister":
                myArrowShot = GameObject.Find("ArrowPosition").GetComponent<MyArrowShot>();
                break;
            case "MirrorMinister":
                break;
            case "MagicMinister":
                break;
        }
        m_aimode = AIMode.WAIT;
        m_myGameObject = GameObject.Find(m_myObjectName);
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// AIの基本行動
    /// </summary>
    protected virtual void Update()
    {
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
            case "CarryMinister":
                //HPが一定で制限に達していないとき
                if (m_hitPoint < m_hitPoint / 4 && m_specialAttackCount < m_specialAttackLimit)
                {
                    SpecialAttack();
                }
                myArrowShot.Shot(m_attackNum);
                m_isAttacked = true;
                break;

            case "VirusMinister":
                //HPが一定で制限に達していないとき
                if (m_hitPoint < m_hitPoint / 4 && m_specialAttackCount < m_specialAttackLimit)
                {
                    SpecialAttack();
                }
                myBombShot.Shot(m_attackNum);
                m_isAttacked = true;
                break;
            case "MirrorMinister":
                //HPが一定で制限に達していないとき
                if (m_hitPoint < m_hitPoint / 4 && m_specialAttackCount < m_specialAttackLimit)
                {
                    SpecialAttack();
                }
                m_isAttacked = true;
                break;
            case "MagicMinister":
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
            case "CarryMinister":
                m_specialAttackCount += 1;
                Debug.Log("特殊技！！！");
                break;
            case "VirusMinister":
                m_attack = 30;
                m_specialAttackCount += 1;
                Debug.Log("特殊技！！！");
                break;
            case "MirrorMinister":
                m_specialAttackCount += 1;
                Debug.Log("特殊技！！！");
                break;
            case "MagicMinister":
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
    public void ReceiveDamage(int damage)
    {
        m_hitPoint = m_hitPoint - damage;
        ReceiveDamageAnimation();
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
