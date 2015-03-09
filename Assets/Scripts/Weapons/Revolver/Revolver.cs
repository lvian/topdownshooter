using UnityEngine;
using System.Collections;

public class Revolver : BaseWeapon {


	#region implemented abstract members of BaseWeapon

	public override void Fire ()
	{
		//Player Logic
		if(transform.parent.tag == "Player")
		{
			if(!IsReloading)
			{
				if(AmountOfBullets > 0)
				{
					if(lastShot >= weaponFireDelay )
					{
						GameObject.Instantiate(Resources.Load ("Prefabs/Bullets/RevolverBullet") , muzzle.transform.position , muzzle.transform.rotation); 
			 			lastShot = 0;
						amountOfBullets --;
						anim.SetTrigger("Attack");
						NGUITools.PlaySound(shotSound);
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
		} else {
			//Enemies logic
			if(!IsReloading)
			{
				if(AmountOfBullets > 0)
				{
					if(lastShot >= weaponFireDelay )
					{
						GameObject.Instantiate(Resources.Load ("Prefabs/Bullets/RevolverBullet") , muzzle.transform.position , muzzle.transform.rotation); 
						lastShot = 0;
						anim.SetTrigger("Attack");
						amountOfBullets --;
						NGUITools.PlaySound(shotSound);
						//bulletsLabelNumber.GetComponent<UILabel>().text = AmountOfBullets.ToString();
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
	}

	public override void Reload (GameObject rb)
	{
		if(AmountOfBullets < MaxAmountOfBullets && lastShot >= weaponFireDelay)
		{
			if(IsReloading == false){

				//Not cool bro, need a better solution
				if(rb.tag == "Player"){
					NGUITools.SetActive (rb, true);
					rb.GetComponent<UISlider> ().value = 0;

				}
				reloadTimer = 0;
				isReloading = true;
				base.reloadBar = rb;
				anim.SetTrigger("Reload");
				NGUITools.PlaySound(reloadSound);
				GameObject gb = (GameObject) GameObject.Instantiate(weaponCases , transform.position , transform.rotation); 
				gb.transform.parent = GameObject.Find ("WeaponCases").transform;

			} else{
				Debug.Log ("Already reloading!");
			}
		} else
		{
			//Show a message "Weapon fully loaded"
			Debug.Log ("Weapon fully loaded");
		}

	}

	#endregion
}
