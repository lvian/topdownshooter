using UnityEngine;
using System.Collections;

public abstract class BaseWeapon : MonoBehaviour {

	//Weapon Variables
	public float weaponMoveSpeed, weaponFireDelay, weaponReloadSpeed, weaponSwapSpeed, amountOfBullets, maxAmountOfBullets, rotationSpeed;
	public GameObject muzzle, weaponCases, muzzleFireEffect, muzzleFlashEffect;
	public AudioClip  shotSound,reloadSound, triggerSound;

	//Auxiliary Variables
	protected GameObject  bulletsLabelNumber, bulletsLabelMax;
	protected float lastShot,reloadTimer, triggerSoundDelay;
	protected bool isReloading;
	protected GameObject reloadBar;
	protected Animator anim;
	protected PlayerCamera playerCamera;

	// Use this for initialization
	void Start () {
		isReloading = false;
		lastShot = 0;

		playerCamera = Camera.main.GetComponent<PlayerCamera> ();
		bulletsLabelNumber = GameObject.Find ("Bullets Number");
		bulletsLabelMax = GameObject.Find ("Bullets Max");
		anim = GetComponent<Animator> ();
		triggerSoundDelay = 0;
	}
	
	// Update is called once per frame
	void Update () {

		lastShot += Time.deltaTime;


		if(amountOfBullets == 0)
		{
			triggerSoundDelay += Time.deltaTime;
		}

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

				if(reloadTimer >= weaponReloadSpeed)
				{
					amountOfBullets = maxAmountOfBullets;
					isReloading = false;
				} 
				
				reloadTimer += Time.deltaTime;
			}

		}
	}

	protected IEnumerator muzzleEffect()
	{
		muzzleFlashEffect.SetActive (true);
		yield return new WaitForSeconds (0.2f);
		muzzleFlashEffect.SetActive (false);

	}

	public string currentAnimation()
	{
		return anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
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

	public float RotationSpeed {
		get {
			return rotationSpeed;
		}
	}
}
