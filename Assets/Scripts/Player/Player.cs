using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public BaseWeapon[] playerWeapons;
	public BaseWeapon currentWeapon;
	public GameObject leftHand, rightHand;

	//GUI Panels and objects
	public GameObject reloadBar, bulletsNumber, bulletsMax;

	// Use this for initialization
	void Start () {
		currentWeapon = (BaseWeapon) GameObject.Instantiate(playerWeapons [0] , rightHand.transform.position , rightHand.transform.rotation);  
		currentWeapon.transform.parent = rightHand.transform;

		bulletsNumber.GetComponent<UILabel> ().text = currentWeapon.AmountOfBullets.ToString();
		bulletsMax.GetComponent<UILabel> ().text = currentWeapon.MaxAmountOfBullets.ToString();
	}
	
	// Update is called once per frame
	void Update () {

		//if game is in play state
		if (true) {

			//Turns the player towards the current mouse position
			Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			diff.Normalize();
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);



			//Buttons
			if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
			{
				transform.Translate(new Vector3(0, currentWeapon.WeaponMoveSpeed, 0) * Time.deltaTime);
			}
			
			if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
			{
				transform.Translate(new Vector3(0, - currentWeapon.WeaponMoveSpeed, 0) * Time.deltaTime);
			}		 

			if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			{
				transform.Translate(new Vector3(-currentWeapon.WeaponMoveSpeed, 0, 0) * Time.deltaTime);
			}
			
			if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
				transform.Translate(new Vector3(currentWeapon.WeaponMoveSpeed, 0, 0) * Time.deltaTime);
			}

			if(Input.GetKey(KeyCode.Mouse0))
			{
				currentWeapon.Fire();
			}

			if(Input.GetKeyDown(KeyCode.Q))
			{
				currentWeapon.transform.position = leftHand.transform.position;
				currentWeapon.transform.parent = leftHand.transform;
			}

			if(Input.GetKeyDown(KeyCode.E))
			{
				currentWeapon.transform.position = rightHand.transform.position;
				currentWeapon.transform.parent = rightHand.transform;
			}
			if(Input.GetKeyDown(KeyCode.R))
			{
				currentWeapon.Reload(reloadBar);
			}
		}
	
	}

}
