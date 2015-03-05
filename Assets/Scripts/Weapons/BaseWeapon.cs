using UnityEngine;
using System.Collections;

public abstract class BaseWeapon : MonoBehaviour {

	//Weapon Variables
	public float weaponMoveSpeed, weaponFireDelay, weaponReloadSpeed, weaponSwapSpeed, amountOfBullets, maxAmountOfBullets;

	//Auxiliary Variables
	protected GameObject  bulletsLabelNumber, bulletsLabelMax;
	protected float lastShot,reloadTimer;
	protected bool isReloading;
	protected GameObject reloadBar;

	// Use this for initialization
	void Start () {
		isReloading = false;
		lastShot = 0;

		bulletsLabelNumber = GameObject.Find ("Bullets Number");
		bulletsLabelMax = GameObject.Find ("Bullets Max");
	}
	
	// Update is called once per frame
	void Update () {



		lastShot += Time.deltaTime;
		if(transform.parent.tag == "Player")
		{
			if(isReloading)
			{
				float sliderPercentage = 1 / weaponReloadSpeed;
				UISlider slider = reloadBar.GetComponent<UISlider>();
				if(reloadTimer >= weaponReloadSpeed)
				{
					amountOfBullets = maxAmountOfBullets;
					bulletsLabelNumber.GetComponent<UILabel>().text = AmountOfBullets.ToString();
					isReloading = false;
					NGUITools.SetActive(reloadBar , false);
				} else
				{
					slider.value = reloadTimer * sliderPercentage;
				}

				reloadTimer += Time.deltaTime;
			}
		} else
		{
			if(isReloading)
			{
				//float sliderPercentage = 1 / weaponReloadSpeed;
				//UISlider slider = reloadBar.GetComponent<UISlider>();
				if(reloadTimer >= weaponReloadSpeed)
				{
					amountOfBullets = maxAmountOfBullets;
				//	bulletsLabelNumber.GetComponent<UILabel>().text = AmountOfBullets.ToString();
					isReloading = false;
				//	NGUITools.SetActive(reloadBar , false);
				} else
				{
				//	slider.value = reloadTimer * sliderPercentage;
				}
				
				reloadTimer += Time.deltaTime;
			}

		}
	}

	public abstract void Fire();
	public abstract void Reload(GameObject rb);

	public float WeaponMoveSpeed {
		get {
			return weaponMoveSpeed;
		}
	}

	public float WeaponSwapSpeed {
		get {
			return weaponSwapSpeed;
		}
	}

	public float MaxAmountOfBullets {
		get {
			return maxAmountOfBullets;
		}
	}

	public float AmountOfBullets {
		get {
			return amountOfBullets;
		}
	}

	public bool IsReloading {
		get {
			return isReloading;
		}
	}
}
