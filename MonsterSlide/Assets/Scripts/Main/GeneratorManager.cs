using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneratorManager : SingletonMonoBehavior<GeneratorManager> {

	/// <summary>
	/// 生成するモンタマジェネレーター
	/// </summary>
	public GameObject montamaGenerator;

	/// <summary>
	/// ジェネレーター
	/// </summary>
	private List<GameObject> generators;

	/// <summary>
	/// スフィアの生成間隔
	/// </summary>
	public float interval;
	
	/// <summary>
	/// 本来の生成間隔
	/// </summary>
	private float orgInterval;

	/// <summary>
	/// スフィア生成までの残り時間
	/// </summary>
	private float geneTimeLeft;

	/// <summary>
	/// 前フレームのIndex
	/// </summary>
	private int prevIndex;

	/// <summary>
	/// シーン開始時の時間
	/// </summary>
	private float sceneStartTime;

	/// <summary>
	/// しきい値
	/// </summary>
	private float threshold;

	// Use this for initialization
	void Start () {
		geneTimeLeft = interval;
		orgInterval = interval;
		sceneStartTime = Time.time;
		generators = new List<GameObject>();
		// 一定間隔でジェネレータを設置
		for (int x = 1; x <= 7; x++)
		{
			GameObject generator = Instantiate(montamaGenerator, new Vector3(x, 1, 0), transform.rotation) as GameObject;
			generator.GetComponent<MontamaGenerator>().SetMontama(PartyManager.I.GetRandomPuzzleMonkuri(), PartyManager.I.GetRandomPuzzleMonkuri());
			generators.Add(generator);
		}
	}
	
	// Update is called once per frame
	void Update () {
		geneTimeLeft -= Time.deltaTime;
		if (geneTimeLeft < 0)
		{
			int geneCt = 1;
			float rnd = Random.Range(0.0f, 1.0f);
			if (rnd < threshold) { geneCt++; }
			for (int i = 0; i < geneCt; i++)
			{
				int index = prevIndex;
				while (prevIndex == index) { index = Random.Range(0, 7); }
				generators[index].GetComponent<MontamaGenerator>().SetMontama(PartyManager.I.GetRandomPuzzleMonkuri());
				prevIndex = index;
			}
			geneTimeLeft = interval * GetIntervalRatio();
		}
	}

	public void SetMontama(int row, GameObject montama) { generators[row].GetComponent<MontamaGenerator>().SetMontama(montama); }

	public float GetIntervalRatio()
	{
		float mag = 30.0f;
		if (ElapsedTime < 1 * mag) { threshold = 0.2f; return 1.0f; }
		else if (ElapsedTime < 2 * mag) { threshold = 0.3f; return 0.9f; }
		else if (ElapsedTime < 3 * mag) { threshold = 0.4f; return 0.8f; }
		else if (ElapsedTime < 4 * mag) { threshold = 0.5f; return 0.7f; }
		else if (ElapsedTime < 5 * mag) { threshold = 0.6f; return 0.6f; }
		else { threshold = 0.7f; return 0.5f; }
	}

	/// <summary>
	/// ゲーム開始からの経過時間
	/// </summary>
	public float ElapsedTime { get { return Time.time - sceneStartTime; } }

	/// <summary>
	/// ゲーム開始時に呼ぶメソッド
	/// </summary>
	public void GameStart() { enabled = true; }

	/// <summary>
	/// ゲーム終了時に呼ぶメソッド
	/// </summary>
	public void GameEnd() { enabled = false; }

	/// <summary>
	/// 生成までの残り時間
	/// </summary>
	public float GeneTimeLimit { get { return geneTimeLeft; } set { geneTimeLeft = value; } }

	public float Interval { get { return interval; } set { interval = value; } }

	public float OrgInterval { get { return orgInterval; } }

}
