using UnityEngine;
using System.Collections;

public class PuzzleMontama : MonoBehaviour {
	public int aryIndex = -1;
	/// <summary>
	/// 0から始まるモンクリ固有のID
	/// </summary>
	public int serialID;

	/// <summary>
	/// 親レーンオブジェクト
	/// </summary>
	public GameObject parentLane;

	/// <summary>
	/// アタッチされているRenderer
	/// </summary>
	private SpriteRenderer myRenderer;

	/// <summary>
	/// 消滅時のパーティクル
	/// </summary>
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
		if (parentLane != null) { transform.position = parentLane.transform.position; }

		// 止まらない限りfallSpeedで落下する
		if (!IsStop) { transform.Translate(0, -FallSpeedManager.I.GetFallSpeedMagnification() * Time.deltaTime, 0); }

		// 目標ポイントと一定以上近いなら
		if(transform.position.y < TargetPos.y + 0.1f)
		{
			if (LaneManager.I.IsLockLane(transform.position) && !IsStop)
			{
				if (!LaneManager.I.IsHoldLane(transform.position))
				{
					GameObject laneBlock = LaneManager.I.GetTouchPosLaneObject(transform.position);
					transform.parent = laneBlock.transform;
					transform.position = TargetPos;
					laneBlock.GetComponent<LaneBlock>().SetHold(gameObject, true);
					LaneManager.I.ReSetColumnMonkuris(Mathf.RoundToInt(transform.position.x), false);
				}
			}
			else { TargetPos += new Vector3(0, -1, 0); /* 一段目標を下げる */}
		}

		// 連続して落ちた時上手く行くように下のブロックに設置されていたら行こうえで止まる
		if ((LaneManager.I.IsHoldLane(TargetPos)) && !IsStop)
		{
			Vector3 setPos = TargetPos + new Vector3(0, 1, 0);
			GameObject laneBlock = LaneManager.I.GetTouchPosLaneObject(setPos);
			if (laneBlock)
			{
				transform.parent = laneBlock.transform;
				laneBlock.GetComponent<LaneBlock>().SetHold(gameObject, true);
			}
		}

		// 親レーンがモンタマを保持していない時落ちる
		if (parentLane != null && parentLane.GetComponent<LaneBlock>().HoldMontama == null) { ReFallMontama(); }
	}

	/// <summary>
	/// 止まっているモンタマを再落下
	/// </summary>
	public void ReFallMontama()
	{
		transform.position = parentLane.transform.position;
		TargetPos = parentLane.transform.position + new Vector3(0, -1, 0);
		parentLane = null;
		transform.parent = null;
		parentLane = null;
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
	public bool IsStop { get { return parentLane != null; } }

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
