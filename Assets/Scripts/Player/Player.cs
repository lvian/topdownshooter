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
	public GameObject dynamite;
	public float maxSpeed, acellerationSpeed, dodgeCooldown;
	public AudioClip[] steps;
	public AudioClip dodgeSound;

	protected GameObject leftJoystick, rightJoystick;
	protected BaseWeapon[] availableWeapons;
	protected PlayerState playerState;
	protected int dynamiteAmount;
	private GameObject[] obstacles;
	private float newY = 0, newX = 0, speedY = 0, speedX = 0, oldX, oldY;
	private bool isMovingX, isMovingY;
	private float maxHealth, maxArmor, dodgeTimer, stepSoundTimer;
	private ParticleSystem dustEmitter;

	// Use this for initialization
	public IEnumerator Start () {
		EventDelegate.Add (GameObject.Find("Dodge Background").GetComponent<UIButton>().onClick, Dodge);
		EventDelegate.Add (GameObject.Find("Reload Button").GetComponent<UIButton>().onClick, Reload);

		EventDelegate changeRevolver = new EventDelegate(this, "changeWeapons");
		changeRevolver.parameters[0].value = 0;
		GameObject.Find("Revolver Button").GetComponent<UIButton>().onClick.Add( changeRevolver );

		EventDelegate changeShotgun = new EventDelegate(this, "changeWeapons");
		changeShotgun.parameters[0].value = 1;
		GameObject.Find("Shotgun Button").GetComponent<UIButton>().onClick.Add( changeShotgun );

		EventDelegate changeRifle = new EventDelegate(this, "changeWeapons");
		changeRifle.parameters[0].value = 2;
		GameObject.Find("Rifle Button").GetComponent<UIButton>().onClick.Add( changeRifle );

		EventDelegate changeDualRevolver = new EventDelegate(this, "changeWeapons");
		changeDualRevolver.parameters[0].value = 3;
		GameObject.Find("Dual Revolvers Button").GetComponent<UIButton>().onClick.Add( changeDualRevolver );

		if(GameManager.instance.Upgrades.Dodge == 1)
		{
			dodgeCooldown = dodgeCooldown / 2; 
		}
		dodgeTimer = 0;
		stepSoundTimer = 0;
		Armor = GetArmorUpgrades ();
		maxArmor = GetArmorUpgrades();
		maxHealth = HitPoints;
		isMovingX = false;
		isMovingY = false;
		playerState = PlayerState.Idle;
		if (GameManager.instance.Upgrades.ExtraDynamite == 1) {
			dynamiteAmount = 2;
		}else
		{
			dynamiteAmount = 1;
		}
		spawnWeapons ();
		dustEmitter = transform.FindChild("DustEmmiter").GetComponent<ParticleSystem>();
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag ("Wall");
		leftJoystick = GameObject.Find ("Left Joystick"); 
		rightJoystick = GameObject.Find ("Right Joystick");
		InitHumanoid();
		GUIManager.instance.HealthGUI(HitPoints, maxHealth);
		GUIManager.instance.ArmorGUI(Armor, maxArmor);
		GUIManager.instance.UpdateDynamite(dynamiteAmount);
		yield return null;
	}

	// Update is called once per frame
	void Update () {

		//if game is in play state
		if (GameManager.instance.State == GameManager.GameState.Playing) {
			//Calculates cooldown and sends to GUI
			dodgeTimer -= Time.deltaTime;

			if(dodgeTimer >= 0 && dodgeTimer < dodgeCooldown)
			{
				float sliderPercentage = 1 / dodgeCooldown;
				GUIManager.instance.UpdateDodgeCooldown(dodgeTimer * sliderPercentage);
			}

			if(playerState != Player.PlayerState.Dying)
			{
				//Turns the player towards the current mouse position
				if(UICamera.hoveredObject.name == "GUI")
				{

				}
				Move();

				Fire();
				

				
				if(Input.GetKeyDown(KeyCode.Mouse1))
				{
					if(GameManager.instance.Upgrades.DynamiteUnlocked == 1)
					{
						throwDynamite();
					}
				}
				

				
				if(Input.GetKeyDown(KeyCode.R))
				{
					currentWeapon.Reload();
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

				if(Input.GetKeyDown(KeyCode.Alpha4))
				{
					if(GameManager.instance.Upgrades.DualRevolverUnlocked == 1)
					{
						changeWeapons(3);
					}
				}
				if(Input.GetKeyDown(KeyCode.Escape))
				{
					GameManager.instance.EscPause();
				}
				if(Input.GetKeyDown(KeyCode.Space))
				{
					if(dodgeTimer <= 0)
					{
						StartCoroutine(dodgeMove());
						dodgeTimer = dodgeCooldown;
						GUIManager.instance.UpdateDodgeCooldown(1);

					}
				}
			} 
			
		} else if(GameManager.instance.State == GameManager.GameState.Paused)
		{
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				GameManager.instance.EscPause();
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

		#if UNITY_ANDROID
			if( leftJoystick.GetComponent<UIJoystick>().joyStickPosY > 0)
			{
				if(speedY < maxSpeed)
				{
					speedY += acellerationSpeed * Time.deltaTime;
				}
				isMovingY = true;
			}
			
			if( leftJoystick.GetComponent<UIJoystick>().joyStickPosY < 0)
			{
				if(speedY > - maxSpeed)
				{
					speedY -= acellerationSpeed * Time.deltaTime;
				}
				isMovingY = true;
			}		 
			
			if(leftJoystick.GetComponent<UIJoystick>().joyStickPosX < 0)
			{
				if(speedX > -maxSpeed)
				{
					speedX -= acellerationSpeed * Time.deltaTime;
				}
				isMovingX = true;
			}
			
			if(leftJoystick.GetComponent<UIJoystick>().joyStickPosX > 0)
			{
				if(speedX < maxSpeed)
				{
					speedX += acellerationSpeed * Time.deltaTime;
				}
				isMovingX = true;
			}
		#else
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
		#endif

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
			stepSoundTimer += Time.deltaTime;
			if(stepSoundTimer >= 0.65f)
			{
				//Choose a random pitch to play back our clip at between our high and low pitch ranges.
				float randomPitch = Random.Range(lowPitchRange, highPitchRange);
				audioSource.pitch = randomPitch;
				audioSource.PlayOneShot (steps[Random.Range(0,steps.Length)]);
				hitSoundCooldown.Reset();
				stepSoundTimer = 0;
			}
		}
		transform.parent.Translate(new Vector3( newX, newY, 0) * Time.deltaTime, Space.World);
	}

	public void Fire()
	{
		if(rightJoystick.GetComponent<UIJoystick>().joyStickPosX != 0)
		{

			float rot_z = Mathf.Atan2(rightJoystick.GetComponent<UIJoystick>().joyStickPosY , rightJoystick.GetComponent<UIJoystick>().joyStickPosX) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.Euler(0f, 0f, rot_z - 90);
			transform.rotation = Quaternion.RotateTowards(transform.rotation , q , Time.deltaTime * 250 * currentWeapon.RotationSpeed);

			#if UNITY_ANDROID
			if(Quaternion.Angle(transform.rotation, q) <= 2)
			{
				if(playerState != Player.PlayerState.Dodging)
				{
					currentWeapon.Fire();
				} else{
					Debug.Log ("Can't fire while dodging!");
				}
			}
			#else
			if(UICamera.hoveredObject.name == "GUI")
			{
				if(playerState != Player.PlayerState.Dodging)
				{
					currentWeapon.Fire();
				} else{
					Debug.Log ("Can't fire while dodging!");
				}
			}
			#endif
		}
		
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

	public void Dodge()
	{
		if(dodgeTimer <= 0)
		{
			StartCoroutine(dodgeMove());
			dodgeTimer = dodgeCooldown;
			GUIManager.instance.UpdateDodgeCooldown(1);
			
		}
	}

	public void Reload()
	{
			currentWeapon.Reload();
	}
	
	public IEnumerator dodgeMove ()
	{
		if(playerState == Player.PlayerState.Moving)
		{
			dustEmitter.emissionRate = 15;
			float randomPitch = Random.Range(lowPitchRange, highPitchRange);
			audioSource.pitch = randomPitch;
			audioSource.PlayOneShot (dodgeSound);

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
			dustEmitter.emissionRate = 2;
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
			b.ReloadGUI();
			if(availableWeapons[key].name == "Revolver(Clone)")
			{
				availableWeapons[key].coneBase.gameObject.SetActive(true);
				availableWeapons[key].coneBase.GetComponent<SpriteRenderer>().sprite =  Resources.Load("Sprites/15 Cone", typeof(Sprite)) as Sprite;
				if(GameManager.instance.Upgrades.RevolverCone == 1)
				{
					availableWeapons[key].bulletDeviationAngle = availableWeapons[key].bulletDeviationAngle / 2 ;
					availableWeapons[key].coneBase.GetComponent<SpriteRenderer>().sprite =    Resources.Load("Sprites/5 Cone", typeof(Sprite)) as Sprite;
					//availableWeapons[key].coneUpgraded.gameObject.SetActive(true);
				}
				if(GameManager.instance.Upgrades.RevolverReload == 1)
				{
					//Changing the whole animator speed, because unity doesn't allow for single state speed changes yet. Comming on 5.1
					availableWeapons[key].weaponReloadSpeed = (availableWeapons[key].weaponReloadSpeed / 2)  ;
					availableWeapons[key].GetComponent<Animator>().speed = 1.5f;

				}
			}
			if(availableWeapons[key].name == "Shotgun(Clone)")
			{
				availableWeapons[key].coneBase.gameObject.SetActive(true);
				availableWeapons[key].coneBase.GetComponent<SpriteRenderer>().sprite =    Resources.Load("Sprites/50 Cone", typeof(Sprite)) as Sprite;
				if(GameManager.instance.Upgrades.ShotgunCone == 1)
				{
					availableWeapons[key].bulletDeviationAngle = (availableWeapons[key].bulletDeviationAngle / 5) * 3 ;
					availableWeapons[key].coneBase.GetComponent<SpriteRenderer>().sprite =    Resources.Load("Sprites/30 Cone", typeof(Sprite)) as Sprite;
				}
				if(GameManager.instance.Upgrades.ShotgunPellets == 1)
				{
					availableWeapons[key].GetComponent<Shotgun>().bulletsperShot = availableWeapons[key].GetComponent<Shotgun>().bulletsperShot * 2;
				}
			}
			if(availableWeapons[key].name == "Rifle(Clone)")
			{
				availableWeapons[key].coneBase.gameObject.SetActive(true);
				if(GameManager.instance.Upgrades.RifleMobility == 1)
				{
					availableWeapons[key].weaponMoveSpeed = 1.1f;
					availableWeapons[key].rotationSpeed = 1.1f;
				}
			}

			if(availableWeapons[key].name == "Dual Revolver(Clone)")
			{
				availableWeapons[key].coneBase.gameObject.SetActive(true);
				DualRevolver dr = (DualRevolver) availableWeapons[key];
				dr.coneBaseLeft.gameObject.SetActive(true);
				availableWeapons[key].coneBase.GetComponent<SpriteRenderer>().sprite =  Resources.Load("Sprites/15 Cone", typeof(Sprite)) as Sprite;
				dr.coneBaseLeft.GetComponent<SpriteRenderer>().sprite =  Resources.Load("Sprites/15 Cone", typeof(Sprite)) as Sprite;
				if(GameManager.instance.Upgrades.RevolverCone == 1)
				{  
					availableWeapons[key].bulletDeviationAngle = availableWeapons[key].bulletDeviationAngle / 2 ;
					availableWeapons[key].coneBase.GetComponent<SpriteRenderer>().sprite =  Resources.Load("Sprites/5 Cone", typeof(Sprite)) as Sprite;
					dr.coneBaseLeft.GetComponent<SpriteRenderer>().sprite =  Resources.Load("Sprites/5 Cone", typeof(Sprite)) as Sprite;
				}

			}

			
		}
		availableWeapons [0].gameObject.SetActive (true);
		GUIManager.instance.ShowRevolverBullets ();
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
			if(weaponNumber == 3)
			{
				GUIManager.instance.ShowDualRevolverBullets();
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
			GUIManager.instance.UpdateDynamite(dynamiteAmount);
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
		GameManager.instance.Defeat ();
	}
	
	#endregion

	public int DynamiteAmount {
		get {
			return dynamiteAmount;
		}
		set {
			dynamiteAmount = value;
		}
	}

	public PlayerState GetPlayerState {
		get {
			return playerState;
		}
	}

}
