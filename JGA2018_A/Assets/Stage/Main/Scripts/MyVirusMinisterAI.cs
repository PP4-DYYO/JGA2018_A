﻿//////////////////////////////////////////////////////////////////
//
//2018/5/8～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
//////////////////////////////////////////////////////////////////


using UnityEngine;

///<summary>
///ウイルス大臣のAI
///</summary>
public class MyVirusMinisterAI : MyAiBoss
{

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 初期状態設定
    /// </summary>
    protected override void Start()
    {
        m_attackNum = 1;

        m_myObjectName = this.gameObject.name;
        m_playerObject = GameObject.Find(m_playerObjectName);
        m_hitPoint = 310;
        m_attack = 50;
        m_perceivedRange = 5;
        m_distance = 100;
        m_isAttacked = false;
        m_attackInterval = 2.0f;
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
            //距離が５より小さければ離れる
            if (m_distance < 5)
            {
                m_aimode = AIMode.LEAVE;

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
            //距離がかなり離れると見失う
            else if (m_distance > m_perceivedRange * 3)
            {
                m_aimode = AIMode.IDLE;
            }
            else
            {
                m_aimode = AIMode.ATTACK;
            }
        }

        //攻撃後次の攻撃までは離れるまたはその場に留まる
        if (m_isAttacked == true)
        {
            m_aimode = AIMode.LEAVE;
            if (m_distance > 5)
            {
                m_isAttacked = false;
                m_aimode = AIMode.IDLE;
            }
        }

        //距離によって爆弾の投げ方が変わる
        if (m_aimode == AIMode.LEAVE)
        {
            m_attackNum = 0;
            m_attack = 70;
        }
        else
        {
            m_attackNum = 1;
            m_attack = 50;
        }
        //状態によって行動を切り替える
        switch (m_aimode)
        {
            case AIMode.IDLE:
                break;
            case AIMode.ATTACK:
                //一定時間毎に攻撃をする
                if (m_gameTime >= m_attackInterval)
                {
                    NomalAttack();
                }
                break;
            case AIMode.DEFENSE:
                break;
            case AIMode.APPROACH:
                //近づいてこない
                break;
            case AIMode.LEAVE:
                //逃げながら投げる
                if (m_gameTime >= m_attackInterval)
                {
                    NomalAttack();
                }
                //離れるまたは近づく(ここは同じ)            
                this.transform.Translate(new Vector3(m_moveX, 0, m_moveZ));
                break;
        }
    }
}