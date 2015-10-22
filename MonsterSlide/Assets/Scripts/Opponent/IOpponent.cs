using UnityEngine;
using System.Collections;

/// <summary>
/// 対戦相手のインターフェース
/// </summary>
public interface IOpponent {

	/// <summary>
	/// 対戦相手の現在のHP
	/// </summary>
	/// <returns></returns>
	float GetNowHp();
}
