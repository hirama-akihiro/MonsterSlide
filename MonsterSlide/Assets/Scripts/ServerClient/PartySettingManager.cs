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
		// ベタ書き指定で申し訳ない
		PartyManager.Instance.SetPuzzleMontama(0, allPuzzleMontamas[1]);
		PartyManager.Instance.SetSkillMontama(0, allSkillMontamas[1]);
		PartyManager.Instance.SetPuzzleMontama(1, allPuzzleMontamas[4]);
		PartyManager.Instance.SetSkillMontama(1, allSkillMontamas[4]);
		PartyManager.Instance.SetPuzzleMontama(2, allPuzzleMontamas[2]);
		PartyManager.Instance.SetSkillMontama(2, allSkillMontamas[2]);
		PartyManager.Instance.SetPuzzleMontama(3, allPuzzleMontamas[3]);
		PartyManager.Instance.SetSkillMontama(3, allSkillMontamas[3]);
	}

	public void SetClientParty()
	{
		PartyManager.Instance.SetPuzzleMontama(0, allPuzzleMontamas[0]);
		PartyManager.Instance.SetSkillMontama(0, allSkillMontamas[0]);
		PartyManager.Instance.SetPuzzleMontama(1, allPuzzleMontamas[5]);
		PartyManager.Instance.SetSkillMontama(1, allSkillMontamas[5]);
		PartyManager.Instance.SetPuzzleMontama(2, allPuzzleMontamas[4]);
		PartyManager.Instance.SetSkillMontama(2, allSkillMontamas[4]);
		PartyManager.Instance.SetPuzzleMontama(3, allPuzzleMontamas[2]);
		PartyManager.Instance.SetSkillMontama(3, allSkillMontamas[2]);
	}

	public void SetSingleParty()
	{
		SetServerParty();
	}
}
