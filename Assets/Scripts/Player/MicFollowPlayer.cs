﻿using UnityEngine;
using System.Collections;

public class MicFollowPlayer : MonoBehaviour {
	private GameObject player;
	private Vector2 reloadBarPos;
	private Camera GUICamera;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		GUICamera =(Camera) GameObject.FindGameObjectWithTag ("GUI Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if(player)
		{
			reloadBarPos = Camera.main.WorldToViewportPoint(new Vector3(player.transform.position.x,player.transform.position.y, player.transform.position.z) );
			reloadBarPos = GUICamera.ViewportToWorldPoint (reloadBarPos);
			transform.localPosition = transform.parent.InverseTransformPoint(reloadBarPos);
			transform.rotation = player.transform.rotation;
		} else{
			player = GameObject.FindGameObjectWithTag ("Player");
		}
	}
}
