using UnityEngine;
using System.Collections;

public class OffensiveBehaviour : AIScript, IEnemyBehaviour {
	private Enemy _enemy;
	private Player _player;

	private float newY = 0, newX = 0;

	#region IEnemyBehaviour implementation
	public void Init (Enemy enemy) {
		Debug.Log("Initializing");
		_enemy = enemy;
		_enemy.currentWeapon = (BaseWeapon) GameObject.Instantiate(_enemy.weapons[0], _enemy.transform.position, _enemy.transform.rotation);  
		_enemy.currentWeapon.transform.parent = _enemy.transform;
		_player = GameObject.Find("Player").GetComponent<Player>();
		_enemy.enemyState = Enemy.EnemyState.Setup;
		_enemy.enemyStance = Enemy.EnemyStance.Defensive;
	}

	public void Setup () {
		Debug.Log("Setting up!");
		_enemy.Start();
		_enemy.enemyState = Enemy.EnemyState.Searching;
	}

	public void Search ()
	{
		Debug.Log("Searching!");
		float distance = Vector3.Distance(_enemy.transform.position, _player.transform.position);
		
		Vector3 vectorToTarget = _player.transform.position - _enemy.transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		Vector3 euler = q.eulerAngles;
		euler.z -= 90;
		q = Quaternion.Euler(euler);
		_enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, q, Time.deltaTime * _enemy.rotationSpeed);
		if(distance >= 4){
			_enemy.enemyState = Enemy.EnemyState.Moving;
		}
		else{
			if(_enemy.currentWeapon.AmountOfBullets > 0)
				_enemy.enemyState = Enemy.EnemyState.Attacking;
			else
				_enemy.enemyState = Enemy.EnemyState.Reloading;
		}
	}

	public void Move() {
		Debug.Log("Moving!");
		float distance = Vector3.Distance(_enemy.transform.position, _player.transform.position);
		if(distance > 4) {
			newY = _enemy.currentWeapon.WeaponMoveSpeed * Time.deltaTime;
			newX = _enemy.currentWeapon.WeaponMoveSpeed * Time.deltaTime;

			checkObstacles();
			_enemy.transform.Translate(new Vector2(newX, newY));
		}
		else {
			newY = 0;
			newX = _enemy.currentWeapon.WeaponMoveSpeed * Time.deltaTime;

			checkObstacles();
			_enemy.transform.Translate(new Vector2(newX, newY));
		}
		if(_enemy.currentWeapon.AmountOfBullets > 0)
			_enemy.enemyState = Enemy.EnemyState.Attacking;
		else
			_enemy.enemyState = Enemy.EnemyState.Reloading;
	}

	public void checkObstacles() {	
		Collider2D[] colX = Physics2D.OverlapCircleAll (new Vector2 ((newX * Time.deltaTime) + _enemy.transform.position.x, _enemy.transform.position.y), _enemy.transform.GetComponent<CircleCollider2D> ().radius);
		Collider2D[] colY = Physics2D.OverlapCircleAll (new Vector2 (_enemy.transform.position.x , (newY * Time.deltaTime) + _enemy.transform.position.y), _enemy.transform.GetComponent<CircleCollider2D> ().radius);
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

	public void Attack () {
		Debug.Log("Attacking!");
		if(CanSeeTarget(_enemy.transform, _player.transform))
			_enemy.currentWeapon.Fire();
		_enemy.enemyState = Enemy.EnemyState.Searching;
	}

	public void Reload () {
		Debug.Log("Reloading!");
		_enemy.currentWeapon.Reload(_enemy.transform.gameObject);
		_enemy.enemyState = Enemy.EnemyState.Attacking;
	}
	public void Dodge ()
	{
		//Debug.Log("Dodging");
	}
	public void Die ()
	{
		//Debug.Log("Dying");
	}
	#endregion
}

