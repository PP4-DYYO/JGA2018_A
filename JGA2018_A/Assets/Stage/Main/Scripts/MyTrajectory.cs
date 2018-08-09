////////////////////////////////////////////////////////////////////////////////////////////////////
//
//2018/8/9～
//制作者　京都コンピュータ学院京都駅前校　ゲーム学科　四回生　奥田裕也
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 軌跡クラス
/// </summary>
public class MyTrajectory : MonoBehaviour
{
	/// <summary>
	/// 座標１
	/// </summary>
	[SerializeField]
	Transform m_pos1;

	/// <summary>
	/// 座標２
	/// </summary>
	[SerializeField]
	Transform m_pos2;

	/// <summary>
	/// 表示
	/// </summary>
	bool m_isDisplay;

	/// <summary>
	/// 表示時間
	/// </summary>
	[SerializeField]
	float m_displayTime;

	/// <summary>
	/// 表示時間を数える
	/// </summary>
	float m_countDisplayTime;

	/// <summary>
	/// 軌跡のメッシュ
	/// </summary>
	Mesh TrajectoryMesh;

	/// <summary>
	/// 軌跡のメッシュレンダラー
	/// </summary>
	MeshRenderer TrajectoryMeshRenderer;

	/// <summary>
	/// 軌跡のマテリアル
	/// </summary>
	[SerializeField]
	Material TrajectoryMaterial;

	/// <summary>
	/// 四角形を表示する個数
	/// </summary>
	[SerializeField]
	int m_quantityToDisplaySquare;

	/// <summary>
	/// 頂点リスト
	/// </summary>
	List<Vector3> m_verticesLists = new List<Vector3>();

	/// <summary>
	/// UVリスト
	/// </summary>
	List<Vector2> m_uvsLists = new List<Vector2>();

	/// <summary>
	/// 座標１の位置リスト
	/// </summary>
	List<Vector3> m_pos1Points = new List<Vector3>();

	/// <summary>
	/// 座標２の位置リスト
	/// </summary>
	List<Vector3> m_pos2Points = new List<Vector3>();

	/// <summary>
	/// 三角形の頂点リスト
	/// </summary>
	List<int> m_vertexListOfTriangles = new List<int>();

	/// <summary>
	/// UVの比率
	/// </summary>
	float m_uvRatio;

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 初期
	/// </summary>
	void Start()
	{
		TrajectoryMesh = gameObject.AddComponent<MeshFilter>().mesh;
		TrajectoryMeshRenderer = gameObject.AddComponent<MeshRenderer>();

		TrajectoryMeshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		TrajectoryMeshRenderer.receiveShadows = false;
		TrajectoryMeshRenderer.material = TrajectoryMaterial;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 定期フレーム
	/// </summary>
	void FixedUpdate()
	{
		//表示しない
		if (!m_isDisplay)
			return;

		m_countDisplayTime += Time.deltaTime;

		//表示時間の終わり
		if (m_countDisplayTime > m_displayTime)
			m_isDisplay = false;

		TrajectoryMeshRenderer.enabled = m_isDisplay;
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 後フレーム
	/// </summary>
	void LateUpdate()
	{
		//表示する四角形が超えた
		if (m_pos1Points.Count > m_quantityToDisplaySquare)
		{
			//古い四角形の削除
			m_pos1Points.RemoveAt(0);
			m_pos2Points.RemoveAt(0);
		}

		//現在の座標１と座標２の登録
		m_pos1Points.Add(m_pos1.position);
		m_pos2Points.Add(m_pos2.position);

		//表示できる頂点が指定数を超えた
		if (m_pos1Points.Count > m_quantityToDisplaySquare)
			CreateMesh();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// メッシュの作成
	/// </summary>
	void CreateMesh()
	{
		//メッシュのリセット
		TrajectoryMesh.Clear();

		//メッシュに必要なリストのクリア
		m_verticesLists.Clear();
		m_uvsLists.Clear();
		m_vertexListOfTriangles.Clear();

		//表示する四角形の数
		for (var i = 0; i < m_quantityToDisplaySquare; i++)
		{
			//四角形として頂点を登録
			m_verticesLists.AddRange(new Vector3[] {
				m_pos1Points[i], m_pos2Points[i], m_pos1Points[i + 1],
				m_pos1Points[i + 1], m_pos2Points[i], m_pos2Points[i + 1]
			});
		}

		// UVMapのパラメータ設定
		m_uvRatio = 0f;
		for (var i = 0; i < m_quantityToDisplaySquare; i++)
		{
			//四角形のテクスチャの割り当てを設定
			m_uvsLists.AddRange(new Vector2[]{
				new Vector2(m_uvRatio, 0f), new Vector2(m_uvRatio, 1f), new Vector2(m_uvRatio + 1f / m_quantityToDisplaySquare, 0f),
				new Vector2(m_uvRatio + 1f / m_quantityToDisplaySquare, 0f), new Vector2(m_uvRatio, 1f), new Vector2(m_uvRatio + 1f / m_quantityToDisplaySquare, 1f)
			});

			//表示する四角形数で割合を計算
			m_uvRatio += 1f / m_quantityToDisplaySquare;
		}

		//メッシュ用の三角形を登録した頂点で設定
		for (var i = 0; i < m_verticesLists.Count; i++)
		{
			m_vertexListOfTriangles.Add(i);
		}

		//メッシュの設定
		TrajectoryMesh.vertices = m_verticesLists.ToArray();
		TrajectoryMesh.uv = m_uvsLists.ToArray();
		TrajectoryMesh.triangles = m_vertexListOfTriangles.ToArray();

		//メッシュの再計算
		TrajectoryMesh.RecalculateBounds();
		TrajectoryMesh.RecalculateNormals();
	}

	//----------------------------------------------------------------------------------------------------
	/// <summary>
	/// 表示する
	/// </summary>
	public void Display()
	{
		m_isDisplay = true;
		m_countDisplayTime = 0;
	}
}
