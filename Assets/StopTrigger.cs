using UnityEngine;
using System.Collections;

public class StopTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {

		if(other.tag == "Wall" || other.tag == "Enemy")
		{
			if(!other.name.Contains("Invisible"))
			{
				transform.parent.GetComponent<Dynamite>().Destination = transform.parent.position;
			}
		}
	}
}
