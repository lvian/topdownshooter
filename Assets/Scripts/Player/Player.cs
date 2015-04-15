using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	public GameObject reloadBar, bulletsNumber, bulletsMax, healthNumber, armorNumber, dynamite, dynamiteNumber;
	public float maxSpeed, acellerationSpeed, dodgeCooldown;
	
	protected BaseWeapon[] availableWeapons;
	protected PlayerState playerState;
	protected int dynamiteAmount;
	private GameObject[] obstacles;
	private float newY = 0, newX = 0, speedY = 0, speedX = 0, oldX, oldY;
	private bool isMovingX, isMovingY;
	private float maxHealth, maxArmor, dodgeTimer;
	
	// Use this for initialization
	public IEnumerator Start () {
		if(GameManager.instance.Upgrades.Dodge == 1)
		{
			dodgeCooldown = dodgeCooldown / 2; 
		}
		dodgeTimer = 0;
		Armor = GetArmorUpgrades ();
		maxArmor = GetArmorUpgrades();
		maxHealth = HitPoints;
		isMovingX = false;
		isMovingY = false;
		playerState = PlayerState.Idle;
		dynamiteAmount = 2;
		spawnWeapons ();
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag ("Wall");
		InitHumanoid();
		GUIManager.instance.HealthGUI(HitPoints, maxHealth);
		GUIManager.instance.ArmorGUI(Armor, maxArmor);
		dynamiteNumber.GetComponent<UILabel> ().text = dynamiteAmount.ToString();
		yield return null;
	}
	
	// Update is called once per frame
	void Update () {
		
		//if game is in play state
		if (GameManager.instance.State == GameManager.GameState.Playing) {
			//Debug.Log (playerState);
			dodgeTimer += Time.deltaTime;
			if(playerState != Player.PlayerState.Dying)
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
					if(playerState != Player.PlayerState.Dodging)
					{
						currentWeapon.Fire();
					} else{
						Debug.Log ("Can't fire while dodging!");
					}
				}
				
				if(Input.GetKeyDown(KeyCode.Mouse1))
				{
					throwDynamite();
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
					if(GameManager.instance.Upgrades.ShotgunUnlocked == 1)
					{
						changeWeapons(1);
					}
				}
				
				if(Input.GetKeyDown(KeyCode.Alpha3))
				{
					if(GameManager.instance.Upgrades.RifleUnlocked == 1)
					{
						changeWeapons(2);
					}
				}
				if(Input.GetKeyDown(KeyCode.Space))
				{
					if(dodgeTimer >= dodgeCooldown)
					{
						StartCoroutine(dodgeMove());
						dodgeTimer = 0;
					}
				}
			} 
			
		}
		
	}
	
	#region implemented abstract members of Entity
	public override void Move ()
	{
		newY = 0;
		newX = 0;
		isMovingX = false;
		isMovingY = false;
		
		//Buttons
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			if(speedY < maxSpeed)
			{
				speedY += acellerationSpeed * Time.deltaTime;
			}
			isMovingY = true;
		}
		
		if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			if(speedY > - maxSpeed)
			{
				speedY -= acellerationSpeed * Time.deltaTime;
			}
			isMovingY = true;
		}		 
		
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			if(speedX > -maxSpeed)
			{
				speedX -= acellerationSpeed * Time.deltaTime;
			}
			isMovingX = true;
		}
		
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			if(speedX < maxSpeed)
			{
				speedX += acellerationSpeed * Time.deltaTime;
			}
			isMovingX = true;
		}
		
		newY += speedY * currentWeapon.WeaponMoveSpeed;
		newX += speedX * currentWeapon.WeaponMoveSpeed;
		
		checkObstacles ();
		
		if(speedY != 0 || speedX != 0)
		{
			if(playerState != Player.PlayerState.Dodging )
			{
				playerState = Player.PlayerState.Moving;
			}
			anim.SetBool ("isMoving", true);
		}
		transform.parent.Translate(new Vector3( newX, newY, 0) * Time.deltaTime, Space.World);
	}
	
	#endregion
	void LateUpdate ()
	{
		if(!isMovingX)
		{
			if( speedX > 0)
			{
				speedX -= acellerationSpeed * Time.deltaTime;
			} else if(speedX < 0)
			{
				speedX += acellerationSpeed * Time.deltaTime;
			}
			if(speedX > -0.2f && speedX < 0.2f)
			{
				speedX = 0;
			}
		}
		if(!isMovingY)
		{
			if(speedY > 0)
			{
				speedY -= acellerationSpeed * Time.deltaTime;
			} else if(isMovingY == false && speedY < 0)
			{
				speedY += acellerationSpeed * Time.deltaTime;
			}
			if(speedY > -0.2f && speedY < 0.2f)
			{
				speedY = 0;
			}
		}
		if(speedY == 0 && speedX == 0 && playerState != PlayerState.Dying)
		{
			playerState = Player.PlayerState.Idle;
			anim.SetBool ("isMoving", false);
		}
		
	}
	
	public IEnumerator dodgeMove ()
	{
		if(playerState == Player.PlayerState.Moving)
		{
			oldX = speedX;
			oldY = speedY;
			playerState = Player.PlayerState.Dodging;
			speedX = speedX * 3f;
			speedY = speedY * 3f;
			yield return new WaitForSeconds(0.2f);
			StartCoroutine(dodgeMove());
		}else if (playerState == Player.PlayerState.Dodging)
		{
			speedX = oldX;
			speedY = oldY;
			playerState = Player.PlayerState.Moving;
		}
		
	}
	
	public void spawnWeapons()
	{
		availableWeapons = new BaseWeapon[weapons.Length];
		for ( int key = 0 ; key < weapons.Length ; key ++)
		{
			
			BaseWeapon b = (BaseWeapon) GameObject.Instantiate(weapons[key], transform.position, transform.rotation);  
			b.transform.parent = transform;
			availableWeapons[key] = b;
			b.gameObject.SetActive(false);

			if(availableWeapons[key].name == "Revolver(Clone)")
			{
				if(GameManager.instance.Upgrades.RevolverCone == 1)
					availableWeapons[key].bulletDeviationAngle = availableWeapons[key].bulletDeviationAngle / 2 ;
				if(GameManager.instance.Upgrades.RevolverReload == 1)
				{
					//Changing the whole animator speed, because unity doesn't allow for single state speed changes yet. Comming on 5.1
					availableWeapons[key].weaponReloadSpeed = (availableWeapons[key].weaponReloadSpeed / 4) * 3 ;
					availableWeapons[key].GetComponent<Animator>().speed = 1.25f;

				}
			}
			if(availableWeapons[key].name == "Shotgun(Clone)")
			{
				if(GameManager.instance.Upgrades.ShotgunCone == 1)
				{
					availableWeapons[key].bulletDeviationAngle = availableWeapons[key].bulletDeviationAngle / 2 ;
				}
				if(GameManager.instance.Upgrades.ShotgunPellets == 1)
				{
					availableWeapons[key].GetComponent<Shotgun>().bulletsperShot = availableWeapons[key].GetComponent<Shotgun>().bulletsperShot * 2;
				}
			}
			if(availableWeapons[key].name == "Rifle(Clone)")
			{
				if(GameManager.instance.Upgrades.RifleCone == 1)
					availableWeapons[key].bulletDeviationAngle = 0 ;
				if(GameManager.instance.Upgrades.RifleMobility == 1)
				{
					availableWeapons[key].weaponMoveSpeed = 1.1f;
					availableWeapons[key].rotationSpeed = 1.1f;
				}
			}

			
		}
		availableWeapons [0].gameObject.SetActive (true);
		currentWeapon = availableWeapons[0];
		currentWeapon.gameObject.SetActive(true);
	}
	
	public void changeWeapons(int weaponNumber)
	{
		if(currentWeapon.currentAnimation() == "Idle")
		{
			if(weaponNumber == 0)
			{
				GUIManager.instance.ShowRevolverBullets();
			}
			if(weaponNumber == 1)
			{
				GUIManager.instance.ShowShotgunBullets();
			}
			if(weaponNumber == 2)
			{
				GUIManager.instance.ShowRifleBullets();
			}
			currentWeapon.gameObject.SetActive (false);
			currentWeapon = availableWeapons [weaponNumber];
			currentWeapon.gameObject.SetActive (true);
			
		} else
		{
			Debug.Log ("Busy");
		}
		
	}
	
	void throwDynamite ()
	{
		if(dynamiteAmount > 0)
		{
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition - transform.position);
			GameObject dyna = (GameObject) GameObject.Instantiate(dynamite, transform.position, transform.rotation);  
			dyna.GetComponent<Dynamite> ().Destination = mousePosition;
			dynamiteAmount --;
			dynamiteNumber.GetComponent<UILabel> ().text = dynamiteAmount.ToString();
		}
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
			GUIManager.instance.HealthGUI(HitPoints , maxHealth);
			
		}
	}
	
	public override int Armor {
		get {
			return base.Armor;
		}
		set {
			base.Armor = value;
			GUIManager.instance.ArmorGUI(Armor , maxArmor);
		}
	}


	public int GetArmorUpgrades()
	{
		int armorBought = 0;

		if(GameManager.instance.Upgrades.Armor1 == 1)
		{
			armorBought ++;
		}
		if(GameManager.instance.Upgrades.Armor2 == 1)
		{
			armorBought ++;
		}
		if(GameManager.instance.Upgrades.Armor3 == 1)
		{
			armorBought ++;
		}
		if(GameManager.instance.Upgrades.Armor4 == 1)
		{
			armorBought ++;
		}
		return armorBought;

	}

	#region implemented abstract members of Entity
	
	public override void Died ()
	{
		anim.SetTrigger("isDying");
		playerState = PlayerState.Dying;
		GUIManager.instance.ReloadBarActive (false);
		GetComponent<CircleCollider2D>().enabled = false;
	}
	
	#endregion
}
