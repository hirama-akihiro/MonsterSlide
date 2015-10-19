using UnityEngine;
using System.Collections;

/// <summary>
/// ゆくゆくはTouchとMouseを呼ぶクラスにする
/// </summary>
public class CrossInput : SingletonMonoBehavior<CrossInput>
{
	#region Private State Value
	private bool isPrevDown;
	private bool isNowDown;
	private Vector2 startPosition = Vector2.zero;
	private Vector2 lastPosition = Vector2.zero;
	#endregion

	// Use this for initialization
	void Start () {
		isPrevDown = false;
		isNowDown = false;
	}
	
	// Update is called once per frame
	void Update () {
		// タッチ状態の更新
		isPrevDown = isNowDown;
		if (IsTouchPlatform()) { isNowDown = Input.touchCount == 1; }
		else { isNowDown = Input.GetMouseButton(0); }

		// 座標の更新
		if (IsDown()) { startPosition = lastPosition = ScreenPosition; }
		if (IsMove()) { lastPosition = ScreenPosition; }
	}

	/// <summary>
	/// タッチパネル向けのプラっとフォームかどうか取得
	/// </summary>
	/// <returns></returns>
	private bool IsTouchPlatform()
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) { return true; }
		else { return false; }
	}

	public Direction GetFrickDirection()
	{
		float degree = Mathf.Atan2(lastPosition.y - startPosition.y, lastPosition.x - startPosition.x) * Mathf.Rad2Deg;
		if (degree < 0) { degree += 360; }
		if (Vector2.Distance(lastPosition, startPosition) < 5.0f) { return Direction.NOMOVE; }

		if (degree <= 45.0f || 315.0f <= degree) { return Direction.RIGHT; }
		if (125.0f < degree && degree <= 225.0f) { return Direction.LEFT; }
		if (225.0f < degree && degree <= 315.0f) { return Direction.DOWN; }
		return Direction.NOMOVE;
	}

	/// <summary>
	/// スクリーンの座標
	/// 今はタッチだけだがクリックと両立したい
	/// </summary>
	public Vector2 ScreenPosition
	{
		get
		{
			if (IsTouchPlatform()) { return Input.GetTouch(0).position; }
			else { return Input.mousePosition; }
		}
	}

	/// <summary>
	/// スクリーン上での移動座標
	/// 
	/// </summary>
	public Vector2 DeltaScreenPosition
	{
		get
		{
			return ScreenPosition - lastPosition;
		}
	}

	public Vector3 DeltaWorldPosition
	{
		get
		{
			return Camera.main.ScreenToWorldPoint(ScreenPosition) - Camera.main.ScreenToWorldPoint(lastPosition);
		}
	}

	/// <summary>
	/// タッチ or クリックした瞬間
	/// </summary>
	/// <returns></returns>
	public bool IsDown() { return !isPrevDown && isNowDown; }

	/// <summary>
	/// 今タッチ or クリックしているか
	/// </summary>
	/// <returns></returns>
	public bool IsMove() { return isPrevDown && isNowDown; }

	/// <summary>
	/// タッチ or クリックが終了した瞬間
	/// </summary>
	/// <returns></returns>
	public bool IsUp() { return isPrevDown && !isNowDown; }

	public bool IsNowDown { get { return isNowDown; } }
}
