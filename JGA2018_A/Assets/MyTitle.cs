////////////////////////////////////////////////////////////
//
//2018/7月/17日～
//製作者 京都コンピュータ学院 ゲーム学科 四回生 中村智哉
//
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyTitle : MonoBehaviour
{
    /// <summary>
    /// オープニング
    /// </summary>
    [SerializeField]
    MyOpening myOpening;

    /// <summary>
    /// 二つ目のタイトル
    /// </summary>
    [SerializeField]
    GameObject SecondTitle;

    /// <summary>
    /// 何か押してください
    /// </summary>
    [SerializeField]
    Text PleaseAnyKey;

    /// <summary>
    /// オープニングフラグ
    /// </summary>
    bool m_isOpening;

    float m_count;

    [SerializeField]
    float m_second;

    void Start()
    {

    }

    void Update()
    {

        m_count = m_count + Time.deltaTime;

        if (m_count >= m_second)
        {
            if (PleaseAnyKey.enabled)
            {
                PleaseAnyKey.enabled = false;
            }
            else
            {
                PleaseAnyKey.enabled = true;
            }
            m_count = 0;
        }

        if (!m_isOpening)
        {
            if (Input.anyKey)
            {
                m_isOpening = true;
                SecondTitle.SetActive(true);
                myOpening.StartOpening();
            }

        }

    }

    public void OnClickStartButton()
    {
        MySceneManager.Instance.ChangeScene(MyScene.Main);
    }
}
