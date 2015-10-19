using UnityEngine;
using System.Collections;

public class BattleTypeManager : SingletonMonoBehavior<BattleTypeManager> {

	public enum BattleType { OnlineBattle, NearBattle_Client, NearBattle_Server, SingleBattle };

	/// <summary>
	/// 対戦方式
	/// </summary>
	public BattleType battleType;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
