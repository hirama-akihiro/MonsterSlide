using UnityEngine;
using System.Collections;

/// <summary>
/// Player manager.
/// Author Kazuki Ito
/// </summary>

public class PlayerManager : MonoBehaviour {

	public GameObject playerSkillGauge;

	public GameObject[] playerPrefabs;

	// Use this for initialization
	void Start () {
		GameObject player = Instantiate (playerPrefabs [0]);
		player.transform.SetParent (gameObject.transform);
		player.GetComponent<PlayerSkillBar> ().SetGauge (playerSkillGauge);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
