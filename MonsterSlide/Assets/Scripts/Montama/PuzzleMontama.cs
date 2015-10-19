using UnityEngine;
using System.Collections;

public class PuzzleMontama : MonoBehaviour {
	public int aryIndex = -1;
	/// <summary>
	/// 1 ～ 4のモンタマ判定番号
	/// </summary>
	public int serialID;

	/// <summary>
	/// 親レーンオブジェクト
	/// </summary>
	public GameObject ParentLane;

	private SpriteRenderer myRenderer;

	public GameObject particle;

	//  ↓  Author kazuki ito
	private bool IsGameOver;
	//  ↑ Author kabuki ito

	// Use this for initialization
	void Start () {
		myRenderer = GetComponent<SpriteRenderer>();
		myRenderer.enabled = !(transform.position.x < 0.5 || LaneManager.LANEMAINWIDTH + 0.5 < transform.position.x);
		IsBited = false;
		IsMaguma = false;
	}
	
	// Update is called once per frame
	void Update () {
		myRenderer.enabled = !(transform.position.x < 0.5 || LaneManager.LANEMAINWIDTH + 0.5 < transform.position.x);

		if (IsNoBegin) { return; }

		if (IsGameOver) {return;}

		// レーンに設置されている時レーンの座標に固定
		if (ParentLane != null) { transform.position = ParentLane.transform.position; }

		// 止まらない限りfallSpeedで落下する
		if (!IsStop) { transform.Translate(0, -FallSpeedManager.Instance.GetFallSpeedMagnification() * Time.deltaTime, 0); }

		// 目標ポイントと一定以上近いなら
		//if (Vector3.Distance(transform.position, TargetPos) < 0.1)
		if(transform.position.y < TargetPos.y + 0.1f)
		{
			if (LaneManager.Instance.IsLockLane(transform.position) && !IsStop)
			{
				if (!LaneManager.Instance.IsHoldLane(transform.position))
				{
					GameObject laneBlock = LaneManager.Instance.GetTouchPosLaneObject(transform.position);
					transform.parent = laneBlock.transform;
					transform.position = TargetPos;
					laneBlock.GetComponent<LaneBlock>().SetHold(gameObject, true);
				}
			}
			else
			{
				TargetPos += new Vector3(0, -1, 0); /* 一段目標を下げる */
			}
		}

		// 連続して落ちた時上手く行くように下のブロックに設置されていたら行こうえで止まる
		if ((LaneManager.Instance.IsHoldLane(TargetPos)) && !IsStop)
		{
			Vector3 setPos = TargetPos + new Vector3(0, 1, 0);
			GameObject laneBlock = LaneManager.Instance.GetTouchPosLaneObject(setPos);
			if (laneBlock)
			{
				transform.parent = laneBlock.transform;
				laneBlock.GetComponent<LaneBlock>().SetHold(gameObject, true);
			}
		}

		// 親レーンがモンタマを保持していない時落ちる
		if (ParentLane != null && ParentLane.GetComponent<LaneBlock>().HoldMontama == null) { ReFallMontama(); }
	}

	/// <summary>
	/// 止まっているモンタマを再落下
	/// </summary>
	public void ReFallMontama()
	{
		transform.position = ParentLane.transform.position;
		TargetPos = ParentLane.transform.position + new Vector3(0, -1, 0);
		ParentLane = null;
		transform.parent = null;
		ParentLane = null;
	}

	
	//  ↓  Author kazuki ito
	public void GameEnd()
	{
		IsGameOver = true;
	}
	
	public void GameStart()
	{
		IsGameOver = false;
	}
	//  ↑ Author kabuki ito

	public void DestroyMonkuri()
	{
		ParticleSystem system = (Instantiate(particle, transform.position, transform.rotation) as GameObject).GetComponent<ParticleSystem>();
		system.Play();
		Destroy(gameObject);
	}


	/// <summary>
	/// 次に移動するターゲット
	/// </summary>
	public Vector3 TargetPos;// { get; set; }

	/// <summary>
	/// パズルモンタマが止まっているか
	/// </summary>
	public bool IsStop { get { return ParentLane != null; } }

	/// <summary>
	/// まだ落下していないか
	/// </summary>
	public bool IsNoBegin { get; set; }

	/// <summary>
	/// スキルで噛まれているか
	/// </summary>
	public bool IsBited { get; set; }

	/// <summary>
	/// マグマ状態か
	/// </summary>
	public bool IsMaguma { get; set; }
}
