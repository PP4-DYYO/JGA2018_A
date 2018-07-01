//////////////////////////////////////////////////////////////////
//
//2018/5/21～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
//////////////////////////////////////////////////////////////////

using System;
using UnityEditor;
using UnityEngine;

///<summary>
///キャリー大臣のAI
///</summary>
public class MyCarryMinisterAI : MonoBehaviour
{

    /// <summary>
    /// 行動制御用(時間)
    /// </summary>
    int m_gameTime;

    /// <summary>
    //スクリプト参照用//
    /// </summary>
    MyAiBoss m_MyAiBoss;

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 初期状態設定
    /// </summary>
    void Start()
    {
        m_MyAiBoss = this.GetComponent<MyAiBoss>();

        m_MyAiBoss.m_myObjectName = this.gameObject.name;
        m_MyAiBoss.m_playerObject = GameObject.Find(m_MyAiBoss.m_playerObjectName);
        m_MyAiBoss.m_hitPoint = 230;
        m_MyAiBoss.m_attack = 60;
        m_MyAiBoss.m_perceivedRange = 30;
        m_MyAiBoss.m_distance = 100;
        m_MyAiBoss.m_isAttacked = false;
        m_MyAiBoss.m_attackInterval = 240;
        m_MyAiBoss.m_step = 0.03f;
        m_MyAiBoss.m_moveX = 0;
        m_MyAiBoss.m_moveZ = 0;
        m_MyAiBoss.m_movingX = false;
        m_MyAiBoss.m_movingZ = false;
        m_MyAiBoss.m_specialAttackLimit = 2;
        m_MyAiBoss.m_specialAttackCount = 0;
        m_MyAiBoss.m_playerAttacked = false;
        m_MyAiBoss.m_aimode = MyAiBoss.AIMode.WAIT;

        m_gameTime = m_MyAiBoss.m_attackInterval;
    }


    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 移動、行動
    /// </summary>
    void Update()
    {
        if (m_gameTime < m_MyAiBoss.m_attackInterval)
        {
            m_gameTime++;
        }

        if (m_MyAiBoss.m_aimode != MyAiBoss.AIMode.WAIT)
        {
            m_MyAiBoss.m_attackNum = 1;
            m_MyAiBoss.m_attack = 50;
            //距離が５より小さければ離れる
            if (m_MyAiBoss.m_distance < 5)
            {
                m_MyAiBoss.m_attackNum = 0;
                m_MyAiBoss.m_attack = 60;
                //ATTACK_INTERVALまで到達していれば攻撃する
                if (m_gameTime >= m_MyAiBoss.m_attackInterval)
                {
                    m_MyAiBoss.m_aimode = MyAiBoss.AIMode.ATTACK;
                }
                else
                {
                    m_MyAiBoss.m_aimode = MyAiBoss.AIMode.LEAVE;
                }
                //移動の+-切り替え
                if (m_MyAiBoss.m_movingX == true)
                {
                    m_MyAiBoss.m_moveX = -m_MyAiBoss.m_step;
                }
                else
                {
                    m_MyAiBoss.m_moveX = m_MyAiBoss.m_step;
                }
                if (m_MyAiBoss.m_movingZ == true)
                {
                    m_MyAiBoss.m_moveZ = -m_MyAiBoss.m_step;
                }
                else
                {
                    m_MyAiBoss.m_moveZ = m_MyAiBoss.m_step;
                }

            }
            //距離が15より小さければ攻撃継続
            else if (m_MyAiBoss.m_distance < 15)
            {
                //ATTACK_INTERVALまで到達していれば攻撃する
                if (m_gameTime >= m_MyAiBoss.m_attackInterval)
                {
                    m_MyAiBoss.m_aimode = MyAiBoss.AIMode.ATTACK;
                }
                else
                {
                    m_MyAiBoss.m_aimode = MyAiBoss.AIMode.IDLE;
                }
            }
            else
            {
                //それ以上離れるとターゲットから外れる
                m_MyAiBoss.m_aimode = MyAiBoss.AIMode.IDLE;
            }
        }

        //攻撃時に下がる
        if (m_MyAiBoss.m_isAttacked == true)
        {
            m_MyAiBoss.m_aimode = MyAiBoss.AIMode.LEAVE;
            if (m_MyAiBoss.m_distance > 6)
            {
                m_MyAiBoss.m_isAttacked = false;
                m_MyAiBoss.m_aimode = MyAiBoss.AIMode.IDLE;
            }
        }

        //状態によって行動を切り替える
        switch (m_MyAiBoss.m_aimode)
        {
            case MyAiBoss.AIMode.IDLE:
                break;
            case MyAiBoss.AIMode.ATTACK:
                //一定時間毎に攻撃をする
                m_MyAiBoss.NomalAttack();
                break;
            case MyAiBoss.AIMode.DEFENSE:
                break;
            case MyAiBoss.AIMode.APPROACH:
            case MyAiBoss.AIMode.LEAVE:
                //離れるまたは近づく(ここは同じ)            
                this.transform.Translate(new Vector3(m_MyAiBoss.m_moveX, 0, m_MyAiBoss.m_moveZ));
                break;
        }
    }

}
