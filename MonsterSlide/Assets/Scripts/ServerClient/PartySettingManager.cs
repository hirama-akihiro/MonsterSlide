using UnityEngine;
using System.Collections;

public class PartySettingManager : SingletonMonoBehavior<PartySettingManager>
{
	public GameObject[] allPuzzleMontamas;
	public GameObject[] allSkillMontamas;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void SetServerParty()
	{
		// プレイヤーのパーティ選択
		PartyManager.I.SetPuzzleMontama(0, allPuzzleMontamas[1]);
		PartyManager.I.SetSkillMontama(0, allSkillMontamas[1]);
		PartyManager.I.SetPuzzleMontama(1, allPuzzleMontamas[4]);
		PartyManager.I.SetSkillMontama(1, allSkillMontamas[4]);
		PartyManager.I.SetPuzzleMontama(2, allPuzzleMontamas[2]);
		PartyManager.I.SetSkillMontama(2, allSkillMontamas[2]);
		PartyManager.I.SetPuzzleMontama(3, allPuzzleMontamas[3]);
		PartyManager.I.SetSkillMontama(3, allSkillMontamas[3]);
	}

	public void SetClientParty()
	{
		PartyManager.I.SetPuzzleMontama(0, allPuzzleMontamas[0]);
		PartyManager.I.SetSkillMontama(0, allSkillMontamas[0]);
		PartyManager.I.SetPuzzleMontama(1, allPuzzleMontamas[5]);
		PartyManager.I.SetSkillMontama(1, allSkillMontamas[5]);
		PartyManager.I.SetPuzzleMontama(2, allPuzzleMontamas[4]);
		PartyManager.I.SetSkillMontama(2, allSkillMontamas[4]);
		PartyManager.I.SetPuzzleMontama(3, allPuzzleMontamas[2]);
		PartyManager.I.SetSkillMontama(3, allSkillMontamas[2]);
	}

	public void SetSingleParty()
	{
		SetServerParty();

		// 対戦相手のパーティ選択
		if (BattleTypeManager.I.battleType != BattleTypeManager.BattleType.SingleBattle) { return; }
		PartyManager.I.SetRivalPuzzleMontama(0, allPuzzleMontamas[0]);
		PartyManager.I.SetRivalSkillMontama(0, allSkillMontamas[0]);
		PartyManager.I.SetRivalPuzzleMontama(1, allPuzzleMontamas[5]);
		PartyManager.I.SetRivalSkillMontama(1, allSkillMontamas[5]);
		PartyManager.I.SetRivalPuzzleMontama(2, allPuzzleMontamas[2]);
		PartyManager.I.SetRivalSkillMontama(2, allSkillMontamas[2]);
		PartyManager.I.SetRivalPuzzleMontama(3, allPuzzleMontamas[1]);
		PartyManager.I.SetRivalSkillMontama(3, allSkillMontamas[1]);
	}
}
