////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/5/21～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using UnityEngine;

///<summary>
///キャリー大臣のAI
///</summary>
public class MyCarryMinisterAI : MyAiBoss
{
    /// <summary>
    /// スクリプト
    /// </summary>
    MyArrowShot myArrowShot;

    ///<summary>
    ///ワープ先の座標
    ///</summary>
    float m_warpPosX, m_warpPosY, m_warpPosZ;

    /// <summary>
    /// ワープ兆しオブジェクト
    /// </summary>
    [SerializeField]
    GameObject m_warpSign;

    /// <summary>
    /// ワープ兆し
    /// </summary>
    int m_warpSignFlag;


    /// <summary>
    /// ワープ待機時間
    /// </summary>
    float m_waitTime =1;

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 初期状態設定
    /// </summary>
    protected override void Start()
    {
        m_myObjectName = this.gameObject.name;
        m_maskPositionObject = transform.FindChild(MaskPositionObjectName).gameObject;
        m_maxHitPoint = 230;
        m_attack = 60;
        m_perceivedRange = 30;
        m_distance = 30;
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

        m_gameTime = m_attackInterval/2;
        myArrowShot = GameObject.Find("ArrowPositionObject").GetComponent<MyArrowShot>();

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

            if (m_warpSignFlag>0)
            {
                CarryWarp();
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
        if (m_warpSignFlag==0&& AttackCount % 4==1)
        {
            m_warpSignFlag = 1;
        }

        if (m_warpSignFlag == 3 && AttackCount % 4 == 3)
        {
            m_warpSignFlag = 0;
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
            case AIMode.LEAVE:
                //離れる           
                transform.position = Vector3.MoveTowards(transform.position, myPlayer.transform.position, -m_step);
                break;
        }
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// ワープ処理
    /// </summary>
    void CarryWarp()
    {
        if (m_waitTime<=0&& m_warpSignFlag == 2)
        {
            //ワープする
            gameObject.transform.position = new Vector3(m_warpPosX, m_warpPosY, m_warpPosZ);
            m_waitTime = 1;
            m_warpSignFlag = 3;
        }

        if (m_warpSignFlag ==1)
        {
            //最初に座標を決め、兆しが出現
            float randamX =UnityEngine.Random.Range(-4, 4);
            float randamZ = UnityEngine.Random.Range(-4, 4);
            m_warpPosX = myStage.CurrentField.BossRoomCenterPos.x + randamX;
            m_warpPosY = myStage.CurrentField.BossRoomCenterPos.y ;
            m_warpPosZ = myStage.CurrentField.BossRoomCenterPos.z + randamZ;

            GameObject light = GameObject.Instantiate(m_warpSign) as GameObject;
            light.transform.position = new Vector3(m_warpPosX, m_warpPosY, m_warpPosZ);
            m_warpSignFlag = 2;
        }

        if (m_warpSignFlag==2)
        {
            m_waitTime -= Time.deltaTime;
        }
    }
}
