using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ResultManager : MonoBehaviour {

	/// <summary>
	/// リトライダイアログ
	/// </summary>
	public GameObject retryDialog;

	/// <summary>
	/// Yesボタン
	/// </summary>
	public GameObject yesButton;

	/// <summary>
	/// リトライシェード
	/// </summary>
	public GameObject retryShade;

	/// <summary>
	/// 再挑戦ダイアログ
	/// </summary>
	public GameObject rematchDialog;

	/// <summary>
	/// 待機スプライトオブジェクト
	/// </summary>
	public GameObject waitingSprite;

	/// <summary>
	/// 1P勝利時に表示するオブジェクト
	/// </summary>
	public GameObject winObjects;

	/// <summary>
	/// 1p敗北時に表示するオブジェクト
	/// </summary>
	public GameObject loseObjects;

	//  ↓  Author kazuki ito
	GameObject Bt;

	AndroidBlueToothAdapter BtAdapter;

	static bool noRetry = false;

	bool oldNoRetry = false;

	bool retryFlag = false;

	bool cancelFlag = false;

	bool request = false;
	//  ↑ Author kabuki ito


	// Use this for initialization
	void Start()
	{
		// ゲームのBGMを止める
		AudioManager.I.StopAudio();

		if (WinLoseManager.I.battleResult == WinLoseManager.BattleResult.Win)
		{
			winObjects.SetActive(true);
			loseObjects.SetActive(false);
		}
		else
		{
			winObjects.SetActive(false);
			loseObjects.SetActive(true);
		}

		//  ↓  Author kazuki ito
#if UNITY_ANDROID && !UNITY_EDITOR
		Bt = GameObject.FindGameObjectWithTag("BlueTooth");
		BtAdapter = Bt.GetComponent<AndroidBlueToothAdapter> ();
	
#endif
		//  ↑ Author kabuki ito
	}

	// Update is called once per frame
	void Update()
	{
		if (CrossInput.I.IsDown () && !request) {
			retryDialog.SetActive(true);
			retryShade.SetActive(true);
		}

		if (oldNoRetry != noRetry){
			if (!noRetry) {
				yesButton.SetActive(true);
			} else {
				yesButton.SetActive(false);
			}
			oldNoRetry = noRetry;
		}


		if (request) {
			retryDialog.SetActive(false);
			waitingSprite.SetActive(true);
		}
	}

	public void ReceiveRetryRequest()
	{
		rematchDialog.SetActive (true);
	}

	/// <summary>
	/// Ons the click retry.
	/// Author Kazuki Ito
	/// </summary>
	public void onClickRetry()
	{
		if (BtAdapter != null) {
			BtAdapter.SendIntergerData (0,Tag.RetryRequest);
			request = true;
		}
		else { FadeManager.Instance.LoadLevel("Main", 0.5f); }
	}

	/// <summary>
	/// Sets the no retry.
	/// Author Kazuki Ito
	/// </summary>
	/// <param name="enable">If set to <c>true</c> enable.</param>
	public static void SetNoRetry(bool enable)
	{
		noRetry = enable;
	}

	/// <summary>
	/// Games the retry.
	/// Author Kazuki Ito
	/// </summary>
	public void GameRetry()
	{
		if (!retryFlag) {
			DontDestroyOnLoad (Bt);
			if (BtAdapter != null) {
				BtAdapter.SendIntergerData (0, Tag.RetryOK);
			}
			FadeManager.Instance.LoadLevel("Main", 0.5f);
			retryFlag = true;
		}
	}


	/// <summary>
	/// Determines whether this instance cancel retry.
	/// Author Kazuki Ito
	/// </summary>
	/// <returns><c>true</c> if this instance cancel retry; otherwise, <c>false</c>.</returns>
	public void CancelRetry()
	{
		if (!cancelFlag) {
			if (BtAdapter != null) {
				BtAdapter.SendIntergerData (0, Tag.RetryNO);
			}
			cancelFlag = true;
			ReturnToMain();
		}
	}

	/// <summary>
	/// Returns to main.
	/// Author Kazuki Ito
	/// </summary>
	public void ReturnToMain()
	{
		// ゲーム終了
		if(BtAdapter != null && !noRetry)
		{
			BtAdapter.disConnect();
		}
		
		noRetry = false;
		Destroy(Bt);
		AudioManager.I.PlayAudio("game_maoudamashii_4_field11");
		FadeManager.Instance.LoadLevel("Home", 0.5f);
	}
}
