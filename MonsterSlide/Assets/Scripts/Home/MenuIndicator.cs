using UnityEngine;
using System.Collections;

public class MenuIndicator : MonoBehaviour {

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey (KeyCode.Escape)) {
			// ゲーム終了
			Application.LoadLevel("Title");
		}
	}

	public void OnClickHome()
	{
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		FadeManager.Instance.LoadLevel("Home", 0.5f);
	}

	/// <summary>
	/// Raises the click server client event.
	/// Author Kazuki Ito
	/// </summary>
	public void OnClickGame()
	{
		// シーン遷移
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		FadeManager.Instance.LoadLevel("GameModeSelect", 0.5f);
	}

	
	/// <summary>
	/// Raises the click party select event.
	/// Author Kazuki Ito
	/// </summary>
	public void OnClickPartySelect()
	{
		// シーン遷移
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		FadeManager.Instance.LoadLevel("PartySelect", 0.5f);
	}
}
