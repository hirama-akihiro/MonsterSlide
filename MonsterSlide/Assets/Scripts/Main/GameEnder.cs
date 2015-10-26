using UnityEngine;
using System.Collections;

public class GameEnder : SingletonMonoBehavior<GameEnder> {

	private bool oldIsGameEnd;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (oldIsGameEnd != IsGameEnd) {
			if (!IsGameEnd) { 
				GameStart ();
				oldIsGameEnd = IsGameEnd;
				return;
			} else {
				GameEnd ();
				oldIsGameEnd = IsGameEnd;
			}
		}
	}

	/// <summary>
	/// ゲーム終了時に呼ぶメソッド
	/// </summary>
	public void GameEnd()
	{
		GeneratorManager.Instance.GameEnd();
		SkillMontamaManager.Instance.GameEnd();
		LaneManager.Instance.GameEnd();
		GameObject[] puzzleMontamas = GameObject.FindGameObjectsWithTag("Montama");
		foreach (GameObject puzzleMontama in puzzleMontamas) {
			puzzleMontama.GetComponent<PuzzleMontama>().GameEnd();
		}
	}

	public void GameStart()
	{
		GeneratorManager.Instance.GameStart ();
		SkillMontamaManager.Instance.GameStart ();
		LaneManager.Instance.GameStart ();
		GameObject[] puzzleMontamas = GameObject.FindGameObjectsWithTag("Montama");
		foreach (GameObject puzzleMontama in puzzleMontamas) {
			puzzleMontama.GetComponent<PuzzleMontama>().GameStart();
		}
	}

	/// <summary>
	/// ゲームが終了かどうか
	/// </summary>
	public bool IsGameEnd { get; set; }


}
