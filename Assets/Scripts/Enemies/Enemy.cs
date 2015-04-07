using UnityEngine;
using System.Collections;

public abstract class Enemy : Humanoid{
	public enum EnemyState {
		Init,
		Setup,
		Idle,
		Searching,
		Moving,
		Attacking,
		Reloading,
		Dodging,
		Dying,
		Dead
	}

	public enum EnemyStance{
		Offensive,
		Defensive,
		Aggresive
	}

	public Player player;
	public float rotationSpeed;
	public float maxShootingDistance;
	public AudioClip enemyHit;
	public GameObject bounty;
	public float shootDelay;

	public EnemyState enemyState = EnemyState.Init;
	public EnemyStance enemyStance = EnemyStance.Offensive;
	public IEnemyBehaviour enemyBehaviour = new OffensiveBehaviour();


	public IEnumerator Start () {
		InitHumanoid();
		while(true) {
			if(GameManager.instance.State == GameManager.GameState.Playing){
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
				case EnemyState.Dead:
					break;
				}
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
		anim.SetBool ("isMoving", isMoving);
		enemyBehaviour.Move();
	}

	#region implemented abstract members of Entity

	public void DisableWeapons()
	{
		currentWeapon.gameObject.SetActive(false);
		GetComponentInChildren<SpriteRenderer> ().sortingLayerName = "Background";
	}

	public override void Died ()
	{
		anim.SetTrigger("isDying");
		enemyState = EnemyState.Dying;
		bounty = (GameObject) GameObject.Instantiate(bounty , transform.position , transform.rotation); 
		audioSource.PlayOneShot (deathSounds[Random.Range(0,deathSounds.Length)]);
		transform.parent.parent = valhalla;
	}

	#endregion

}
