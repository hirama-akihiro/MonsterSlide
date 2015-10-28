using UnityEngine;
using System.Collections;

public class PSkillBase : MonoBehaviour{

	/// <summary>
	/// このスキルが生きているかどうか
	/// </summary>
	private bool IsEnable;

	protected float startTime;

	public float skillTime;

	// Use this for initialization
	void Awake () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetSkill() { }

	public virtual void ActionSkill() { }

	protected float ElapsedTime { get { return Time.time - startTime; } }
}
