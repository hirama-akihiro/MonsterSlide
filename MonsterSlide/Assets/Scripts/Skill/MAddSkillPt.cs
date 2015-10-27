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
		GameObject[] montamas = GameObject.FindGameObjectsWithTag("SkillMontama");
		foreach (GameObject montama in montamas)
		{
			Instantiate(healEffectObj, montama.transform.position, montama.transform.rotation);
			montama.GetComponent<SkillMontama>().addSkillPt(addSkillPt);
		}
	}
}
