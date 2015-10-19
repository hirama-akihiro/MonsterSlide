using UnityEngine;
using System.Collections;

public class PartyManager : SingletonMonoBehavior<PartyManager>
{
	/// <summary>
	/// パーティサイズ(モンタマ構成:4)
	/// </summary>
	public readonly static int MAXPARTYCOUNT = 4;

	/// <summary>
	/// パーティパズルモンタマ配列
	/// </summary>
	public GameObject[] partyPuzzleMontama = new GameObject[MAXPARTYCOUNT];

	/// <summary>
	/// パーティスキルモンタマ
	/// </summary>
	public GameObject[] partySkillMontama = new GameObject[MAXPARTYCOUNT];

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
	/// パーティ内のモンタマをランダムで取得
	/// </summary>
	/// <returns></returns>
	public GameObject GetRandomPuzzleMonkuri()
	{
		int index = Random.Range(0, MAXPARTYCOUNT);
		return partyPuzzleMontama[index];
	}
}
