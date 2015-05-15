using UnityEngine;
using System.Collections;

public class RifleBullet : BaseBullet {
	#region implemented abstract members of BaseBullet
	public override void OnTriggerEnter2D (Collider2D other){
		//Debug.Log (other.tag + " " + other.name );
		if(other.tag == "Wall")
		{
			Destroy();
		}

	}
	#endregion
}
