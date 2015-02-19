using UnityEngine;
using System.Collections;

public class Revolver : BaseWeapon {



	#region implemented abstract members of BaseWeapon

	public override void fire ()
	{
		Debug.Log (lastShot);
		if(lastShot >= weaponFireDelay )
		{
			Debug.Log ("Fire 3");
			GameObject.Instantiate(Resources.Load ("Prefabs/Bullets/RevolverBullet") , transform.position , transform.rotation); 
			
			base.lastShot = 0;
		} 
	}

	public override void reload ()
	{
		throw new System.NotImplementedException ();
	}

	#endregion
}
