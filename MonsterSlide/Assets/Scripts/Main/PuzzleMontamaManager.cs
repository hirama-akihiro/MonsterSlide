using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PuzzleMontamaManager : SingletonMonoBehavior<PuzzleMontamaManager>
{

	/// <summary>
	/// ゲーム画面中に存在するパズルリスト
	/// </summary>
	private List<PuzzleMontama> puzzleMontamas;

	// Use this for initialization
	void Awake()
	{
		puzzleMontamas = new List<PuzzleMontama>();
	}

	// Update is called once per frame
	void Update()
	{
		//Debug.Log("ManagerSiuze:" + puzzleMontamas.Count);
	}

	/// <summary>
	/// 配列からNull要素を削除して配列を作りなおす
	/// </summary>
	public void DeleteNullObject()
	{
		puzzleMontamas.RemoveAll(s => s == null);
	}

	public List<PuzzleMontama> PuzzleMontamas { get { return puzzleMontamas; } set { puzzleMontamas = value; } }
}
