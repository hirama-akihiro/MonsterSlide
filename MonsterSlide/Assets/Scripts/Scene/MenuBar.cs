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
		AudioManager.Instance.PlayAudio("se_tap");
		FadeManager.Instance.LoadLevel("Home", 0.5f);
	}

	/// <summary>
	/// ゲームシーンへ遷移
	/// </summary>
	public void OnClickGame()
	{
		AudioManager.Instance.PlayAudio("se_tap");
		FadeManager.Instance.LoadLevel("GameModeSelect", 0.5f);
	}

	/// <summary>
	/// パーティセレクトシーンへ遷移
	/// </summary>
	public void OnClickParty()
	{
		AudioManager.Instance.PlayAudio("se_tap");
		FadeManager.Instance.LoadLevel("PartySelect", 0.5f);
	}

	/// <summary>
	/// ガチャシーンへ遷移
	/// </summary>
	public void OnClickGacha()
	{
		AudioManager.Instance.PlayAudio("se_tap");
	}

	/// <summary>
	/// サポートシーンへ遷移
	/// </summary>
	public void OnClickSupport()
	{
		AudioManager.Instance.PlayAudio("se_tap");
	}
}
