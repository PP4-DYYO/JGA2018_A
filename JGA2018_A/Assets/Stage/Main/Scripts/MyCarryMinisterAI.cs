﻿//////////////////////////////////////////////////////////////////
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
public class MyCarryMinisterAI : MyAiBoss
{

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 初期状態設定
    /// </summary>
    protected override void Start()
    {
        m_myObjectName = this.gameObject.name;
        m_playerObject = GameObject.Find(m_playerObjectName);
        m_hitPoint = 230;
        m_attack = 60;
        m_perceivedRange = 30;
        m_distance = 100;
        m_isAttacked = false;
        m_attackInterval = 4.0f;
        m_step = 0.03f;
        m_moveX = 0;
        m_moveZ = 0;
        m_movingX = false;
        m_movingZ = false;
        m_specialAttackLimit = 2;
        m_specialAttackCount = 0;
        m_playerAttacked = false;
        m_aimode = AIMode.WAIT;

        m_gameTime = m_attackInterval;

        base.Start();
    }


    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 移動、行動
    /// </summary>
    protected override void Update()
    {
        base.Update();
        if (m_aimode != AIMode.WAIT)
        {
            m_attackNum = 1;
            m_attack = 50;
            //距離が５より小さければ離れる
            if (m_distance < 5)
            {
                m_attackNum = 0;
                m_attack = 60;
                //ATTACK_INTERVALまで到達していれば攻撃する
                if (m_gameTime >= m_attackInterval)
                {
                    m_aimode = AIMode.ATTACK;
                }
                else
                {
                    m_aimode = AIMode.LEAVE;
                }
                //移動の+-切り替え
                if (m_movingX == true)
                {
                    m_moveX = -m_step;
                }
                else
                {
                    m_moveX = m_step;
                }
                if (m_movingZ == true)
                {
                    m_moveZ = -m_step;
                }
                else
                {
                    m_moveZ = m_step;
                }

            }
            //距離が15より小さければ攻撃継続
            else if (m_distance < 15)
            {
                //ATTACK_INTERVALまで到達していれば攻撃する
                if (m_gameTime >= m_attackInterval)
                {
                    m_aimode = MyAiBoss.AIMode.ATTACK;
                }
                else
                {
                    m_aimode = MyAiBoss.AIMode.IDLE;
                }
            }
            else
            {
                //それ以上離れるとターゲットから外れる
                m_aimode = MyAiBoss.AIMode.IDLE;
            }
        }

        //攻撃時に下がる
        if (m_isAttacked == true)
        {
            m_aimode = AIMode.LEAVE;
            if (m_distance > 6)
            {
                m_isAttacked = false;
                m_aimode = AIMode.IDLE;
            }
        }

        //状態によって行動を切り替える
        switch (m_aimode)
        {
            case AIMode.IDLE:
                break;
            case AIMode.ATTACK:
                //一定時間毎に攻撃をする
                NomalAttack();
                break;
            case AIMode.DEFENSE:
                break;
            case AIMode.APPROACH:
            case AIMode.LEAVE:
                //離れるまたは近づく(ここは同じ)            
                this.transform.Translate(new Vector3(m_moveX, 0, m_moveZ));
                break;
        }
    }

}