using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MEvenLaneLock : MSkillBase {

	/// <summary>
	/// 蛇のエフェクト画像
	/// </summary>
	public GameObject skillHebiEffefct;

	private List<GameObject> skillEffects = new List<GameObject>();

	// Use this for initialization
	void Start () {
		AudioManager.Instance.PlayAudio("se_Kimera");
	}
	
	// Update is called once per frame
	void Update () {
		if (ElapsedTime > skillTime)
		{
			DrawLaneBlockManager.Instance.NoMoveColumns.Clear();
			LaneManager.Instance.NoMoveColumns.Clear();
			foreach (GameObject aSkill in skillEffects) { Destroy(aSkill);  }
			skillEffects.Clear();
			Destroy(gameObject);
		}
	}

	public override void ActionSkill()
	{
		// 偶数のレーンを止める
		for (int i = 0; i < LaneManager.LANEMAINHEIGHT; i++)
		{
			if ((i % 2) != 1) { continue; }
			//DrawLaneBlockManager.Instance.NoMoveColumns.Add(i);
		}
		for (int i = 0; i < LaneManager.LANEFRAMEHEIGHT; i++)
		{
			if ((i % 2) != 0) { continue; }
			if (i != 0 && i != LaneManager.LANEMAINHEIGHT + 1)
			{
				skillEffects.Add(Instantiate(skillHebiEffefct, new Vector3(4f, -i, 0), transform.rotation) as GameObject);
			}
			LaneManager.Instance.NoMoveColumns.Add(i);
		}

		DrawLaneBlockManager.Instance.DrawNoMoveLane();
	}
}
