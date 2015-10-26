using UnityEngine;
using System.Collections;

public class MainManager : SingletonMonoBehavior<MainManager> {

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
	protected override void Awake()
	{
		base.Awake();
		AudioManager.Instance.StopAudio();
		AudioManager.Instance.PlayAudio("bgm_battle", AudioManager.PlayMode.Repeat);

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

		if (IsGameOver) {
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
		}
	}

	public void GameOver()
	{
		SkillMontamaManager.Instance.SkillButtomEnable(false);
		GameEnder.Instance.GameEnd ();
		winner = true;

		// 勝利BGM
		AudioManager.Instance.PlayAudio("bgm_win");
		GameObject WinLose = Instantiate(WinLosePrefab);
		WinLose.GetComponent<WinLose>().SetWinEnable(winner);
	}

	/// <summary>
	/// ゲームがクリアされているかどうか
	/// </summary>
	public bool IsGameOver { get; set; }
}
