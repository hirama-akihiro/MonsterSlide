using UnityEngine;
using System.Collections;

public class AutoKizuDestroy : MonoBehaviour {

	public float destroyTime;

	// Use this for initialization
	void Awake () {
		Destroy(gameObject, destroyTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
