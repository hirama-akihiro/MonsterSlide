using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour
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
	void Update()
	{
		if(isInitialUpdate && !Application.isShowingSplashScreen)
		{
			AudioManager.I.StopAudio();
			AudioManager.I.PlayAudio("bgm_title", AudioManager.PlayMode.Repeat);
			isInitialUpdate = false;
		}

		if (isInitialUpdate) { return; }
		if (Input.GetKey(KeyCode.Escape)) { Application.Quit(); }

		if (CrossInput.I.IsDown())
		{
			AudioManager.I.PlayAudio("se_titleButton");
			FadeManager.Instance.LoadLevel("Home", 0.5f);
		}
	}
}
