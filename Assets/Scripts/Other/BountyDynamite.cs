using UnityEngine;
using System.Collections;

public class BountyDynamite : MonoBehaviour {

	public AudioClip pickupSound;
	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		
		//Will be used in the future ... I'll  be back!!!!
		if (other.tag == "Player")
		{
			player.GetComponent<Player>().DynamiteAmount ++;
			
			SoundManager.instance.clipOneShotRandomPitch(pickupSound);
			GetComponent<SpriteRenderer>().enabled = false;

			GameObject.Destroy(transform.gameObject, pickupSound.length);
		}
	}

}
