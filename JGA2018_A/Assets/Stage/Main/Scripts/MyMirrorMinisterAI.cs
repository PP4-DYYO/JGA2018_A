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
public class MyMirrorMinisterAI : MyAiBoss
{

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 初期状態設定
    /// </summary>
    protected override void Start()
    {
        m_attackNum = 0;
        m_myObjectName = this.gameObject.name;
        m_myGameObject= GameObject.Find(m_myObjectName);
        m_playerObject = GameObject.Find(m_playerObjectName);
        m_maxHitPoint = 400;
        m_attack = 60;
        m_perceivedRange = 5;
        m_distance = 100;
        m_isAttacked = false;
        m_attackInterval = 0.5f;
        m_step = 0.06f;
        m_moveX = 0;
        m_moveZ = 0;
        m_movingX = false;
        m_movingZ = false;
        m_specialAttackLimit = 2;
        m_specialAttackCount = 0;
        m_playerAttacked = false;
        m_aimode = AIMode.WAIT;

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
            //距離が0.5より小さければ離れる
            if (m_distance < 0.5)
            {

                //ATTACK_INTERVALまで到達していれば攻撃する
                if (m_gameTime >= m_attackInterval)
                {
                    m_aimode = AIMode.ATTACK;
                }
                else
                {
                    m_aimode = AIMode.LEAVE;
                }
            }
            //距離が2より小さければ攻撃継続
            else if (m_distance < 2)
            {
                //ATTACK_INTERVALまで到達していれば攻撃する
                if (m_gameTime >= m_attackInterval)
                {
                    m_aimode = AIMode.ATTACK;
                }
                else
                {
                    m_aimode = AIMode.IDLE;
                }
            }
            else
            {
                //それ以上離れると近づく
                m_aimode = AIMode.APPROACH;
                //移動の+-切り替え
                if (m_movingX == true)
                {
                    m_moveX = m_step;
                }
                else
                {
                    m_moveX = -m_step;
                }
                if (m_movingZ == true)
                {
                    m_moveZ = m_step;
                }
                else
                {
                    m_moveZ = -m_step;
                }
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
                //近づく(y座標は固定)            
                this.transform.position = Vector3.MoveTowards(
                   new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z),
                   new Vector3(m_playerObject.transform.position.x, this.transform.position.y, m_playerObject.transform.position.z),
                   m_step);
                break;
            case AIMode.LEAVE:
                //離れる            
                this.transform.Translate(new Vector3(m_moveX, 0, m_moveZ));
                break;
        }
    }

}
