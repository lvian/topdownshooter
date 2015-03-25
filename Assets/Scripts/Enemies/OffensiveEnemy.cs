using UnityEngine;
using System.Collections;

public class OffensiveEnemy : Enemy {
	void Awake () {
		enemyBehaviour = new OffensiveBehaviour();
		base.Init();
	}
}
