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

    [SerializeField]
    Image select1;

    [SerializeField]
    Image select2;

    int m_selectNum;

    /// <summary>
    /// オープニングフラグ
    /// </summary>
    bool m_isOpening;

    bool m_changeFlag;

    float m_count;

    [SerializeField]
    float m_second;

    void Start()
    {
        m_selectNum = 1;
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
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            if (m_selectNum == 1)
            {
                OnClickStartButton();
            }
            else
            {
                Debug.Log("Load中");
            }
        }
        float HorizontalKeyInput = Input.GetAxis("HorizontalKey");
        
        if (HorizontalKeyInput == -1.0f&&m_changeFlag==false)
        {
            if (m_selectNum == 1)
            {
                m_selectNum = 2;
            }
            else
            {
                m_selectNum = 1;
            }
            m_changeFlag = true;
        }
        else if (HorizontalKeyInput == 1.0f&&m_changeFlag==false)
        {
            if (m_selectNum == 1)
            {
                m_selectNum = 2;
            }
            else
            {
                m_selectNum = 1;
            }
            m_changeFlag = true;
        }
        if (m_selectNum == 1)
        {
            select1.enabled = true;
            select2.enabled = false;
        }
        else if (m_selectNum == 2)
        {
            select1.enabled = false;
            select2.enabled = true;
        }
        if (HorizontalKeyInput <= 0.1 && HorizontalKeyInput >= -0.1)
        {
            m_changeFlag = false;
        }
    }

    public void OnClickStartButton()
    {
        MySceneManager.Instance.ChangeScene(MyScene.Main);
    }
}
