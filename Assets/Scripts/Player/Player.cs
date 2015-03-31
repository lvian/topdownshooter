using UnityEngine;
using System.Collections;

public class Player : Humanoid {

	public enum PlayerState {
		Setup,
		Idle,
		Moving,
		Attacking,
		Reloading,
		Dodging,
		Dying
	}

	//GUI Panels and objects
	public GameObject reloadBar, bulletsNumber, bulletsMax, healthNumber, armorNumber;

	protected BaseWeapon[] availableWeapons;
	protected PlayerState playerState;

	private GameObject[] obstacles;
	private float newY = 0, newX = 0;


	// Use this for initialization
	protected override void Start () {

		isMoving = false;
		playerState = PlayerState.Idle;
		spawnWeapons ();
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag ("Wall");
		base.Start();
		bulletsNumber.GetComponent<UILabel> ().text = currentWeapon.AmountOfBullets.ToString();
		bulletsMax.GetComponent<UILabel> ().text = currentWeapon.MaxAmountOfBullets.ToString();
		healthNumber.GetComponent<UILabel> ().text = HitPoints.ToString();
		armorNumber.GetComponent<UILabel> ().text = Armor.ToString();
	}
	
	// Update is called once per frame
	void Update () {

		//if game is in play state
		if (true) {

			if(playerState != PlayerState.Dying)
			{
				//Turns the player towards the current mouse position
				Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
				diff.Normalize();
				float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
				Quaternion q = Quaternion.Euler(0f, 0f, rot_z - 90);
				transform.rotation = Quaternion.RotateTowards(transform.rotation , q , Time.deltaTime * 250 * currentWeapon.RotationSpeed);

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
				if(Input.GetKeyDown(KeyCode.Alpha1))
				{
					changeWeapons(0);
				}

				if(Input.GetKeyDown(KeyCode.Alpha2))
				{
					changeWeapons(1);
				}

				if(Input.GetKeyDown(KeyCode.Alpha3))
				{
					changeWeapons(2);
				}
			} else if(playerState == PlayerState.Dying)
			{
				GetComponent<CircleCollider2D>().enabled = false;
			}

		}
	
	}

	#region implemented abstract members of Entity
	public override void Move ()
	{
		newY = 0;
		newX = 0;
		isMoving = false;

		//Buttons
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			newY += Speed * currentWeapon.WeaponMoveSpeed;
			isMoving = true;
		}
		
		if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			newY -= Speed * currentWeapon.WeaponMoveSpeed;
			isMoving = true;
		}		 
		
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			newX -= Speed * currentWeapon.WeaponMoveSpeed;
			isMoving = true;
		}
		
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			newX += Speed * currentWeapon.WeaponMoveSpeed;
			isMoving = true;
		}

		checkObstacles ();
		anim.SetBool ("isMoving", isMoving);
		transform.parent.Translate(new Vector3( newX, newY, 0) * Time.deltaTime, Space.World);
	}
	#endregion


	public void spawnWeapons()
	{
		availableWeapons = new BaseWeapon[weapons.Length];
		for ( int key = 0 ; key < weapons.Length ; key ++)
		{

			BaseWeapon b = (BaseWeapon) GameObject.Instantiate(weapons[key], transform.position, transform.rotation);  
			b.transform.parent = transform;
			availableWeapons[key] = b;
			b.gameObject.SetActive(false);

		}
		availableWeapons [0].gameObject.SetActive (true);
		currentWeapon = availableWeapons[0];
		currentWeapon.gameObject.SetActive(true);
	}

	public void changeWeapons(int weaponNumber)
	{
		if(currentWeapon.currentAnimation() == "Idle")
		{
			currentWeapon.gameObject.SetActive (false);
			currentWeapon = availableWeapons [weaponNumber];
			currentWeapon.gameObject.SetActive (true);
			bulletsNumber.GetComponent<UILabel> ().text = currentWeapon.AmountOfBullets.ToString();
			bulletsMax.GetComponent<UILabel> ().text = currentWeapon.MaxAmountOfBullets.ToString();

		} else
		{
			Debug.Log ("Busy");
		}

	}

	public void DisableWeapons()
	{
		currentWeapon.gameObject.SetActive(false);
	}

	public void checkObstacles()
	{

		Collider2D[] colX = Physics2D.OverlapCircleAll (new Vector2 ((newX * Time.deltaTime) + transform.position.x , transform.position.y), transform.GetComponent<CircleCollider2D> ().radius);
		Collider2D[] colY = Physics2D.OverlapCircleAll (new Vector2 (transform.position.x , (newY * Time.deltaTime) + transform.position.y), transform.GetComponent<CircleCollider2D> ().radius);
		//Tests X and Y separetely so you can move them individually during collisions
		foreach(Collider2D c in colX )
		{
			if(c.tag == "Wall" || c.tag == "Small Obstacle")
			{
				newX = 0;
			}
		}

		foreach(Collider2D c in colY )
		{
			if(c.tag == "Wall" || c.tag == "Small Obstacle")
			{
				newY = 0;
			}
		}
	}

	public override int HitPoints {
		get {
			return base.HitPoints;
		}
		set {
			base.HitPoints = value;
			healthNumber.GetComponent<UILabel> ().text = HitPoints.ToString();

		}
	}
	
	public override int Armor {
		get {
			return base.Armor;
		}
		set {
			base.Armor = value;
			armorNumber.GetComponent<UILabel> ().text = Armor.ToString();
		}
	}

	#region implemented abstract members of Entity

	public override void Died ()
	{
		anim.SetTrigger("isDying");
		playerState = PlayerState.Dying;
	}

	#endregion
}
