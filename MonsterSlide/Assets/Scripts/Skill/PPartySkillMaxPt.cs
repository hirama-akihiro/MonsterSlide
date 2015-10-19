using UnityEngine;
using System.Collections;

public class PPartySkillMaxPt : PSkillBase {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void ActionSkill()
	{
		GameObject[] montamas = GameObject.FindGameObjectsWithTag("SkillMontama");
		montamas[0].GetComponent<SkillMontama>().SkillPointMax();
	}
}
