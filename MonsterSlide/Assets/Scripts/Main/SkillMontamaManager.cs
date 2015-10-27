using UnityEngine;
using System.Collections;

public class SkillMontamaManager : SingletonMonoBehavior<SkillMontamaManager> {

	/// <summary>
	/// 各モンクリのスキルゲージ
	/// </summary>
	public GameObject[] SkillGauge;

	/// <summary>
	/// 各モンクリのスキルゲージ枠
	/// </summary>
	public GameObject[] skillGaugeWaku;

	/// <summary>
	/// 最大スキルポイント時に用いるマテリアル
	/// </summary>
	public Material maxSkillPtMat;

	/// <summary>
	/// スキルポイント追加時のエフェクト
	/// </summary>
	public GameObject maxSkillPtEffect;
	
	/// <summary>
	/// スキルオブジェクト
	/// </summary>
	GameObject[] skills = new GameObject[4];

	// Use this for initialization
	void Start()
	{
		GameObject skillParent = GameObject.Find("SkillMontamas");
		int[] ary = new int[4];
		switch (BattleTypeManager.I.battleType)
		{
			case BattleTypeManager.BattleType.NearBattle_Server:
				ary[0] = 1;
				ary[1] = 4;
				ary[2] = 2;
				ary[3] = 3;
				break;
			case BattleTypeManager.BattleType.NearBattle_Client:
			case BattleTypeManager.BattleType.SingleBattle:
				ary[0] = 0;
				ary[1] = 5;
				ary[2] = 4;
				ary[3] = 2;
				break;
		}

		for (int i = 0; i < 4; i++)
		{
			skills[i] = Instantiate(PartyManager.I.GetSkillMonkuri(i));
			skills[i].transform.SetParent(skillParent.transform);
			skills[i].transform.position = SkillGauge[i].transform.position;
			skills[i].GetComponent<SkillMontama>().SetGauge(SkillGauge[i]);
			skills[i].GetComponent<SkillMontama>().SetGaugeWaku(skillGaugeWaku[i]);
		}
	}

	
	// Update is called once per frame
	void Update () {
	}

	/// <summary>
	/// 指定してSerialIDに対してスキルポイントを加算する
	/// </summary>
	/// <param name="serialId"></param>
	/// <param name="skillPt"></param>
	public void AddSkillPt(int serialId,float skillPt)
	{
		foreach (GameObject montama in skills)
		{
			SkillMontama skillclass = montama.GetComponent<SkillMontama>();
			if (skillclass.serialId == serialId) { skillclass.addSkillPt(skillPt); }
		}
	}

	public GameObject GetSkillMontama(int serialId)
	{
		GameObject[] montamas = GameObject.FindGameObjectsWithTag("SkillMontama");
		foreach (GameObject montama in montamas)
		{
			if(montama.GetComponent<SkillMontama>().serialId == serialId){return montama;}
		}
		return montamas[0];
	}

	public void GameEnd() { enabled = false; }

	public void GameStart() { enabled = true; }

	public void SkillButtomEnable(bool enable)
	{
		if (enable) {
		foreach (GameObject obj in skills) { obj.GetComponent<SkillMontama>().enabled = true; }
		} else {
			foreach (GameObject obj in skills) { obj.GetComponent<SkillMontama>().enabled = false; }
		}
	}

	/// <summary>
	/// ゲーム終了時にTrue
	/// </summary>
	public bool IsGameEnd { get; set; }
}
