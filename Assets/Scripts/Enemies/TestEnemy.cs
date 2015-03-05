using UnityEngine;
using System.Collections;

public class TestEnemy : Enemy {
	public Player _player;
	public float rotationSpeed;
	public BaseWeapon[] weapons;
	public BaseWeapon currentWeapon;
	public GameObject leftHand, rightHand;

	#region implemented abstract members of Enemy

	protected override void Init() {
		Debug.Log("Initializing");
		currentWeapon = (BaseWeapon) GameObject.Instantiate(weapons [0] , rightHand.transform.position , rightHand.transform.rotation);  
		currentWeapon.transform.parent = rightHand.transform;
		_player = GameObject.Find("Player").GetComponent<Player>();
		_state = EnemyState.Setup;
	}

	protected override void Setup() {
		Debug.Log("Setting up!");
		_state = EnemyState.Searching;
	}

	protected override void Search() {
		Debug.Log("Searching!");
		float distance = Vector3.Distance(transform.position, _player.transform.position);

		Vector3 dir = (transform.position - _player.transform.position);
		dir.Normalize();

		float direction = Vector3.Dot(dir, transform.forward);

		Vector3 vectorToTarget = _player.transform.position - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.left);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);

		//Debug.Log(distance + " " + direction);
		//if(direction > 0){
			if(distance > 4){
				_state = EnemyState.Moving;
			}
			else{
				if(currentWeapon.AmountOfBullets > 0)
					_state = EnemyState.Attacking;
				else
					_state = EnemyState.Reloading;
			}
		//}
	}

	protected override void Move() {
		Debug.Log("Moving!");
		float distance = Vector3.Distance(transform.position, _player.transform.position);
		
		Vector3 dir = (_player.transform.position - transform.position).normalized;
		
		float direction = Vector3.Dot(dir, transform.forward);
		if(distance > 4){
			_state = EnemyState.Searching;
		}
		else{
			if(currentWeapon.AmountOfBullets > 0)
				_state = EnemyState.Attacking;
			else
				_state = EnemyState.Reloading;
		}
		//transform.Translate(new Vector3(0f, 1f * Time.deltaTime, 0f));
	}

	protected override void Attack(){
		Debug.Log("Attacking!");
		currentWeapon.Fire();
		_state = EnemyState.Searching;
	}

	protected override void Reload(){
		Debug.Log("Reloading!");
		currentWeapon.amountOfBullets = 6;
		_state = EnemyState.Attacking;
	}

	protected override void Dodge() {
		Debug.Log("Dodging");
	}

	protected override void Die() {
		Debug.Log("Dying!");
	}

	#endregion
}
