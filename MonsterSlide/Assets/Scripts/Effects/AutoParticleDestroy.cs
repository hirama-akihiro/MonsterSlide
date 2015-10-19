using UnityEngine;
using System.Collections;

public class AutoParticleDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ParticleSystem particleSystem = GetComponent<ParticleSystem>();
		Destroy(gameObject, particleSystem.duration);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
