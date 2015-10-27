using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaneManager : SingletonMonoBehavior<LaneManager> {

	/// <summary>
	/// パズル部分の横レーン数
	/// </summary>
	public static int LANEMAINWIDTH = 7;

	/// <summary>
	/// パズル部分の縦レーン数
	/// </summary>
	public static int LANEMAINHEIGHT = 9;

	/// <summary>
	/// レーンの横数
	/// </summary>
	public static int LANEFRAMEWIDTH = LANEMAINWIDTH + 2;

	/// <summary>
	/// レーンの立て数
	/// </summary>
	public static int LANEFRAMEHEIGHT = LANEMAINHEIGHT + 2;

	/// <summary>
	/// レーンブロック配列
	/// </summary>
	private GameObject[,] laneMatrix = new GameObject[LANEFRAMEHEIGHT, LANEFRAMEWIDTH];

	/// <summary>
	/// 移動中のレーンブロック配列
	/// </summary>
	private List<GameObject> movingBlocks = new List<GameObject>();

	/// <summary>
	/// 左上のブロックのオフセット位置
	/// </summary>
	private Vector3 blockOffset = new Vector3(0.0f, 0.0f, 0.0f);

	/// <summary>
	/// レーンブロックの中心間の距離
	/// </summary>
	private Vector3 blockDist = new Vector3(1.00f, -1.00f, 0);

	/// <summary>
	/// レーンブロックオブジェクト
	/// </summary>
	public GameObject laneBlock;

	/// <summary>
	/// タッチされた行
	/// </summary>
	private int moveRow;

	/// <summary>
	/// タッチされた列
	/// </summary>
	private int moveColumn;

	/// <summary>
	/// スキルポイント運んでくれるやつ
	/// </summary>
	public GameObject SpCarrier;

	/// <summary>
	/// 前フレームのHP
	/// </summary>
	private float oldHP = 0.0f;

	/// <summary>
	/// 現フレームのHP
	/// </summary>
	private float nowHP = 0.0f;

	/// <summary>
	/// 最後にフリックされた時のフリック方向
	/// </summary>
	private Direction lastDirection;

	/// <summary>
	/// 移動不可能なレーン番号
	/// </summary>
	private List<int> noMoveColumns;

	private bool isSlideMoveLane;

	private Vector3 beginTouchPos;

	//  ↓ Author kazuki ito
	private GameObject Bt;

	private AndroidBlueToothAdapter BtAdapter;
	//  ↑ Author kabuki ito
	 

	// Use this for initialization
	void Start () {
		moveRow = -1;
		moveColumn = -1;
		noMoveColumns = new List<int>();
		for (int i = 0; i < LANEFRAMEHEIGHT; i++)
		{
			for (int j = 0; j < LANEFRAMEWIDTH ; j++)
			{
				Vector3 pos = blockOffset + new Vector3(blockDist.x * j, blockDist.y * i, blockOffset.z);
				GameObject block = Instantiate(laneBlock, pos, transform.rotation) as GameObject;
				block.GetComponent<LaneBlock>().SetIndex(i, j);
				block.transform.parent = transform;
				if (i == LANEMAINHEIGHT) { block.GetComponent<LaneBlock>().IsLock = true; }
				laneMatrix[i, j] = block;
			}
		}

		// モンクリを下段にセットする
		InitialMonkuriSet();

#if UNITY_ANDROID && !UNITY_EDITOR
		//  ↓ Author kazuki ito
		Bt = GameObject.FindGameObjectWithTag("BlueTooth");
		BtAdapter = Bt.GetComponent<AndroidBlueToothAdapter> ();
		//  ↑ Author kabuki ito
#endif

	}
	
	// Update is called once per frame
	void Update () {
		// ゲーム終了の判定
		if (nowHP > 1.0f)
		{
			MainManager.I.GameOver(false);
			GameEnder.I.IsGameEnd = true;
		}

		// 入力された瞬間に呼ばれる
		if (CrossInput.I.IsDown()) { EventInputIsDown(); }

		// 入力されている間に呼ばれる
		if (CrossInput.I.IsMove()) { EventInputIsMove(); }

		// 入力が終了した瞬間に呼ばれる
		if (CrossInput.I.IsUp()) { EventInputIsUp(); }

		// レーンマトリックのGameObjectの更新
		if (!IsMoveLane()) { UpdateLaneMatrixIndex(); }

		// レーン移動後に各モンタマが4つ繋がっているか確認
		if (!IsMoveLane() && !CrossInput.I.IsNowDown)
		{
			foreach (GameObject blockObj in movingBlocks)
			{
				LaneBlock block = blockObj.GetComponent<LaneBlock>();
				if (block.HoldMontama == null) { continue; }
				MontamaChainCheck(block.HoldMontama.GetComponent<PuzzleMontama>().serialID, block.Row, block.Column);
			}

			moveRow = -1;
			moveColumn = -1;
			movingBlocks.Clear();
		}

		UpdateLaneMatrix();
		UpdateHP ();
		UpdateBothEndMontama();
	}

	/// <summary>
	/// 入力がDownした時に呼ぶメソッド
	/// </summary>
	private void EventInputIsDown()
	{
		isSlideMoveLane = false;
		beginTouchPos = Camera.main.ScreenToWorldPoint(CrossInput.I.ScreenPosition);
		GameObject obj = GetTouchPosLaneObject(beginTouchPos);
		if (obj == null) { return; }
		if (noMoveColumns.Contains(obj.GetComponent<LaneBlock>().Column)) { return; }
		moveRow = obj.GetComponent<LaneBlock>().Row;
		moveColumn = obj.GetComponent<LaneBlock>().Column;
		DrawLaneBlockManager.I.DrawTapLane();
	}

	/// <summary>
	/// 入力が移動中に呼ぶメソッド
	/// </summary>
	private void EventInputIsMove()
	{
		Vector3 nowTouchPos = Camera.main.ScreenToWorldPoint(CrossInput.I.ScreenPosition);
		if ((nowTouchPos.x - beginTouchPos.x) > 1 && !IsMoveLane())
		{
			AudioManager.I.PlayAudio("se_frick");
			isSlideMoveLane = true;
			UpdateLaneMatrix();
			UpdateLaneMatrixIndex();
			UpdateBothEndMontama();
			beginTouchPos += new Vector3(1, 0, 0);
			MoveLaneBlocks(Direction.RIGHT);
		}
		if ((nowTouchPos.x - beginTouchPos.x) < -1 && !IsMoveLane())
		{
			AudioManager.I.PlayAudio("se_frick");
			isSlideMoveLane = true;
			UpdateLaneMatrix();
			UpdateLaneMatrixIndex();
			UpdateBothEndMontama();
			beginTouchPos += new Vector3(-1, 0, 0);
			MoveLaneBlocks(Direction.LEFT);
		}
	}

	/// <summary>
	/// 入力がUpした時に呼ぶメソッド
	/// </summary>
	private void EventInputIsUp()
	{
		if(!isSlideMoveLane)
		{
			lastDirection = CrossInput.I.GetFrickDirection();
			if (lastDirection == Direction.DOWN)
			{
				AudioManager.I.PlayAudio("se_frick");
				RapidFallMontama();
			}
		}
		DrawLaneBlockManager.I.DrawClear();
	}

	/// <summary>
	/// 両端のモンタマの更新処理
	/// </summary>
	public void UpdateBothEndMontama()
	{
		// 左右の端のレーンを更新する
		for (int i = 0; i < LANEFRAMEHEIGHT; i++)
		{
			LaneBlock tmpLaneBlock = laneMatrix[i, 0].GetComponent<LaneBlock>();
			LaneBlock setLaneBlock = laneMatrix[i, LANEMAINWIDTH].GetComponent<LaneBlock>();
			Destroy(tmpLaneBlock.HoldMontama);
			if (setLaneBlock.HoldMontama != null)
			{
				tmpLaneBlock.HoldMontama = Instantiate(setLaneBlock.HoldMontama, tmpLaneBlock.transform.position, tmpLaneBlock.transform.rotation) as GameObject;
				tmpLaneBlock.HoldMontama.transform.parent = setLaneBlock.transform;
				tmpLaneBlock.HoldMontama.GetComponent<PuzzleMontama>().ParentLane = tmpLaneBlock.gameObject;
				PuzzleMontamaManager.I.PuzzleMontamas.Add(tmpLaneBlock.HoldMontama.GetComponent<PuzzleMontama>());
			}

			tmpLaneBlock = laneMatrix[i, LANEFRAMEWIDTH - 1].GetComponent<LaneBlock>();
			setLaneBlock = laneMatrix[i, 1].GetComponent<LaneBlock>();
			Destroy(tmpLaneBlock.HoldMontama);
			if (setLaneBlock.HoldMontama != null)
			{
				tmpLaneBlock.HoldMontama = Instantiate(setLaneBlock.HoldMontama, tmpLaneBlock.transform.position, tmpLaneBlock.transform.rotation) as GameObject;
				tmpLaneBlock.HoldMontama.transform.parent = setLaneBlock.transform;
				tmpLaneBlock.HoldMontama.GetComponent<PuzzleMontama>().ParentLane = tmpLaneBlock.gameObject;
				PuzzleMontamaManager.I.PuzzleMontamas.Add(tmpLaneBlock.HoldMontama.GetComponent<PuzzleMontama>());
				PuzzleMontamaManager.I.DeleteNullObject();
			}
		}
	}

	/// <summary>
	/// 下動作が行われた時に一瞬で落下させるメソッド(フリックしたレーンのだけ)
	/// </summary>
	private void RapidFallMontama()
	{
		SortedDictionary<float, PuzzleMontama> rapidTargetMontama = new SortedDictionary<float, PuzzleMontama>();
		var puzzleMontamas = PuzzleMontamaManager.I.PuzzleMontamas;
		foreach (var montama in puzzleMontamas)
		{
			if (montama == null) { continue; }
			if (Mathf.RoundToInt(montama.transform.position.x) == moveRow)
			{
				rapidTargetMontama.Add(montama.transform.position.y, montama);
			}
		}
		// 高さが低いものからレーンにはめていく
		float offset = 0.0f;
		foreach (var montama in rapidTargetMontama.Values)
		{
			GameObject lane = GetTouchPosLaneObject(new Vector3(moveRow, -LANEMAINHEIGHT + offset));
			lane.GetComponent<LaneBlock>().SetHold(montama.gameObject, false);
			offset++;
		}

		foreach (var montama in rapidTargetMontama.Values)
		{
			MontamaChainCheck(montama.serialID, montama.ParentLane.GetComponent<LaneBlock>().Row, montama.ParentLane.GetComponent<LaneBlock>().Column);
		}
	}

	/// <summary>
	/// 移動可能なレーンブロック取得
	/// </summary>
	/// <param name="column"></param>
	/// <param name="row"></param>
	/// <returns></returns>
	public List<GameObject> GetMoveLaneBlocks(int column, int row)
	{
		List<GameObject> result = new List<GameObject>();
		for (int i = 0; i < LANEFRAMEHEIGHT; i++)
		{
			result.Add(laneMatrix[i, row]);
		}
		for (int i = 0; i < LANEFRAMEWIDTH; i++)
		{
			if (i == row) { continue; }
			result.Add(laneMatrix[column, i]);
		}
		return result;
	}

	/// <summary>
	/// レーンブロックをDirectionの方向へ1つ移動する
	/// </summary>
	/// <param name="direction"></param>
	private void MoveLaneBlocks(Direction direction)
	{
		if (LaneManager.I.MoveRow <= 0 || LaneManager.LANEMAINWIDTH < LaneManager.I.MoveRow) { return; }
		if (LaneManager.I.MoveColumn <= 0 || LaneManager.LANEMAINHEIGHT < LaneManager.I.MoveColumn) { return; }

		// フリックが行われていない時は何もしない
		if (direction == Direction.NOMOVE) { return; }

		// フリック方向でレーンブロックを移動
		if (direction == Direction.LEFT || direction == Direction.RIGHT)
		{
			for (int i = 0; i < LANEFRAMEWIDTH; i++)
			{
				laneMatrix[moveColumn, i].GetComponent<LaneBlock>().MoveLane(direction);
				movingBlocks.Add(laneMatrix[moveColumn, i]);
			}
		}
		else if (direction == Direction.DOWN)
		{
			for(int i = 0; i < LANEFRAMEHEIGHT;i++)
			{
				laneMatrix[i, moveRow].GetComponent<LaneBlock>().MoveLane(direction);
				movingBlocks.Add(laneMatrix[i, moveRow]);
			}
		}
	}

	/// <summary>
	/// 移動中のブロックの移動が終わったか
	/// </summary>
	/// <returns></returns>
	private bool IsMoveEndLane()
	{
		if (movingBlocks.Count == 0) { return false; }
		foreach(GameObject block in movingBlocks)
		{
			if (block.GetComponent<LaneBlock>().IsMove) { return false; }
		}
		return true;
	}

	/// <summary>
	/// レーンマネージャ内のブロック情報修正
	/// </summary>
	public void UpdateLaneMatrix()
	{
		// 最下層は必ずロック状態にする
		for (int i = 0; i < LANEFRAMEWIDTH; i++)
		{
			laneMatrix[LANEFRAMEHEIGHT - 1, i].GetComponent<LaneBlock>().IsLock = true;
		}

		for (int i = 0; i < LANEFRAMEWIDTH; i++)
		{
			int checkHeight = 0;
			for (int j = LANEMAINHEIGHT; j >= 0; j--)
			{
				LaneBlock block = laneMatrix[j, i].GetComponent<LaneBlock>();
				if (j < checkHeight)
				{
					block.IsLock = false;
					// レーンがモンタマを保持している場合落として上げる必要があり
					if (block.HoldMontama != null) { block.ReFallMontama(); }
					continue;
				}

				if (!block.IsHold && j == LANEMAINHEIGHT)
				{
					if (j == LANEMAINHEIGHT)
					{
						checkHeight = j;
						block.IsLock = true;
					}
				}
				if (block.IsHold)
				{
					block.IsLock = true;
				}
				else if (!block.IsHold)
				{
					checkHeight = j;
					block.IsLock = true;
				}
			}
		}
	}

	/// <summary>
	/// レーンマトリックスの配列番号を更新
	/// </summary>
	public void UpdateLaneMatrixIndex()
	{
		GameObject[,] tmpLaneMatrix = new GameObject[LANEFRAMEHEIGHT, LANEFRAMEWIDTH];
		for (int i = 0; i < LANEFRAMEHEIGHT; i++)
		{
			for (int j = 0; j < LANEFRAMEWIDTH; j++)
			{
				int row = laneMatrix[i, j].GetComponent<LaneBlock>().Row;
				int column = laneMatrix[i, j].GetComponent<LaneBlock>().Column;
				tmpLaneMatrix[column, row] = laneMatrix[i, j];
			}
		}
		laneMatrix = tmpLaneMatrix;
	}

	/// <summary>
	/// レーンが移動中かどうか
	/// </summary>
	/// <returns></returns>
	public bool IsMoveLane()
	{
		for (int i = 0; i < LANEFRAMEHEIGHT; i++)
		{
			for (int j = 0; j < LANEFRAMEWIDTH; j++)
			{
				if (laneMatrix[i, j].GetComponent<LaneBlock>().IsMove) { return true; }
			}
		}
		return false;
	}
	
	/// <summary>
	/// タッチ座標にあるレーンオブジェクト取得
	/// </summary>
	/// <param name="pos"></param>
	/// <returns></returns>
	public GameObject GetTouchPosLaneObject(Vector3 pos)
	{
		Collider2D[] collider2Ds = Physics2D.OverlapPointAll(pos);
		{
			if (collider2Ds.Length > 0)
			{
				foreach (Collider2D col in collider2Ds)
				{
					if (col.gameObject.tag == "Lane") { return col.gameObject; }
				}
			}
		}
		return null;
	}

	/// <summary>
	/// 座標にあるレーンがモンタマを保有しているか
	/// </summary>
	/// <param name="pos"></param>
	/// <returns></returns>
	public bool IsHoldLane(Vector3 pos)
	{
		GameObject laneBlock = GetTouchPosLaneObject(pos);
		if (laneBlock == null) { return false; }
		if (laneBlock.GetComponent<LaneBlock>().IsHold) { return true; }
		else { return false; }
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="pos"></param>
	/// <returns></returns>
	public bool IsMoveLockLane(Vector3 pos)
	{
		GameObject laneBlock = GetTouchPosLaneObject(pos);
		if (laneBlock == null) { return false; }
		if (laneBlock.GetComponent<LaneBlock>().Column == moveColumn) { return true; }
		else { return false; }
	}

	/// <summary>
	/// 場所にあるレーンがモンタマを設置出来るかどうか
	/// </summary>
	/// <param name="pos"></param>
	/// <returns></returns>
	public bool IsLockLane(Vector3 pos)
	{
		GameObject laneBlock = GetTouchPosLaneObject(pos);
		if (laneBlock == null) { return false; }
		if (laneBlock.GetComponent<LaneBlock>().IsLock) { return true; }
		else { return false; }
	}

	/// <summary>
	/// 連鎖しているモンタマを消す
	/// </summary>
	/// <param name="serialId"></param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	public void MontamaChainCheck(int serialId, int x, int y)
	{
		/*
		 * 探索用に二次元配列を作る
		 * 未探索は0
		 * 探索済みのブロック(同じブロック:1,異なるブロック:2)
		 */
		int[,] cells_check = new int[LANEFRAMEHEIGHT, LANEFRAMEWIDTH];

		MontamaChainCheckRecursive(serialId, x, y, cells_check);

		// 隣接するブロック数を数える
		int count = 0;
		for (int i = 0; i < LANEFRAMEHEIGHT; i++)
		{
			for (int j = 0; j < LANEFRAMEWIDTH; j++)
			{
				if (cells_check[i, j] == 1) { count++; }
			}
		}

		// 隣接数:4以上の時に消す
		if (count < 5) { return; }
		ComboManager.I.AddCombo(1);
		for (int i = 1; i <= LANEMAINHEIGHT; i++)
		{
			for (int j = 1; j <= LANEMAINWIDTH; j++)
			{
				if (cells_check[i, j] == 1)
				{
					GameObject puzzleMontama = laneMatrix[i, j].GetComponent<LaneBlock>().HoldMontama;
					// 対象のモンタマのスキルポイントを貯める
					GameObject carrier = Instantiate(SpCarrier, puzzleMontama.transform.position, puzzleMontama.transform.rotation) as GameObject;
					GameObject skillMontama = SkillMontamaManager.I.GetSkillMontama(puzzleMontama.GetComponent<PuzzleMontama>().serialID);
					carrier.GetComponent<SkillCarrier>().CarrySkillPt(skillMontama.GetComponent<SkillMontama>().serialId, 1, puzzleMontama.transform.position, skillMontama.transform.position);

					puzzleMontama.GetComponent<PuzzleMontama>().DestroyMonkuri();
					laneMatrix[i, j].GetComponent<LaneBlock>().HoldMontama = null;
					if (i != LANEMAINHEIGHT)
					{
						laneMatrix[i, j].GetComponent<LaneBlock>().IsLock = false;
					}
				}
			}
		}
		// 消えたので消滅SEを鳴らす
		AudioManager.I.PlayAudio("se_restMonkuri");
	}

	/// <summary>
	/// モンタマの連鎖判定
	/// </summary>
	/// <param name="serialId"></param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="cells_check"></param>
	public void MontamaChainCheckRecursive(int serialId, int x,int y , int[,] cells_check)
	{
		if (x <= 0 || LANEMAINWIDTH < x) { return; }
		if (y <= 0 || LANEMAINHEIGHT < y) { return; }
		if (cells_check[y, x] != 0) { return; }
		GameObject puzzleMontama = laneMatrix[y, x].GetComponent<LaneBlock>().HoldMontama;
		if (puzzleMontama == null
			|| puzzleMontama.GetComponent<PuzzleMontama>().serialID != serialId 
			|| puzzleMontama.GetComponent<PuzzleMontama>().IsBited
			|| puzzleMontama.GetComponent<PuzzleMontama>().IsMaguma)
		{
			cells_check[y, x] = 2;
			return;
		}
		// 一致
		cells_check[y, x] = 1;
		MontamaChainCheckRecursive(serialId, x + 1, y, cells_check);
		MontamaChainCheckRecursive(serialId, x - 1, y, cells_check);
		MontamaChainCheckRecursive(serialId, x, y + 1, cells_check);
		MontamaChainCheckRecursive(serialId, x, y - 1, cells_check);

		return;
	}

	/// <summary>
	/// レーンの下三段に予めモンクリセットしとくメソッド
	/// </summary>
	public void InitialMonkuriSet()
	{
		int prevIndex = -1;
		int index = -1;
		for (int i = LANEMAINHEIGHT - 3; i <= LANEMAINHEIGHT; i++)
		{
			for (int j = 1; j <= LANEMAINWIDTH; j++)
			{
				while (prevIndex == index) { index = Random.Range(0, PartyManager.MAXPARTYCOUNT); }
				GameObject puzzleMonkuri = PartyManager.I.GetPuzzleMontama(index);
				GameObject laneObj = laneMatrix[i, j];
				GameObject holdMonkuri = Instantiate(puzzleMonkuri, laneObj.transform.position, laneObj.transform.rotation) as GameObject;
				holdMonkuri.GetComponent<PuzzleMontama>().aryIndex = PuzzleMontamaManager.I.PuzzleMontamas.Count;
				PuzzleMontamaManager.I.DeleteNullObject();
				PuzzleMontamaManager.I.PuzzleMontamas.Add(holdMonkuri.GetComponent<PuzzleMontama>());
				laneObj.GetComponent<LaneBlock>().SetHold(holdMonkuri, false);
				prevIndex = index;
			}
		}
	}

	/// <summary>
	/// HP情報の更新
	/// </summary>
	///  ↓ Author kazuki ito
	void UpdateHP()
	{
		int maxHeight = 10;
		for (int i = 1; i < LANEFRAMEWIDTH - 1; i++)
		{
			for (int j = 0; j < LANEFRAMEHEIGHT; j++)
			{
				foreach (Transform n in laneMatrix[j, i].transform)
				{
					if (n.gameObject.CompareTag("Montama"))
					{
						int height = laneMatrix[j, i].GetComponent<LaneBlock>().Column;
						if (height < maxHeight + 1)
						{
							maxHeight = height - 1;
							break;
						}
					}
					else
					{
						break;
					}
				}
			}
		}

		nowHP = (LANEMAINHEIGHT - maxHeight) / (float)LANEMAINHEIGHT;
		Debug.Log(NowHP);
		if (oldHP != nowHP) {
			oldHP = nowHP;
			if (BtAdapter != null) { BtAdapter.SendFloatData(nowHP, Tag.HP); }
		}
		return;
	}
	//  ↑ Author kabuki ito

	/// <summary>
	/// クラス停止時に呼ばれるメソッド
	/// </summary>
	public void GameEnd() { enabled = false; }

	/// <summary>
	/// このクラスを開始させるメソッド
	/// </summary>
	public void GameStart(){ enabled = true; }

	/// <summary>
	/// レーン行列
	/// </summary>
	public GameObject[,] LaneMatrix { get { return laneMatrix; } }

	/// <summary>
	/// 移動不可能なレーン番号
	/// </summary>
	public List<int> NoMoveColumns { get { return noMoveColumns; } }

	/// <summary>
	/// 最大行数
	/// </summary>
	public int MoveRow { get { return moveRow; } }

	/// <summary>
	/// 最大列数
	/// </summary>
	public int MoveColumn { get { return moveColumn; } }

	/// <summary>
	/// 前フレームのHP
	/// </summary>
	public float OldHp { get { return oldHP; } }

	/// <summary>
	/// 現フレームのHP
	/// </summary>
	public float NowHP { get { return nowHP; } }

	/// <summary>
	/// 本当にゲームが終わっているか
	/// </summary>
	/// <returns></returns>
	public bool IsGameEnd
	{
		get
		{
			for (int i = 0; i < LANEMAINWIDTH; i++)
			{
				if (laneMatrix[0, i].GetComponent<LaneBlock>().IsHold) { return true; }
			}
			return false;
		}
	}
}
