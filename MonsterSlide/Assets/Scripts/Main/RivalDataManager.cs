using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RivalDataManager : MonoBehaviour {

	//public GameObject[] skillMontamas;

	public GameObject[] playerPrefabs;

	public GameObject[] skillMontamasPrefabs;

	public GameObject[] gauge;

	private GameObject[] skills = new GameObject[4];

	// Use this for initialization
	void Start () {
		int player = PlayerPrefs.GetInt("Rival", 0);
		Instantiate (playerPrefabs [player]);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
//
//	public void receiveRivalSkill(string serialIDData)
//	{
//		if (serialIDData == null) {
//			return;
//		}
//
//		string[] serialIDs = serialIDData.Split('_');
//
//		for (int i = 0; i< serialIDs.Length; i++) {
//			int serialID = int.Parse(serialIDs[i]);
//			skills[i] = Instantiate(skillMontamasPrefabs[serialID]);
//			skills[i].transform.SetParent(gameObject.transform);
//			skills[i].transform.position = gauge[i].transform.position;
//			skills[i].GetComponent<SkillMontama>().enabled = false;
//			skills[i].GetComponent<CircleCollider2D>().enabled = false;
//			RivalMontama rm = skills[i].GetComponent<RivalMontama>();
//			rm.enabled =true;
//			rm.setGauge(gauge[i]);
//			rm.setSerialID(serialID);
//	
//		}
//	}


}
