using UnityEngine;
using System.Collections;

public class Revolver : BaseWeapon {


	#region implemented abstract members of BaseWeapon

	public override void Fire ()
	{
		if(!IsReloading)
		{
			if(AmountOfBullets > 0)
			{
				if(lastShot >= weaponFireDelay )
				{
					GameObject.Instantiate(Resources.Load ("Prefabs/Bullets/RevolverBullet") , transform.position , transform.rotation); 
		 			lastShot = 0;
					amountOfBullets --;
					bulletsLabelNumber.GetComponent<UILabel>().text = AmountOfBullets.ToString();
				}
			} else{
				//play out of bullets sound 
			}
		} else
		{
			//Show the player a message "I'm Reloading"
			Debug.Log ("I'm Reloading");
		}
	}

	public override void Reload (GameObject rb)
	{
		if(AmountOfBullets < MaxAmountOfBullets)
		{
			NGUITools.SetActive (rb, true);
			rb.GetComponent<UISlider> ().value = 0;
			reloadTimer = 0;
			isReloading = true;
			base.reloadBar = rb;
		} else
		{
			//Show a message "Weapon fully loaded"
			Debug.Log ("Weapon fully loaded");
		}

	}

	#endregion
}
