﻿using UnityEngine;
using System.Collections;

public class FaceChanger : MonoBehaviour {

	private SpriteRenderer myRenderer;
	public Sprite p1_woman01;
	public Sprite p1_woman02;
	public Sprite p1_woman03;
	public bool isPlayer1;
	public float hp;

	// Use this for initialization
	void Start () {
		myRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isPlayer1) { hp = LaneManager.Instance.OldHp; }

		if(hp < 0.6)
		{
			myRenderer.sprite = p1_woman01;
		}
		else if(hp < 0.8)
		{
			myRenderer.sprite = p1_woman02;
		}
		else
		{
			myRenderer.sprite = p1_woman03;
		}
	}
}
