using UnityEngine;
using System.Collections;

public class Flashing : MonoBehaviour {

	public float fadeTime;
	SpriteRenderer tapScreen;
	int cnt;

	// Use this for initialization
	void Start () {
		tapScreen = GetComponent<SpriteRenderer>();
		cnt = 0;
	}
	
	// Update is called once per frame
	void Update()
	{
		float alpha = (float)(0.5 + System.Math.Sin(2 * 3.14 / (fadeTime * 60f) * cnt) * 0.5);
		tapScreen.color = new Color(1, 1, 1, alpha);
		cnt++;
	}
}
