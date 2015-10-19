using UnityEngine;
using System.Collections;

public class PartyInfoManager : MonoBehaviour {

	private SpriteRenderer myRenderer;

	public Sprite masterInfoSprite;
	public Sprite kimeraInfoSprite;
	public Sprite ketsiInfoSprite;
	public Sprite suzakuInfoSprite;
	public Sprite genbuInfoSprite;

	// Use this for initialization
	void Start () {
		myRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickKimera()
	{
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		myRenderer.sprite = kimeraInfoSprite;
	}

	public void OnClickKetsi()
	{
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		myRenderer.sprite = ketsiInfoSprite;
	}

	public void OnClickSuzaku()
	{
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		myRenderer.sprite = suzakuInfoSprite;
	}

	public void OnClickGenbu()
	{
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		myRenderer.sprite = genbuInfoSprite;
	}

	public void OnClickMaster()
	{
		AudioManager.Instance.PlayAudio("se_maoudamashii_system49");
		myRenderer.sprite = masterInfoSprite;
	}
}
