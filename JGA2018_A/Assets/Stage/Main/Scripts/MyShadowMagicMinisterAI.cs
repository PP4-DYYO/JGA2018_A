////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/7/3～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class MyShadowMagicMinisterAI : MyAiBoss
{
    [SerializeField]
     float m_WaitingTime;

   protected override void Start()
    {
        m_myObjectName =gameObject.name;
        m_maxHitPoint = 1;
        m_WaitingTime = 0;
        m_isFakeBody = true;
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 移動、行動
    /// </summary>
    protected override void FixedUpdate()
    {
        m_WaitingTime += Time.deltaTime;
        if (m_WaitingTime > 7)
        {
            GameObject.Find("MagicMinister(Clone)").GetComponent<MyMagicMinisterAI>().m_isApproach = true;
            GameObject.Find("MagicMinister(Clone)").GetComponent<MyMagicMinisterAI>().m_appearReset = true;
            Destroy(gameObject);
        }
    }
}
