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
/// AIマネージャ
/// </summary>
public class MyAiManager : MonoBehaviour
{
	/// <summary>
	/// キャラクター
	/// </summary>
	[SerializeField]
	MyCharacter myCharacter;
	public MyCharacter CharacterScript
	{
		get { return myCharacter; }
	}

	#region 生成されるプレファブ
	[Header("生成されるプレファブ")]
	/// <summary>
	/// ステージのボス配列
	/// </summary>
	[SerializeField]
	MyAiBoss[] m_stagesBoss;
	#endregion

	/// <summary>
	/// ボス
	/// </summary>
	MyAiBoss m_boss;

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// ボスを生成する
	/// </summary>
	/// <param name="stageNum">ステージ番号</param>
	public void GenerateBoss(int stageNum)
	{
		//ステージ番号チェック
		stageNum = (stageNum < 0) ? 0 : (stageNum >= m_stagesBoss.Length) ? m_stagesBoss.Length - 1 : stageNum;

		//破棄と生成と登録
		if (m_boss)
			Destroy(m_boss.gameObject);
		m_boss = Instantiate(m_stagesBoss[stageNum].gameObject, transform).GetComponent<MyAiBoss>();
		m_boss.transform.position = myCharacter.GameScript.StageScript.CurrentField.BossStartPos;
		m_boss.transform.LookAt(m_boss.transform.position + myCharacter.GameScript.StageScript.CurrentField.BossStartDirection);
		myCharacter.BossScript = m_boss;
	}
}
