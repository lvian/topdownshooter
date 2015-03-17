using UnityEngine;
using System.Collections;

public class ShotgunCases : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//GetComponent<TweenAlpha> ().PlayForward ();
		//EventDelegate ev = new EventDelegate (this, "Destroy");
		//GetComponent<TweenAlpha> ().AddOnFinished (ev);
		foreach(Transform child in transform)
		{
			child.position = new Vector3 (transform.position.x + Random.Range(-0.5F , 0.5f),transform.position.y + Random.Range(-0.5F , 0.5f), 0f); 
			child.Rotate(0f ,0f , Random.Range(0f, 360f));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Destroy(){
		GameObject.Destroy (transform.gameObject);
	}
}
