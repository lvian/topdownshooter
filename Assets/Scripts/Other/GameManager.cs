using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public enum GameState {
		Menu,
		Playing,
		Paused
	}

	public static GameManager instance = null;
	private GameState _state;
	private int playerCash;

	void Awake () {
		if(instance == null)
			instance = this;
		else if(instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad (this);
		_state = GameState.Playing;
	}

	public void GameStart()
	{
		Application.LoadLevel (1);
		_state = GameState.Playing;

		playerCash = 0;

	}

	public GameState State {
		get {
			return _state;
		}
		set {
			_state = value;
		}
	}

	public int PlayerCash {
		get {
			return playerCash;
		}
		set {
			playerCash = value;
		}
	}

}
