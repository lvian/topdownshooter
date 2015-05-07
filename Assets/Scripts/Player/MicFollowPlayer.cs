using UnityEngine;
using System.Collections;

public class MicFollowPlayer : MonoBehaviour {
	private GameObject player;
	private Vector2 reloadBarPos;
	private Camera GUICamera;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(player)
		{

			transform.position = player.transform.position;
			transform.rotation = player.transform.rotation;
		} else{
			player = GameObject.FindGameObjectWithTag ("Player");
		}
	}
}
