﻿using UnityEngine;
using System.Collections;

public class ServerClientIndicator : MonoBehaviour {

	/// <summary>
	/// GUISkin
	/// </summary>
	public GUISkin guiSkin;

	//  ↓  Author kazuki ito
	static bool isServer;
	public GameObject BtPrefab;
	GameObject Bt;
	AndroidBlueToothAdapter BtAdapter;
	//  ↑ Author kabuki ito



	// Use this for initialization
	/// <summary>
	/// Start this instance.
	/// Author Kazuki Ito
	/// </summary>
	void Start()
	{
		if (GameObject.FindGameObjectWithTag ("BlueTooth") == null) {
			Bt = Instantiate (BtPrefab);

			BtAdapter = Bt.GetComponent<AndroidBlueToothAdapter> ();

			BtAdapter.Create ();

			DontDestroyOnLoad (Bt);
		} else {
			BtAdapter = GameObject.FindGameObjectWithTag("BlueTooth").GetComponent<AndroidBlueToothAdapter> ();
		}
		BtAdapter.BlueToothEnable ();

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey (KeyCode.Escape)) {

			StartManager.SetManagerEnable(false);
			// ゲーム終了
			Application.LoadLevel("Menu");

		}

	}

	/// <summary>
	/// Gets the is server.
	/// Author Kazuki Ito
	/// </summary>
	/// <returns><c>true</c>, if is server was gotten, <c>false</c> otherwise.</returns>
	public static bool getIsServer()
	{
		return isServer;
	}

	/// <summary>
	/// Raises the click server event.
	/// Author Kazuki Ito
	/// </summary>
	public void OnClickServer()
	{
		isServer = true;
		PartySettingManager.Instance.SetServerParty();
		BattleTypeManager.Instance.battleType = BattleTypeManager.BattleType.NearBattle_Server;
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		FadeManager.Instance.LoadLevel("Matching", 0.5f);
	}

	/// <summary>
	/// Raises the click client event.
	/// Author Kazuki Ito
	/// </summary>
	public void OnClickClient()
	{
		isServer = false;
		PartySettingManager.Instance.SetClientParty();
		BattleTypeManager.Instance.battleType = BattleTypeManager.BattleType.NearBattle_Client;
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		FadeManager.Instance.LoadLevel("Matching", 0.5f);
	}

	/// <summary>
	/// Raises the click jump to main event.
	/// Author Kazuki Ito
	/// </summary>
	public void OnClickJumpToMain()
	{
		StartManager.SetManagerEnable(false);
		PartySettingManager.Instance.SetSingleParty();
		// シーン遷移
		BattleTypeManager.Instance.battleType = BattleTypeManager.BattleType.SingleBattle;
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		FadeManager.Instance.LoadLevel("VS", 0.5f);
	}

	/// <summary>
	/// Raises the click return event.
	/// Author Kazuki Ito
	/// </summary>
	public void OnClickReturn()
	{
		StartManager.SetManagerEnable(false);
		// シーン遷移
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		FadeManager.Instance.LoadLevel("BattleModeSelect", 0.5f);
	}

	/// <summary>
	/// Raises the click home event.
	/// Autohr Kazuki Ito
	/// </summary>
	public void OnClickHome()
	{
		StartManager.SetManagerEnable(false);
		// シーン遷移
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		FadeManager.Instance.LoadLevel("Home", 0.5f);
	}

	public void OnClickGame()
	{
		StartManager.SetManagerEnable(false);
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		FadeManager.Instance.LoadLevel("GameModeSelect", 0.5f);
	}

	/// <summary>
	/// Raises the click party select event.
	/// Author Kazuki Ito
	/// </summary>
	public void OnClickPartySelect()
	{
		StartManager.SetManagerEnable(false);
		// シーン遷移
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		FadeManager.Instance.LoadLevel("PartySelect", 0.5f);
	}
}
