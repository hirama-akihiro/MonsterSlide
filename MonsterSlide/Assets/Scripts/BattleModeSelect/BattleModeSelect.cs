using UnityEngine;
using System.Collections;

public class BattleModeSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Escape)) { Application.LoadLevel("Title"); }
	}

	public void OnClickNearBattle()
	{
		StartManager.SetManagerEnable(true);
		AudioManager.I.PlayAudio("se_tap");
		FadeManager.Instance.LoadLevel("ServerClientSelect", 0.5f);
	}
}
