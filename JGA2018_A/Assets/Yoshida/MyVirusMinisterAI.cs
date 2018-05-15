//////////////////////////////////////////////////////////////////
//
//2018/5/8～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
//////////////////////////////////////////////////////////////////

using System;
using UnityEngine;

///<summary>
///ウイルス大臣のAI
///</summary>
public class MyVirusMinisterAI : MonoBehaviour
{

    /// <summary>
    /// プレイヤーのオブジェクト
    /// </summary>
     GameObject m_playerObjct;

    /// <summary>
    /// プレイヤーオブジェクトの名前
    /// </summary>
    const string PLAYER_OBJECT_NAME = "DummyPlayer";

    /// <summary>
    /// HP//
    /// </summary>
    const int VIRUS_MINISTER_HIT_POINT = 100;

    /// <summary>
    /// 攻撃力
    /// </summary>
    const int VIRUS_MINISTER_ATTACK = 100;

    /// <summary>
    /// 知覚範囲
    /// </summary>
    const int PERCEIVEDRANGE = 5;

    /// <summary>
    /// このAIが気づいたか
    /// </summary>
    bool isPerceived;

    /// <summary>
    /// 攻撃間隔//
    /// </summary>
    const int ATTACK_INTERVAL = 120;

    /// <summary>
    /// 一歩の移動距離//
    /// </summary>
    const float step = 0.05f;
    /// <summary>
    /// xへの移動はt:プラス/f:マイナス//
    /// </summary>
    bool moveX;

    /// <summary>
    /// zへの移動はt:プラス/f:マイナス//
    /// </summary>
    bool moveZ;

    /// <summary>
    /// xzそれぞれの移動量//
    /// </summary>
    float m_moveX;
    float m_moveZ;

    /// <summary>
    /// 特殊技の使用制限数//
    /// </summary>
    const int SPECIAL_ATTACKLIMIT = 2;

    /// <summary>
    /// 特殊技の使用数//
    /// </summary>
    int m_specialAttackCount;

    /// <summary>
    /// プレイヤーが攻撃してきたフラグ
    /// </summary>
    bool playerAttacked;

    /// <summary>
    /// 自分の状態//
    /// </summary>
    public AIMode aiMode;

    /// <summary>
    /// 行動制御用(時間)
    /// </summary>
    int m_gameTime;

    //スクリプト参照用//
    MyBombShot mb;

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 初期状態設定
    /// </summary>
    void Start()
    {
        m_playerObjct = GameObject.Find(PLAYER_OBJECT_NAME);
        aiMode = AIMode.STOP;
        mb = GameObject.Find("BombPoint").GetComponent<MyBombShot>();
    }

    /// <summary>
    /// AIの行動タイプ
    /// </summary>
    public enum AIMode
    {
        STOP,
        ATTACK,
        DEFENSE,
        APPROACH,
        LEAVE
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 移動、行動
    /// </summary>
    void Update()
    {
        ////////////////////（仮）プレイヤーをここで操作する//////////////
        if (Input.GetKey("right"))
        {
            m_playerObjct.transform.Translate(new Vector3(0.2f, 0, 0));
        }
        if (Input.GetKey("left"))
        {
            m_playerObjct.transform.Translate(new Vector3(-0.2f, 0, 0));
        }
        if (Input.GetKey("up"))
        {
            m_playerObjct.transform.Translate(new Vector3(0, 0, 0.2f));
        }
        if (Input.GetKey("down"))
        {
            m_playerObjct.transform.Translate(new Vector3(0, 0, -0.2f));
        }
        /////////////////////////////////////////////////////////////

        m_gameTime++;

        //プレイヤーとの距離
        float m_distance = (m_playerObjct.transform.position - this.gameObject.transform.position).magnitude;

        //位置関係を確認して、移動の+-を変更する
        if (m_playerObjct.transform.position.x > this.gameObject.transform.position.x)
        {
            moveX = true;
        }
        else
        {
            moveX = false;
        }

        if (m_playerObjct.transform.position.z > this.gameObject.transform.position.z)
        {
            moveZ = true;
        }
        else
        {
            moveZ = false;
        }

        //Debug.Log("距離は"+ m_distance);

        //知覚範囲に入れば気づいた状態に遷移する
        if (m_distance < PERCEIVEDRANGE)
            isPerceived = true;
        
        if (isPerceived)
        {
            //距離が５より小さければ離れる
            if (m_distance < 5)
            {
                aiMode = AIMode.LEAVE;
                //移動の+-切り替え
                if (moveX == true)
                {
                    m_moveX = -step;
                }
                else
                {
                    m_moveX = step;
                }
                if (moveZ == true)
                {
                    m_moveZ = -step;
                }
                else
                {
                    m_moveZ = step;
                }

            }
            //距離が8より大きければ近づく
            else if (m_distance > 8)
            {
                if (m_distance < PERCEIVEDRANGE * 3)
                {
                    aiMode = AIMode.APPROACH;
                }
                //大きく離れるとターゲットから外れる
                else
                {
                    aiMode = AIMode.STOP;
                    isPerceived = false;
                }

                //移動の+-切り替え
                if (moveX == true)
                {
                    m_moveX = step;
                }
                else
                {
                    m_moveX = -step;
                }

                if (moveZ == true)
                {
                    m_moveZ = step;
                }
                else
                {
                    m_moveZ = -step;
                }

            }
            else
            {
                aiMode = AIMode.ATTACK;
            }
        }

        //状態によって行動を切り替える
        switch (aiMode)
        {
            case AIMode.STOP:
                break;
            case AIMode.ATTACK:
                //一定時間毎に攻撃をする
                if (m_gameTime > ATTACK_INTERVAL)
                    NomalAttack();
                    m_gameTime = 0;
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

    //----------------------------------------------------------------------------------------------------
    ///<summary>
    ///爆弾投げ//通常攻撃
    ///</summary>
    void NomalAttack()
    {
        //HPが一定で制限に達していないとき
        if (VIRUS_MINISTER_HIT_POINT < VIRUS_MINISTER_HIT_POINT / 4 && m_specialAttackCount < SPECIAL_ATTACKLIMIT)
            SpecialAttack();

        mb.Shot();
    }

    //----------------------------------------------------------------------------------------------------
    ///<summary>
    ///特殊攻撃//hpが1/4の時など
    ///</summary>
    void SpecialAttack()
    {
        m_specialAttackCount += 1;
        Debug.Log("特殊技！！！");
    }
}
