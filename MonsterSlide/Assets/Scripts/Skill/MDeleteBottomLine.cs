using UnityEngine;
using System.Collections;

public class MDeleteBottomLine : MSkillBase {

	public GameObject SpCarrier;

	public GameObject fireEffect;

	public GameObject firePrefab;

	// Use this for initialization
	void Start () {
		AudioManager.Instance.PlayAudio("se_Dragon");
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
			GameObject puzzleMontama =  LaneManager.Instance.LaneMatrix[LaneManager.LANEMAINHEIGHT, i].GetComponent<LaneBlock>().HoldMontama;
			if (puzzleMontama == null) { continue; }

			// 火炎エフェクト
			//if (i == (int)(LaneManager.LANEMAINHEIGHT * 0.5)) { Instantiate(fireEffect, new Vector3(0, 0, 0), Quaternion.identity); }
			//Instantiate(firePrefab, puzzleMontama.transform.position, puzzleMontama.transform.rotation);

			// 対象のモンタマのスキルポイントを貯める
			GameObject carrier = Instantiate(SpCarrier, puzzleMontama.transform.position, puzzleMontama.transform.rotation) as GameObject;
			GameObject skillMontama = SkillMontamaManager.Instance.GetSkillMontama(puzzleMontama.GetComponent<PuzzleMontama>().serialID);
			carrier.GetComponent<SkillCarrier>().CarrySkillPt(skillMontama.GetComponent<SkillMontama>().serialId, 1, puzzleMontama.transform.position, skillMontama.transform.position);
			SkillMontamaManager.Instance.AddSkillPt(puzzleMontama.GetComponent<PuzzleMontama>().serialID, 1.0f);
			PlayerSkillBar.Instance.AddSkillPt(1);

			//puzzleMontama.GetComponent<PuzzleMontama>().DestroyMonkuri();
			Destroy(puzzleMontama);
			LaneManager.Instance.LaneMatrix[LaneManager.LANEMAINHEIGHT, i].GetComponent<LaneBlock>().HoldMontama = null;
		}
	}
}
