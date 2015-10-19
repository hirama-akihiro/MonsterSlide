using UnityEngine;
using System.Collections;

/// <summary>
/// Player manager.
/// Author Kazuki Ito
/// </summary>

public class PlayerManager : MonoBehaviour {

	public GameObject PlayerSkillGauge;

	public GameObject[] PlayerPrefabs;

	// Use this for initialization
	void Start () {

		int playerID = PlayerPrefs.GetInt("PlayerID", 0);
		GameObject Player = Instantiate (PlayerPrefabs [0]);
		Player.transform.SetParent (gameObject.transform);
		Player.GetComponent<PlayerSkillBar> ().SetGauge (PlayerSkillGauge);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
