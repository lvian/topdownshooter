using UnityEngine;
using System.Collections;

public class Player : Entity {
	//GUI Panels and objects
	public GameObject reloadBar, bulletsNumber, bulletsMax;
	private GameObject[] obstacles;
	private float newY = 0, newX = 0;
	// Use this for initialization
	protected override void Start () {
		spawnWeapon (weapons[0]);
		bulletsNumber.GetComponent<UILabel> ().text = currentWeapon.AmountOfBullets.ToString();
		bulletsMax.GetComponent<UILabel> ().text = currentWeapon.MaxAmountOfBullets.ToString();
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag ("Wall");
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

		newY = 0;
		newX = 0;

		//Buttons
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			newY += Speed * currentWeapon.WeaponMoveSpeed;
		}
		
		if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			newY -= Speed * currentWeapon.WeaponMoveSpeed;
		}		 
		
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			newX -= Speed * currentWeapon.WeaponMoveSpeed;
		}
		
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			newX += Speed * currentWeapon.WeaponMoveSpeed;
		}

		checkObstacles ();

		transform.Translate(new Vector3( newX, newY, 0) * Time.deltaTime, Space.World);
	}
	#endregion


	public void spawnWeapon(BaseWeapon weapon)
	{
		currentWeapon = (BaseWeapon) GameObject.Instantiate(weapon, transform.position, transform.rotation);  
		currentWeapon.transform.parent = transform;
	}

	public void checkObstacles()
	{

		Collider2D[] colX = Physics2D.OverlapCircleAll (new Vector2 ((newX * Time.deltaTime) + transform.position.x , transform.position.y), transform.GetComponent<CircleCollider2D> ().radius);
		Collider2D[] colY = Physics2D.OverlapCircleAll (new Vector2 (transform.position.x , (newY * Time.deltaTime) + transform.position.y), transform.GetComponent<CircleCollider2D> ().radius);
		//Tests X and Y separetely so you can move them individually during collisions
		foreach(Collider2D c in colX )
		{
			if(c.tag == "Wall")
			{
				newX = 0;
			}
		}

		foreach(Collider2D c in colY )
		{
			if(c.tag == "Wall")
			{
				newY = 0;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {

		//Will be used in the future ... I'll  be back!!!!
	}
}
