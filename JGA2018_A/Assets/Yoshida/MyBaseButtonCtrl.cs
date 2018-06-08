//////////////////////////////////////////////////////////////////
//
//2018/5/29～
//製作者 京都コンピュータ学院京都駅前校ゲーム学科四回生　吉田純基
//
//////////////////////////////////////////////////////////////////

using UnityEngine;

///<summary>
///ボタンの基底クラス
///</summary>
public class MyBaseButtonCtrl : MonoBehaviour {

    /// <summary>
    /// ボタンのスクリプト（ボタン本体はunity上でButtonCtrlを設定）
    /// </summary>
    public MyBaseButtonCtrl m_baseButtonCtrl;


    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// クリックしたときにオブジェクト名を渡す
    /// </summary>
    public void OnClick()
    {
        m_baseButtonCtrl.OnClick(this.gameObject.name);
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 基底クラス
    /// </summary>
    protected virtual void OnClick(string objectName)
    {
        // 呼ばれない
        Debug.Log("Base Button");
    }
}