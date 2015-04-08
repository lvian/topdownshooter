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

	private Upgrades upgrades;

	void Awake () {
		if(instance == null)
			instance = this;
		else if(instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad (this);
		_state = GameState.Playing;
		upgrades = new Upgrades();
	}

	public void GameStart()
	{
		Application.LoadLevel (1);
		_state = GameState.Playing;

		Upgrades.Cash = 0;


	}

	public GameState State {
		get {
			return _state;
		}
		set {
			_state = value;
		}
	}

	public Upgrades Upgrades {
		get {
			return upgrades;
		}
		set {
			upgrades = value;
		}
	}
}
