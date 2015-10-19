using UnityEngine;
using System.Collections;

public class TitleIndicator : MonoBehaviour {

	/// <summary>
	/// GUISkin
	/// </summary>
	public GUISkin guiSkin;

	// Use this for initialization
	void Start () {
		AudioManager.Instance.StopAudio();
		AudioManager.Instance.PlayAudio("game_maoudamashii_4_field11");
		AudioManager.Instance.GetAudioClip("game_maoudamashii_4_field11").loop = true;
	}
	
	// Update is called once per frame
	//  Author kazuki ito
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			// ゲーム終了
			Application.Quit();
		}
		if (CrossInput.Instance.IsDown ()) {

			AudioManager.Instance.PlayAudio("se_titleButton");
			// Menuシーンへ遷移
			FadeManager.Instance.LoadLevel("Home", 0.5f);
		}
	
	}
}
