using UnityEngine;
using System.Collections;

public class Bottle : MonoBehaviour {

	public ParticleSystem sparks;
	public AudioClip explosion;
	private Timer respawn;
	private bool destroyed ;
	public bool tutorial;

	// Use this for initialization
	void Start () {
		destroyed = false;
		respawn = new Timer (3);
	}
	
	// Update is called once per frame
	void Update () {
		if(tutorial && destroyed)
		{
			if(respawn.RemainingTime <= 0)
			{
				GetComponent<SpriteRenderer>().enabled = true;
				GetComponent<CircleCollider2D>().enabled = true;
				destroyed= false;
			}
		}
	}


	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Bullet")
		{
			GetComponent<AudioSource>().PlayOneShot(explosion);
			GetComponent<ParticleSystem>().Play();
			GetComponent<SpriteRenderer>().enabled = false;
			GetComponent<CircleCollider2D>().enabled = false;
			if(tutorial)
			{
				GameObject.Find("Training").GetComponent<TrainingLevel>().BottlesDestroyed ++;
				respawn.Reset();
				destroyed = true;
			} else
			{
				GameObject.Destroy(this);
			}

		}

	}

}
