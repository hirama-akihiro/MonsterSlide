using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Hp gauge.
/// Author Kazuki Ito
/// </summary>
public class HpGauge : MonoBehaviour {

	float hp =0.0f;
	Image imag;

	// Use this for initialization
	void Start () {
		imag = gameObject.GetComponent<Image> ();
		imag.fillAmount = hp;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateHp(float _hp)
	{
		hp = _hp;
		if (imag == null){
			Debug.Log("imagnull");
			return;
		}
		imag.fillAmount = hp;
	}
}
