using UnityEngine;
using System.Collections;

public class Bounty : MonoBehaviour {

	public int bountyAmount;
	public AudioClip bountySound;

	// Use this for initialization
	void Start () {
	
		if(GameManager.instance.Upgrades.Money == 1)
		{
			bountyAmount = (int) (bountyAmount * 1.25f); 
		}

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other) {
		
		//Will be used in the future ... I'll  be back!!!!
		if (other.tag == "Player")
		{
			GameManager.instance.Upgrades.Cash += bountyAmount;
			SoundManager.instance.clipOneShotRandomPitch(bountySound);
			GetComponent<SpriteRenderer>().enabled = false;

			GameObject.Destroy(transform.gameObject, bountySound.length);
		}
	}
}
