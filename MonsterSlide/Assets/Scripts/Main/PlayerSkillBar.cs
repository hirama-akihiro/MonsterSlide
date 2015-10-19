using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerSkillBar : SingletonMonoBehavior<PlayerSkillBar> {

//	public GameObject backBar;
//
//	public GameObject skillBar;

	public float maxSkillPt;

	public float nowSkillPt;

	public bool debug;

	public GameObject pSkillPrefab;

	CutInManager cutInManagerCls;

	//  ↓  Author kazuki ito
	private GameObject gauge;
	//  ↑ Author kabuki ito

	// Use this for initialization
	void Start () {
		nowSkillPt = 0.0f;
		GameObject cutInManager = GameObject.Find("CutInManager");
		cutInManagerCls = cutInManager.GetComponent<CutInManager>();
	}
	
	// Update is called once per frame
	void Update () {
		// タッチ時にスキル発動
		if (CrossInput.Instance.IsDown ()) {
			Vector3 aTapPoint = Camera.main.ScreenToWorldPoint (CrossInput.Instance.ScreenPosition);
			Collider2D[] collider2Ds = Physics2D.OverlapPointAll (aTapPoint);
			if (collider2Ds.Length > 0) {
				foreach (Collider2D aCollider2D in collider2Ds) {
					if (aCollider2D.transform == transform) {
						if ((nowSkillPt >= maxSkillPt) || debug) {
							cutInManagerCls.CreateCutIn(-1, false, true);
							nowSkillPt = 0f;
							gauge.GetComponent<Image> ().fillAmount = nowSkillPt / maxSkillPt;
						}
					}
				}
			}
		}
	}

//	void OnGUI()
//	{
//		Texture backTexture = backBar.GetComponent<GUITexture>().texture;
//		Texture skillTexture = skillBar.GetComponent<GUITexture>().texture;
//		int sh = Screen.height;
//		int sw = Screen.width;
//		float ratio = nowSkillPt / maxSKillPt;
//		if (ratio > 1) { ratio = 1.0f; }
//		float spBatheight = sh * 0.5f * ratio;
//
//		GUI.DrawTexture(new Rect(sw * 0.02f , sh * 0.3f, sw*0.05f, sh * 0.5f), backTexture);
//		GUI.DrawTexture(new Rect(sw * 0.02f, sh * 0.8f - spBatheight, sw * 0.05f, spBatheight), skillTexture);
//	}


	/// <summary>
	/// スキルポイントを加算
	/// </summary>
	/// <param name="skillPt"></param>
	public void AddSkillPt(int skillPt)
	{
		nowSkillPt += skillPt;
		gauge.GetComponent<Image> ().fillAmount = nowSkillPt / maxSkillPt;
	}

	//  ↓  Author kazuki ito
	public void SetGauge(GameObject _gauge)
	{
		gauge = _gauge;
	}
	//  ↑ Author kabuki ito
}
