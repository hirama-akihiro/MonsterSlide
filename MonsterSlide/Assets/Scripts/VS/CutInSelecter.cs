using UnityEngine;
using System.Collections;

public class CutInSelecter : MonoBehaviour {

	public GameObject cutInPlayer1;
	public GameObject cutInPlayer2;

	public Sprite serverPlayer1Sprite;
	public Sprite serverPlayer2Sprite;
	public Sprite clientPlayer1Sprite;
	public Sprite clientPlayer2Sprite;

	// Use this for initialization
	void Awake() {
		switch(BattleTypeManager.Instance.battleType)
		{
			case BattleTypeManager.BattleType.NearBattle_Server:
				cutInPlayer1.GetComponent<SpriteRenderer>().sprite = serverPlayer1Sprite;
				cutInPlayer2.GetComponent<SpriteRenderer>().sprite = serverPlayer2Sprite;
				break;
			case BattleTypeManager.BattleType.NearBattle_Client:
				cutInPlayer1.GetComponent<SpriteRenderer>().sprite = clientPlayer1Sprite;
				cutInPlayer2.GetComponent<SpriteRenderer>().sprite = clientPlayer2Sprite;
				break;
			case BattleTypeManager.BattleType.SingleBattle:
				cutInPlayer1.GetComponent<SpriteRenderer>().sprite = serverPlayer1Sprite;
				cutInPlayer2.GetComponent<SpriteRenderer>().sprite = serverPlayer2Sprite;
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
