using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PartyInfoManager : MonoBehaviour {

	#region Info Sprites
	public Sprite masterInfoSprite;
	public Sprite kimeraInfoSprite;
	public Sprite ketsiInfoSprite;
	public Sprite suzakuInfoSprite;
	public Sprite genbuInfoSprite;
	#endregion

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickKimera()
	{
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		GetComponent<Image>().sprite = kimeraInfoSprite;
	}

	public void OnClickKetsi()
	{
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		GetComponent<Image>().sprite = ketsiInfoSprite;
	}

	public void OnClickSuzaku()
	{
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		GetComponent<Image>().sprite = suzakuInfoSprite;
	}

	public void OnClickGenbu()
	{
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		GetComponent<Image>().sprite = genbuInfoSprite;
	}

	public void OnClickMaster()
	{
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		GetComponent<Image>().sprite = masterInfoSprite;
	}
}
