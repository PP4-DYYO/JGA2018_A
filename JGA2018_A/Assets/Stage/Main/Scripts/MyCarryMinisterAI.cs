////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/5/21～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using UnityEditor;
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
    float warpPosX, warpPosY, warpPosZ;

    /// <summary>
    /// ワープ兆しオブジェクト
    /// </summary>
    GameObject WarpSign;

    /// <summary>
    /// ワープ兆し
    /// </summary>
    int WarpSignFlag;

    /// <summary>
    /// ワープ用逃げている時間
    /// </summary>
    float escapeTime;

    /// <summary>
    /// ワープ待機時間
    /// </summary>
    float waitTime=1;

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 初期状態設定
    /// </summary>
    protected override void Start()
    {
        m_myObjectName = this.gameObject.name;
        m_playerObject = GameObject.Find(PLAYER_OBJECT_NAME);
        m_myGameObject = this.gameObject;
        m_maskPositionObject = transform.FindChild(MaskPositionObjectName).gameObject;
        m_stageObject = GameObject.Find("Stage");
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
        WarpSign = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Stage/Main/Prefab/WarpSign.prefab");

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

            if (WarpSignFlag>0)
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
        if (WarpSignFlag==0&& AttackCount % 4==1)
        {
            WarpSignFlag = 1;
        }

        if (WarpSignFlag == 3 && AttackCount % 4 == 3)
        {
            WarpSignFlag = 0;
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
                transform.position = Vector3.MoveTowards(transform.position, m_playerObject.transform.position, -m_step);
                escapeTime += Time.deltaTime;
                break;
        }
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// ワープ処理
    /// </summary>
    void CarryWarp()
    {
        if (waitTime<=0&& WarpSignFlag == 2)
        {
            //ワープする
            gameObject.transform.position = new Vector3(warpPosX, warpPosY, warpPosZ);
            waitTime = 1;
            WarpSignFlag = 3;
        }

        if (WarpSignFlag ==1)
        {
            //最初に座標を決め、兆しが出現
            float randamX =UnityEngine.Random.Range(-4, 4);
            float randamZ = UnityEngine.Random.Range(-4, 4);
            warpPosX = m_stageObject.GetComponent<MyStage>().CurrentField.BossRoomCenterPos.x + randamX;
            warpPosY = m_stageObject.GetComponent<MyStage>().CurrentField.BossRoomCenterPos.y ;
            warpPosZ = m_stageObject.GetComponent<MyStage>().CurrentField.BossRoomCenterPos.z + randamZ;

            GameObject light = GameObject.Instantiate(WarpSign) as GameObject;
            light.transform.position = new Vector3(warpPosX, warpPosY, warpPosZ);
            WarpSignFlag = 2;
        }

        if (WarpSignFlag==2)
        {
            waitTime -= Time.deltaTime;
        }
    }
}
