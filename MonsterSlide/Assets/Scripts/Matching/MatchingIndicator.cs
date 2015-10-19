using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Matching indicator.
/// Author Kazuki Ito
/// </summary>

public class MatchingIndicator : MonoBehaviour {

	/// <summary>
	/// GUISkin
	/// </summary>
	//public GUISkin guiSkin;
	public GameObject message;
	public InputField addressField;
	public GameObject connectButton;
	string address="";
	GameObject Bt;
	AndroidBlueToothAdapter BtAdapter;
	bool isServer;
	bool rivalOk;
	bool playerOk;

	// UI差し替え後の追加変数
	public GameObject spriteMessage;
	public Sprite serverSprite;
	public Sprite clientSprite;

	public GameObject spriteLabel;
	public Sprite serverLabel;
	public Sprite clientLabel;

	public GameObject serverTextField;
	public GameObject clientTextField;

	// Use this for initialization
	void Start()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		Bt = GameObject.FindGameObjectWithTag ("BlueTooth");

		BtAdapter = Bt.GetComponent<AndroidBlueToothAdapter> ();
#endif

		isServer = ServerClientIndicator.getIsServer ();


		Text messageText = message.GetComponent<Text> ();

		if (isServer) {
			//messageText.text = "下のアドレスをクライアントに入力させて下さい";
			spriteMessage.GetComponent<SpriteRenderer>().sprite = serverSprite;
			spriteLabel.GetComponent<SpriteRenderer>().sprite = serverLabel;
			clientTextField.GetComponent<SpriteRenderer>().enabled = false;
			connectButton.SetActive(false);
			RectTransform rect = addressField.GetComponent<RectTransform>();
			rect.sizeDelta = new Vector2(1000f, 100f);
			Vector3 pos = rect.position;
			rect.position = new Vector3(0f,pos.y,pos.z);

			PlayerPrefs.SetInt("PlayerID",0);
			PlayerPrefs.SetInt("Rival",1);
			PlayerPrefs.SetInt("SkillMontama0",1);
			PlayerPrefs.SetInt("SkillMontama1",4);
			PlayerPrefs.SetInt("SkillMontama2",2);
			PlayerPrefs.SetInt("SkillMontama3",3);

		}
		else{
			spriteMessage.GetComponent<SpriteRenderer>().sprite = clientSprite;
			spriteLabel.GetComponent<SpriteRenderer>().sprite = clientLabel;
			serverTextField.GetComponent<SpriteRenderer>().enabled = false;
			//messageText.text = "下にサーバーのアドレスを入力して下さい";

			PlayerPrefs.SetInt("PlayerID", 0);
			PlayerPrefs.SetInt("Rival",0);
			PlayerPrefs.SetInt("SkillMontama0",0);
			PlayerPrefs.SetInt("SkillMontama1",5);
			PlayerPrefs.SetInt("SkillMontama2",4);
			PlayerPrefs.SetInt("SkillMontama3",2);
		}
	

		if (isServer && BtAdapter != null) {

			address = BtAdapter.StartServer ();
			addressField.text = address;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey (KeyCode.Escape)) {
			if(BtAdapter != null)
			{
				BtAdapter.Cancel();
			}

			FadeManager.Instance.LoadLevel("ServerClientSelect", 0.5f);
		}

		if (rivalOk && playerOk)
		{
			// シーン遷移
			FadeManager.Instance.LoadLevel("VS", 0.5f);
		}
	}

	public void rivalConnectedOK()
	{
		rivalOk = true;
	}

	public void OnClickConnected()
	{
		playerOk = true;
		if (BtAdapter != null) {
			BtAdapter.SendIntergerData(0,Tag.CONNECTEDOK);
		}
	}

	public void OnClickConnect()
	{
		address = addressField.text;
		if(BtAdapter != null)
		{
			BtAdapter.Connect (address);
		}
	}

	public void OnClickReturn()
	{
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		FadeManager.Instance.LoadLevel("ServerClientSelect", 0.5f);
	}
}
