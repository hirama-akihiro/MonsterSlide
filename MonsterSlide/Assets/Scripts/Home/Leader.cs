using UnityEngine;
using System.Collections;

public class Leader : MonoBehaviour {

	public Sprite[] mosters;

	// Use this for initialization
	void Start () {
		int serialID = PlayerPrefs.GetInt("SkillMontama1", 0);
		gameObject.GetComponent<SpriteRenderer> ().sprite = mosters [serialID];
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
