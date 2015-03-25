using UnityEngine;
using System.Collections;

public class CustomTimer : MonoBehaviour {

	public static CustomTimer instance;

	private float _gameTimer;

	void Awake () {
		if(instance == null)
			instance = this;
		else if(instance != this)
			Destroy(gameObject);
		_gameTimer = Time.timeSinceLevelLoad;
			
	}

	// Update is called once per frame
	void Update () {
		if(GameManager.instance.State == GameManager.GameState.Playing){
			_gameTimer += Time.deltaTime;
		}
	}

	public float GameTimer {
		get {
			return _gameTimer;
		}
	}
}
