using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLaneBlockManager : SingletonMonoBehavior<DrawLaneBlockManager> {

	private GameObject[,] drawLaneMatrix = new GameObject[LaneManager.LANEMAINHEIGHT, LaneManager.LANEMAINWIDTH];

	/// <summary>
	/// 描画用レーンブロック
	/// </summary>
	public GameObject drawLaneBlock;

	/// <summary>
	/// 移動不可能なレーン番号
	/// </summary>
	private List<int> noMoveColumns;

	/// <summary>
	/// タッチされていないレーンの画像
	/// </summary>
	public Sprite noTouchLaneSprite;

	/// <summary>
	/// タッチされていて移動するレーンの画像
	/// </summary>
	public Sprite touchLaneSprite;

	/// <summary>
	/// 横向きのスライド画像
	/// </summary>
	public GameObject yokoSlideLine;

	/// <summary>
	/// 縦向きのスライド画像
	/// </summary>
	public GameObject tateSlideLine;

	// Use this for initialization
	protected override void Awake () {
		base.Awake();
		noMoveColumns = new List<int>();
		for(int i = 1; i <= LaneManager.LANEMAINHEIGHT ;i++)
		{
			for(int j = 1; j <= LaneManager.LANEMAINWIDTH;j++)
			{
				GameObject gameObj = Instantiate(drawLaneBlock, new Vector3(j, -i, 0), transform.rotation) as GameObject;
				gameObj.transform.parent = transform;
				gameObj.GetComponent<SpriteRenderer>().sprite = noTouchLaneSprite;
				drawLaneMatrix[i - 1, j - 1] = gameObj;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DrawTapLane()
	{
		if (LaneManager.I.MoveRow <= 0 || LaneManager.LANEMAINWIDTH < LaneManager.I.MoveRow) { return; }
		if (LaneManager.I.MoveColumn <= 0 || LaneManager.LANEMAINHEIGHT < LaneManager.I.MoveColumn) { return; }
		for (int i = 0; i < LaneManager.LANEMAINWIDTH; i++)
		{
			drawLaneMatrix[LaneManager.I.MoveColumn - 1, i].GetComponent<SpriteRenderer>().sprite = touchLaneSprite;
		}
		yokoSlideLine.transform.position = new Vector3(4f, -LaneManager.I.MoveColumn, 0.0f);
		yokoSlideLine.GetComponent<SpriteRenderer>().enabled = true;
		tateSlideLine.transform.position = new Vector3(LaneManager.I.MoveRow, -5f, 0.0f);
		tateSlideLine.GetComponent<SpriteRenderer>().enabled = true;
	}

	public void DrawNoMoveLane()
	{
		for (int i = 0; i < LaneManager.LANEMAINHEIGHT; i++)
		{
			if (!noMoveColumns.Contains(i)) { continue; }
			for (int j = 0; j < LaneManager.LANEMAINWIDTH; j++)
			{
				drawLaneMatrix[i, j].GetComponent<SpriteRenderer>().sprite = touchLaneSprite;
			}
		}
	}

	public void DrawClear()
	{
		for (int i = 0; i < LaneManager.LANEMAINHEIGHT; i++)
		{
			for (int j = 0; j < LaneManager.LANEMAINWIDTH; j++)
			{
				drawLaneMatrix[i, j].GetComponent<SpriteRenderer>().sprite = noTouchLaneSprite;
			}
		}
		yokoSlideLine.GetComponent<SpriteRenderer>().enabled = false;
		tateSlideLine.GetComponent<SpriteRenderer>().enabled = false;
	}

	public List<int> NoMoveColumns { get { return noMoveColumns; } }
}
