using UnityEngine;
using System.Collections;

public class RevolverBullet : BaseBullet {

	// Use this for initialization
	void Start () {

	
	}

	#region implemented abstract members of BaseBullet
	public override void destroy ()
	{
		GameObject.Destroy(gameObject);
	}

	public override void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log (other.tag + " " + other.name );
		if(other.tag == "Wall")
		{
			destroy();
		}

	}

	public void OnCollisionEnter2D (Collision2D other)
	{
		//Debug.Log ("Pew pew");
		//Debug.Log (other.tag + " " + other.name );
	}
	#endregion
}
