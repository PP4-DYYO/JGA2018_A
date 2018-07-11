//////////////////////////////////////////////////////////////////
//
//2018/6/24～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
//////////////////////////////////////////////////////////////////

using UnityEngine;

public class MyWarpManager : MonoBehaviour
{
    /// <summary>
    /// ワープのゲームオブジェクト
    /// </summary>
    GameObject warpA;
    GameObject warpA_;
    GameObject warpB;
    GameObject warpB_;
    GameObject warpC;
    GameObject warpC_;
    GameObject warpD;
    GameObject warpD_;
    GameObject warpE1;
    GameObject warpE2;
    GameObject warpE3;
    GameObject warpF;
    GameObject warpF_;
    GameObject warpG1;
    GameObject warpG2;
    GameObject warpG3;
    GameObject warpH1;
    GameObject warpH2;

    /// <summary>
    /// プレイヤーのゲームオブジェクト
    /// </summary>
    GameObject m_playerObject;

	/// <summary>
	/// カメラのゲームオブジェクト
	/// </summary>
	GameObject m_cameraObject;

	/// <summary>
	/// ワープでのカメラ相対的位置
	/// </summary>
	[SerializeField]
	Vector3 m_relativePosCameraInWarp_A1;
	[SerializeField]
	Vector3 m_relativePosCameraInWarp_A2;
	[SerializeField]
	Vector3 m_relativePosCameraInWarp_B1;
	[SerializeField]
	Vector3 m_relativePosCameraInWarp_B2;
	[SerializeField]
	Vector3 m_relativePosCameraInWarp_C1;
	[SerializeField]
	Vector3 m_relativePosCameraInWarp_C2;
	[SerializeField]
	Vector3 m_relativePosCameraInWarp_D1;
	[SerializeField]
	Vector3 m_relativePosCameraInWarp_D2;
	[SerializeField]
	Vector3 m_relativePosCameraInWarp_E1;
	[SerializeField]
	Vector3 m_relativePosCameraInWarp_E2;
	[SerializeField]
	Vector3 m_relativePosCameraInWarp_E3;
	[SerializeField]
	Vector3 m_relativePosCameraInWarp_F1;
	[SerializeField]
	Vector3 m_relativePosCameraInWarp_F2;
	[SerializeField]
	Vector3 m_relativePosCameraInWarp_G1;
	[SerializeField]
	Vector3 m_relativePosCameraInWarp_G2;
	[SerializeField]
	Vector3 m_relativePosCameraInWarp_G3;
	[SerializeField]
	Vector3 m_relativePosCameraInWarp_H1;
	[SerializeField]
	Vector3 m_relativePosCameraInWarp_H2;


	/// <summary>
	/// 時間計測用
	/// </summary>
	float m_time = 0;


    /// <summary>
    /// ワープ使用可能状態
    /// </summary>
    bool m_canUseWarp;


    /// <summary>
    /// スクリプト
    /// </summary>
    MyTransition myTransition;

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 最初に代入
    /// </summary>
    void Start()
    {
        myTransition = GameObject.Find("TransitionPanel").GetComponent<MyTransition>();
        warpA = GameObject.Find("WarpA");
        warpA_ = GameObject.Find("WarpA_");
        warpB = GameObject.Find("WarpB");
        warpB_ = GameObject.Find("WarpB_");
        warpC = GameObject.Find("WarpC");
        warpC_ = GameObject.Find("WarpC_");
        warpD = GameObject.Find("WarpD");
        warpD_ = GameObject.Find("WarpD_");
        warpF = GameObject.Find("WarpF");
        warpF_ = GameObject.Find("WarpF_");

        warpE1 = GameObject.Find("WarpE1");
        warpE2 = GameObject.Find("WarpE2");
        warpE3 = GameObject.Find("WarpE3");
        warpG1 = GameObject.Find("WarpG1");
        warpG2 = GameObject.Find("WarpG2");
        warpG3 = GameObject.Find("WarpG3");
        warpH1 = GameObject.Find("WarpH1");
        warpH2 = GameObject.Find("WarpH2");

        m_playerObject = GameObject.Find("DummyPlayer");
		m_cameraObject = GameObject.Find("Main Camera");
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 時間を測るだけ
    /// </summary>
    public void Update()
    {
        if (m_canUseWarp == false)
        {
            m_time += Time.deltaTime;
            if (m_time > 2)
            {
                m_time = 0;
                m_canUseWarp = true;
                m_playerObject.GetComponent<MyPlayer>().enabled = true;
            }
        }
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// ワープ処理
    /// </summary>
    public void Warp(string warppointName, GameObject player)
    {
        if (m_canUseWarp == true)
        {
            if (warppointName == warpA.name)
            {
                player.transform.position = warpA_.transform.position;
				player.transform.LookAt(player.transform.position - Vector3.Scale(m_relativePosCameraInWarp_A2, (Vector3.right + Vector3.forward)));
				m_cameraObject.GetComponent<MyCamera>().SetPosition(m_relativePosCameraInWarp_A2);
            }
            else if (warppointName == warpA_.name)
            {
                player.transform.position = warpA.transform.position;
				player.transform.LookAt(player.transform.position - Vector3.Scale(m_relativePosCameraInWarp_A1, (Vector3.right + Vector3.forward)));
				m_cameraObject.GetComponent<MyCamera>().SetPosition(m_relativePosCameraInWarp_A1);
			}
			else if (warppointName == warpB.name)
            {
                player.transform.position = warpB_.transform.position;
				player.transform.LookAt(player.transform.position - Vector3.Scale(m_relativePosCameraInWarp_B2, (Vector3.right + Vector3.forward)));
				m_cameraObject.GetComponent<MyCamera>().SetPosition(m_relativePosCameraInWarp_B2);
			}
			else if (warppointName == warpB_.name)
            {
                player.transform.position = warpB.transform.position;
				player.transform.LookAt(player.transform.position - Vector3.Scale(m_relativePosCameraInWarp_B1, (Vector3.right + Vector3.forward)));
				m_cameraObject.GetComponent<MyCamera>().SetPosition(m_relativePosCameraInWarp_B1);
			}
			else if (warppointName == warpC.name)
            {
                player.transform.position = warpC_.transform.position;
				player.transform.LookAt(player.transform.position - Vector3.Scale(m_relativePosCameraInWarp_C2, (Vector3.right + Vector3.forward)));
				m_cameraObject.GetComponent<MyCamera>().SetPosition(m_relativePosCameraInWarp_C2);
			}
			else if (warppointName == warpC_.name)
            {
                player.transform.position = warpC.transform.position;
				player.transform.LookAt(player.transform.position - Vector3.Scale(m_relativePosCameraInWarp_C1, (Vector3.right + Vector3.forward)));
				m_cameraObject.GetComponent<MyCamera>().SetPosition(m_relativePosCameraInWarp_C1);
			}
			else if (warppointName == warpD.name)
            {
                player.transform.position = warpD_.transform.position;
				player.transform.LookAt(player.transform.position - Vector3.Scale(m_relativePosCameraInWarp_D2, (Vector3.right + Vector3.forward)));
				m_cameraObject.GetComponent<MyCamera>().SetPosition(m_relativePosCameraInWarp_D2);
			}
			else if (warppointName == warpD_.name)
            {
                player.transform.position = warpD.transform.position;
				player.transform.LookAt(player.transform.position - Vector3.Scale(m_relativePosCameraInWarp_D1, (Vector3.right + Vector3.forward)));
				m_cameraObject.GetComponent<MyCamera>().SetPosition(m_relativePosCameraInWarp_D1);
			}
			else if (warppointName == warpF.name)
            {
                player.transform.position = warpF_.transform.position;
				player.transform.LookAt(player.transform.position - Vector3.Scale(m_relativePosCameraInWarp_F2, (Vector3.right + Vector3.forward)));
				m_cameraObject.GetComponent<MyCamera>().SetPosition(m_relativePosCameraInWarp_F2);
			}
			else if (warppointName == warpF_.name)
            {
                player.transform.position = warpF.transform.position;
				player.transform.LookAt(player.transform.position - Vector3.Scale(m_relativePosCameraInWarp_F1, (Vector3.right + Vector3.forward)));
				m_cameraObject.GetComponent<MyCamera>().SetPosition(m_relativePosCameraInWarp_F1);
			}
			else if (warppointName == warpE1.name)
            {
                player.transform.position = warpE2.transform.position;
				player.transform.LookAt(player.transform.position - Vector3.Scale(m_relativePosCameraInWarp_E2, (Vector3.right + Vector3.forward)));
				warpE2.SetActive(false);
				m_cameraObject.GetComponent<MyCamera>().SetPosition(m_relativePosCameraInWarp_E2);
			}
			else if (warppointName == warpE2.name)
            {
                player.transform.position = warpE3.transform.position;
				player.transform.LookAt(player.transform.position - Vector3.Scale(m_relativePosCameraInWarp_E3, (Vector3.right + Vector3.forward)));
				m_cameraObject.GetComponent<MyCamera>().SetPosition(m_relativePosCameraInWarp_E3);
			}
			else if (warppointName == warpE3.name)
            {
                player.transform.position = warpE1.transform.position;
				player.transform.LookAt(player.transform.position - Vector3.Scale(m_relativePosCameraInWarp_E1, (Vector3.right + Vector3.forward)));
				m_cameraObject.GetComponent<MyCamera>().SetPosition(m_relativePosCameraInWarp_E1);
			}
			else if (warppointName == warpG1.name)
            {
                player.transform.position = warpG2.transform.position;
				player.transform.LookAt(player.transform.position - Vector3.Scale(m_relativePosCameraInWarp_G2, (Vector3.right + Vector3.forward)));
				warpG2.SetActive(false);
				m_cameraObject.GetComponent<MyCamera>().SetPosition(m_relativePosCameraInWarp_G2);
			}
			else if (warppointName == warpG2.name)
            {
                player.transform.position = warpG3.transform.position;
				player.transform.LookAt(player.transform.position - Vector3.Scale(m_relativePosCameraInWarp_G3, (Vector3.right + Vector3.forward)));
				m_cameraObject.GetComponent<MyCamera>().SetPosition(m_relativePosCameraInWarp_G3);
			}
			else if (warppointName == warpG3.name)
            {
                player.transform.position = warpG1.transform.position;
				player.transform.LookAt(player.transform.position - Vector3.Scale(m_relativePosCameraInWarp_G1, (Vector3.right + Vector3.forward)));
				m_cameraObject.GetComponent<MyCamera>().SetPosition(m_relativePosCameraInWarp_G1);
			}
			else if (warppointName == warpH1.name)
            {
                player.transform.position = warpH2.transform.position;
				player.transform.LookAt(player.transform.position - Vector3.Scale(m_relativePosCameraInWarp_H2, (Vector3.right + Vector3.forward)));
				warpH2.SetActive(false);
				m_cameraObject.GetComponent<MyCamera>().SetPosition(m_relativePosCameraInWarp_H2);
			}
			else if (warppointName == warpH2.name)
            {
                player.transform.position = warpH1.transform.position;
				player.transform.LookAt(player.transform.position - Vector3.Scale(m_relativePosCameraInWarp_H1, (Vector3.right + Vector3.forward)));
				m_cameraObject.GetComponent<MyCamera>().SetPosition(m_relativePosCameraInWarp_H1);
			}
			m_canUseWarp = false;
            myTransition.StartTransition();
            player.GetComponent<MyPlayer>().enabled = false;
			player.GetComponent<MyPlayer>().StartAnimIdle();
        }
    }
}
