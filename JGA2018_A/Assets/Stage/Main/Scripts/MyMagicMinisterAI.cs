////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/5/29～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using UnityEngine;

///<summary>
///ミラー大臣のAI
///</summary>
public class MyMagicMinisterAI : MyAiBoss
{
    /// <summary>
    ///影武者制御
    /// </summary>
    public static int s_shadowCount;

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 初期状態設定
    /// </summary>
    protected override void Start()
    {
        m_attackNum = 0;

        m_myObjectName = this.gameObject.name;
        m_maskPositionObject = transform.FindChild(MaskPositionObjectName).gameObject;
        m_maxHitPoint = 450;
        m_attack = 65;
        m_perceivedRange = 30;
        m_distance = 30;
        m_isAttacked = false;
        m_attackInterval = 3.0f;
        m_step = 0.06f;
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
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (m_aimode != AIMode.WAIT)
        {

        }

        //状態によって行動を切り替える
        switch (m_aimode)
        {
            case AIMode.IDLE:
                if (m_counterAttackFlag == 0)
                {
                    GetReadyCounterAttack();
                }
                break;
            case AIMode.ATTACK:
                break;
            case AIMode.LEAVE:
                //離れる            
                transform.position = Vector3.MoveTowards(transform.position, myPlayer.transform.position, -m_step);
                break;
        }

        if (m_counterAttackFlag==2)
        {
            CounterAttack();
        }
        if (m_counterAttackFlag == 0)
        {
            Debug.Log("発動前");
        }
        else if (m_counterAttackFlag == 1)
        {
            Debug.Log("発動中");

        }
        else if (m_counterAttackFlag == 2)
        {
            Debug.Log("カウンター発動！！");
            CounterAttack();
        }

    }
    
    void CounterAttack()
    {
        float m_length = 0.6f;

        Vector3 vLDB = new Vector3(transform.position.x - m_length, 1+transform.position.y - m_length, transform.position.z - m_length);
        Vector3 vLDF = new Vector3(transform.position.x - m_length, 1 + transform.position.y - m_length, transform.position.z + m_length);
        Vector3 vLUB = new Vector3(transform.position.x - m_length, 1 + transform.position.y + m_length, transform.position.z - m_length);
        Vector3 vLUF = new Vector3(transform.position.x - m_length, 1 + transform.position.y + m_length, transform.position.z + m_length);
        Vector3 vRDB = new Vector3(transform.position.x + m_length, 1 + transform.position.y - m_length, transform.position.z - m_length);
        Vector3 vRDF = new Vector3(transform.position.x + m_length, 1 + transform.position.y - m_length, transform.position.z + m_length);
        Vector3 vRUB = new Vector3(transform.position.x + m_length, 1 + transform.position.y + m_length, transform.position.z - m_length);
        Vector3 vRUF = new Vector3(transform.position.x + m_length, 1 + transform.position.y + m_length, transform.position.z + m_length);

        //当たり判定発生と攻撃
        MyCube attackRange = new MyCube(vLDB, vRDB, vLDF, vRDF, vLUB, vRUB, vLUF, vRUF);
        AttackManagerScript.EnemyAttack(attackRange, MaskAttribute.Non, m_attack, 0.1f);
        m_counterAttackFlag = 0;
    }

    void GetReadyCounterAttack()
    {
        if (m_gameTime >= 3)
        {
            float rand= Random.Range(0.0f, 1.0f);
            Debug.Log(rand);
            if (rand > 0.3)
            {
                m_counterAttackFlag = 1;
            }
            m_gameTime = 0;
        }
    }
}
