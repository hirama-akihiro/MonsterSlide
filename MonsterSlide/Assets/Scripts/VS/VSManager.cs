using UnityEngine;
using System.Collections;

public class VSManager : SingletonMonoBehavior<VSManager> {

	public float loadTime;

	private float startTime;

	private bool isFadeIn = false;

	private bool isSe = false;

	// Use this for initialization
	void Start () {
		startTime = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.timeSinceLevelLoad - startTime > 1 && !isSe)
		{
			// カットイン用のSEを鳴らす
			AudioManager.I.PlayAudio("se_skillCutIn");
			isSe = true;
		}

		if (Time.timeSinceLevelLoad - startTime > loadTime && !isFadeIn)
		{
			isFadeIn = true;
			// Menuシーンへ遷移
			FadeManager.Instance.LoadLevel("Main", 0.5f);
		}
	}
}
