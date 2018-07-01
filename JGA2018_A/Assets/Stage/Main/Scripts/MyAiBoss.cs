//////////////////////////////////////////////////////////////////
//
//2018/6/25～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
//////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAiBoss : MonoBehaviour {

    /// <summary>
    /// プレイヤーのオブジェクト
    /// </summary>
   public GameObject m_playerObject;

    /// <summary>
    /// プレイヤーオブジェクトの名前
    /// </summary>
   public string m_playerObjectName = "DummyPlayer";

    MyCharacter myCharacter;
    MyBombShot myBombShot;
    MyArrowShot myArrowShot;


    /// <summary>
    /// 自分の名前"CarryMinister","VirusMinister","MirrorMinister","MagicMinister"
    /// </summary>
    public string m_myObjectName;

    /// <summary>
    /// HP//
    /// </summary>
    public int m_hitPoint;

    /// <summary>
    /// 攻撃力
    /// </summary>
    public int m_attack;

    /// <summary>
    /// 攻撃番号（近距離、遠距離など）
    /// </summary>
    public int m_attackNum;

    /// <summary>
    /// 知覚範囲
    /// </summary>
    public int m_perceivedRange;


    /// <summary>
    /// 攻撃後か
    /// </summary>
    public bool m_isAttacked;

    /// <summary>
    /// 攻撃間隔//
    /// </summary>
    public int m_attackInterval;

    /// <summary>
    /// 一歩の移動距離//
    /// </summary>
    public float m_step ;

    /// <summary>
    /// xへの移動はt:プラス/f:マイナス//
    /// </summary>
    public bool m_movingX;

    /// <summary>
    /// zへの移動はt:プラス/f:マイナス//
    /// </summary>
    public bool m_movingZ;

    /// <summary>
    /// xzそれぞれの移動量//
    /// </summary>
    public float m_moveX;
    public float m_moveZ;

    /// <summary>
    /// 特殊技の使用制限数//
    /// </summary>
    public int m_specialAttackLimit;

    /// <summary>
    /// 特殊技の使用数//
    /// </summary>
    public int m_specialAttackCount;

    /// <summary>
    /// プレイヤーが攻撃してきたフラグ
    /// </summary>
    public bool m_playerAttacked;

    /// <summary>
    /// プレイヤーとの距離
    /// </summary>
    public float m_distance;

    /// <summary>
    /// 自分の状態//
    /// </summary>
    public AIMode m_aimode;

    /// <summary>
    /// 行動制御用(時間)
    /// </summary>
    public int m_gameTime;

    /// <summary>
    /// AIの行動タイプ
    /// </summary>
    public enum AIMode
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
    void Start () {
        switch (m_myObjectName) {
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
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// AIの基本行動
    /// </summary>
    void Update () {
        Debug.Log("現在["+m_aimode+"]状態");
        if (m_aimode != AIMode.WAIT)
        {
            //プレイヤーとの距離
            m_distance = (m_playerObject.transform.position - this.gameObject.transform.position).magnitude;

            //位置関係を確認して、移動の+-を変更する
            if (m_playerObject.transform.position.x > this.gameObject.transform.position.x)
            {
                m_movingX = true;
            }
            else
            {
                m_movingX = false;
            }

            if (m_playerObject.transform.position.z > this.gameObject.transform.position.z)
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
                m_gameTime = 0;
                myArrowShot.Shot(m_attackNum);
                m_isAttacked = true;
                break;

            case "VirusMinister":
                //HPが一定で制限に達していないとき
                if (m_hitPoint < m_hitPoint / 4 && m_specialAttackCount < m_specialAttackLimit)
                {
                    SpecialAttack();
                }
                m_gameTime = 0;
                myBombShot.Shot(m_attackNum);
                m_isAttacked = true;
                break;
            case "MirrorMinister":
                //HPが一定で制限に達していないとき
                if (m_hitPoint < m_hitPoint / 4 && m_specialAttackCount < m_specialAttackLimit)
                {
                    SpecialAttack();
                }
                m_gameTime = 0;
                m_isAttacked = true;
                break;
            case "MagicMinister":
                //HPが一定で制限に達していないとき
                if (m_hitPoint < m_hitPoint / 4 && m_specialAttackCount < m_specialAttackLimit)
                {
                    SpecialAttack();
                }
                m_gameTime = 0;
                m_isAttacked = true;
                break;
        }
    }

    //----------------------------------------------------------------------------------------------------
    ///<summary>
    ///特殊攻撃//
    ///</summary>
  public  void SpecialAttack()
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
