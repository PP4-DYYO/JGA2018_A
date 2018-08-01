using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//----------------------------------------------------------------------------------------------------
//クラス
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------

    /// <summary>
    /// エンディングクラス
    /// </summary>
public class MyEnding : MonoBehaviour
{
    /// <summary>
    /// スタッフロールのテキスト
    /// </summary>
    [SerializeField]
    RectTransform StaffRoll;
   
    /// <summary>
    /// 文字の速度
    /// </summary>
    [SerializeField]
    float m_speed;

    /// <summary>
    /// テキストを流し終わったら
    /// </summary>
    bool m_endFlag;

    /// <summary>
    /// 1フレーム前のエンドフラグ
    /// </summary>
    bool m_endFlagPrev;

    /// <summary>
    /// 終了座標
    /// </summary>
    [SerializeField]
    float m_endPosition;

    /// <summary>
    /// 定期フレーム
    /// </summary>
    void FixedUpdate ()
    {
        //テキストの移動
        StaffRoll.localPosition += new Vector3(0, m_speed * Time.deltaTime, 0);

        //テキストが流れ終わったとき
        if (StaffRoll.position.y >= m_endPosition)
        {
            m_endFlag = true;
        }

        //テキストが流れ終わったタイミング
        if (m_endFlag&&!m_endFlagPrev)
        {
            //タイトルに戻る
            MySceneManager.Instance.ChangeScene(MyScene.Title);
        }
        m_endFlagPrev = m_endFlag;
	}
}
