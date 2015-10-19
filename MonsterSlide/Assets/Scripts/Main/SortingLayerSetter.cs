using UnityEngine;
using System.Collections;

public class SortingLayerSetter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().sortingLayerName = "Particle";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
