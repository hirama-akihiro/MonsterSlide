using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 対戦相手の基底クラス
/// </summary>
public class BOpponent : MonoBehaviour, IOpponent {

	/// <summary>
	/// 現状の対戦相手のHp
	/// </summary>
	protected float nowHp = 1.0f;

	/// <summary>
	/// 対戦相手が使用するモンクリのパーティ
	/// </summary>
	public List<GameObject> partyMonkuris = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// 現状の対戦相手のHP
	/// </summary>
	/// <returns></returns>
	public float GetNowHp() { return nowHp; }
}
