using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AIScript {
	private Dictionary<Enemy.EnemyState, Timer> timers;

	protected bool CanSeeTarget(Transform self, Transform target){
		CircleCollider2D c = self.GetComponent<CircleCollider2D>();
		c.enabled = false;
		LayerMask layerMask = ~( (1 << 8) | (1 << 11 ) );
		RaycastHit2D hit = Physics2D.Raycast(self.position, self.up, 1000f, layerMask);
		c.enabled = true;
		if( hit.collider != null){
			//Debug.Log(hit.transform.tag);
			if(hit.transform.Equals(target))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else{
			return false;
		}
	}

	protected bool[] CheckCollisions (Transform self) {
		/*
		 *
		 * 0	1	 2
		 *    \	| /
		 * 7 --	  -- 3		
		 * 	  /	| \	
		 * 6	5	 4
		 * 
		 * 
		 */

		float distance = 30f;
		//Vector2 up = Vector2.up;
		//Vector2 right = Vector2.right;
		Vector2 up = self.up;
		Vector2 right = self.right;
		RaycastHit2D[] hit = new RaycastHit2D[8];
		bool[] collisions = new bool[8];

		LayerMask layerMask = ~((1 << 8));
		hit[0] = Physics2D.Raycast(self.position, up + (-right), distance, layerMask);
		hit[1] = Physics2D.Raycast(self.position, up, distance, layerMask);
		hit[2] = Physics2D.Raycast(self.position, up + right, distance, layerMask);
		hit[3] = Physics2D.Raycast(self.position, right, distance, layerMask);
		hit[4] = Physics2D.Raycast(self.position, -up + right, distance, layerMask);
		hit[5] = Physics2D.Raycast(self.position, -up, distance, layerMask);
		hit[6] = Physics2D.Raycast(self.position, -up + (-right), distance, layerMask);
		hit[7] = Physics2D.Raycast(self.position, -right, distance, layerMask);

		/*
		Debug.DrawRay(self.position, up + (-right), Color.green, distance);
		Debug.DrawRay(self.position, up, Color.white, distance);
		Debug.DrawRay(self.position, up + right, Color.cyan, distance);
		Debug.DrawRay(self.position, right, Color.magenta, distance);
		Debug.DrawRay(self.position, -up + right, Color.grey, distance);
		Debug.DrawRay(self.position, -up, Color.blue, distance);
		Debug.DrawRay(self.position, -up + (-right), Color.black, distance);
		Debug.DrawRay(self.position, -right, Color.red, distance);
		*/

		for(int i = 0; i < hit.Length; i++){
			if(hit[i].collider != null){
				collisions[i] = Vector2.Distance(self.position, hit[i].transform.position) < 2f? true:false;
				//Debug.Log("Direction " + i + " distance " + collisions[i] + " collision with " + hit[i].transform.name);
			}
		}
		return collisions;
	}

	public Vector2 GetNearestCovering(Transform self, Transform player, GameObject[] obstacles) {
		GameObject closest = null;
		float distancePlayer = Mathf.Infinity;
		float distanceWall = Mathf.Infinity;
		float closestMeanDistance = Mathf.Infinity;

		foreach(GameObject go in obstacles) {
			distancePlayer = Vector3.Distance(player.position, go.transform.position);
			distanceWall = Vector3.Distance(self.position, go.transform.position);
			float curMeanDistance = (distancePlayer + distanceWall) / 2;
			if(curMeanDistance < closestMeanDistance){
				closest = go;
				closestMeanDistance = curMeanDistance;
			}
		}

		Vector3 dir = closest.transform.position - player.position;
		Ray2D ray = new Ray2D(closest.transform.position, new Vector2(dir.x, dir.y));
		Debug.DrawRay(closest.transform.position, new Vector2(dir.x, dir.y));
		return ray.GetPoint(3f);
	}

	protected void AddTimer(float time, Enemy.EnemyState state){
		if(timers == null) {
			timers = new Dictionary<Enemy.EnemyState, Timer>();
			timers.Add(state, new Timer(time));
		}
		else{
			if(!timers.ContainsKey(state))
				timers.Add(state, new Timer(time));
		}
	}

	protected bool IsElapsed(Enemy.EnemyState state){
		if(timers.ContainsKey(state)){
			bool ret = timers[state].IsElapsed;
			if(ret)
				timers.Remove(state);
			return ret;
		}
		else{
			return false;
		}
	}
}
