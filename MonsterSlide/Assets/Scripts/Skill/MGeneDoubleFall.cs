using UnityEngine;
using System.Collections;

public class MGeneDoubleFall : MSkillBase{

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(ElapsedTime > skillTime)
		{
			GeneratorManager.Instance.Interval = GeneratorManager.Instance.OrgInterval;
			Destroy(gameObject);
		}
	}

	public override void ActionSkill()
	{
		GeneratorManager.Instance.Interval = GeneratorManager.Instance.OrgInterval * 0.5f;
	}
}
