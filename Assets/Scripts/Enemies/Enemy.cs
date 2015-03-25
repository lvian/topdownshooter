using UnityEngine;
using System.Collections;

public abstract class Enemy : Entity{
	public enum EnemyState {
		Init,
		Setup,
		Idle,
		Searching,
		Moving,
		Attacking,
		Reloading,
		Dodging,
		Dying
	}

	public enum EnemyStance{
		Offensive,
		Defensive,
		Aggresive
	}

	public Player player;
	public float rotationSpeed;
	public float maxShootingDistance;

	public EnemyState enemyState = EnemyState.Init;
	public EnemyStance enemyStance = EnemyStance.Offensive;
	public IEnemyBehaviour enemyBehaviour = new OffensiveBehaviour();


	public IEnumerator Start () {
		while(GameManager.instance.State == GameManager.GameState.Playing){
			switch(enemyState) {
			case EnemyState.Init:
				Init();
				break;
			case EnemyState.Setup:
				Setup();
				break;
			case EnemyState.Idle:
				break;
			case EnemyState.Searching:
				Search();
				break;
			case EnemyState.Moving:
				Move();
				break;
			case EnemyState.Attacking:
				Attack();
				break;
			case EnemyState.Reloading:
				Reload();
				break;
			case EnemyState.Dodging:
				Dodge();
				break;
			case EnemyState.Dying:
				Die();
				break;
			}
			yield return null;
		}
	}

	protected void Init () {
		enemyBehaviour.Init(this);
	}

	protected void Setup () {
		enemyBehaviour.Setup();
	}

	protected void Search () {
		enemyBehaviour.Search();
	}

	protected void Attack () {
		enemyBehaviour.Attack();
	}

	protected void Reload () {
		enemyBehaviour.Reload();
	}

	protected void Dodge () {
		enemyBehaviour.Dodge();
	}

	protected void Die () {
		enemyBehaviour.Die();
	}

	public override void Move (){
		enemyBehaviour.Move();
	}

	#region implemented abstract members of Entity


	public override void Died ()
	{
		enemyState = EnemyState.Dying;
	}

	void OnTriggerEnter2D(Collider2D other) {


		//Will be used in the future ... I'll  be back!!!!
		if (other.tag == "Bullet")
		{
			controlPlayerHitPoints(other.GetComponent<BaseBullet>().bulletDamage);
			if(Armor <= 0)
			{
				GameObject blood = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/Bloodhit") , other.transform.position, other.transform.rotation);  
				blood.GetComponentInChildren<ParticleSystem>().Play();
			}
			GameObject.Destroy(other.gameObject);
		}
	}


	public void controlPlayerHitPoints(int damage)
	{
		if(Armor > 0)
		{
			Armor -= damage;
		} else
		{
			HitPoints -= damage;
		}
	}

	#endregion

}
