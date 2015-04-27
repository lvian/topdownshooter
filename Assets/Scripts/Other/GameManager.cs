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
	private Vector2 mouseOffset = new Vector2(16,16);

	void Awake () {
		if(instance == null)
			instance = this;
		else if(instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad (this);
		//Cursor.SetCursor(Resources.Load("Sprites/MainCursor", typeof(Texture2D)) as Texture2D, Vector2.zero, CursorMode.Auto );
	
	}

	void Start(){
		State = GameState.Menu;
		upgrades = new Upgrades();
		Cursor.visible = false;
		Localization.language = "Portuguese";
	}

	public void GameStart()
	{
		GUIManager.instance.BeforeLoadLevel ();
		if(State != GameState.Menu)
			State = GameState.Paused;

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


		GUIManager.instance.StartLevelGUI ();
		playerCash = Upgrades.Cash;


	}


	public void StartLevel()
	{
		GUIManager.instance.LeaveLoadScreen ();
		State = GameState.Playing;
	}
	public void BackToMenu()
	{
		State = GameState.Menu;
		GUIManager.instance.BeforeLoadMenu ();
		GUIManager.instance.RestartLoadScreen ();
		StartCoroutine(delayRestart(0));	

	}

	private IEnumerator delayRestart(int level )
	{
		yield return new WaitForSeconds(1);
		Application.LoadLevel(level);
		GUIManager.instance.RestartWaveInformation ();
	}

	public void RestartLevel()
	{
		State = GameState.Menu;
		GUIManager.instance.RestartLoadScreen ();
		GUIManager.instance.RestartWaveInformation ();
		Application.LoadLevel(Application.loadedLevel);
	}


	public void Defeat ()
	{
		State = GameState.Paused;
		GUIManager.instance.ShowDefeatScreen ();
		upgrades.Cash = (upgrades.Cash / 4) * 3;
	}

	public void Victory ()
	{
		if(State != GameState.Paused)
		{
			State = GameState.Paused;
			GUIManager.instance.ShowVictoryScreen ();
		}
		
	}

	public void Pause()
	{
		if(UIToggle.current.value == true)
		{
			State = GameState.Paused;
			GUIManager.instance.GamePaused(true);
		} else
		{
			if(State != GameState.Menu)
			{
				State = GameState.Playing;
				GUIManager.instance.GamePaused(false);
			}
		}
	}

	public GameState State {
		get {
			return _state;
		}
		set {
			_state = value;
			if(value == GameState.Playing )
			{
				GUIManager.instance.ChangeCursor(false);
				//Cursor.SetCursor(Resources.Load("Sprites/PlayCursor", typeof(Texture2D)) as Texture2D, mouseOffset, CursorMode.Auto );
			} else 
			{
				GUIManager.instance.ChangeCursor(true);
				//Cursor.SetCursor(Resources.Load("Sprites/MainCursor", typeof(Texture2D)) as Texture2D, Vector2.zero, CursorMode.Auto );
			} 
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
