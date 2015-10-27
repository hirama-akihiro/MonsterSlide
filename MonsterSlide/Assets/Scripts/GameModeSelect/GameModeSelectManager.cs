using UnityEngine;
using System.Collections;

public class GameModeSelectManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickBattle()
	{
		AudioManager.I.PlayAudio("se_tap");
		FadeManager.Instance.LoadLevel("BattleModeSelect", 0.5f);
	}
}
