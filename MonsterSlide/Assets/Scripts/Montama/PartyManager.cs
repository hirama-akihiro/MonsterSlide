using UnityEngine;
using System.Collections;

public class PartyManager : SingletonMonoBehavior<PartyManager>
{
	/// <summary>
	/// パーティサイズ(モンクリ構成:4)
	/// </summary>
	public readonly static int MAXPARTYCOUNT = 4;

	/// <summary>
	/// パーティパズルモンクリ配列
	/// </summary>
	public GameObject[] partyPuzzleMontama = new GameObject[MAXPARTYCOUNT];

	/// <summary>
	/// パーティスキルモンクリ配列
	/// </summary>
	public GameObject[] partySkillMontama = new GameObject[MAXPARTYCOUNT];

	/// <summary>
	/// 対戦相手のパーティパズルモンクリ配列
	/// </summary>
	public GameObject[] rivalPuzzleMonkuri = new GameObject[MAXPARTYCOUNT];

	/// <summary>
	/// 対戦相手のスキルモンクリ配列
	/// </summary>
	public GameObject[] rivalSkillMonkuri = new GameObject[MAXPARTYCOUNT];

	// Use this for initialization
	void Start()
	{
		DontDestroyOnLoad(this);
	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// パーティモンタマ取得(0から始まる番号指定)
	/// </summary>
	/// <returns></returns>
	public GameObject GetPuzzleMontama(int index) { return partyPuzzleMontama[index]; }

	/// <summary>
	/// パーティモンタマに設定
	/// </summary>
	/// <param name="index"></param>
	/// <param name="monkuri"></param>
	public void SetPuzzleMontama(int index, GameObject monkuri) { partyPuzzleMontama[index] = monkuri; }

	/// <summary>
	/// パーティモンタマ取得(0から始まる番号指定)
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	public GameObject GetSkillMonkuri(int index) { return partySkillMontama[index]; }

	/// <summary>
	/// パーティモンタマに設定
	/// </summary>
	/// <param name="index"></param>
	/// <param name="monkuri"></param>
	public void SetSkillMontama(int index, GameObject monkuri) { partySkillMontama[index] = monkuri; }

	/// <summary>
	/// パーティモンタマ取得(0から始まる番号指定)
	/// </summary>
	/// <returns></returns>
	public GameObject GetRivalPuzzleMontama(int index) { return rivalPuzzleMonkuri[index]; }

	/// <summary>
	/// パーティモンタマに設定
	/// </summary>
	/// <param name="index"></param>
	/// <param name="monkuri"></param>
	public void SetRivalPuzzleMontama(int index, GameObject monkuri) { rivalPuzzleMonkuri[index] = monkuri; }

	/// <summary>
	/// パーティモンタマ取得(0から始まる番号指定)
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	public GameObject GetRivalSkillMonkuri(int index) { return rivalSkillMonkuri[index]; }

	/// <summary>
	/// パーティモンタマに設定
	/// </summary>
	/// <param name="index"></param>
	/// <param name="monkuri"></param>
	public void SetRivalSkillMontama(int index, GameObject monkuri)
	{
		monkuri.GetComponent<SkillMontama>().isRival = true;
		rivalSkillMonkuri[index] = monkuri;
	}

	/// <summary>
	/// パーティ内のモンタマをランダムで取得
	/// </summary>
	/// <returns></returns>
	public GameObject GetRandomPuzzleMonkuri()
	{
		int index = Random.Range(0, MAXPARTYCOUNT);
		return partyPuzzleMontama[index];
	}
}
