using UnityEngine;
using System.Collections;

/// <summary>
/// スプライトの描画範囲の制限
/// </summary>
public class SpriteDrawRest : MonoBehaviour {

	private SpriteRenderer myRenderer;

	// Use this for initialization
	void Start () {
		myRenderer = GetComponent<SpriteRenderer>();
		myRenderer.enabled = !(transform.position.x < 0.5 || LaneManager.LANEMAINWIDTH + 0.5 < transform.position.x);	
	}
	
	// Update is called once per frame
	void Update () {
		myRenderer.enabled = !(transform.position.x < 0.5 || LaneManager.LANEMAINWIDTH + 0.5 < transform.position.x);
	}
}
