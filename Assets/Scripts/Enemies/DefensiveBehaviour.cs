﻿using UnityEngine;
using System.Collections;

public class DefensiveBehaviour : AIScript, IEnemyBehaviour {
	private Enemy enemy;
	private Player player;
	private Vector3 _covering;
	private bool reverseCircling;
	private bool goBack;
	private Collider2D collider;
	private GameObject[] obstacles;
	private Timer goBackTimer;
	private Timer reverseCirclingTimer;
	private float errorMargin = 0.01f;

	private float newY = 0, newX = 0;

	#region IEnemyBehaviour implementation
	public void Init (Enemy enemy) {
		//Debug.Log("Initializing");
		this.enemy = enemy;
		this.enemy.IsMoving = true;
		this.enemy.currentWeapon = (BaseWeapon) GameObject.Instantiate(enemy.weapons[0], enemy.transform.position, enemy.transform.rotation);  
		this.enemy.currentWeapon.transform.parent = enemy.transform;
		player = GameObject.Find("Player").GetComponent<Player>();
		collider = this.enemy.GetComponent<Collider2D>();
		this.enemy.enemyState = Enemy.EnemyState.Setup;
		this.enemy.enemyStance = Enemy.EnemyStance.Defensive;
		reverseCircling = false;
		reverseCirclingTimer = new Timer(Random.Range(5f, 15f));
		obstacles = GameObject.FindGameObjectsWithTag("Wall");
		_covering = GetNearestCovering(this.enemy.transform, player.transform, obstacles);
	}
	
	public void Setup () {
		//Debug.Log("Setting up!!");
		enemy.Start();
		enemy.enemyState = Enemy.EnemyState.Searching;
	}
	
	public void Search ()
	{
		//Debug.Log("Searching!");
		//bool[] collisions = CheckCollisions(_enemy.transform);
		float distance = Vector3.Distance(enemy.transform.position, player.transform.position);
		
		Vector3 vectorToTarget = player.transform.position - enemy.transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		Vector3 euler = q.eulerAngles;
		euler.z -= 90;
		q = Quaternion.Euler(euler);
		enemy.transform.parent.rotation = Quaternion.Slerp(enemy.transform.parent.rotation, q, Time.deltaTime * enemy.rotationSpeed);
		if(distance >= 4){
			enemy.enemyState = Enemy.EnemyState.Moving;
		}
		else{
			if(enemy.currentWeapon.AmountOfBullets > 0){
				enemy.enemyState = Enemy.EnemyState.Attacking;
				AddTimer(enemy.shootDelay, Enemy.EnemyState.Attacking);
			}
			else
				enemy.enemyState = Enemy.EnemyState.Reloading;
		}
	}
	
	public void Move() {
		//Debug.Log("Moving!");
		_covering = GetNearestCovering(enemy.transform, player.transform, obstacles);
		float distance = Vector3.Distance(enemy.transform.position, _covering);
		//Debug.Log("Distance: " + distance );
		LayerMask layerMask = ((1 << 10) | (1 << 9));
		if(distance > 1f) {
			if(goBackTimer != null){
				if(goBackTimer.IsElapsed){
					goBack = !goBack;
					goBackTimer = null;
				}
			}
			if(reverseCirclingTimer.IsElapsed){
				reverseCircling = !reverseCircling;
				reverseCirclingTimer = new Timer(Random.Range(5f, 15f));
				reverseCirclingTimer.Reset();
			}

			Quaternion oldQ = enemy.transform.parent.rotation;
			Vector3 diff = _covering - enemy.transform.parent.position;
			diff.Normalize();
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			enemy.transform.parent.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

			if(reverseCircling)
				newX = -enemy.currentWeapon.WeaponMoveSpeed * Time.deltaTime;
			else
				newX = enemy.currentWeapon.WeaponMoveSpeed * enemy.Speed * Time.deltaTime;
			if(goBack)
				newX = -enemy.currentWeapon.WeaponMoveSpeed * Time.deltaTime;
			else
				newY = enemy.currentWeapon.WeaponMoveSpeed * enemy.Speed * Time.deltaTime;

			Vector3 oldPos = enemy.transform.parent.position;
			enemy.transform.parent.Translate(new Vector2(newX, newY));

			collider.enabled = false;
			Collider2D[] colX = Physics2D.OverlapCircleAll (
				new Vector2 (enemy.transform.parent.position.x, oldPos.y), 
				enemy.transform.GetComponent<CircleCollider2D>().radius, layerMask);
			Collider2D[] colY = Physics2D.OverlapCircleAll (
				new Vector2 (oldPos.x , enemy.transform.parent.position.y),
				enemy.transform.GetComponent<CircleCollider2D>().radius, layerMask);
			collider.enabled = true;

			if(colX.Length != 0){
				enemy.transform.parent.Translate(new Vector2(-(newX/Mathf.Abs(newX) * (Mathf.Abs(newX) + errorMargin)), 0f));
				newX = 0;
				reverseCircling = !reverseCircling;
			}
			
			if(colY.Length != 0){
				enemy.transform.parent.Translate(new Vector2(0f, -(newY/Mathf.Abs(newY) * (Mathf.Abs(newY) + errorMargin))));
				newY = 0;
				goBackTimer = new Timer(2f);
				goBack = !goBack;
			}
			
			if(newX == 0 && newY == 0){
				enemy.anim.SetBool ("isMoving", false);
			}
			else{
				enemy.anim.SetBool ("isMoving", true);
			}
			enemy.transform.parent.rotation = oldQ;
		}
		else
			enemy.anim.SetBool ("isMoving", false);
		if(enemy.currentWeapon.AmountOfBullets > 0){
			enemy.enemyState = Enemy.EnemyState.Attacking;
			AddTimer(enemy.shootDelay, Enemy.EnemyState.Attacking);
		}
		else
			enemy.enemyState = Enemy.EnemyState.Reloading;
	}
	
	public void Attack () {
		//Debug.Log("Attacking!");
		if(IsElapsed(Enemy.EnemyState.Attacking)){
			float distance = Vector3.Distance(enemy.transform.position, player.transform.position);
			if(CanSeeTarget(enemy.transform, player.transform) && distance < enemy.maxShootingDistance){
				if(enemy.throwsDynamite == true && enemy.DynamiteThrowTimer <= 0)
				{
					//throws dynamite
					GameObject dyna = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Weapons/Dynamite/Dynamite") , enemy.transform.position, enemy.transform.rotation);  
					dyna.GetComponent<Dynamite> ().Destination = player.transform.position;
					enemy.DynamiteThrowTimer = enemy.dynamiteThrowInterval;
				}
				else {
					enemy.currentWeapon.Fire();
				}
			}
		}
		enemy.enemyState = Enemy.EnemyState.Searching;
	}
	
	public void Reload () {
		//Debug.Log("Reloading!");
		enemy.currentWeapon.Reload();
		enemy.enemyState = Enemy.EnemyState.Attacking;
	}

	public void Dodge ()
	{
		//Debug.Log("Dodging");
	}

	public void Die ()
	{
		enemy.transform.GetComponent<CircleCollider2D> ().enabled = false;
	}
	#endregion
	
}
