using UnityEngine;
using System.Collections;

/// <summary>
/// シングルモードで戦うAI対戦相手のクラス
/// </summary>
public class AIRival : BRival {

	/// <summary>
	/// ゲーム開始時のHPが増加する確率閾値(0 ~ 1)
	/// </summary>
	[Range(0.0f, 1.0f)]
	public float beginHpUpThreshold = 0.8f;

	/// <summary>
	/// 現状のHPが増加する確率閾値(0 ~ 1)
	/// </summary>
	[Range(0.0f, 1.0f)]
	private float nowHpUpThreshold;

	/// <summary>
	/// ゲーム開始時のHPが減少する確率閾値(0 ~ 1)
	/// </summary>
	[Range(0.0f, 1.0f)]
	public float beginHpDownThreshold = 0.1f;

	/// <summary>
	/// 現状のHPが減少する確率閾値(0 ~ 1)
	/// </summary>
	[Range(0.0f, 1.0f)]
	private float nowHpDownThreshold;

	/// <summary>
	/// HP変化量
	/// </summary>
	[Range(0.0f, 1.0f)]
	public float hpVariation;

	/// <summary>
	/// スキル発動確率閾値
	/// </summary>
	[Range(0.0f, 1.0f)]
	public float skillTriggerThreshold;

	// Use this for initialization
	protected override void Start()
	{
		nowHpUpThreshold = beginHpUpThreshold;
		nowHpDownThreshold = beginHpDownThreshold;
		StartCoroutine("AIUpdate");
	}

	// Update is called once per frame
	protected override void Update()
	{
		base.Update();
	}

	private IEnumerator AIUpdate()
	{
		while (true)
		{
			// HP増減チェック(減少時にスキルポイントUp)
			float check = Random.Range(0.0f, 1.0f);
			if (IsHpUp(check)) { nowHP += hpVariation; }
			else if (IsHpDown(check))
			{
				nowHP -= hpVariation;
				SkillMontamaManager.I.AddRandomRivalSkillPt(5);
			}
			yield return new WaitForSeconds(1.0f);

			// スキル発動チェック
			int index = Random.Range(0, 4);
			SkillMontama skillMonkuri = SkillMontamaManager.I.RivalSkillMonkuris[index].GetComponent<SkillMontama>();
			if (IsSkillActive(check) && skillMonkuri.IsSkillActivatable)
			{
				if (CutInManager.I.CreateCutIn(skillMonkuri.serialId, true, false)) { skillMonkuri.SkillActivate(); }
			}
			yield return new WaitForSeconds(1.0f);
		}
	}

	/// <summary>
	/// HPが増加するか
	/// </summary>
	private bool IsHpUp(float check)
	{
		return check > nowHpUpThreshold;
	}

	/// <summary>
	/// HPが減少するか
	/// /// </summary>
	/// <returns></returns>
	private bool IsHpDown(float check)
	{
		return check < nowHpDownThreshold;
	}

	private bool IsSkillActive(float check)
	{
		int serialId = Random.Range(0, 4);
		return check > 0.3f;
	}
}
