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

	/// <summary>
	/// デバッグモード
	/// </summary>
	public bool isDebug = false;

	/// <summary>
	/// 対戦相手が使用しているモンクリか
	/// </summary>
	public bool isRival = false;
	
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
						if((nowSkillPt >= maxSkillPt) || isDebug && !isRival)
						{
							GameObject obj = aCollider2D.transform.gameObject;

							if (BtAdapter != null) { BtAdapter.SendIntergerData(serialId, Tag.Skill); }
							if (CutInManager.I.CreateCutIn(serialId, false, false)) { SkillActivate(); }
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

		if (IsSkillActivatable)
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

	/// <summary>
	/// スキル発動時に呼ぶべき後処理メソッド
	/// </summary>
	public void SkillActivate()
	{
		nowSkillPt = 0f;
		gauge.GetComponent<Image>().fillAmount = nowSkillPt;
		gaugeWaku.GetComponent<Image>().material = null;
	}

	/// <summary>
	/// スキルが発動かのうか
	/// </summary>
	public bool IsSkillActivatable { get { return nowSkillPt >= maxSkillPt; } }
}
