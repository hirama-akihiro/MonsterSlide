using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ResultIndicator : MonoBehaviour {

	/// <summary>
	/// GUISkin
	/// </summary>
	public GUISkin guiSkin;

	public GameObject Retry;

	public GameObject YesButton;

	//public GameObject EndButton;
	public GameObject Darken;

	public GameObject RetryDialog;

	public GameObject waitingSprite;

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
			Retry.SetActive(true);
			Darken.SetActive(true);
		}


		if (oldNoRetry != noRetry){
			if (!noRetry) {
				YesButton.SetActive(true);
			} else {
				YesButton.SetActive(false);
			}
			oldNoRetry = noRetry;
		}


		if (request) {
			Retry.SetActive(false);
			//YesButton.SetActive(false);
			//EndButton.SetActive(false);
			waitingSprite.SetActive(true);
		}
	}

//	void OnGUI()
//	{
//		int sw = Screen.width;
//		int sh = Screen.height;
//		GUI.skin = guiSkin;

		// シーン名
//		GUI.Label (new Rect (0, 0, sw, sh), "リザルト画面", "SceneName");

//		if (!noRetry) {
//			// シーン遷移ボタン
//			if (GUI.Button (new Rect (sw * 0.25f, sh * 0.6f, sw * 0.5f, sh / 12), "もう一度対戦")) {
//
//
//				//  ↑ Author kabuki ito
//
//
//				// シーン遷移
//				//Application.LoadLevel ("Main");
//			}
//		}
//		if (GUI.Button (new Rect (sw * 0.25f, sh * 0.75f, sw * 0.5f, sh / 12), "Menu画面へ")) {
//			// ゲーム終了
//			//  ↓  Author kazuki ito
//			ReturnToMain ();
//			//  ↑ Author kabuki ito
//
//		}
//	}

	public void receiveRetryRequest()
	{
		RetryDialog.SetActive (true);
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
	public static void setNoRetry(bool enable)
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
