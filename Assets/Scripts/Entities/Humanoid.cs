using UnityEngine;
using System.Collections;

public abstract class Humanoid : Entity {
	protected PlayerCamera playerCamera;
	protected Timer hitSoundCooldown;
	protected AudioSource audioSource;
	public AudioClip playerHit;
	public AudioClip[] deathSounds;
	public GameObject graveyard;
	public Animator anim;
	protected bool isMoving;

	public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
	public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

	private int damageLeft;

	public void Start() {
	}

	public void InitHumanoid() {
		//Debug.Log("Starting humanoid!!");
		InitEntity();
		playerCamera = Camera.main.GetComponent<PlayerCamera> ();
		audioSource = GetComponent<AudioSource> ();
		hitSoundCooldown = new Timer(0.5f);
		damageLeft = 0;
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
		damageLeft = 0;
		if(hitSoundCooldown.IsElapsed)
		{
			//Choose a random pitch to play back our clip at between our high and low pitch ranges.
			float randomPitch = Random.Range(lowPitchRange, highPitchRange);
			audioSource.pitch = randomPitch;
			audioSource.PlayOneShot (playerHit);
			hitSoundCooldown.Reset();
		}
		while(damage > 0)
		{
			if(Armor > 0)
			{
				Armor --;

			} else if( HitPoints > 0 )
			{
				HitPoints --;
			}
			damage --;
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

	public void DisableOnDeath()
	{
		transform.parent.parent = graveyard.transform;
		currentWeapon.gameObject.SetActive(false);
	}
}
