using UnityEngine;
using System.Collections;

/// <summary>
/// Window lose.
/// Author Kazuki Ito
/// </summary>


public class WinLose : MonoBehaviour {


	public Sprite WinTexture;

	public Sprite LoseTexture;

	private float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
		// タッチ時
		if (ElapsedTime > 2) {
			FadeManager.Instance.LoadLevel("Result", 0.5f);
		}
	
	}

	public void SetWinEnable(bool enable)
	{
		if (enable) {
			gameObject.GetComponent<SpriteRenderer>().sprite = WinTexture;
		} else {
			gameObject.GetComponent<SpriteRenderer>().sprite = LoseTexture;
		}
	}

	public float ElapsedTime { get { return Time.timeSinceLevelLoad - startTime; } }
}
