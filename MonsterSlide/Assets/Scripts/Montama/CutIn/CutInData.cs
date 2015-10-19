using UnityEngine;
using System.Collections;

/// <summary>
/// Cut in data.
/// Author Kazuki Ito
/// </summary>

public class CutInData : MonoBehaviour {

	public Sprite[] CutInSprite = new Sprite[2];

	public GameObject SkillPrefb;

	public bool attackSkill;

	Vector3 startPosition;

	bool rivalSkill;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setStartPosition(Vector3 _startPosition)
	{
		startPosition = _startPosition;
	}

	public Vector3 getStartPosition()
	{
		return startPosition;
	}

	public void setRivalSkill(bool _rivalSkill)
	{
		rivalSkill = _rivalSkill;
		if (rivalSkill) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = CutInSprite [0];
		} else {
			gameObject.GetComponent<SpriteRenderer>().sprite = CutInSprite[1];
		}
	}

	public bool getRivalSkill()
	{
		return rivalSkill;
	}

	public void applicationSkill()
	{
		if (!SkillPrefb) { return; }
		if (rivalSkill && attackSkill) {
			GameObject skill = Instantiate (SkillPrefb);
			skill.SendMessage ("ActionSkill");
		} else if (!rivalSkill && !attackSkill) {
			GameObject skill = Instantiate (SkillPrefb);
			skill.SendMessage ("ActionSkill");
		}
	}
}
