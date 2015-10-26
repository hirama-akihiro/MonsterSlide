using UnityEngine;
using System.Collections;

/// <summary>
/// Cut in.
/// </summary>
public class CutIn : MonoBehaviour
{
	
	[SerializeField, Range(0, 10)]
	private float time = 1;

	/// <summary>
	/// 移動終了位置
	/// </summary>
	public Vector3	endPosition;
	
	/// <summary>
	/// アニメーションカーブ
	/// </summary>
	public AnimationCurve curve;

	/// <summary>
	/// 移動開始時間
	/// </summary>
	private float startTime;

	/// <summary>
	/// 移動開始位置
	/// </summary>
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