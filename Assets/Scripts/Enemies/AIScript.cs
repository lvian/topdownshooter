using UnityEngine;
using System.Collections;

public class AIScript {

	protected bool CanSeeTarget(Transform self, Transform target){
		LayerMask layerMask = ~( (1 << 10) | (1 << 8) );
		RaycastHit2D hit = Physics2D.Raycast(self.position, self.up, 1000f, layerMask);
		if( hit.collider != null){
			Debug.Log(hit.transform.tag);
			if(hit.transform.Equals(target))
				return true;
			else
				return false;
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
}
