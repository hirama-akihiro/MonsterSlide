using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillMontama : MonoBehaviour {

	/// <summary>
	/// モンタマを判別するシリアル番号
	/// </summary>
	public int serialId;

	/// <summary>
	/// コストポイント
	/// </summary>
	public int costPt;

	/// <summary>
	/// スキル発動に必要なスキルポイント
	/// </summary>
	public float maxSkillPt;

	/// <summary>
	/// 現フレームでのスキルポイント
	/// </summary>
	public float nowSkillPt;

	public bool debug = false;

	GameObject cutInManager;

	CutInManager cutInManagerCls;
	
	GameObject Bt;

	/// <summary>
	/// スキルポイントの倍率
	/// </summary>
	public float mag = 1;

	AndroidBlueToothAdapter BtAdapter;

	/// <summary>
	/// スキルスクリプト
	/// </summary>
	//public GameObject skillPrefab;

	private GameObject gauge;
	private GameObject gaugeWaku;

	public void SetGauge(GameObject _gauge)
	{
		gauge = _gauge;
	}

	public void SetGaugeWaku(GameObject _gaugeWaku)
	{
		gaugeWaku = _gaugeWaku;
	}

	// Use this for initialization
	void Start () {
		nowSkillPt = 0.0f;

#if UNITY_ANDROID && !UNITY_EDITOR
		Bt = GameObject.FindGameObjectWithTag("BlueTooth");
		BtAdapter = Bt.GetComponent<AndroidBlueToothAdapter> ();
#endif
		cutInManager = GameObject.Find("CutInManager");
		cutInManagerCls = cutInManager.GetComponent<CutInManager>();
	}
	
	// Update is called once per frame
	void Update () {
		// タッチ時にスキル発動
		if(CrossInput.I.IsDown())
		{
			Vector3 aTapPoint = Camera.main.ScreenToWorldPoint(CrossInput.I.ScreenPosition);
			Collider2D[] collider2Ds = Physics2D.OverlapPointAll(aTapPoint);
			if (collider2Ds.Length > 0)
			{
				foreach(Collider2D aCollider2D in collider2Ds)
				{
					if (aCollider2D.transform == transform)
					{
						if((nowSkillPt >= maxSkillPt) || debug)
						{
							GameObject obj = aCollider2D.transform.gameObject;
							Debug.Log (obj.name);
							if(BtAdapter != null)
							{
								BtAdapter.SendIntergerData(serialId,Tag.Skill);
							}
							if (cutInManagerCls.CreateCutIn(serialId, false, false))
							{
								nowSkillPt = 0f;
								gauge.GetComponent<Image>().fillAmount = nowSkillPt;
								gaugeWaku.GetComponent<Image>().material = null;
							}
						}
					}
				}
			}
		}
	}

	public void addSkillPt(float pt)
	{
		nowSkillPt += pt * mag;
		Instantiate(SkillMontamaManager.I.maxSkillPtEffect, transform.position, transform.rotation);

		if (nowSkillPt >= maxSkillPt)
		{
			nowSkillPt = maxSkillPt;
			gaugeWaku.GetComponent<Image>().material = SkillMontamaManager.I.maxSkillPtMat;
		}
		else { gaugeWaku.GetComponent<Image>().material = null; }
		gauge.GetComponent<Image>().fillAmount = nowSkillPt / maxSkillPt;
	}

	public void SkillPointMax()
	{
		nowSkillPt = maxSkillPt;
		gaugeWaku.GetComponent<Image>().material = SkillMontamaManager.I.maxSkillPtMat;
		gauge.GetComponent<Image>().fillAmount = nowSkillPt / maxSkillPt;
	}
}
