using UnityEngine;
using System.Collections;

public class DefensiveEnemy : Enemy {
	void Awake () {
		enemyBehaviour = new DefensiveBehaviour();
		base.Init();
	}
}
