using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

	public Entity[] entities;
	public GameObject[] spawnPoints;
	public List<Entity> spawnedEntities;
	public int max;
	public float initialDelay;
	public float delayFactor;

	public float _delay = 3f;

	// Use this for initialization
	void Start () {
		spawnedEntities = new List<Entity>();
	}
	
	// Update is called once per frame
	void Update () {
		if(_delay <= 0 && transform.childCount < max){
			GameObject go = Instantiate(entities[Random.Range(0,entities.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity) as GameObject;
			CalculateDelay();
		}
		_delay -= Time.deltaTime;
	}

	private void CalculateDelay(){
		_delay = initialDelay + ((transform.childCount + 1) * delayFactor);
	}
}
