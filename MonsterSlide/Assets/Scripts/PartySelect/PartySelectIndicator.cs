using UnityEngine;
using System.Collections;

public class PartySelectIndicator : MonoBehaviour {

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	
	/// <summary>
	/// Raises the click return event.
	/// Author Kazuki Ito
	/// </summary>
	public void OnClickReturn()
	{
		OnClickHome();
	}

	/// <summary>
	/// Raises the click home event.
	/// Autohr Kazuki Ito
	/// </summary>
	public void OnClickHome()
	{
		// シーン遷移
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		FadeManager.Instance.LoadLevel("Home", 0.5f);
	}

	public void OnClickGame()
	{
		StartManager.SetManagerEnable(true);
		// シーン遷移
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		FadeManager.Instance.LoadLevel("GameModeSelect", 0.5f);
	}

	public void OnClickParty()
	{
		// シーン遷移
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		FadeManager.Instance.LoadLevel("PartySelect", 0.5f);
	}
}
