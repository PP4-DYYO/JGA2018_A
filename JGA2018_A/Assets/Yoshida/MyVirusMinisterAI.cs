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
public class MyVirusMinisterAI : MonoBehaviour {


    private GameObject playerObjct;
    private string playerObjctName = "DummyPlayer";
    //HP//
    int virusMinisterHitPoint = 100;
    ////攻撃力
    int virusMinisterAttack = 100;
    //初期知覚範囲//
    int perceivedRange =5;
    //このAIが気づいたか//
    bool isPerceived;
    //攻撃間隔//
    private int attackInterval = 120;
    //移動関連//
    float step =0.05f;
    //xへの移動はt:プラス/f:マイナス//
    public bool moveX;
    //zへの移動はt:プラス/f:マイナス//
    public bool moveZ;
    //それぞれの移動量//
    float m_moveX;
    float m_moveZ;

    //特殊技の使用制限数//
    int specialAttackLimit=2;
    //特殊技の使用数//
    int specialAttackCount;
    ////プレイヤーが攻撃してきたフラグ
    bool playerAttacked;
    //自分の状態//
    public AIMode aiMode;
    ////防御判断
    ////攻撃判断

    //行動制御用//
    int gameTime;

    //スクリプト参照用//
    private MyBombShot mb;

    void Start()
    {
        playerObjct = GameObject.Find(playerObjctName);
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


    void Update()
    {
        ////////////////////（仮）プレイヤーをここで操作する//////////////
        if (Input.GetKey("right"))
        {
            playerObjct.transform.Translate(new Vector3(0.2f, 0,0));
        }
        if (Input.GetKey("left"))
        {
            playerObjct.transform.Translate(new Vector3(-0.2f, 0, 0));
        }
        if (Input.GetKey("up"))
        {
            playerObjct.transform.Translate(new Vector3(0, 0, 0.2f));
        }
        if (Input.GetKey("down"))
        {
            playerObjct.transform.Translate(new Vector3(0, 0, -0.2f));
        }
        /////////////////////////////////////////////////////////////

        gameTime++;
        float m_distance =(playerObjct.transform.position-this.gameObject.transform.position).magnitude;//プレイヤーとの距離
        if (playerObjct.transform.position.x > this.gameObject.transform.position.x)
        {
            moveX = true;
        }
        else
        {
            moveX = false;
        }

        if (playerObjct.transform.position.z > this.gameObject.transform.position.z)
        {
            moveZ = true;
        }
        else
        {
            moveZ = false;
        }

        //Debug.Log("距離は"+ m_distance);

        if (m_distance < perceivedRange)
        {
            isPerceived = true;
        }
       if (isPerceived)
        {
            
            if (m_distance < 5)//距離が５より小さければ離れる
            {
                aiMode = AIMode.LEAVE;
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
            else if ( m_distance > 8)//距離が8より大きければ近づく
            {
                if (m_distance < perceivedRange * 3)
                {
                    aiMode = AIMode.APPROACH;
                }
                else//かなり離れるとターゲットから外れる
                {
                    aiMode = AIMode.STOP;
                    isPerceived = false;
                }

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

        switch (aiMode)
        {
            case AIMode.STOP:
                break;
            case AIMode.ATTACK:
                if (gameTime > attackInterval)
                {
                    NomalAttack();
                    gameTime = 0;
                }
                break;
            case AIMode.DEFENSE:
                break;
            case AIMode.APPROACH:
                this.transform.Translate(new Vector3(m_moveX, 0, m_moveZ));
                break;
            case AIMode.LEAVE:
                this.transform.Translate(new Vector3(m_moveX, 0, m_moveZ));
                break;
        }
    }

    ///<summary>
    ///爆弾投げ//通常攻撃
    ///</summary>
    void NomalAttack()
    {
        if(virusMinisterHitPoint< virusMinisterHitPoint / 4&& specialAttackCount<specialAttackLimit)
        {
            SpecialAttack();
        }
        mb.Shot();
    }

    ///<summary>
    ///特殊攻撃//hpが1/4の時など
    ///</summary>
    void SpecialAttack()
    {
        specialAttackCount += 1;
        Debug.Log("特殊技！！！");
    }

}
