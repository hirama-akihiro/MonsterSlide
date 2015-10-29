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
	public float beginHpUpThreshold = 0.7f;

	/// <summary>
	/// 現状のHPが増加する確率閾値(0 ~ 1)
	/// </summary>
	[Range(0.0f, 1.0f)]
	private float nowHpUpThreshold;

	/// <summary>
	/// ゲーム開始時のHPが減少する確率閾値(0 ~ 1)
	/// </summary>
	[Range(0.0f, 1.0f)]
	public float beginHpDownThreshold = 0.2f;

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
		if(BattleTypeManager.I.battleType != BattleTypeManager.BattleType.SingleBattle) { return; }
		base.Update();
	}

	private IEnumerator AIUpdate()
	{
		while (true)
		{
			// HP増減チェック(減少時にスキルポイントUp)
			yield return new WaitForSeconds(2.0f);
			float check = Random.Range(0.0f, 1.0f);
			if (IsHpUp(check)) { nowHP += hpVariation; }
			else if (IsHpDown(check) && nowHP > 0.2)
			{
				nowHP -= hpVariation;
				SkillMontamaManager.I.AddRandomRivalSkillPt(5); 
			}

			// スキル発動チェック
			int index = Random.Range(0, 4);
			SkillMontama skillMonkuri = SkillMontamaManager.I.RivalSkillMonkuris[index].GetComponent<SkillMontama>();
			if (IsSkillActive(check) && skillMonkuri.IsSkillActivatable)
			{
				if (CutInManager.I.CreateCutIn(skillMonkuri.serialId, true, false)) { skillMonkuri.SkillActivate(); }
			}
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
