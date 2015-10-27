using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 対戦相手の基底クラス
/// </summary>
public class BRival : SingletonMonoBehavior<BRival>, IRival {

	/// <summary>
	/// ゲーム終了しているか
	/// </summary>
	private bool isGameEnd;

	/// <summary>
	/// 現状の対戦相手(ライバル)のHp
	/// </summary>
	protected float nowHP = 0.0f;

	/// <summary>
	/// ライバルのHPゲージ
	/// </summary>
	public GameObject hpGauge;

	/// <summary>
	/// 対戦相手が使用するモンクリのパーティ
	/// </summary>
	public List<GameObject> partyMonkuris = new List<GameObject>();

	protected override void Awake()
	{
		base.Awake();
		nowHP = 0.4444444f;
	}

	// Use this for initialization
	protected virtual void Start () {
	
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		// ゲーム終了後は何もしない
		if (isGameEnd) { return; }

		// ゲーム終了の判定
		if (nowHP > 1.0f)
		{
			MainManager.I.GameOver(true);
			GameEnder.I.IsGameEnd = true;
			isGameEnd = true;
		}
		hpGauge.GetComponent<Image>().fillAmount = nowHP;
	}

	/// <summary>
	/// ゲーム開始時に呼ぶメソッド
	/// </summary>
	public void GameStart() { enabled = true; }

	/// <summary>
	/// ゲーム終了時に呼ぶメソッド
	/// </summary>
	public void GameEnd() { enabled = false; }

	/// <summary>
	/// 現状の対戦相手のHP
	/// </summary>
	/// <returns></returns>
	public float NowHp { get { return nowHP; } set { nowHP = value; } }

	/// <summary>
	/// ゲーム終了しているか
	/// </summary>
	public bool IsGameEnd { get { return isGameEnd; } set { isGameEnd = value; } }
}
