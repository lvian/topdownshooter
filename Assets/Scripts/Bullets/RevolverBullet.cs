using UnityEngine;
using System.Collections;

public class RevolverBullet : BaseBullet {

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate(new Vector3(0, bulletSpeed, 0) * Time.deltaTime);
	}


	#region implemented abstract members of BaseBullet
	public override void destroy ()
	{
		throw new System.NotImplementedException ();
	}
	public override void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log (other.name);
		if(other.tag == "Wall")
		{
			GameObject.Destroy(gameObject);
		}

	}

	public void OnCollisionEnter2D (Collision2D other)
	{
		Debug.Log (other.gameObject.name);
	}
	#endregion
}
