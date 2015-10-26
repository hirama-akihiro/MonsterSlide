using UnityEngine;
using System.Collections;

public class PartySelectIndicator : MonoBehaviour {

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// 前シーンへ戻る
	/// </summary>
	public void OnClickReturn()
	{
		// シーン遷移
		AudioManager.Instance.PlayAudio("se_tap");
		FadeManager.Instance.LoadLevel("Home", 0.5f);
	}
}
