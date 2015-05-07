using UnityEngine;
using System.Collections;

public class DualRevolver : BaseWeapon {

	public GameObject muzzleLeft, muzzleFireEffectLeft , muzzleFlashEffectLeft , coneBaseLeft;
	private bool leftRight = true;
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
						if(leftRight == true)
						{
							GameObject bullet = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/Bullets/RevolverBullet") , muzzle.transform.position , muzzle.transform.rotation); 
							bullet.transform.Rotate(0f ,0f , Random.Range(- bulletDeviationAngle/ 2 , bulletDeviationAngle/2));
				 			lastShot = 0;
							amountOfBullets --;
							anim.SetTrigger("Attack Right");
							StartCoroutine(muzzleEffect(muzzleFlashEffect));
							audioSource.PlayOneShot(shotSound);
							//GUIManager.instance.RevolverBullets(AmountOfBullets,MaxAmountOfBullets);
							muzzleFireEffect.GetComponent<ParticleSystem>().Play();
							playerCamera.shakeCamera(0.1f , 0.05f);
							leftRight = false;
						} else
						{
							GameObject bullet = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/Bullets/RevolverBullet") , muzzleLeft.transform.position , muzzleLeft.transform.rotation); 
							bullet.transform.Rotate(0f ,0f , Random.Range(- bulletDeviationAngle/ 2 , bulletDeviationAngle/2));
							lastShot = 0;
							amountOfBullets --;
							anim.SetTrigger("Attack Left");
							StartCoroutine(muzzleEffect(muzzleFlashEffectLeft));
							audioSource.PlayOneShot(shotSound);
							//GUIManager.instance.RevolverBullets(AmountOfBullets,MaxAmountOfBullets);
							muzzleFireEffectLeft.GetComponent<ParticleSystem>().Play();
							playerCamera.shakeCamera(0.1f , 0.05f);
							leftRight = true;
						}

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
						Debug.Log (leftRight);
						if(leftRight == true)
						{
							GameObject bullet = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/Bullets/RevolverBullet") , muzzle.transform.position , muzzle.transform.rotation); 
							bullet.transform.Rotate(0f ,0f , Random.Range(- bulletDeviationAngle/ 2 , bulletDeviationAngle/2));
							lastShot = 0;
							amountOfBullets --;
							anim.SetTrigger("Attack Right");
							StartCoroutine(muzzleEffect(muzzleFlashEffect));
							audioSource.PlayOneShot(shotSound);
							muzzleFireEffect.GetComponent<ParticleSystem>().Play();
							leftRight = false;
						} else
						{
							GameObject bullet = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/Bullets/RevolverBullet") , muzzleLeft.transform.position , muzzleLeft.transform.rotation); 
							bullet.transform.Rotate(0f ,0f , Random.Range(- bulletDeviationAngle/ 2 , bulletDeviationAngle/2));
							lastShot = 0;
							amountOfBullets --;
							anim.SetTrigger("Attack Left");
							StartCoroutine(muzzleEffect(muzzleFlashEffectLeft));
							audioSource.PlayOneShot(shotSound);
							muzzleFireEffectLeft.GetComponent<ParticleSystem>().Play();
							leftRight = true;
						}
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
		//GUIManager.instance.RevolverBullets(AmountOfBullets,MaxAmountOfBullets);
	}
	#endregion
}
