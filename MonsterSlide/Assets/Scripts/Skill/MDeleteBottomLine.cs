using UnityEngine;
using System.Collections;

public class MDeleteBottomLine : MSkillBase {

	public GameObject SpCarrier;

	public GameObject fireEffect;

	public GameObject firePrefab;

	// Use this for initialization
	void Start () {
		AudioManager.I.PlayAudio("se_Dragon");
	}
	
	// Update is called once per frame
	void Update () {
		if (ElapsedTime > skillTime) { Destroy(gameObject); }
	}

	public override void ActionSkill()
	{
		Instantiate(fireEffect, new Vector3(4, -LaneManager.LANEMAINHEIGHT, 0), Quaternion.identity);
		// レーンの最下層にあるモンタマを消してしまう
		for (int i = 1; i <= LaneManager.LANEMAINWIDTH; i++)
		{

			// モンクリが消えた時
			GameObject puzzleMontama =  LaneManager.I.LaneMatrix[LaneManager.LANEMAINHEIGHT, i].GetComponent<LaneBlock>().HoldMontama;
			if (puzzleMontama == null) { continue; }

			// 対象のモンタマのスキルポイントを貯める
			GameObject carrier = Instantiate(SpCarrier, puzzleMontama.transform.position, puzzleMontama.transform.rotation) as GameObject;
			GameObject skillMontama = SkillMontamaManager.I.GetSkillMontama(puzzleMontama.GetComponent<PuzzleMontama>().serialID);
			carrier.GetComponent<SkillCarrier>().CarrySkillPt(skillMontama.GetComponent<SkillMontama>().serialId, 1, puzzleMontama.transform.position, skillMontama.transform.position);
			SkillMontamaManager.I.AddSkillPt(puzzleMontama.GetComponent<PuzzleMontama>().serialID, 1.0f);
			PlayerSkillBar.I.AddSkillPt(1);

			Destroy(puzzleMontama);
			LaneManager.I.LaneMatrix[LaneManager.LANEMAINHEIGHT, i].GetComponent<LaneBlock>().HoldMontama = null;
		}
	}
}
