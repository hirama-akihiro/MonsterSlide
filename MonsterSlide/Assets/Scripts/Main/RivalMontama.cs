using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RivalMontama : MonoBehaviour {

	public GameObject gauge;

	public Sprite[] Skilltextures;

	private int serialID;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setSerialID(int _serialID)
	{
		serialID = _serialID;
		gameObject.GetComponent<SpriteRenderer> ().sprite = Skilltextures [serialID];
	}

	public int getSerialID()
	{
		return serialID;
	}

	public void setGauge(GameObject _gauge)
	{
		gauge = _gauge;
	}

	public void UpDateGauge(float  percent)
	{
		gauge.GetComponent<Image> ().fillAmount = percent;
	}
}
