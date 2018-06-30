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
        m_MyAiBoss.m_attackNum = 0;

        m_MyAiBoss.m_myObjectName = this.gameObject.name;
        m_MyAiBoss.m_playerObject = GameObject.Find(m_MyAiBoss.m_playerObjectName);
        m_MyAiBoss.m_hitPoint = 400;
        m_MyAiBoss.m_attack = 60;
        m_MyAiBoss.m_perceivedRange = 5;
        m_MyAiBoss.m_distance = 100;
        m_MyAiBoss.m_isAttacked = false;
        m_MyAiBoss.m_attackInterval = 30;
        m_MyAiBoss.m_step = 0.06f;
        m_MyAiBoss.m_moveX = 0;
        m_MyAiBoss.m_moveZ = 0;
        m_MyAiBoss.m_movingX = false;
        m_MyAiBoss.m_movingZ = false;
        m_MyAiBoss.m_specialAttackLimit = 2;
        m_MyAiBoss.m_specialAttackCount = 0;
        m_MyAiBoss.m_playerAttacked = false;
        m_MyAiBoss.m_aimode = MyAiBoss.AIMode.WAIT;
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
            //距離が0.5より小さければ離れる
            if (m_MyAiBoss.m_distance < 0.5)
            {

                //ATTACK_INTERVALまで到達していれば攻撃する
                if (m_gameTime >= m_MyAiBoss.m_attackInterval)
                {
                    m_MyAiBoss.m_aimode = MyAiBoss.AIMode.ATTACK;
                }
                else
                {
                    m_MyAiBoss.m_aimode = MyAiBoss.AIMode.LEAVE;
                }
            }
            //距離が2より小さければ攻撃継続
            else if (m_MyAiBoss.m_distance < 2)
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
                //それ以上離れると近づく
                m_MyAiBoss.m_aimode = MyAiBoss.AIMode.APPROACH;
                //移動の+-切り替え
                if (m_MyAiBoss.m_movingX == true)
                {
                    m_MyAiBoss.m_moveX = m_MyAiBoss.m_step;
                }
                else
                {
                    m_MyAiBoss.m_moveX = -m_MyAiBoss.m_step;
                }
                if (m_MyAiBoss.m_movingZ == true)
                {
                    m_MyAiBoss.m_moveZ = m_MyAiBoss.m_step;
                }
                else
                {
                    m_MyAiBoss.m_moveZ = -m_MyAiBoss.m_step;
                }
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
                //近づく(y座標は固定)            
                this.transform.position = Vector3.MoveTowards(
                   new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z),
                   new Vector3(m_MyAiBoss.m_playerObject.transform.position.x, this.transform.position.y, m_MyAiBoss.m_playerObject.transform.position.z),
                   m_MyAiBoss.m_step);
                break;
            case MyAiBoss.AIMode.LEAVE:
                //離れる            
                this.transform.Translate(new Vector3(m_MyAiBoss.m_moveX, 0, m_MyAiBoss.m_moveZ));
                break;
        }
    }

}
