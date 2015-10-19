using UnityEngine;
using System.Collections;

/// <summary>
/// Unity用Singleton基底クラス
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMonoBehavior<T> : MonoBehaviour where T : MonoBehaviour
{
	/// <summary>
	/// シングルトンStatic変数
	/// </summary>
	private static T singleton;

	public static T Instance
	{
		get
		{
			if (singleton == null) { singleton = (T)FindObjectOfType(typeof(T)); }
			return singleton;
		}
	}
}
