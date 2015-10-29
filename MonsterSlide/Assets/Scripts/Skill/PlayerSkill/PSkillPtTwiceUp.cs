using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PSkillPtTwiceUp : PSkillBase {


	private float mag = 2;

	private GameObject _masterSkillBackEffect;
	public GameObject masterSkillBackEffect;

	public GameObject twiceEffect;
	private List<GameObject> twiceEffectList = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.LogFormat("ElapsedTime{0}", ElapsedTime);
		if (ElapsedTime > skillTime)
		{
			GameObject[] montamas = GameObject.FindGameObjectsWithTag("SkillMontama");
			foreach (GameObject montama in montamas)
			{
				montama.GetComponent<SkillMontama>().mag = 1.0f;
			}
			foreach (var effect in twiceEffectList) { Destroy(effect); }
			Destroy(_masterSkillBackEffect);
			Destroy(gameObject);
		}
	}

	public override void ActionSkill()
	{
		foreach (GameObject montama in SkillMontamaManager.I.SkillMonkuris)
		{
			montama.GetComponent<SkillMontama>().mag = mag;
			GameObject effect = Instantiate(twiceEffect, montama.transform.position, montama.transform.rotation) as GameObject;
			twiceEffectList.Add(effect);
		}
		_masterSkillBackEffect = Instantiate(masterSkillBackEffect);
	}
}
