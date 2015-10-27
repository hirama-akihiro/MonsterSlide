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
	public float beginHpUpThreshold;

	/// <summary>
	/// 現状のHPが増加する確率閾値(0 ~ 1)
	/// </summary>
	[Range(0.0f, 1.0f)]
	private float nowHpUpThreshold;

	/// <summary>
	/// ゲーム開始時のHPが減少する確率閾値(0 ~ 1)
	/// </summary>
	[Range(0.0f, 1.0f)]
	public float beginHpDownThreshold;

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
	}

	// Update is called once per frame
	protected override void Update()
	{
		base.Update();
		// HP増減チェック
		float check = Random.Range(0.0f, 1.0f);
		if (IsHpUp(check)) { nowHP += hpVariation; }
		else if (IsHpDown(check)) { nowHP -= hpVariation; }

		// スキル発動チェック
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
}
