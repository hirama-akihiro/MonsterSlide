using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Scale in.
/// Author Kazuki Ito
/// </summary>

public class ScaleIn : MonoBehaviour {

	
	[SerializeField, Range(0, 10)]
	float time = 1;
	
	public Vector3	endScale;
	
	public AnimationCurve curve;

	private float startTime;
	private Vector3 startScale;
	
	void OnEnable ()
	{
		if (time <= 0) {
			transform.localScale = endScale;
			enabled = false;
			return;
		}
		
		startTime = Time.timeSinceLevelLoad;
		startScale = transform.localScale;
	}
	
	void Update ()
	{
		float diff = Time.timeSinceLevelLoad - startTime;
		if (diff > time) {
			transform.localScale = endScale;
			enabled = false;
		}
		
		float rate = diff / time;
		float scale = curve.Evaluate(rate);

		transform.localScale = Vector3.Lerp (startScale, endScale, scale);
	}
}
