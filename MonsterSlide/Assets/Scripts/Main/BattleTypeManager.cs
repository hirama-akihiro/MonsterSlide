using UnityEngine;
using System.Collections;

public class BattleTypeManager : SingletonMonoBehavior<BattleTypeManager> {

	public enum BattleType { OnlineBattle, NearBattle_Client, NearBattle_Server, SingleBattle };

	/// <summary>
	/// 対戦方式
	/// </summary>
	public BattleType battleType;

	protected override void Awake() { base.Awake(); }

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
