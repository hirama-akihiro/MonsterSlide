using UnityEngine;
using System.Collections;


/// <summary>
/// Cut in manager.
/// Author Kazuki Ito
/// </summary>

public class CutInManager : SingletonMonoBehavior<CutInManager> {

	public float initialPosY;

	public float intervalPosY;

	public float playerSidePosX;

	public float rivalSidePosX;

	public GameObject darken;
	
	public GameObject[] cutInPrefabs;
	
	public GameObject[] cutIns;

	public GameObject masterCutIn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int i = 0;
		while (i < cutIns.Length && cutIns[i] == null) {
			i++;
		}
	
		if (i >= cutIns.Length && !LaneManager.I.IsGameEnd) {
			darken.SetActive(false);
			GameEnder.I.IsGameEnd = false;
		}
	}

	public bool CreateCutIn(int serialID, bool rivalSkill, bool isMaster)
	{
		int i = 0;
		bool isCreate = false;
		while (i < cutIns.Length && cutIns[i] != null)
		{
			i++;
		}

		if (isMaster)
		{
			// カットイン用のSEを鳴らす
			isCreate = true;
			AudioManager.I.PlayAudio("se_skillCutIn");
			GameEnder.I.IsGameEnd = true;
			darken.SetActive(true);
			if(rivalSkill)
			{
				Instantiate(masterCutIn, new Vector3(7, 0, 0), Quaternion.identity);
			}
			else
			{
				Instantiate(masterCutIn, new Vector3(-7, -4, 0), Quaternion.identity);
			}
		}
		else
		{
			if (i < cutIns.Length)
			{
				// カットイン用のSEを鳴らす
				AudioManager.I.PlayAudio("se_skillCutIn");
				isCreate = true;
				GameEnder.I.IsGameEnd = true;
				darken.SetActive(true);
				if (rivalSkill)
				{
					cutIns[i] = Instantiate(cutInPrefabs[serialID],
															   new Vector3(rivalSidePosX, initialPosY + intervalPosY * i, 0f),
															   transform.rotation) as GameObject;
				}
				else
				{
					cutIns[i] = Instantiate(cutInPrefabs[serialID],
											new Vector3(playerSidePosX, initialPosY + intervalPosY * i, 0f),
											transform.rotation) as GameObject;

				}
				cutIns[i].transform.SetParent(gameObject.transform);
				cutIns[i].GetComponent<CutInData>().setRivalSkill(rivalSkill);
			}
		}
		return isCreate;
	}
}
