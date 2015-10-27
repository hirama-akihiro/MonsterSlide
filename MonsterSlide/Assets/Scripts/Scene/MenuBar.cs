using UnityEngine;
using System.Collections;

public class MenuBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// ホームシーンへ遷移
	/// </summary>
	public void OnClickHome()
	{
		AudioManager.I.PlayAudio("se_tap");
		FadeManager.Instance.LoadLevel("Home", 0.5f);
	}

	/// <summary>
	/// ゲームシーンへ遷移
	/// </summary>
	public void OnClickGame()
	{
		AudioManager.I.PlayAudio("se_tap");
		FadeManager.Instance.LoadLevel("GameModeSelect", 0.5f);
	}

	/// <summary>
	/// パーティセレクトシーンへ遷移
	/// </summary>
	public void OnClickParty()
	{
		AudioManager.I.PlayAudio("se_tap");
		FadeManager.Instance.LoadLevel("PartySelect", 0.5f);
	}

	/// <summary>
	/// ガチャシーンへ遷移
	/// </summary>
	public void OnClickGacha()
	{
		AudioManager.I.PlayAudio("se_tap");
	}

	/// <summary>
	/// サポートシーンへ遷移
	/// </summary>
	public void OnClickSupport()
	{
		AudioManager.I.PlayAudio("se_tap");
	}
}
