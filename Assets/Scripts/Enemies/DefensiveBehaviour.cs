using UnityEngine;
using System.Collections;

public class DefensiveBehaviour : AIScript, IEnemyBehaviour {
	private Enemy _enemy;
	private Player _player;
	private Vector3 _covering;
	private bool reverseCircling;
	private bool goBack;

	private float newY = 0, newX = 0;

	#region IEnemyBehaviour implementation
	public void Init (Enemy enemy) {
		//Debug.Log("Initializing");
		_enemy = enemy;
		_enemy.IsMoving = true;
		_enemy.currentWeapon = (BaseWeapon) GameObject.Instantiate(_enemy.weapons[0], _enemy.transform.position, _enemy.transform.rotation);  
		_enemy.currentWeapon.transform.parent = _enemy.transform;
		_player = GameObject.Find("Player").GetComponent<Player>();
		_enemy.enemyState = Enemy.EnemyState.Setup;
		_enemy.enemyStance = Enemy.EnemyStance.Defensive;
		reverseCircling = false;
		_covering = GetNearestCovering(_enemy.transform, _player.transform);
	}
	
	public void Setup () {
		//Debug.Log("Setting up!!");
		_enemy.Start();
		_enemy.enemyState = Enemy.EnemyState.Searching;
	}
	
	public void Search ()
	{
		//Debug.Log("Searching!");
		//bool[] collisions = CheckCollisions(_enemy.transform);
		float distance = Vector3.Distance(_enemy.transform.position, _player.transform.position);
		
		Vector3 vectorToTarget = _player.transform.position - _enemy.transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		Vector3 euler = q.eulerAngles;
		euler.z -= 90;
		q = Quaternion.Euler(euler);
		_enemy.transform.parent.rotation = Quaternion.Slerp(_enemy.transform.parent.rotation, q, Time.deltaTime * _enemy.rotationSpeed);
		if(distance >= 4){
			_enemy.enemyState = Enemy.EnemyState.Moving;
		}
		else{
			if(_enemy.currentWeapon.AmountOfBullets > 0){
				_enemy.enemyState = Enemy.EnemyState.Attacking;
				AddTimer(_enemy.shootDelay, Enemy.EnemyState.Attacking);
			}
			else
				_enemy.enemyState = Enemy.EnemyState.Reloading;
		}
	}
	
	public void Move() {
		//Debug.Log("Moving!");
		_covering = GetNearestCovering(_enemy.transform, _player.transform);
		float distance = Vector3.Distance(_enemy.transform.position, _covering);
		if(distance > 1f) {
			Quaternion oldQ = _enemy.transform.rotation;
			Vector3 diff = _covering - _enemy.transform.position;
			diff.Normalize();
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			_enemy.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
			bool[] collisions = CheckCollisions(_enemy.transform);
			newY = _enemy.currentWeapon.WeaponMoveSpeed * _enemy.Speed * Time.deltaTime;
			newX = _enemy.currentWeapon.WeaponMoveSpeed * _enemy.Speed * Time.deltaTime;

			//newY = newPos.y;
			//newX = newPos.x;
			
			if(collisions[0] || collisions[1] || collisions[2]){ // detect collision in front
				newY = 0;
				if(collisions[3]){ // detect collision on the right
					newX = 0;
					if(!collisions[7]) // detect collision on the left
						reverseCircling = true;
					else{
						if(!collisions[5]) // detect collision behind
							goBack = true;
					}
				}
			}

			if(reverseCircling)
				newX = -_enemy.currentWeapon.WeaponMoveSpeed * Time.deltaTime;
			if(goBack)
				newX = -_enemy.currentWeapon.WeaponMoveSpeed * Time.deltaTime;

			_enemy.transform.parent.Translate(new Vector2(newX, newY));

			_enemy.transform.rotation = oldQ;
		}
		if(_enemy.currentWeapon.AmountOfBullets > 0){
			_enemy.enemyState = Enemy.EnemyState.Attacking;
			AddTimer(_enemy.shootDelay, Enemy.EnemyState.Attacking);
		}
		else
			_enemy.enemyState = Enemy.EnemyState.Reloading;
	}
	
	public void Attack () {
		//Debug.Log("Attacking!");
		if(IsDelayTimeElapsed(Enemy.EnemyState.Attacking)){
			float distance = Vector3.Distance(_enemy.transform.position, _player.transform.position);
			if(CanSeeTarget(_enemy.transform, _player.transform) && distance < _enemy.maxShootingDistance){
				_enemy.currentWeapon.Fire();
			}
		}
		_enemy.enemyState = Enemy.EnemyState.Searching;
	}
	
	public void Reload () {
		//Debug.Log("Reloading!");
		_enemy.currentWeapon.Reload(_enemy.transform.gameObject);
		_enemy.enemyState = Enemy.EnemyState.Attacking;
	}
	public void Dodge ()
	{
		//Debug.Log("Dodging");
	}
	public void Die ()
	{
		_enemy.transform.GetComponent<CircleCollider2D> ().enabled = false;
	}
	#endregion
	
}
