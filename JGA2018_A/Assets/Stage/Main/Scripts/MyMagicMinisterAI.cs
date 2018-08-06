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
        m_myGameObject = gameObject;
        m_maskPositionObject = transform.FindChild(MaskPositionObjectName).gameObject;
        m_hitPoint = 450;
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
            //距離が0.5より小さければ離れる
            if (m_distance < 0.5)
            {
                    m_aimode = AIMode.LEAVE;
            }
        }

        //状態によって行動を切り替える
        switch (m_aimode)
        {
            case AIMode.IDLE:
                if (!m_counterAttackFlag)
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
       
    }
    
    void CounterAttack()
    {
        Debug.Log("カウンター攻撃");
        m_counterAttackFlag = false;
    }

    void GetReadyCounterAttack()
    {
        if (m_gameTime >= 3)
        {
            float rand= Random.Range(0.0f, 1.0f);
            Debug.Log(rand);
            if (rand > 0.3)
            {
                Debug.Log("カウンター起動");
                m_counterAttackFlag = true;
            }
            m_gameTime = 0;
        }
    }
}
