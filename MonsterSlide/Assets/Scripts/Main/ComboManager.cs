using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ComboManager : SingletonMonoBehavior<ComboManager> {
	/// <summary>
	/// コンボ数
	/// </summary>
	public int comboCt;

	/// <summary>
	/// コンボを許可するインターバル
	/// </summary>
	public float comboInterval;

	/// <summary>
	/// 現状のインターバル時間
	/// </summary>
	private float nowInterval;

	/// <summary>
	/// COMBOの文字画像
	/// </summary>
	public Sprite comboStrImage;

	/// <summary>
	/// コンボに使う数字イメージ配列
	/// </summary>
	public List<Sprite> comboNumImages;

	public Image comboStrObj;
	public Image comboNumObj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		nowInterval -= Time.deltaTime;
		if (nowInterval < 0)
		{
			comboCt = 0;
		}

		DrawComboCt(comboCt);
	}

	public void AddCombo(int ct)
	{
		nowInterval = comboInterval;
		comboCt += ct;
	}

	private void DrawComboCt(int comboCt)
	{
		if (comboCt == 0)
		{
			comboStrObj.enabled = false;
			comboNumObj.enabled = false;
			return;
		}
		else
		{
			comboStrObj.enabled = true;
			comboNumObj.enabled = true;
			comboNumObj.sprite = comboNumImages[comboCt];
		}
	}

	public float GetcomboMag()
	{
		return 1 + 0.1f * comboCt;
	}

	/// <summary>
	/// 消滅に必要な連鎖数
	/// </summary>
	/// <returns></returns>
	public int GetReqNumOfChain()
	{
		switch(comboCt)
		{
			case 0:
				return 5;
			case 1:
				return 5;
			case 2:
				return 4;
			case 3:
				return 4;
			case 4:
				return 3;
			case 5:
				return 3;
			case 6:
				return 3;
			default:
				return 2;
		}
	}

//	public void OnGUI()
//	{
//		int sw = Screen.width;
//		int sh = Screen.height;
//		GUI.skin = guiSkin;
//
//		// シーン名
//		GUI.Label(new Rect(0, 0, sw, sh), "コンボ数:" + comboCt.ToString(), "SceneName");
//		GUI.Label(new Rect(0, 0, sw, sh + sh * 0.5f), "落下速度:" + FallSpeedManager.Instance.GetFallSpeedMagnification(), "SceneName");
//	}
}
