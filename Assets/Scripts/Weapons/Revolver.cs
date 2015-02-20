using UnityEngine;
using System.Collections;

public class Revolver : BaseWeapon {



	#region implemented abstract members of BaseWeapon

	public override void fire ()
	{
		if(lastShot >= weaponFireDelay )
		{
			GameObject.Instantiate(Resources.Load ("Prefabs/Bullets/RevolverBullet") , transform.position , transform.rotation); 
 			lastShot = 0;
		} 
	}

	public override void reload ()
	{
		throw new System.NotImplementedException ();
	}

	#endregion
}
