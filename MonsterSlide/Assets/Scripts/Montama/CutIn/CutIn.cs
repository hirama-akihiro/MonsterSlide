using UnityEngine;
using System.Collections;


/// <summary>
/// Cut in.
/// Author Kazuki Ito
/// </summary>

public class CutIn : MonoBehaviour
{
	
	[SerializeField, Range(0, 10)]
	float time = 1;

	public Vector3	endPosition;
	
	public AnimationCurve curve;

	private float startTime;
	private Vector3 startPosition;
	
	void OnEnable ()
	{
		endPosition.y = transform.position.y;
		if (time <= 0) {
			transform.position = endPosition;
			enabled = false;
			return;
		}
		
		startTime = Time.timeSinceLevelLoad;
		startPosition = transform.position;
		gameObject.GetComponent<CutInData> ().setStartPosition (startPosition);
	}
	
	void Update ()
	{
		float diff = Time.timeSinceLevelLoad - startTime;
		if (diff > time) {
			transform.position = endPosition;
			gameObject.GetComponent<CutOut>().enabled = true;
			enabled = false;
		}
		
		float rate = diff / time;
		float pos = curve.Evaluate(rate);

		transform.position = Vector3.Lerp (startPosition, endPosition, pos);
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