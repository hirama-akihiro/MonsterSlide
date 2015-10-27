using UnityEngine;
using System.Collections;

public class LaneBlock : MonoBehaviour {

	/// <summary>
	/// 現在の座標
	/// </summary>
	private Vector3 nowPos;

	/// <summary>
	/// 移動後の座標
	/// </summary>
	private Vector3 nextPos;

	/// <summary>
	/// レーンブロックの移動方向
	/// </summary>
	private Direction moveDirection;

	/// <summary>
	/// 移動開始時間
	/// </summary>
	private float startTime;

	/// <summary>
	/// 1ブロック間の移動時間
	/// </summary>
	private float moveTime;

	// Use this for initialization
	void Start () {
		nowPos = nextPos = transform.position;
		moveTime = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (IsHold) { HoldMontama.transform.position = transform.position; }
		MoveLaneBlock();
	}

	public void SetLaneMatrix(GameObject[,] laneMatrix)
	{

	}

	public void SetIndex(int column, int row)
	{
		this.Column = column;
		this.Row = row;
	}

	public void SetHold(GameObject puzzleMontama, bool isChainCheck)
	{
		IsLock = false;
		HoldMontama = puzzleMontama;
		HoldMontama.transform.position = transform.position;
		puzzleMontama.GetComponent<PuzzleMontama>().ParentLane = this.gameObject;
		puzzleMontama.GetComponent<PuzzleMontama>().transform.parent = transform;

		// 最上段に設置された時，ゲームクリア
		//if (Column == 0 && 1 <= Row && Row <= LaneManager.LANEMAINWIDTH)
		//{
		//	MainManager.I.IsGameOver = true;
		//	GameEnder.I.IsGameEnd = true;
		//}

		// モンタマをセット後，連鎖チェック
		if (isChainCheck) { LaneManager.I.MontamaChainCheck(HoldMontama.GetComponent<PuzzleMontama>().serialID, Row, Column); }
		LaneManager.I.UpdateLaneMatrix();
	}

	public void MoveLane(Direction direction)
	{
		if (direction == Direction.NOMOVE) { return; }
		moveDirection = direction;
		startTime = Time.timeSinceLevelLoad;
		if (direction == Direction.RIGHT) { nextPos += Vector3.right; }
		if (direction == Direction.LEFT) { nextPos += Vector3.left; }
		IsMove = true;
	}

	/// <summary>
	/// MoveLaneが呼ばれると実行されるレーンブロックの移動メソッド
	/// </summary>
	public void MoveLaneBlock()
	{
		if (nowPos != nextPos)
		{
			float diff = Time.timeSinceLevelLoad - startTime;
			if (diff > moveTime)
			{
				switch (moveDirection)
				{
					case Direction.RIGHT:
						Row++;
						if (Row == LaneManager.LANEFRAMEWIDTH)
						{
							Row = 0;
							nextPos.x = Row * 1.0f; // ブロック間の距離1.0f
						}
						break;
					case Direction.LEFT:
						Row--;
						if (Row == -1)
						{
							Row = LaneManager.LANEFRAMEWIDTH - 1;
							nextPos.x = Row * 1.0f; // ブロック間の距離1.0f
						}
						break;
					case Direction.DOWN:
						Column++;
						if (Column == LaneManager.LANEFRAMEHEIGHT)
						{
							Column = 0;
							nextPos.y = Column * 1.0f; // ブロック間の距離1.0f
						}
						// 最下段の時，生成場所に新たに追加する
						if (Column == LaneManager.LANEFRAMEHEIGHT - 1)
						{
							if (HoldMontama != null)
							{
								GeneratorManager.I.SetMontama(Row - 1, HoldMontama);
								Destroy(HoldMontama);
								HoldMontama = null;
							}
						}
						break;
				}
				nowPos = transform.position = nextPos;
				IsMove = false;
			}
			float rate = diff / moveTime;
			transform.position = Vector3.Lerp(nowPos, nextPos, rate);
		}
	}

	/// <summary>
	/// レーンに設置されているモンタマを再落下
	/// </summary>
	public void ReFallMontama()
	{
		HoldMontama.GetComponent<PuzzleMontama>().ReFallMontama();
		HoldMontama = null;
	}

	/// <summary>
	/// 行番号
	/// </summary>
	public int Row { get; set; }
	
	/// <summary>
	/// 列番号
	/// </summary>
	public int Column { get; set; }

	/// <summary>
	/// レーンブロックがモンタマを保持しているか
	/// </summary>
	public bool IsHold { get { return HoldMontama != null; } }

	/// <summary>
	/// ロックブロックかどうか
	/// </summary>
	public bool IsLock;// { get; set; }

	/// <summary>
	/// レーンが移動中かどうか
	/// </summary>
	public bool IsMove { get; set; }

	/// <summary>
	/// レーンブロックがホールドしているモンタマ
	/// </summary>
	public GameObject HoldMontama;// { get; set; }
}
