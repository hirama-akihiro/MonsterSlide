using UnityEngine;
using System.Collections;

public class MainManager : SingletonMonoBehavior<MainManager> {

	/// <summary>
	/// 勝敗用Prefab
	/// </summary>
	public GameObject WinLosePrefab;

	GameObject Bt;
	
	AndroidBlueToothAdapter BtAdapter;
	
	static bool winner = false;
	
	// Use this for initialization
	protected override void Awake()
	{
		base.Awake();
		AudioManager.I.StopAudio();
		AudioManager.I.PlayAudio("bgm_battle", AudioManager.PlayMode.Repeat, 0.5f);

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
		if (BtAdapter != null) { BtAdapter.SendIntergerData(0, Tag.End); }
		FadeManager.Instance.LoadLevel("Result", 0.5f);
	}

	// Update is called once per frame
    void Update()
	{
		//if (IsGameOver)
		//{
		//	SkillMontamaManager.I.SkillButtomEnable(false);
		//	GameEnder.I.IsGameEnd = true;
		//	if (BtAdapter != null) { BtAdapter.SendFloatData(0f, Tag.End); }
		//	GameObject WinLose = Instantiate(WinLosePrefab);
		//	WinLose.GetComponent<WinLose>().SetWinEnable(winner);
		//	IsGameOver = false;
		//}
	}

	//public void GameOver()
	//{
	//	SkillMontamaManager.I.SkillButtomEnable(false);
	//	GameEnder.I.GameEnd ();
	//	winner = true;

	//	// 勝利BGM
	//	AudioManager.I.PlayAudio("bgm_win");
	//	GameObject WinLose = Instantiate(WinLosePrefab);
	//	WinLose.GetComponent<WinLose>().SetWinEnable(winner);
	//}

	public void GameOver(bool isWin)
	{
		SkillMontamaManager.I.SkillButtomEnable(false);
		GameEnder.I.GameEnd();
		if (BtAdapter != null) { BtAdapter.SendFloatData(0f, Tag.End); }
		GameObject WinLose = Instantiate(WinLosePrefab);
		WinLose.GetComponent<WinLose>().SetWinEnable(isWin);
		if (isWin) { AudioManager.I.PlayAudio("bgm_win"); }
	}
}
