using UnityEngine;
using System.Collections;

public abstract class BaseBullet  : MonoBehaviour{


	protected string bulletName;
	public float bulletSpeed;
	public int bulletDamage;
	//maybe we can apply effects like poison, fire, slow etc

	// Update is called once per frame
	void Update () {
		
		transform.Translate(new Vector3(0, bulletSpeed, 0) * Time.deltaTime);
	}

	public abstract void destroy();

	public abstract void OnTriggerEnter2D(Collider2D other ); 
	
}