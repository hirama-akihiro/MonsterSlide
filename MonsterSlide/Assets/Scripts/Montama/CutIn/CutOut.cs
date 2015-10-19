using UnityEngine;
using System.Collections;

/// <summary>
/// Cut out.
/// Author Kazuki Ito
/// </summary>

public class CutOut : MonoBehaviour {

	[SerializeField, Range(0, 10)]
	float time = 1;

	[SerializeField, Range(0, 2)]
	float freezeTime = 0.5f;

	Vector3	endPosition;
	
	[SerializeField]
	AnimationCurve curve;

	
	private float startTime;
	private Vector3 startPosition;
	private bool freeze = false;

	/// <summary>
	/// 終了位置
	/// </summary>
	public Vector3 endPos;

	/// <summary>
	/// 自動的にカットインの位置に戻るかどうか
	/// </summary>
	public bool isAutoCutOut = true;

	void OnEnable ()
	{
		endPosition = endPos;
		if (isAutoCutOut) { endPosition = gameObject.GetComponent<CutInData>().getStartPosition(); }
		startTime = Time.timeSinceLevelLoad;
		freeze = true;
	}
	
	// Update is called once per frame
	void Update () {

		float diff = Time.timeSinceLevelLoad - startTime;
		if (freeze) {
			if (diff > freezeTime) {
				if (time <= 0) {
					transform.position = endPosition;
					enabled = false;
					return;
				}
				startTime = Time.timeSinceLevelLoad;
				startPosition = transform.position;
				freeze = false;
			} 
		}else if (diff > time) {
			gameObject.GetComponent<CutInData>().applicationSkill();
			transform.position = endPosition;
			Destroy(gameObject);
			enabled = false;
		} else {
			float rate = diff / time;
			float pos = curve.Evaluate (rate);

			transform.position = Vector3.Lerp (startPosition, endPosition, pos);
		}
	}

	void OnDrawGizmosSelected ()
	{
		#if UNITY_EDITOR
		
		if( !UnityEditor.EditorApplication.isPlaying || enabled == false ){
			startPosition = transform.position;
		}
		
		UnityEditor.Handles.Label(endPosition, endPosition.ToString());
		UnityEditor.Handles.Label(startPosition, startPosition.ToString());
		#endif
		Gizmos.DrawSphere (endPosition, 0.1f);
		Gizmos.DrawSphere (startPosition, 0.1f);
		
		Gizmos.DrawLine (startPosition, endPosition);
	}
}
