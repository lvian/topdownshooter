using UnityEngine;
using System.Collections;

public class TestEnemy : Enemy {
	public Player _player;
	public float rotationSpeed;

	#region implemented abstract members of Enemy

	protected override void Init() {
		Debug.Log("Initializing");
		currentWeapon = (BaseWeapon) GameObject.Instantiate(weapons [0] , transform.position , transform.rotation);  
		currentWeapon.transform.parent = transform;
		_player = GameObject.Find("Player").GetComponent<Player>();
		_state = EnemyState.Setup;
		_stance = EnemyStance.Defensive;

	}

	protected override void Setup() {
		Debug.Log("Setting up!");
		base.Start();
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
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		Vector3 euler = q.eulerAngles;
		euler.z -= 90;
		q = Quaternion.Euler(euler);
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
		// dir = (_player.transform.position - transform.position);
		//dir.Normalize();
		//float direction = Vector3.Dot(dir, transform.forward);
			if(currentWeapon.AmountOfBullets > 0)
				_state = EnemyState.Attacking;
			else
				_state = EnemyState.Reloading;
	}

	protected override void Attack(){
		Debug.Log("Attacking!");
		currentWeapon.Fire();
		_state = EnemyState.Searching;
	}

	protected override void Reload(){
		Debug.Log("Reloading!");
		currentWeapon.Reload(transform.gameObject);
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
