using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Matching indicator.
/// Author Kazuki Ito
/// </summary>

public class MatchingManager : MonoBehaviour {

	public InputField addressField;
	public GameObject connectButton;
	private string address="";
	private GameObject Bt;
	private AndroidBlueToothAdapter BtAdapter;
	private bool isServer;
	private bool rivalOk;
	private bool playerOk;


	public GameObject spriteMessage;
	public Sprite serverMessageSprite;
	public Sprite clientMessageSprite;

	/// <summary>
	/// ラベル用オブジェクト
	/// </summary>
	public GameObject spriteLabel;
	
	/// <summary>
	/// サーバラベルイメージ
	/// </summary>
	public Sprite serverLabel;

	/// <summary>
	/// クライアントラベルイメージ
	/// </summary>
	public Sprite clientLabel;

	/// <summary>
	/// サーバテキストフィールド
	/// </summary>
	public GameObject serverTextField;

	/// <summary>
	/// クライアントテキストフィールド
	/// </summary>
	public GameObject clientTextField;

	// Use this for initialization
	void Start()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		Bt = GameObject.FindGameObjectWithTag ("BlueTooth");

		BtAdapter = Bt.GetComponent<AndroidBlueToothAdapter> ();
#endif

		isServer = ServerClientManager.IsServer ();

		if (isServer) {
			spriteMessage.GetComponent<Image>().sprite = serverMessageSprite;
			spriteLabel.GetComponent<Image>().sprite = serverLabel;
			clientTextField.GetComponent<Image>().enabled = false;
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
			spriteMessage.GetComponent<Image>().sprite = clientMessageSprite;
			spriteLabel.GetComponent<Image>().sprite = clientLabel;
			serverTextField.GetComponent<Image>().enabled = false;

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
			BtAdapter.SendIntergerData(0, Tag.CONNECTEDOK);
		}
	}

	public void OnClickConnect()
	{
		address = addressField.text;
		if (BtAdapter != null) { BtAdapter.Connect(address); }
	}

	public void OnClickReturn()
	{
		AudioManager.I.PlayAudio("se_tap");
		FadeManager.Instance.LoadLevel("ServerClientSelect", 0.5f);
	}
}
