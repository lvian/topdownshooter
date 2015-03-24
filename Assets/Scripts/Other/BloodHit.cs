using UnityEngine;
using System.Collections;

public class BloodHit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Destroy()
	{
		GameObject.Destroy (gameObject);
	}
}
