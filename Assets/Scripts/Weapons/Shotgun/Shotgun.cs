using UnityEngine;
using System.Collections;

public class Shotgun : BaseWeapon {

	//How many bullets are instantiated on each shot
	public int bulletsperShot;
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
						for(int x = 0 ; x < bulletsperShot ; x++)
						{
							GameObject gb =(GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/Bullets/ShotgunBullet") , muzzle.transform.position , muzzle.transform.rotation); 
							gb.transform.Rotate(0f ,0f , Random.Range(- bulletDeviationAngle/ 2 , bulletDeviationAngle/2));
						}
			 			lastShot = 0;
						amountOfBullets --;
						anim.SetTrigger("Attack");
						StartCoroutine(muzzleEffect());
						audioSource.PlayOneShot(shotSound);
						GUIManager.instance.ShotgunBullets(AmountOfBullets,MaxAmountOfBullets);
						muzzleFireEffect.GetComponent<ParticleSystem>().Play();
						playerCamera.shakeCamera(0.1f , 0.05f);

					}
				} else{
					if(triggerSoundDelay >= weaponReloadSpeed)
					{
						NGUITools.PlaySound(triggerSound);
						triggerSoundDelay = 0;
					}
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
						for(int x = 0 ; x < bulletsperShot ; x++)
						{
							GameObject gb =(GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/Bullets/ShotgunBullet") , muzzle.transform.position , muzzle.transform.rotation); 
							gb.transform.Rotate(0f ,0f , Random.Range(- bulletDeviationAngle/ 2 , bulletDeviationAngle/2));
						}
						lastShot = 0;
						anim.SetTrigger("Attack");
						amountOfBullets --;
						audioSource.PlayOneShot(shotSound);
						StartCoroutine(muzzleEffect());
						muzzleFireEffect.GetComponent<ParticleSystem>().Play();
						//bulletsLabelNumber.GetComponent<UILabel>().text = AmountOfBullets.ToString();
					}
				} else{
					if(triggerSoundDelay >= weaponReloadSpeed)
					{
						NGUITools.PlaySound(triggerSound);
						triggerSoundDelay = 0;
					}
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
					GUIManager.instance.ReloadBarActive(true);

				}
				reloadTimer = 0;
				isReloading = true;
				base.reloadBar = rb;
				anim.SetTrigger("Reload");
				audioSource.PlayOneShot(reloadSound);
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


	public override void ReloadGUI()
	{
		GUIManager.instance.ShotgunBullets(AmountOfBullets,MaxAmountOfBullets);
	}
	#endregion
}
