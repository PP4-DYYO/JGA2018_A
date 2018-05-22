//////////////////////////////////////////////////////////////////
//
//2018/5/22～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
//////////////////////////////////////////////////////////////////

using System;
using UnityEditor;
using UnityEngine;


///<summary>
///ミラー大臣のAI
///</summary>
public class MyMirrorMinisterAI : MonoBehaviour
{

    /// <summary>
    /// プレイヤーのオブジェクト
    /// </summary>
    GameObject m_playerObjct;

    /// <summary>
    /// プレイヤーオブジェクトの名前
    /// </summary>
    const string PLAYER_OBJECT_NAME = "DummyPlayer";

    /// <summary>
    /// HP//
    /// </summary>
    int MirrorMinisterHitPoint = 100;

    /// <summary>
    /// 攻撃力
    /// </summary>
    const int MIRROR_MINISTER_ATTACK = 100;

    /// <summary>
    /// 知覚範囲
    /// </summary>
    const int PERCEIVED_RANGE = 30;

    /// <summary>
    /// このAIが気づいたか
    /// </summary>
    bool isPerceived;

    /// <summary>
    /// 攻撃後か
    /// </summary>
    bool isAttacked;

    /// <summary>
    /// 攻撃間隔//
    /// </summary>
    const int ATTACK_INTERVAL = 30;

    /// <summary>
    /// 一歩の移動距離//
    /// </summary>
    const float step = 0.06f;
    /// <summary>
    /// xへの移動はt:プラス/f:マイナス//
    /// </summary>
    bool moveX;

    /// <summary>
    /// zへの移動はt:プラス/f:マイナス//
    /// </summary>
    bool moveZ;

    /// <summary>
    /// xzそれぞれの移動量//
    /// </summary>
    float m_moveX;
    float m_moveZ;

    /// <summary>
    /// 特殊技の使用制限数//
    /// </summary>
    const int SPECIAL_ATTACKLIMIT = 2;

    /// <summary>
    /// 特殊技の使用数//
    /// </summary>
    int m_specialAttackCount;

    /// <summary>
    /// プレイヤーが攻撃してきたフラグ
    /// </summary>
    bool playerAttacked;

    /// <summary>
    /// 自分の状態//
    /// </summary>
    public AIMode aiMode;

    /// <summary>
    /// 行動制御用(時間)
    /// </summary>
    int m_gameTime;


    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 初期状態設定
    /// </summary>
    void Start()
    {  
        m_playerObjct = GameObject.Find(PLAYER_OBJECT_NAME);
        aiMode = AIMode.STOP;
        m_gameTime = ATTACK_INTERVAL;
    }

    /// <summary>
    /// AIの行動タイプ
    /// </summary>
    public enum AIMode
    {
        STOP,
        ATTACK,
        DEFENSE,
        APPROACH,
        LEAVE
    }


    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 移動、行動
    /// </summary>
    void Update()
    {
        //プレイヤーとの距離
        float m_distance = (m_playerObjct.transform.position - this.gameObject.transform.position).magnitude;

        //位置関係を確認して、移動の+-を変更する
        if (m_playerObjct.transform.position.x > this.gameObject.transform.position.x)
        {
            moveX = true;
        }
        else
        {
            moveX = false;
        }

        if (m_playerObjct.transform.position.z > this.gameObject.transform.position.z)
        {
            moveZ = true;
        }
        else
        {
            moveZ = false;
        }

        //Debug.Log("距離は" + m_distance);

        //知覚範囲に入れば気づいた状態に遷移する
        if (m_distance < PERCEIVED_RANGE)
        {
            isPerceived = true;
        }

        if (m_gameTime < ATTACK_INTERVAL)
        {
            m_gameTime++;
        }
        if (isPerceived)
        {
            //距離が0.5より小さければ離れる
            if (m_distance < 0.5)
            {
                //近距離爆弾
                //  ArrowNumber = 0;

                //ATTACK_INTERVALまで到達していれば攻撃する
                if (m_gameTime >= ATTACK_INTERVAL)
                {
                    aiMode = AIMode.ATTACK;
                }
                else
                {
                    aiMode = AIMode.LEAVE;
                }
                //移動の+-切り替え
                if (moveX == true)
                {
                    m_moveX = -step;
                }
                else
                {
                    m_moveX = step;
                }
                if (moveZ == true)
                {
                    m_moveZ = -step;
                }
                else
                {
                    m_moveZ = step;
                }

            }
            //距離が2より小さければ攻撃継続
            else if (m_distance < 2)
            {
                //ATTACK_INTERVALまで到達していれば攻撃する
                if (m_gameTime >= ATTACK_INTERVAL)
                {
                    aiMode = AIMode.ATTACK;
                }
                else
                {
                    aiMode = AIMode.STOP;
                }
            }
            else
            {
                //それ以上離れると近づく
                aiMode = AIMode.APPROACH;
                //移動の+-切り替え
                if (moveX == true)
                {
                    m_moveX = step;
                }
                else
                {
                    m_moveX = -step;
                }
                if (moveZ == true)
                {
                    m_moveZ = step;
                }
                else
                {
                    m_moveZ = -step;
                }
            }
        }

        //状態によって行動を切り替える
        switch (aiMode)
        {
            case AIMode.STOP:
                break;
            case AIMode.ATTACK:
                //一定時間毎に攻撃をする
                NomalAttack();
                break;
            case AIMode.DEFENSE:
                break;
            case AIMode.APPROACH:
                //近づく(y座標は固定)            
               this.transform.position=Vector3.MoveTowards(
                  new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z),
                  new Vector3( m_playerObjct.transform.position.x, this.transform.position.y, m_playerObjct.transform.position.z),
                  step);
                break;
            case AIMode.LEAVE:
                //離れる            
                this.transform.Translate(new Vector3(m_moveX, 0, m_moveZ));
                break;
        }
    }

    //----------------------------------------------------------------------------------------------------
    ///<summary>
    ///爆弾投げ//通常攻撃
    ///</summary>
    void NomalAttack()
    {
        //HPが一定で制限に達していないとき
        if (MirrorMinisterHitPoint < MirrorMinisterHitPoint / 4 && m_specialAttackCount < SPECIAL_ATTACKLIMIT)
        {
            SpecialAttack();
        }
        m_gameTime = 0;
        isAttacked = true;
        Debug.Log("攻撃!");
    }

    //----------------------------------------------------------------------------------------------------
    ///<summary>
    ///特殊攻撃//hpが1/4の時など
    ///</summary>
    void SpecialAttack()
    {
        m_specialAttackCount += 1;
        Debug.Log("特殊技！！！");
    }
}
