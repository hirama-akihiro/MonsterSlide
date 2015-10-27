using UnityEngine;
using System.Collections;

public class StartManager : MonoBehaviour {

	[SerializeField, Range(0.5f, 5)]
	float time = 1f;

	GameObject Bt;

	AndroidBlueToothAdapter BtAdapter;

	bool start;

	float startTime;

	static bool managerEnable;

	// Use this for initialization
	void Start () {
		
		if (managerEnable) {
			GameEnder.I.IsGameEnd = true;
			SkillMontamaManager.I.SkillButtomEnable (false);
#if UNITY_ANDROID && !UNITY_EDITOR
		Bt = GameObject.FindGameObjectWithTag("BlueTooth");
		BtAdapter = Bt.GetComponent<AndroidBlueToothAdapter> ();

#endif
			startTime = Time.timeSinceLevelLoad;
		}
		else
		{
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (managerEnable) {
			float diff = Time.timeSinceLevelLoad - startTime;
			if (diff > time) {

				if (BtAdapter != null) {
					BtAdapter.SendIntergerData (0, Tag.Start);
				}
				GetComponent<SpriteRenderer>().enabled = false;
				SkillMontamaManager.I.SkillButtomEnable(true);
				enabled = false;
			}
		}
		else
		{
			float diff = Time.timeSinceLevelLoad - startTime;
			if (diff > time)
			{
				AudioManager.I.PlayAudio("se_start");
				GetComponent<SpriteRenderer>().enabled = false;
				SkillMontamaManager.I.SkillButtomEnable(true);
				enabled = false;
			}
		}
	}

	public void GameStart()
	{
		if (!start) {
			start = true;
			if(BtAdapter != null)
			{
				BtAdapter.SendIntergerData(0, Tag.Start);
			}
			GameEnder.I.IsGameEnd = false;
			SkillMontamaManager.I.SkillButtomEnable(true);
		}
	}

	public static void SetManagerEnable(bool enable)
	{
		managerEnable = enable;
	}
}
