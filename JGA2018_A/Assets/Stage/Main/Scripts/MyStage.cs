//----------------------------------------------------------------------------------------------------
//
//2018年5月8日～
//作成者　京都コンピュータ学院京都駅前校　ゲーム学科　4回生　奥田裕也
//編集者
//
//----------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージクラス
/// </summary>
public class MyStage : MonoBehaviour
{
	#region 外部のインスタンス
	[Header("外部のインスタンス")]
	/// <summary>
	/// ゲーム
	/// </summary>
	[SerializeField]
	MyGame myGame;
	#endregion

	#region 生成されるプレファブ
	[Header("生成されるプレファブ")]
	/// <summary>
	/// フィールド１
	/// </summary>
	[SerializeField]
	MyField myField1;

	/// <summary>
	/// フィールド２
	/// </summary>
	[SerializeField]
	MyField myField2;

	/// <summary>
	/// フィールド３
	/// </summary>
	[SerializeField]
	MyField myField3;

	/// <summary>
	/// フィールド４
	/// </summary>
	[SerializeField]
	MyField myField4;
	#endregion

	/// <summary>
	/// 現在のフィールド
	/// </summary>
	MyField m_currentField;
	public MyField CurrentField
	{
		get { return m_currentField; }
		set { m_currentField = value; }
	}
	
	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ステージを変更
	/// </summary>
	/// <param name="stageNum">ステージ番号</param>
	/// <returns>成功か</returns>
	public bool ChangeStage(int stageNum)
	{
		//現在のフィールドの削除
		if (m_currentField)
			Destroy(m_currentField.gameObject);

		//ステージ番号
		switch(stageNum)
		{
			case 0:
				m_currentField = Instantiate(myField1.gameObject, transform).GetComponent<MyField>();
				break;
			case 1:
				m_currentField = Instantiate(myField2.gameObject, transform).GetComponent<MyField>();
				break;
			case 2:
				m_currentField = Instantiate(myField3.gameObject, transform).GetComponent<MyField>();
				break;
			case 3:
				m_currentField = Instantiate(myField4.gameObject, transform).GetComponent<MyField>();
				break;
			default:
				return false;
		}

		return true;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 現在のフィールドのボス部屋の中心位置を取得
	/// </summary>
	/// <returns></returns>
	public Vector3 GetCenterPosBossRoomCurrentField()
	{
		return m_currentField.BossRoomCenterPos;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 現在のフィールドの必殺技１の位置を取得
	/// </summary>
	/// <param name="num"></param>
	/// <returns></returns>
	public Vector3 GetPosDeathblow1CurrentField(int num)
	{
		num = (num < 0) ? 0 : (num > 4) ? 4 : num;
		return m_currentField.GetPosDeathblow1()[num];
	}
}
