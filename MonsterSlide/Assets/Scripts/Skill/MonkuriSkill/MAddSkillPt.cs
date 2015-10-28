using UnityEngine;
using System.Collections;

public class MAddSkillPt : MSkillBase {

	public float addSkillPt;

	/// <summary>
	/// 回復してる感のエフェクトオブジェクト
	/// </summary>
	public GameObject healEffectObj;

	// Use this for initialization
	void Start () {
		AudioManager.I.PlayAudio("se_Ketsi");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void ActionSkill()
	{
		foreach (GameObject monkuri in SkillMontamaManager.I.SkillMonkuris)
		{
			Instantiate(healEffectObj, monkuri.transform.position, monkuri.transform.rotation);
			monkuri.GetComponent<SkillMontama>().addSkillPt(addSkillPt);
		}
	}
}
