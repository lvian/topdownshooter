using UnityEngine;
using System.Collections;

public class OffensiveBehaviour : AIScript, IEnemyBehaviour {
	private Enemy _enemy;
	private Player _player;
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
	}

	public void Setup () {
		//Debug.Log("Setting up!");
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

	/*public void Move() {
		//Debug.Log("Moving!");
		float distance = Vector3.Distance(_enemy.transform.position, _player.transform.position);
		if(distance > 4) {
			bool[] collisions = CheckCollisions(_enemy.transform);
			newY = _enemy.currentWeapon.WeaponMoveSpeed * _enemy.Speed * Time.deltaTime;
			newX = _enemy.currentWeapon.WeaponMoveSpeed * _enemy.Speed * Time.deltaTime;
			

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
		}
		else {
			bool[] collisions = CheckCollisions(_enemy.transform);
			newY = 0;
			newX = _enemy.currentWeapon.WeaponMoveSpeed * Time.deltaTime;

			if(collisions[3]){ // detect collision on the right
				newX = 0;
				if(!collisions[7]) // detect collision on the left
					reverseCircling = true;
			}

			if(reverseCircling)
				newX = -_enemy.currentWeapon.WeaponMoveSpeed * Time.deltaTime;
		}
		if(_enemy.currentWeapon.AmountOfBullets > 0){
			_enemy.enemyState = Enemy.EnemyState.Attacking;
			AddTimer(_enemy.shootDelay, Enemy.EnemyState.Attacking);
		}
		else
			_enemy.enemyState = Enemy.EnemyState.Reloading;
	}*/

	public void Move(){
		float distance = Vector3.Distance(_enemy.transform.position, _player.transform.position);
		LayerMask layerMask = ((1 << 9) | (1 << 8));
		if(distance > 4) {
			if(goBack)
				newY = -_enemy.currentWeapon.WeaponMoveSpeed * _enemy.Speed * Time.deltaTime;
			else
				newY = _enemy.currentWeapon.WeaponMoveSpeed * _enemy.Speed * Time.deltaTime;
			if(reverseCircling)
				newX = -_enemy.currentWeapon.WeaponMoveSpeed * _enemy.Speed * Time.deltaTime;
			else
				newX = _enemy.currentWeapon.WeaponMoveSpeed * _enemy.Speed * Time.deltaTime;

			Collider2D[] colX = Physics2D.OverlapCircleAll (
				new Vector2 ((newX * Time.deltaTime) + _enemy.transform.position.x, _enemy.transform.position.y), 
				_enemy.transform.GetComponent<CircleCollider2D> ().radius, layerMask);

			Collider2D[] colY = Physics2D.OverlapCircleAll (
				new Vector2 (_enemy.transform.position.x , (newY * Time.deltaTime) + _enemy.transform.position.y),
				_enemy.transform.GetComponent<CircleCollider2D> ().radius, layerMask);

			if(colX.Length != 0){
				newX = 0;
			}

			if(colY.Length != 0){
				newY = 0;
			}

			if(newX == 0 && newY == 0){
				float nX = 0f, nY = 0f;
				if(goBack)
					nY = _enemy.currentWeapon.WeaponMoveSpeed * _enemy.Speed * Time.deltaTime;
				else
					nY = -_enemy.currentWeapon.WeaponMoveSpeed * _enemy.Speed * Time.deltaTime;
				if(reverseCircling)
					newX = _enemy.currentWeapon.WeaponMoveSpeed * _enemy.Speed * Time.deltaTime;
				else
					newX = -_enemy.currentWeapon.WeaponMoveSpeed * _enemy.Speed * Time.deltaTime;

				Collider2D[] cX = Physics2D.OverlapCircleAll (
					new Vector2 ((nX * Time.deltaTime) + _enemy.transform.position.x, _enemy.transform.position.y), 
					_enemy.transform.GetComponent<CircleCollider2D> ().radius, layerMask);
				
				Collider2D[] cY = Physics2D.OverlapCircleAll (
					new Vector2 (_enemy.transform.position.x , (nY * Time.deltaTime) + _enemy.transform.position.y),
					_enemy.transform.GetComponent<CircleCollider2D> ().radius, layerMask);

				if(cX.Length == 0){
					reverseCircling = !reverseCircling;
				}
				else if(colY.Length == 0){
					goBack = !goBack;
				}
			}

			if(reverseCircling)
				newX = -_enemy.currentWeapon.WeaponMoveSpeed * Time.deltaTime;
			if(goBack)
				newY = -_enemy.currentWeapon.WeaponMoveSpeed * Time.deltaTime;
			
			Debug.Log(newX + " " + newY);
			_enemy.transform.parent.Translate(new Vector2(newX, newY));
		}
		else {
			newY = 0;
			if(reverseCircling)
				newX = _enemy.currentWeapon.WeaponMoveSpeed * Time.deltaTime;
			else
				newX = - _enemy.currentWeapon.WeaponMoveSpeed * Time.deltaTime;

			Collider2D[] colX = Physics2D.OverlapCircleAll (
				new Vector2 ((newX * Time.deltaTime) + _enemy.transform.position.x, _enemy.transform.position.y), 
				_enemy.transform.GetComponent<CircleCollider2D> ().radius, layerMask);

			if(colX.Length != 0){
				Collider2D[] cX = Physics2D.OverlapCircleAll (
					new Vector2 ((newX * Time.deltaTime) + _enemy.transform.position.x, _enemy.transform.position.y), 
					_enemy.transform.GetComponent<CircleCollider2D> ().radius, layerMask);

				if(cX.Length == 0){
					reverseCircling = !reverseCircling;
				}
			}
			
			if(reverseCircling)
				newX = -_enemy.currentWeapon.WeaponMoveSpeed * Time.deltaTime;
		}

		// transitions
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
		////Debug.Log("Dodging");
	}
	public void Die ()
	{
		_enemy.transform.GetComponent<CircleCollider2D> ().enabled = false;
	}
	#endregion
}

