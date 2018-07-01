//////////////////////////////////////////////////////////////////
//
//2018/5/8～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
//////////////////////////////////////////////////////////////////


using UnityEngine;

///<summary>
///ウイルス大臣のAI
///</summary>
public class MyVirusMinisterAI : MonoBehaviour
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
        m_MyAiBoss.m_attackNum = 1;

        m_MyAiBoss.m_myObjectName = this.gameObject.name;
        m_MyAiBoss.m_playerObject = GameObject.Find(m_MyAiBoss.m_playerObjectName);
        m_MyAiBoss.m_hitPoint = 310;
        m_MyAiBoss.m_attack = 50;
        m_MyAiBoss.m_perceivedRange = 5;
        m_MyAiBoss.m_distance = 100;
        m_MyAiBoss.m_isAttacked = false;
        m_MyAiBoss.m_attackInterval = 120;
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
            //距離が５より小さければ離れる
            if (m_MyAiBoss.m_distance < 5)
            {
                m_MyAiBoss.m_aimode = MyAiBoss.AIMode.LEAVE;

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
            //距離がかなり離れると見失う
            else if (m_MyAiBoss.m_distance > m_MyAiBoss.m_perceivedRange * 3)
            {
                m_MyAiBoss.m_aimode = MyAiBoss.AIMode.IDLE;
            }
            else
            {
                m_MyAiBoss.m_aimode = MyAiBoss.AIMode.ATTACK;
            }
        }

        //攻撃後次の攻撃までは離れるまたはその場に留まる
        if (m_MyAiBoss.m_isAttacked == true)
        {
            m_MyAiBoss.m_aimode = MyAiBoss.AIMode.LEAVE;
            if (m_MyAiBoss.m_distance > 5)
            {
                m_MyAiBoss.m_isAttacked = false;
                m_MyAiBoss.m_aimode = MyAiBoss.AIMode.IDLE;
            }
        }

        //距離によって爆弾の投げ方が変わる
        if (m_MyAiBoss.m_aimode == MyAiBoss.AIMode.LEAVE)
        {
            m_MyAiBoss.m_attackNum = 0;
            m_MyAiBoss.m_attack = 70;
        }
        else
        {
            m_MyAiBoss.m_attackNum = 1;
            m_MyAiBoss.m_attack = 50;
        }
        //状態によって行動を切り替える
        switch (m_MyAiBoss.m_aimode)
        {
            case MyAiBoss.AIMode.IDLE:
                break;
            case MyAiBoss.AIMode.ATTACK:
                //一定時間毎に攻撃をする
                if (m_gameTime >= m_MyAiBoss.m_attackInterval)
                {
                    m_MyAiBoss.NomalAttack();
                }
                break;
            case MyAiBoss.AIMode.DEFENSE:
                break;
            case MyAiBoss.AIMode.APPROACH:
                //近づいてこない
                break;
            case MyAiBoss.AIMode.LEAVE:
                //逃げながら投げる
                if (m_gameTime >= m_MyAiBoss.m_attackInterval)
                {
                    m_MyAiBoss.NomalAttack();
                }
                //離れるまたは近づく(ここは同じ)            
                this.transform.Translate(new Vector3(m_MyAiBoss.m_moveX, 0, m_MyAiBoss.m_moveZ));
                break;
        }
    }
}
