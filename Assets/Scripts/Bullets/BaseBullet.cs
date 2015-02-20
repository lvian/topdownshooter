using UnityEngine;
using System.Collections;

public abstract class BaseBullet  : MonoBehaviour{


	protected string bulletName;
	public float bulletSpeed;
	//maybe we can apply effects like poison, fire, slow etc

	public abstract void destroy();

	public abstract void OnTriggerEnter2D(Collider2D other ); 
	
}