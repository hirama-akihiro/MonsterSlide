using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/// <summary>
/// Android blue tooth adapter.
/// Author Kazuki Ito
/// </summary>
public class AndroidBlueToothAdapter : MonoBehaviour {
	
	// datatype
	const int INT = 0;
	const int FLOAT = 1;
	const int STRING = 2;

	//	dataIndex
	const int DATATYPE = 0;
	const int DATA = 1;
	const int TAG = 2;

	// ClickTag
	const int CONNECT = 0;
	const int DISCONNECT = 1;

#if UNITY_ANDROID && !UNITY_EDITOR
	AndroidJavaObject cls;
#endif
	bool serverFlag = false;
	bool retryRequest = false;

	void Update ()
	{
		if (!isConnected ()) {
			serverFlag = false;
		}
		if (Application.loadedLevelName == "Result" && retryRequest) {
			GameObject result = GameObject.Find ("ResultManager");
			if (result == null) {
				return;
			}
			
			result.GetComponent<ResultManager> ().ReceiveRetryRequest ();
			retryRequest = false;
		}
	}

	public void Create () {
#if UNITY_ANDROID && !UNITY_EDITOR
		cls = new AndroidJavaObject("com.framework.bluetooth.P2P");
		cls.Call ("setGameObject", gameObject.name);
		serverFlag = false;

		//cls.Call ("BlueToothEnable");
#endif
	}
	
	public void BlueToothEnable()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		cls.Call ("BlueToothEnable");
#endif
	}

	public string StartServer()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		string address = cls.Call<string> ("StartServer");
		serverFlag = true;
		return address;
#endif
		return "This function is only valid for Android";
	}

	public void Connect(string address)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		if(!serverFlag)
		{
			cls.Call("cancel");
			cls.Call("connect",address);
		}
#endif
	}

	public bool isConnected()
	{
		bool connected = false;
#if UNITY_ANDROID && !UNITY_EDITOR
		connected = cls.Call<bool>("isConnected");
#endif
		return connected;
	}

	public void Cancel()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		cls.Call("cancel");
#endif
	}

	public void disConnect()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		cls.Call("Disconnect");
#endif
	}

	public void SendIntergerData(int data ,Tag tag)
	{

#if UNITY_ANDROID && !UNITY_EDITOR
		string sendData = INT + "," + data + "," + (int)tag;
		cls.Call("sendData",sendData);
#endif
	}

	public void SendFloatData(float data ,Tag tag)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		string sendData = FLOAT + "," + data.ToString() + "," + (int)tag;
		cls.Call("sendData",sendData);
#endif
	}

	public void SendStringData(string data ,Tag tag)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		string sendData = STRING + "," + data + "," + (int)tag;
		cls.Call("sendData",sendData);
#endif
	}

	void onCallReceiveData(string data)
	{
		string[] receiveData = data.Split(',');
		int dataType = int.Parse (receiveData [DATATYPE]);
		int tag = int.Parse (receiveData [TAG]);
		switch (dataType) {
		case INT:
			int intData = int.Parse(receiveData [DATA]);
			onCallReceiveIntegerData(intData ,tag);
			break;

		case FLOAT:
			float floatData = float.Parse(receiveData [DATA]);
			onCallReceiveFloatData(floatData ,tag);
			break;

		case STRING:
			onCallReceiveStringData(receiveData [DATA] ,tag);
			break;
		}

		return;
	}

	void onCallReceiveIntegerData(int intData ,int tag)
	{
		//	Write the necessary processing
	if (Application.loadedLevelName == "Main") {
			switch (tag) {
			case (int)Tag.Start:
				GameObject start = GameObject.Find ("GameStartManager");
				if (start == null) {
					return;
				}

				start.GetComponent<StartManager> ().GameStart ();
				break;

			case (int)Tag.Skill:
				GameObject manager = GameObject.Find ("CutInManager");
				if (manager == null) {
					return;
				}

				manager.GetComponent<CutInManager>().CreateCutIn(intData, true, false);
				break;

			case (int)Tag.RetryRequest:
				retryRequest = true;
				break;
			}
		} else if (Application.loadedLevelName == "Matching") {
			switch (tag) {
			case (int)Tag.CONNECTEDOK:
				GameObject matching = GameObject.Find ("MatchingManager");
				if (matching == null) {
					return;
				}
				
				matching.GetComponent<MatchingManager> ().rivalConnectedOK ();
				break;
			}
		} else if (Application.loadedLevelName == "Result") {
			switch(tag)
			{
			case (int)Tag.RetryRequest:
				GameObject result = GameObject.Find ("ResultManager");
				if (result == null) {
					return;
				}	
				result.GetComponent<ResultManager> ().ReceiveRetryRequest ();
				break;
				
			case (int)Tag.RetryOK:
				GameObject result2 = GameObject.Find ("ResultManager");
				if (result2 == null) {
					return;
				}
				
				result2.GetComponent<ResultManager> ().GameRetry ();
				break;
				
			case (int)Tag.RetryNO:
				GameObject result3 = GameObject.Find ("ResultManager");
				if (result3 == null) {
					return;
				}
				result3.GetComponent<ResultManager> ().CancelRetry ();
				break;
			}
		}

	}

	void onCallReceiveFloatData(float floatData, int tag)
	{
		// Write the necessary processing
		if (Application.loadedLevelName == "Main") {
			switch (tag) {
				case (int)Tag.HP:
					GameObject hpGauge = GameObject.FindGameObjectWithTag("HP");
					if (hpGauge == null) {
						return;
					}
					hpGauge.GetComponent<HpGauge>().UpdateHp(floatData);
					GameObject player2Icon = GameObject.FindGameObjectWithTag("Player2Icon");
					if (player2Icon == null) { return; }
					player2Icon.GetComponent<BRival>().NowHp = floatData;
					break;

				case (int)Tag.End:
					GameObject manager2 = GameObject.Find("MainManager");
					if (manager2 == null) {
						return;
					}
					manager2.GetComponent<MainManager>().GameOver(false);
					break;
			}
		}
	}

	void onCallReceiveStringData(string stringData, int tag)
	{
		// Write the necessary processing
		if (Application.loadedLevelName == "Main") {
			switch (tag) {

			}
		}
	}

	void onCallClickOK(string tagstr)
	{
		int tag = int.Parse (tagstr);

		switch (tag) {
		case CONNECT:
			GameObject matching = GameObject.Find("MatchingManager");
			if(matching == null)
			{
				return;
			}

			matching.GetComponent<MatchingManager>().OnClickConnected();
			break;

		case DISCONNECT:
			Application.LoadLevel("Home");
			break;
		}
	}
	

	void onCallDisConnect(string dummydata)
	{
		ResultManager.SetNoRetry (true);
	}
}
