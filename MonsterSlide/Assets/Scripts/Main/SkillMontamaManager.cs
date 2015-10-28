using UnityEngine;
using System.Collections;

public class SkillMontamaManager : SingletonMonoBehavior<SkillMontamaManager> {

	/// <summary>
	/// 各モンクリのスキルゲージ
	/// </summary>
	public GameObject[] skillGauge;

	/// <summary>
	/// 各モンクリのスキルゲージ枠
	/// </summary>
	public GameObject[] skillGaugeWaku;

	/// <summary>
	/// 対戦相手の各モンクリのスキルゲージ
	/// </summary>
	public GameObject[] rivalSkillGauge;

	/// <summary>
	/// 対戦相手の各モンクリのスキルゲージ枠
	/// </summary>
	public GameObject[] rivalSkillGaugeWaku;

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
	private GameObject[] skillMonkuris = new GameObject[4];

	/// <summary>
	/// 対戦相手のスキルオブジェクト
	/// </summary>
	private GameObject[] rivalSkillMonkuris = new GameObject[4];

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
			skillMonkuris[i] = Instantiate(PartyManager.I.GetSkillMonkuri(i));
			skillMonkuris[i].transform.SetParent(skillParent.transform);
			skillMonkuris[i].transform.position = skillGauge[i].transform.position;
			skillMonkuris[i].GetComponent<SkillMontama>().SetGauge(skillGauge[i]);
			skillMonkuris[i].GetComponent<SkillMontama>().SetGaugeWaku(skillGaugeWaku[i]);

			// 対戦相手の処理
			rivalSkillMonkuris[i] = Instantiate(PartyManager.I.GetRivalSkillMonkuri(i));
			rivalSkillMonkuris[i].transform.SetParent(skillParent.transform);
			rivalSkillMonkuris[i].transform.position = rivalSkillGauge[i].transform.position;
			rivalSkillMonkuris[i].GetComponent<SkillMontama>().SetGauge(rivalSkillGauge[i]);
			rivalSkillMonkuris[i].GetComponent<SkillMontama>().SetGaugeWaku(rivalSkillGaugeWaku[i]);
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
		foreach (GameObject montama in skillMonkuris)
		{
			SkillMontama skillclass = montama.GetComponent<SkillMontama>();
			if (skillclass.serialId == serialId) { skillclass.addSkillPt(skillPt); }
		}
	}

	/// <summary>
	/// 対戦相手のSerialIDに対してスキルポイントを加算する
	/// </summary>
	/// <param name="serialId"></param>
	/// <param name="skillPt"></param>
	public void AddRivalSkillPt(int serialId, float skillPt)
	{
		foreach(GameObject montama in rivalSkillMonkuris)
		{
			SkillMontama skillClass = montama.GetComponent<SkillMontama>();
			if(skillClass.serialId == serialId) { skillClass.addSkillPt(skillPt); }
		}
	}

	public void AddRandomRivalSkillPt(float skillPt)
	{
		int index = Random.Range(0, 4);
		rivalSkillMonkuris[index].GetComponent<SkillMontama>().addSkillPt(skillPt);
	}

	/// <summary>
	/// モンクリ固有IDに対応したモンクリを取得
	/// </summary>
	/// <param name="serialId">0から始まるモンクリ固有のID</param>
	/// <returns></returns>
	public GameObject GetSkillMontama(int serialId)
	{
		foreach (GameObject monkuri in skillMonkuris)
		{
			if (monkuri.GetComponent<SkillMontama>().serialId == serialId) { return monkuri; }
		}
		return skillMonkuris[0];
	}

	public GameObject GetRivalSkillMontama(int serialId)
	{
		foreach (GameObject monkuri in rivalSkillMonkuris)
		{
			if (monkuri.GetComponent<SkillMontama>().serialId == serialId) { return monkuri; }
		}
		return rivalSkillMonkuris[0];
	}

	public void GameEnd() { enabled = false; }

	public void GameStart() { enabled = true; }

	public void SkillButtomEnable(bool enable)
	{
		if (enable) {
		foreach (GameObject obj in skillMonkuris) { obj.GetComponent<SkillMontama>().enabled = true; }
		} else {
			foreach (GameObject obj in skillMonkuris) { obj.GetComponent<SkillMontama>().enabled = false; }
		}
	}

	/// <summary>
	/// スキルオブジェクト配列
	/// </summary>
	public GameObject[] SkillMonkuris { get { return skillMonkuris; } }

	public GameObject[] RivalSkillMonkuris { get { return rivalSkillMonkuris; } }

	/// <summary>
	/// ゲーム終了時にTrue
	/// </summary>
	public bool IsGameEnd { get; set; }
}
