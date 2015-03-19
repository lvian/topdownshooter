using UnityEngine;
using System.Collections;

public interface IEnemyBehaviour {
	void Init(Enemy enemy);
	void Setup();
	void Search();
	void Move();
	void Attack();
	void Reload();
	void Dodge();
	void Die();
}
