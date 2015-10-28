using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MMonkuriBite : MSkillBase {

	private readonly int MAXBITEMONKURI = 8;

	//public float reductionSkillPt;

	public GameObject skillBite;
	public GameObject skillKizu;
	Dictionary<int, PuzzleMontama> montamaDict = new Dictionary<int, PuzzleMontama>();

	// Use this for initialization
	void Start () {
		AudioManager.I.PlayAudio("se_hell");
	}
	
	// Update is called once per frame
	void Update () {
		if (ElapsedTime > skillTime)
		{
			foreach (PuzzleMontama montama in montamaDict.Values) { montama.IsBited = false; }
			montamaDict.Clear();

			var kizus = GameObject.FindGameObjectsWithTag ("SkillBiteKizu");
			foreach (var aKizu in kizus) { Destroy(aKizu); }
			Destroy(gameObject);
		}
	}

	public override void ActionSkill()
	{
		// 仕様変更前のスキル
		//GameObject[] montamas = GameObject.FindGameObjectsWithTag("SkillMontama");
		//foreach (GameObject montama in montamas)
		//{
		//	montama.GetComponent<SkillMontama>().addSkillPt(reductionSkillPt);
		//}



		// 仕様変更後のスキル:凍らせるあれ
		for (int i = 0; i < MAXBITEMONKURI; i++)
		{
			int index = Random.Range(0, PuzzleMontamaManager.I.PuzzleMontamas.Count);
			if (!montamaDict.ContainsKey(index))
			{
				montamaDict.Add(index, PuzzleMontamaManager.I.PuzzleMontamas[index]);
			}
		}

		foreach (PuzzleMontama montama in montamaDict.Values)
		{
			if (!montama) { continue; }
			montama.IsBited = true;
			GameObject bite = Instantiate(skillBite, montama.transform.position, montama.transform.rotation) as GameObject;
			GameObject kizu = Instantiate(skillKizu, montama.transform.position, montama.transform.rotation) as GameObject;
			bite.transform.parent = montama.transform;
			kizu.transform.parent = montama.transform;
		}
	}
}
