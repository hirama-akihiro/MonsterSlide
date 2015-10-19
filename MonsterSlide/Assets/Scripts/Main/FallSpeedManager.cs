using UnityEngine;
using System.Collections;

public class FallSpeedManager :SingletonMonoBehavior<FallSpeedManager> {

	/// <summary>
	/// 落下速度
	/// </summary>
	//[SerializeField ,Range(0f,0.1f)]
	[SerializeField, Range(0f,10f)]
	public float fallSpeed;

	/// <summary>
	/// シーン開始時の時間
	/// </summary>
	private float sceneStartTime;

	public GUISkin skin;

	public bool debug;

	// Use this for initialization
	void Start () {
		sceneStartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// ゲーム開始からの経過時間
	/// </summary>
	public float ElapsedTime { get { return Time.time - sceneStartTime; } }

	public float GetFallSpeedMagnification()
	{
		float mag = 60.0f;
		if (ElapsedTime < 10.0) { return fallSpeed * 1.0f; }
		else if (ElapsedTime < 1 * mag) { return fallSpeed * 1.2f; }
		else if (ElapsedTime < 2 * mag) { return fallSpeed * 1.4f; }
		else if (ElapsedTime < 3 * mag) { return fallSpeed * 1.6f; }
		else if (ElapsedTime < 4 * mag) { return fallSpeed * 1.8f; }
		else if (ElapsedTime < 5 * mag) { return fallSpeed * 2.0f; }
		else if (ElapsedTime < 6 * mag) { return fallSpeed * 2.2f; }
		else if (ElapsedTime < 7 * mag) { return fallSpeed * 2.4f; }
		else if (ElapsedTime < 8 * mag) { return fallSpeed * 2.6f; }
		else if (ElapsedTime < 9 * mag) { return fallSpeed * 2.8f; }
		else { return fallSpeed * 3.0f; }
		//else if (ElapsedTime < 480.0) { return fallSpeed * 2.6f; }
		//else if (ElapsedTime < 540.0) { return fallSpeed * 2.8f; }
		//else { return fallSpeed * 3.0f; }
		//else if (ElapsedTime < 15.0) { return fallSpeed * 1.2f; }
		//else if (ElapsedTime < 20.0) { return fallSpeed * 1.4f; }
		//else if (ElapsedTime < 25.0) { return fallSpeed * 1.6f; }
		//else if (ElapsedTime < 30.0) { return fallSpeed * 1.8f; }
		//else if (ElapsedTime < 35.0) { return fallSpeed * 2.0f; }
		//else if (ElapsedTime < 40.0) { return fallSpeed * 2.2f; }
		//else if (ElapsedTime < 45.0) { return fallSpeed * 2.4f; }
		//else if (ElapsedTime < 50.0) { return fallSpeed * 2.6f; }
		//else if (ElapsedTime < 55.0) { return fallSpeed * 2.8f; }
		//else if (ElapsedTime < 60.0) { return fallSpeed * 3.0f; }
		//else if (ElapsedTime < 65.0) { return fallSpeed * 3.2f; }
		//else if (ElapsedTime < 70.0) { return fallSpeed * 3.4f; }
		//else if (ElapsedTime < 75.0) { return fallSpeed * 3.6f; }
		//else if (ElapsedTime < 80.0) { return fallSpeed * 3.8f; }
		//else if (ElapsedTime < 85.0) { return fallSpeed * 4.0f; }
		//else { return fallSpeed * 4.0f; }
	}

	void OnGUI()
	{
		if (!debug) {
			return;
		}
		GUI.skin = skin;
		GUI.Label(new Rect(Screen.width * 0.5f, Screen.height * 0.3f, Screen.width * 0.1f, Screen.height * 0.1f), "落下速度:" + GetFallSpeedMagnification(), "SceneName");
	}
}
