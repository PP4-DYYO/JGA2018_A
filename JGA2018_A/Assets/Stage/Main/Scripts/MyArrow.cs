////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/7/2～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;

/// <summary>
/// 矢の当たり判定用
/// </summary>
public class MyArrow : MonoBehaviour {
    /// <summary>
    /// スクリプト
    /// </summary>
    MyAttackManager myAttackManager;
    MyAiBoss myAiBoss;

    /// <summary>
    /// 当たり判定Cubeの大きさ
    /// </summary>
    const float m_cubeLength=0.1f;


    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 初期設定
    /// </summary>
    void Start () {
        myAttackManager = transform.parent.GetComponent<MyAttackManager>();
        myAiBoss = myAttackManager.CharacterScript.BossScript;
    }


    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 当たり判定
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
		if (!myAiBoss)
			return;

        if (other.gameObject.name == myAiBoss.PlayerObjectName)
        {
            //頂点の位置
            Vector3 vLDB = new Vector3(gameObject.transform.position.x - m_cubeLength, gameObject.transform.position.y - m_cubeLength, gameObject.transform.position.z - m_cubeLength);
            Vector3 vLDF = new Vector3(gameObject.transform.position.x - m_cubeLength, gameObject.transform.position.y - m_cubeLength, gameObject.transform.position.z + m_cubeLength);
            Vector3 vLUB = new Vector3(gameObject.transform.position.x - m_cubeLength, gameObject.transform.position.y + m_cubeLength, gameObject.transform.position.z - m_cubeLength);
            Vector3 vLUF = new Vector3(gameObject.transform.position.x - m_cubeLength, gameObject.transform.position.y + m_cubeLength, gameObject.transform.position.z + m_cubeLength);
            Vector3 vRDB = new Vector3(gameObject.transform.position.x + m_cubeLength, gameObject.transform.position.y - m_cubeLength, gameObject.transform.position.z - m_cubeLength);
            Vector3 vRDF = new Vector3(gameObject.transform.position.x + m_cubeLength, gameObject.transform.position.y - m_cubeLength, gameObject.transform.position.z + m_cubeLength);
            Vector3 vRUB = new Vector3(gameObject.transform.position.x + m_cubeLength, gameObject.transform.position.y + m_cubeLength, gameObject.transform.position.z - m_cubeLength);
            Vector3 vRUF = new Vector3(gameObject.transform.position.x + m_cubeLength, gameObject.transform.position.y + m_cubeLength, gameObject.transform.position.z + m_cubeLength);

            //当たり判定発生
            MyCube explosionRange = new MyCube(vLDB, vRDB, vLDF, vRDF, vLUB, vRUB, vLUF, vRUF);
            myAttackManager.EnemyAttack(explosionRange, MaskAttribute.Non, myAiBoss.Attack, 0.1f);

            //削除
            Destroy(this.gameObject);
        }
    }

}
