using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private float weaponMoveSpeed, weaponShootDelay; //Needs to get this values from weapons later on
	private float lastShot;

	// Use this for initialization
	void Start () {
		weaponMoveSpeed = 1;
		weaponShootDelay = 0.5f;
		lastShot = 0;
	}
	
	// Update is called once per frame
	void Update () {

		//if game is in play state
		if (true) {

			Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			diff.Normalize();
			
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

			if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
			{
				transform.Translate(new Vector3(0, weaponMoveSpeed, 0) * Time.deltaTime, Space.World);
			}
			
			if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
			{
				transform.Translate(new Vector3(0, - weaponMoveSpeed, 0) * Time.deltaTime, Space.World);
			}		 

			if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			{
				transform.Translate(new Vector3(-weaponMoveSpeed, 0, 0) * Time.deltaTime, Space.World);
			}
			
			if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
				transform.Translate(new Vector3(weaponMoveSpeed, 0, 0) * Time.deltaTime, Space.World);
			}

			if(Input.GetKey(KeyCode.Mouse0))
			{
				if(lastShot >= weaponShootDelay )
				{
					GameObject bt =  (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/RevolverBullet") , transform.position , transform.rotation); 

					lastShot = 0;
				} 

			}

			lastShot += Time.deltaTime;				

		}

	
	}
}
