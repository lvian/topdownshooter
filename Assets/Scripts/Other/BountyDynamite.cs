using UnityEngine;
using System.Collections;

public class BountyDynamite : MonoBehaviour {

	public AudioClip pickupSound;
	private GameObject player;
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
		if(!fades)
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

	private void animateAlpha (){
		float duration = .1f;
		float lerp = Mathf.PingPong (Time.time, duration) / duration;
		float alpha = Mathf.Lerp(0.0f, 1.0f, lerp);
		
		Color c = new Color(_renderer.material.color.r, _renderer.material.color.g, _renderer.material.color.b, alpha);
		_renderer.material.color = c;
	}
}
