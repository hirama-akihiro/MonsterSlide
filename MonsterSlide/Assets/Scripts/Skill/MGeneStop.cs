using UnityEngine;
using System.Collections;

public class MGeneStop : MSkillBase {

	public float stopTime;

	// Use this for initialization
	void Start () {
		AudioManager.Instance.PlayAudio("se_Genbu");
	}
	
	// Update is called once per frame
	void Update () {
		if (ElapsedTime > skillTime) { Destroy(gameObject); }
	}

	public override void ActionSkill()
	{
		// モンタマの生成をStopTime間止める
		GeneratorManager.Instance.GeneTimeLimit = stopTime;
	}
}
