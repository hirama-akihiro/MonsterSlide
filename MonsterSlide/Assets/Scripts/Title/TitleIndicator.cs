using UnityEngine;
using System.Collections;

public class TitleIndicator : MonoBehaviour
{
	/// <summary>
	/// 最初のスプラッシュスクリーン後の最初のUpdateか
	/// </summary>
	private bool isInitialUpdate = true;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	//  Author kazuki ito
	void Update()
	{
		if(isInitialUpdate && !Application.isShowingSplashScreen)
		{
			AudioManager.Instance.StopAudio();
			AudioManager.Instance.PlayAudio("game_maoudamashii_4_field11");
			AudioManager.Instance.GetAudioClip("game_maoudamashii_4_field11").loop = true;
			isInitialUpdate = false;
		}
		if (isInitialUpdate) { return; }
		if (Input.GetKey(KeyCode.Escape)) { Application.Quit(); }

		if (CrossInput.Instance.IsDown())
		{
			AudioManager.Instance.PlayAudio("se_titleButton");
			// Menuシーンへ遷移
			FadeManager.Instance.LoadLevel("Home", 0.5f);
		}

	}
}
