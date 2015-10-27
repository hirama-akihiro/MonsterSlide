using UnityEngine;
using System.Collections;

/// <summary>
/// モンタマ生成用クラス
/// </summary>
public class MontamaGenerator : MonoBehaviour {

	/// <summary>
	/// スフィアの生成間隔(生成と同時に射出)
	/// </summary>
	public float minGeneInterval;

	/// <summary>
	/// スフィアの生成間隔
	/// </summary>
	public float maxGeneInterval;

	/// <summary>
	/// スフィア生成までの残り時間
	/// </summary>
	private float geneTimeLeft;

	/// <summary>
	/// モンタマ生成位置(下側)
	/// </summary>
	public GameObject genePos1;

	/// <summary>
	/// モンタマ生成位置(上側)
	/// </summary>
	public GameObject genePos2;

	private GameObject puzzleMontama1;
	private GameObject puzzleMontama2;
	private GameObject nextMonkuri;

	public float vibrationTime;
	private float inVibrationTime;
	private bool vibring;
	private int vibSpeed = 10;

	// Use this for initialization
	void Awake() {
		puzzleMontama1 = null;
		puzzleMontama2 = null;
		inVibrationTime = 0;
		vibring = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(vibring)
		{
			inVibrationTime -= Time.deltaTime;
			puzzleMontama1.transform.position =transform.position + new Vector3(Random.Range(-0.05f, 0.05f), 0, 0);
			if (inVibrationTime < 0)
			{
				// 下段にあるモンタマを射出
				puzzleMontama1.transform.position = new Vector3(transform.position.x, puzzleMontama1.transform.position.y, 0);
				puzzleMontama1.transform.position += new Vector3(0, -1, 0);
				puzzleMontama2.transform.position += new Vector3(0, -1, 0);
				puzzleMontama1.GetComponent<PuzzleMontama>().TargetPos = puzzleMontama1.transform.position + new Vector3(0, -1, 0);
				puzzleMontama1.GetComponent<PuzzleMontama>().IsNoBegin = false;
				puzzleMontama1.GetComponent<PuzzleMontama>().aryIndex = PuzzleMontamaManager.I.PuzzleMontamas.Count;
				PuzzleMontamaManager.I.PuzzleMontamas.Add(puzzleMontama1.GetComponent<PuzzleMontama>());
				PuzzleMontamaManager.I.DeleteNullObject();

				// 新しいモンタマをセット
				puzzleMontama1 = puzzleMontama2;
				puzzleMontama2 = Instantiate(nextMonkuri, genePos2.transform.position, genePos2.transform.rotation) as GameObject;
				puzzleMontama2.GetComponent<PuzzleMontama>().IsNoBegin = true;
				puzzleMontama2.transform.position = genePos2.transform.position;
				vibring = false;
			}
		}
	}

	public void SetMontama(GameObject montama1, GameObject montama2)
	{
		puzzleMontama1 = Instantiate(montama1, genePos1.transform.position, genePos1.transform.rotation) as GameObject;
		puzzleMontama1.GetComponent<PuzzleMontama>().IsNoBegin = true;
		puzzleMontama1.transform.position = genePos1.transform.position;

		puzzleMontama2 = Instantiate(montama2, genePos2.transform.position, genePos2.transform.rotation) as GameObject;
		puzzleMontama2.GetComponent<PuzzleMontama>().IsNoBegin = true;
		puzzleMontama2.transform.position = genePos2.transform.position;
	}

	public void SetMontama(GameObject montama)
	{
		if (puzzleMontama1 != null && puzzleMontama2 != null)
		{
			nextMonkuri = montama;
			inVibrationTime = vibrationTime;
			vibring = true;
		}
	}
}
