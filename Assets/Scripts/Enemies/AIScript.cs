using UnityEngine;
using System.Collections;

public class AIScript {

	protected bool CanSeeTarget(Transform self, Transform target){
		RaycastHit2D hit = Physics2D.Raycast(self.position, self.up, 1000f);
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
}
