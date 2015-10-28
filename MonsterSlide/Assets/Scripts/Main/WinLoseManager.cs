using UnityEngine;
using System.Collections;

public class WinLoseManager : SingletonMonoBehavior<WinLoseManager> {

	/// <summary>
	/// 戦闘結果
	/// </summary>
	public enum BattleResult { Win, Lose }

	/// <summary>
	/// 戦闘結果
	/// </summary>
	public BattleResult battleResult = BattleResult.Lose;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
