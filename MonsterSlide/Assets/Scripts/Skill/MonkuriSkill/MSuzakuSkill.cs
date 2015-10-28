using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MSuzakuSkill : MSkillBase{

	private readonly int MAXMAGUMAMONKURI = 20;

	public GameObject skillExplosion;
	public GameObject skillMaguma;
	Dictionary<int, PuzzleMontama> montamaDict = new Dictionary<int, PuzzleMontama>();

	// Use this for initialization
	void Start () {
		AudioManager.I.PlayAudio("se_Suzaku");
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (ElapsedTime > skillTime)
		{
			foreach (PuzzleMontama montama in montamaDict.Values) { if (montama) { montama.IsMaguma = false; } }
			montamaDict.Clear();

			var magumas = GameObject.FindGameObjectsWithTag("SkillMaguma");
			foreach (var aMaguma in magumas) { Destroy(aMaguma); }
			Destroy(gameObject);
		}
	}

	public override void ActionSkill()
	{
		// 仕様変更後のスキル:凍らせるあれ
		for (int i = 0; i < MAXMAGUMAMONKURI; i++)
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
			montama.IsMaguma = true;
			GameObject exp = Instantiate(skillExplosion, montama.transform.position, montama.transform.rotation) as GameObject;
			GameObject maguma = Instantiate(skillMaguma, montama.transform.position, montama.transform.rotation) as GameObject;
			exp.transform.parent = montama.transform;
			maguma.transform.parent = montama.transform;
		}
	}
}
