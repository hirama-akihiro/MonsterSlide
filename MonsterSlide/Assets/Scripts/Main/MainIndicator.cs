using UnityEngine;
using System.Collections;

public class MainIndicator : SingletonMonoBehavior<MainIndicator> {

	/// <summary>
	/// GUISkin
	/// </summary>
	public GUISkin guiSkin;

	//  ↓ Author kazuki ito
	public GameObject WinLosePrefab;

	GameObject Bt;
	
	AndroidBlueToothAdapter BtAdapter;
	
	static bool winner = false;
	//  ↑ Author kabuki ito
	
	// Use this for initialization
	void Awake()
	{
		AudioManager.Instance.StopAudio();
		AudioManager.Instance.PlayAudio("bgm_battle");
		AudioManager.Instance.GetAudioClip("bgm_battle").volume = 0.5f;
		AudioManager.Instance.GetAudioClip("bgm_battle").loop = true;

		//  ↓ Author kazuki ito
		ResultIndicator.setNoRetry (false);
		winner = false;
#if UNITY_ANDROID && !UNITY_EDITOR
		Bt = GameObject.FindGameObjectWithTag("BlueTooth");
		BtAdapter = Bt.GetComponent<AndroidBlueToothAdapter>();
		//  ↑ Author kabuki ito
#endif
	}




	public void OnClickMenu()
	{
		if(BtAdapter != null)
		{
			BtAdapter.SendIntergerData (0, Tag.End);

		}
		IsGameOver = false;
		FadeManager.Instance.LoadLevel("Result", 0.5f);
	}

	// Update is called once per frame
	void Update()
	{
//		int sw = Screen.width;
//		int sh = Screen.height;
//		GUI.skin = guiSkin;
		//float fps = 1f / Time.deltaTime;
		//Debug.LogFormat("{0}fps", fps);
		//GUI.Label(new Rect(0, 0, 200, 200), ((int)fps).ToString() + "fps", "SceneName");

		if (IsGameOver) {
			//  ↓ Author kazuki ito
			SkillMontamaManager.Instance.SkillButtomEnable(false);
			GameEnder.Instance.IsGameEnd =true;
			if(BtAdapter != null)
			{
				BtAdapter.SendFloatData(0f,Tag.End);
				//BtAdapter.SendIntergerData(-1, Tag.End);
				Debug.Log(BtAdapter);
			}
			GameObject WinLose = Instantiate(WinLosePrefab);
			WinLose.GetComponent<WinLose>().SetWinEnable(winner);
			IsGameOver = false;
			//  ↑ Author kabuki ito
		}
	}

	//  ↓ Author kazuki ito
	public void GameOver()
	{
		SkillMontamaManager.Instance.SkillButtomEnable(false);
		//GameEnder.Instance.IsGameEnd = true;
		GameEnder.Instance.GameEnd ();
		winner = true;
		// 勝利BGM
		AudioManager.Instance.PlayAudio("bgm_win");
		GameObject WinLose = Instantiate(WinLosePrefab);
		WinLose.GetComponent<WinLose>().SetWinEnable(winner);
		//Application.LoadLevel("Result");
	}
	//  ↑ Author kabuki ito

	/// <summary>
	/// ゲームがクリアされているかどうか
	/// </summary>
	public bool IsGameOver { get; set; }
}
