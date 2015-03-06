using UnityEngine;
using System.Collections;

public abstract class Enemy : Entity {
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

	protected EnemyState _state = EnemyState.Init;
	protected EnemyStance _stance = EnemyStance.Offensive;

	IEnumerator Start () {
		while(true){
			switch(_state) {
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

	protected abstract void Init ();

	protected abstract void Setup ();

	protected abstract void Search ();

	protected abstract void Attack ();

	protected abstract void Reload ();

	protected abstract void Dodge ();

	protected abstract void Die ();
}
