using UnityEngine;
using System.Collections;

public class SkillMontamaManager : SingletonMonoBehavior<SkillMontamaManager> {

	//  ↓  Author kazuki ito	
	public GameObject[] SkillGauge;

	public GameObject[] skillGaugeWaku;

	public Material maxSkillPtMat;
	public GameObject maxSkillPtEffect;

	private bool isBegin = true;
	
	GameObject[] skills = new GameObject[4];

	int[] montamaData = new int[4];
	//  ↑ Author kabuki ito


	// Use this for initialization
	void Start () {
		//DontDestroyOnLoad(this);

		//  ↓  Author kazuki ito
		GameObject skillParent = GameObject.Find ("SkillMontamas");
		int[] ary = new int[4];
		switch (BattleTypeManager.Instance.battleType)
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

		//string montamaData = " ";
		for (int i = 0; i < 4; i++)
		{
			string key = "SkillMontama" + i;
			int serialID = PlayerPrefs.GetInt(key, i);
			serialID = i;
			montamaData[i] = serialID; //ary[i]; ;
			skills[i] = Instantiate(PartyManager.Instance.GetSkillMonkuri(serialID));
			skills[i].transform.SetParent(skillParent.transform);
			skills[i].transform.position = SkillGauge[i].transform.position;
			skills[i].GetComponent<SkillMontama>().SetGauge(SkillGauge[i]);
			skills[i].GetComponent<SkillMontama>().SetGaugeWaku(skillGaugeWaku[i]);
		}
	}
//#if UNITY_ANDROID && !UNITY_EDITOR
//		GameObject Bt = GameObject.FindGameObjectWithTag ("BlueTooth");
//		AndroidBlueToothAdapter BtAdapter = Bt.GetComponent<AndroidBlueToothAdapter> ();
//		if (BtAdapter != null) {
//			BtAdapter.SendStringData(montamaData,Tag.RivalSkillData);
//		}
//#endif
//		//  ↑ Author kabuki ito
//	}

	
	// Update is called once per frame
	void Update () {
	}

	/// <summary>
	/// 指定してSerialIDに対してスキルポイントを加算する
	/// </summary>
	/// <param name="serialId"></param>
	/// <param name="skillPt"></param>
	///  ↓  Author kazuki ito
	public void AddSkillPt(int serialId,float skillPt)
	{
		//GameObject[] montamas = GameObject.FindGameObjectsWithTag("SkillMontama");
		foreach (GameObject montama in skills)//montamas)
		{
			SkillMontama skillclass = montama.GetComponent<SkillMontama>();
			if (skillclass.serialId == serialId) { skillclass.addSkillPt(skillPt); }
		}
	}
	//  ↑ Author kabuki ito

	public GameObject GetSkillMontama(int serialId)
	{
		GameObject[] montamas = GameObject.FindGameObjectsWithTag("SkillMontama");
		foreach (GameObject montama in montamas)
		{
			if(montama.GetComponent<SkillMontama>().serialId == serialId){return montama;}
		}
		return montamas[0];
	}

	public void GameEnd()
	{
		enabled = false;
	}

	//  ↓  Author kazuki ito
	public void GameStart()
	{
		enabled = true;

	}

	public void SkillButtomEnable(bool enable)
	{
		if (enable) {
		foreach (GameObject obj in skills) { obj.GetComponent<SkillMontama>().enabled = true; }
		} else {
			foreach (GameObject obj in skills) { obj.GetComponent<SkillMontama>().enabled = false; }
		}
	}


	public int[] getMontanaData()
	{
		return montamaData;
	}
	//  ↑ Author kabuki ito

	/// <summary>
	/// ゲーム終了時にTrue
	/// </summary>
	public bool IsGameEnd { get; set; }
}
