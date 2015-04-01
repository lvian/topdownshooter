using UnityEngine;
using System.Collections;

public abstract class Humanoid : Entity {
	protected PlayerCamera playerCamera;
	protected Timer hitSoundCooldown;
	protected AudioSource audioSource;
	public AudioClip playerHit;
	protected Animator anim;
	protected bool isMoving;

	public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
	public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

	protected new virtual void Start() {
		Debug.Log("Starting humanoid!!");
		base.Start();
		playerCamera = Camera.main.GetComponent<PlayerCamera> ();
		audioSource = GetComponent<AudioSource> ();
		hitSoundCooldown = new Timer(0.5f);
		anim = GetComponent<Animator> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		
		//Will be used in the future ... I'll  be back!!!!
		if (other.tag == "Bullet")
		{
			if(this.tag == "Player")
				playerCamera.shakeCamera(0.1f , 0.05f);
			controlHitPoints(other.GetComponent<BaseBullet>().bulletDamage);
			if(Armor <= 0)
			{
				GameObject blood = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/Bloodhit") , other.transform.position, other.transform.rotation);  
				blood.GetComponentInChildren<ParticleSystem>().Play();
			}
			GameObject.Destroy(other.gameObject);
		}
	}
	
	public virtual void controlHitPoints(int damage)
	{
		if(hitSoundCooldown.IsElapsed)
		{
			//Choose a random pitch to play back our clip at between our high and low pitch ranges.
			float randomPitch = Random.Range(lowPitchRange, highPitchRange);
			audioSource.pitch = randomPitch;
			audioSource.PlayOneShot (playerHit);
			hitSoundCooldown.Reset();
		}
		if(Armor > 0)
		{
			Armor -= damage;
		} else
		{
			
			HitPoints -= damage;
		}
	}

	public bool IsMoving {
		get {
			return isMoving;
		}
		set {
			isMoving = value;
		}
	}
}
