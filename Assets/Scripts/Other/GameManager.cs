using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void GameStart()
	{
		Debug.Log ("here");
		Application.LoadLevel (1);

	}
}
