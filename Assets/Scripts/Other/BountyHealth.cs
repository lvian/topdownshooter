﻿using UnityEngine;
using System.Collections;

public class BountyHealth : MonoBehaviour {

	private GameObject player;
	protected bool finished;
	public AudioClip pickupSound;
	public bool fades;
	public float timeToLive;
	public float fadeTime;
	
	private Timer ttl;
	private Timer fade;
	
	private Renderer _renderer;

	// Use this for initialization
	void Start () {
	
		player = GameObject.FindGameObjectWithTag("Player");

		ttl = new Timer(timeToLive);
		_renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.instance.State != GameManager.GameState.Playing)
			return;
		if(fade == null){
			if(ttl.IsElapsed){
				fade = new Timer(fadeTime);
			}
		}
		else{
			if(fade.IsElapsed){
				_renderer.enabled = false;
				GameObject.Destroy(transform.gameObject, .1f);
			}
			else{
				animateAlpha();
			}
		}
		if(finished)
		{
			transform.position =  Vector2.Lerp(transform.position , player.transform.position , Time.deltaTime * 1.5f);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		
		
		//Will be used in the future ... I'll  be back!!!!
		if (other.tag == "Player")
		{
			if(player.GetComponent<Player>().HitPoints <= 3)
			{
				player.GetComponent<Player>().HitPoints ++;
			
				SoundManager.instance.clipOneShotRandomPitch(pickupSound);
				GetComponent<SpriteRenderer>().enabled = false;
			
				GameObject.Destroy(transform.gameObject, pickupSound.length);
			}
		}
		if (other.tag == "Magnet")
		{
			if(player.GetComponent<Player>().HitPoints < 4)
			{
				Finished = true;
			}
		}
	}

	private void animateAlpha (){
		float duration = .1f;
		float lerp = Mathf.PingPong (Time.time, duration) / duration;
		float alpha = Mathf.Lerp(0.0f, 1.0f, lerp);
		
		Color c = new Color(_renderer.material.color.r, _renderer.material.color.g, _renderer.material.color.b, alpha);
		_renderer.material.color = c;
	}

	public bool Finished {
		get {
			return finished;
		}
		set {
			finished = value;
		}
	}

}
