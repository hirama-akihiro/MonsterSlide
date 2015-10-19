using UnityEngine;
using System.Collections;

public class BattleModeSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Escape))
		{
			// ゲーム終了
			Application.LoadLevel("Title");
		}
	}

	public void OnClickNearBattle()
	{
		StartManager.SetManagerEnable(true);
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		FadeManager.Instance.LoadLevel("ServerClientSelect", 0.5f);
	}

	public void OnClickHome()
	{
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		FadeManager.Instance.LoadLevel("Home", 0.5f);
	}

	public void OnClickGame()
	{
		// シーン遷移
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		FadeManager.Instance.LoadLevel("GameModeSelect", 0.5f);
	}

	public void OnClickPartySelect()
	{
		// シーン遷移
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		FadeManager.Instance.LoadLevel("PartySelect", 0.5f);
	}
}
