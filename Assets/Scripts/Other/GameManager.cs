using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public enum GameState {
		Menu,
		Playing,
		Paused
	}


	public int reward1,reward2,reward3,reward4,reward5;
	public delegate void Function();
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
		if(UIButton.current.name == "Level One")
		{
			Application.LoadLevel (1);
		}
		if(UIButton.current.name == "Level Two")
		{
			Application.LoadLevel (2);
		}
		if(UIButton.current.name == "Level Three")
		{
			Application.LoadLevel (3);
		}
		if(UIButton.current.name == "Level Four")
		{
			Application.LoadLevel (4);
		}
		if(UIButton.current.name == "Level Five")
		{
			Application.LoadLevel (5);
		}
		if(UIButton.current.name == "Level Endless")
		{
			Application.LoadLevel (6);
		}

		_state = GameState.Playing;

		GUIManager.instance.StartLevel ();
		playerCash = Upgrades.Cash;


	}

	public void BackToMenu()
	{
    		StartCoroutine(delayRestart(0,GUIManager.instance.BackToMenu));		

	}

	public void RestartLevel()
	{

		StartCoroutine(delayRestart(Application.loadedLevel));		
	}

	public void NextLevel()
	{
		
		StartCoroutine(delayRestart(Application.loadedLevel + 1));		
	}

	private IEnumerator delayRestart(int level )
	{
		GUIManager.instance.RestartLoadScreen ();
		
		yield return new WaitForSeconds(1);
		Application.LoadLevel(level);
		GUIManager.instance.RestartWaveInformation ();
		_state = GameState.Playing;
		
	}

	private IEnumerator delayRestart(int level, Function postFunction)
	{
		GUIManager.instance.RestartLoadScreen ();
		
		yield return new WaitForSeconds(1);
		postFunction ();
		Application.LoadLevel(level);
		_state = GameState.Playing;

	}

	public void Defeat ()
	{
		_state = GameState.Paused;
		GUIManager.instance.ShowDefeatScreen ();
		upgrades.Cash = (upgrades.Cash / 4) * 3;
	}

	public void Victory ()
	{
		if(_state != GameState.Paused)
		{
			_state = GameState.Paused;
			GUIManager.instance.ShowVictoryScreen ();
		}
		
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
