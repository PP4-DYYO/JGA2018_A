//////////////////////////////////////////////////////////////////
//
//2018/7/3～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
//////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEngine.UI;

public class MyTransition : MonoBehaviour {

    [SerializeField]
    Sprite sp1;
    [SerializeField]
    Sprite sp2;
    [SerializeField]
    GameObject m_panel;
    public bool m_change;
    float m_time;
    Image image;
    float alfa;

    // Use this for initialization
    void Start () {
        m_panel = this.gameObject;
        image = m_panel.GetComponent<Image>();
        alfa = GetComponent<Image>().color.a;
    }

   public void StartTransition()
    {
        m_change = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (m_change==true)
        {
            m_time += Time.deltaTime;
            if (alfa == 0)
            {
                alfa = 1;
            }
            if (m_time > 0.5)
            {
                image.sprite = sp2;
                if (m_time > 1)
                {
                    alfa = 0;
                    m_change = false;
                    m_time = 0;
                    image.sprite = sp1;
                }
            }
            GetComponent<Image>().color = new Color(255, 255, 255, alfa);          
        }
	}
}
