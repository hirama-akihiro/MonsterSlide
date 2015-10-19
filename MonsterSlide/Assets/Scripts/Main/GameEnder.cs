using UnityEngine;
using System.Collections;

public class GameEnder : SingletonMonoBehavior<GameEnder> {

	//  ↓  Author kazuki ito
	private bool oldIsGameEnd;
	//  ↑ Author kabuki ito

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//  ↓  Author kazuki ito
		if (oldIsGameEnd != IsGameEnd) {
			if (!IsGameEnd) { 
				GameStart ();
				oldIsGameEnd = IsGameEnd;
				Debug.Log("Game Start");
				return;
			} else {
				GameEnd ();
				oldIsGameEnd = IsGameEnd;
				Debug.Log("Game End");
			}
		}
		//  ↑ Author kabuki ito
	}

	/// <summary>
	/// ゲーム終了時に呼ぶメソッド
	/// </summary>
	public void GameEnd()
	{
		GeneratorManager.Instance.GameEnd();
		SkillMontamaManager.Instance.GameEnd();
		LaneManager.Instance.GameEnd();
		//  ↓  Author kazuki ito
		GameObject[] puzzleMontamas = GameObject.FindGameObjectsWithTag("Montama");
		foreach (GameObject puzzleMontama in puzzleMontamas) {
			puzzleMontama.GetComponent<PuzzleMontama>().GameEnd();
		}
		//  ↑ Author kabuki ito
	}

	//  ↓  Author kazuki ito
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
	//  ↑ Author kabuki ito


	/// <summary>
	/// ゲームが終了かどうか
	/// </summary>
	public bool IsGameEnd { get; set; }


}
