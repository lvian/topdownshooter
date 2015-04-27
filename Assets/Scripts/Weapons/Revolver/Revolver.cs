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
						GameObject bullet = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/Bullets/RevolverBullet") , muzzle.transform.position , muzzle.transform.rotation); 
						bullet.transform.Rotate(0f ,0f , Random.Range(- bulletDeviationAngle/ 2 , bulletDeviationAngle/2));
			 			lastShot = 0;
						amountOfBullets --;
						anim.SetTrigger("Attack");
						StartCoroutine(muzzleEffect());
						audioSource.PlayOneShot(shotSound);
						GUIManager.instance.RevolverBullets(AmountOfBullets,MaxAmountOfBullets);
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
						GameObject.Instantiate(Resources.Load ("Prefabs/Bullets/RevolverBullet") , muzzle.transform.position , muzzle.transform.rotation); 
						lastShot = 0;
						anim.SetTrigger("Attack");
						StartCoroutine(muzzleEffect());
						amountOfBullets --;
						audioSource.PlayOneShot(shotSound);
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

	public override void Reload ()
	{
		if(AmountOfBullets < MaxAmountOfBullets && lastShot >= weaponFireDelay)
		{
			if(IsReloading == false){
				//Not cool bro, need a better solution
				if( transform.parent.name == "Player"){
					GUIManager.instance.ReloadBarActive(true);

				}
				coneBase.gameObject.SetActive(false);
				reloadTimer = 0;
				isReloading = true;
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
		GUIManager.instance.RevolverBullets(AmountOfBullets,MaxAmountOfBullets);
	}
	#endregion
}
