using UnityEngine;
using System.Collections;

public class StaticEnemy : Enemy {
	void Awake () {
		enemyBehaviour = new StaticBehaviour();
		base.Init();
	}
}
