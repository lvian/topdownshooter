using UnityEngine;
using System.Collections;

public class Dynamite : MonoBehaviour {

	protected Vector3 destination;
	protected int damage;
	protected float maxDistance ,traveledDistance ;
	public float fuseTimer;
	private Timer timer;
	private PlayerCamera playerCamera;
	private bool exploded;
	public ParticleSystem explo, explo2, fuse, sparks;
	public AudioClip fuseStart, fuseBurn, explosion;
	public Light light;

	// Use this for initialization
	void Start () {
		damage = 4;
		maxDistance = 5f;
		traveledDistance = 0;
		exploded = false;
		playerCamera = Camera.main.GetComponent<PlayerCamera>();
		timer = new Timer(fuseTimer);
		GetComponent<AudioSource>().clip = fuseBurn;
		GetComponent<AudioSource>().PlayOneShot (fuseStart);

		GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.instance.State == GameManager.GameState.Playing)
		{
			if(Vector3.Distance(transform.position, destination) > 0 && traveledDistance <= maxDistance)
			{
				traveledDistance += Vector3.Distance(transform.position , Vector3.MoveTowards( transform.position , destination, 6 * Time.deltaTime ));
				transform.position = Vector3.MoveTowards( transform.position , destination, 6 * Time.deltaTime );
				transform.Rotate (0 , 0, 8);

			} 
		}

		if(timer.RemainingTime == 0 && !exploded)
		{
			explode();
		}
		if(exploded)
		{
			if(!explo2.IsAlive())
			{
				GameObject.Destroy(transform.gameObject);
			}
		}
	
	}
	protected void explode()
	{
		//Booooom
		StartCoroutine (LightEffect());
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<AudioSource>().Stop();
		GetComponent<AudioSource>().PlayOneShot(explosion);
		explo.Play();
		explo2.Play();
		fuse.Stop();
		sparks.Stop();
		float radius = transform.GetComponent<CircleCollider2D>().radius;
		Collider2D[] col =  Physics2D.OverlapCircleAll(transform.position, radius);
		foreach(Collider2D c in col)
		{
			if(c.tag == "Enemy" || c.tag == "Player")
			{
				Humanoid h = (Humanoid) c.gameObject.GetComponent<Humanoid>();
				float distance = Vector3.Distance(transform.position, h.transform.position);
				h.controlHitPoints(Mathf.CeilToInt(damage * (1/distance)));
			}
		}
		playerCamera.shakeCamera(0.3f , 0.5f);
		exploded = true;

	}

	public IEnumerator LightEffect()
	{
		light.range = 6;
		yield return new WaitForSeconds (0.2f);
		light.enabled = false;
	}

	public Vector3 Destination {
		get {
			return destination;
		}
		set {
			destination = new Vector3(value.x , value.y, 0);
		}
	}
}
