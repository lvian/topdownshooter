using UnityEngine;
using System.Collections;

public abstract class BaseWeapon : MonoBehaviour {

	public float weaponMoveSpeed, weaponFireDelay, weaponReloadSpeed, weaponSwapSpeed;
	protected float lastShot;

	// Use this for initialization
	void Start () {
	
		lastShot = 5;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (lastShot);
		lastShot += Time.deltaTime;
	}

	public abstract void fire();
	public abstract void reload();

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
}
