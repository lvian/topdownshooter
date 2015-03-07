using UnityEngine;
using System.Collections;

public class Player : Entity {
	//GUI Panels and objects
	public GameObject reloadBar, bulletsNumber, bulletsMax;

	// Use this for initialization
	protected override void Start () {
		spawnWeapon (weapons[0]);
		bulletsNumber.GetComponent<UILabel> ().text = currentWeapon.AmountOfBullets.ToString();
		bulletsMax.GetComponent<UILabel> ().text = currentWeapon.MaxAmountOfBullets.ToString();

		base.Start();
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

			Move();

			if(Input.GetKey(KeyCode.Mouse0))
			{
				currentWeapon.Fire();
			}

			if(Input.GetKeyDown(KeyCode.Q))
			{
				// previous weapon
			}

			if(Input.GetKeyDown(KeyCode.E))
			{
				// next weapon 
			}
			if(Input.GetKeyDown(KeyCode.R))
			{
				currentWeapon.Reload(reloadBar);
			}
		}
	
	}

	#region implemented abstract members of Entity
	protected override void Move ()
	{
		//Buttons
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			transform.Translate(new Vector3(0, Speed * currentWeapon.WeaponMoveSpeed, 0) * Time.deltaTime, Space.World);
		}
		
		if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			transform.Translate(new Vector3(0, -Speed * currentWeapon.WeaponMoveSpeed, 0) * Time.deltaTime, Space.World);
		}		 
		
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Translate(new Vector3(-Speed * currentWeapon.WeaponMoveSpeed, 0, 0) * Time.deltaTime, Space.World);
		}
		
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			transform.Translate(new Vector3(Speed * currentWeapon.WeaponMoveSpeed, 0, 0) * Time.deltaTime, Space.World);
		}
	}
	#endregion


	public void spawnWeapon(BaseWeapon weapon)
	{
		currentWeapon = (BaseWeapon) GameObject.Instantiate(weapon, transform.position, transform.rotation);  
		currentWeapon.transform.parent = transform;
	}


}
