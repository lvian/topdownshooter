using UnityEngine;
using System.Collections;

public class RevolverBullet : BaseBullet {

	// Use this for initialization
	void Start () {

	
	}
	



	#region implemented abstract members of BaseBullet
	public override void destroy ()
	{
		throw new System.NotImplementedException ();
	}
	public override void OnTriggerEnter2D (Collider2D other)
	{
		//Debug.Log (other.tag);
		if(other.tag == "Wall")
		{
			GameObject.Destroy(gameObject);
		}

	}

	public void OnCollisionEnter2D (Collision2D other)
	{
		//Debug.Log (other.gameObject.name);
	}
	#endregion
}
