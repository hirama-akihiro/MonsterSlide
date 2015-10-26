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
	private static T instance;

	public static T Instance
	{
		get
		{
			if (instance == null) { instance = (T)FindObjectOfType(typeof(T)); }
			return instance;
		}
	}

	void OnDestroy()
	{
		if (instance == this) { instance = null; }
	}

	protected virtual void Awake()
	{
		CheckInstance();
	}

	protected bool CheckInstance()
	{
		if (this == Instance) { return this; }
		Destroy(this);
		return false;
	}

	static public bool IsValid() { return instance != null; }
}
