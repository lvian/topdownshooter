using UnityEngine;
using System.Collections;

public class AgressiveEnemy : Enemy {
	void Awake () {
		enemyBehaviour = new AgressiveBehaviour();
		base.Init();
	}
}
