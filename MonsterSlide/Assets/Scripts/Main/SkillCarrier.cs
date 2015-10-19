using UnityEngine;
using System.Collections;

public class SkillCarrier : MonoBehaviour {

	private Vector3 nowPos;

	private Vector3 nextPos;

	private int skillPt;

	private int serialId;

	/// <summary>
	/// 開始時間
	/// </summary>
	private float startTime;

	/// <summary>
	/// キャリアーの移動時間
	/// </summary>
	public float moveTime;

	// Use this for initialization
	void Start () {
		startTime = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update()
	{
		//Vector3 direction = nextPos - nowPos;
		//nowPos += direction.normalized * 0.1f;
		//transform.position = nowPos;
		//if (Vector3.Distance(nowPos, nextPos) < 1)
		//{
		//	SkillMontamaManager.Instance.AddSkillPt(serialId, 1.0f);
		//	PlayerSkillBar.Instance.AddSkillPt(1);
		//	Destroy(gameObject);
		//}
		float diff = Time.timeSinceLevelLoad - startTime;
		float rate = diff / moveTime;
		transform.position = Vector3.Lerp(nowPos, nextPos, rate);

		if(diff > moveTime)
		{
			nowPos = nextPos;
			SkillMontamaManager.Instance.AddSkillPt(serialId, 1.0f);
			PlayerSkillBar.Instance.AddSkillPt(1);
			Destroy(gameObject);
		}
	}

	public void CarrySkillPt(int serialId, int skillPt, Vector3 nowPos,Vector3 nextPos)
	{
		this.serialId = serialId;
		this.skillPt = skillPt;
		this.nowPos = nowPos;
		this.nextPos = nextPos;
	}
}
