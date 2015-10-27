using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerSkillBar : SingletonMonoBehavior<PlayerSkillBar> {

	/// <summary>
	/// 最大スキルポイント
	/// </summary>
	public float maxSkillPt;

	/// <summary>
	/// 現在のスキルポイント
	/// </summary>
	public float nowSkillPt;

	public GameObject pSkillPrefab;

	CutInManager cutInManagerCls;

	/// <summary>
	/// プレイヤースキルゲージObject
	/// </summary>
	public GameObject gauge;

	// Use this for initialization
	void Start () {
		nowSkillPt = 0.0f;
		GameObject cutInManager = GameObject.Find("CutInManager");
		cutInManagerCls = cutInManager.GetComponent<CutInManager>();
	}
	
	// Update is called once per frame
	void Update () {
		// タッチ時にスキル発動
		if (CrossInput.I.IsDown ()) {
			Vector3 aTapPoint = Camera.main.ScreenToWorldPoint (CrossInput.I.ScreenPosition);
			Collider2D[] collider2Ds = Physics2D.OverlapPointAll (aTapPoint);
			if (collider2Ds.Length > 0) {
				foreach (Collider2D aCollider2D in collider2Ds) {
					if (aCollider2D.transform == transform) {
						if ((nowSkillPt >= maxSkillPt)) {
							cutInManagerCls.CreateCutIn(-1, false, true);
							nowSkillPt = 0f;
							gauge.GetComponent<Image> ().fillAmount = nowSkillPt / maxSkillPt;
						}
					}
				}
			}
		}
	}


	/// <summary>
	/// スキルポイントを加算
	/// </summary>
	/// <param name="skillPt"></param>
	public void AddSkillPt(int skillPt)
	{
		nowSkillPt += skillPt;
		gauge.GetComponent<Image> ().fillAmount = nowSkillPt / maxSkillPt;
	}

	public void SetGauge(GameObject _gauge)
	{
		gauge = _gauge;
	}
}
