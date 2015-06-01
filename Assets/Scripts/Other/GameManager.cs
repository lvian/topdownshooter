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
	public string[] tips;
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
	
	}

	void Start(){
		State = GameState.Menu;
		upgrades = new Upgrades();
		Cursor.visible = false;
		Localization.language = "English";
	}

	public void GameStart()
	{
		GUIManager.instance.BeforeLoadLevel ();
		if(State != GameState.Menu)
			State = GameState.Paused;

		if(UIButton.current.name == "Level One")
		{
			GUIManager.instance.LevelOneDescription();
			Application.LoadLevel (1);
		}
		if(UIButton.current.name == "Level Two")
		{
			GUIManager.instance.LevelTwoDescription();
			Application.LoadLevel (2);
		}
		if(UIButton.current.name == "Level Three")
		{
			GUIManager.instance.LevelThreeDescription();
			Application.LoadLevel (3);
		}
		if(UIButton.current.name == "Level Four")
		{
			GUIManager.instance.LevelFourDescription();
			Application.LoadLevel (4);
		}
		if(UIButton.current.name == "Level Five")
		{
			GUIManager.instance.LevelFiveDescription();
			Application.LoadLevel (5);
		}
		if(UIButton.current.name == "Level Endless")
		{
			GUIManager.instance.LevelEndlessDescription();
			Application.LoadLevel (6);
		}

		if(UIButton.current.name == "Tutorial")
		{
			GUIManager.instance.LevelTutorialDescription();
			Application.LoadLevel (7);
		}

		GUIManager.instance.StartLevelGUI (tips[Random.Range(0, tips.Length)]);
		playerCash = Upgrades.Cash;


	}

	public void CheckTutorial()
	{
		if(GameManager.instance.upgrades.DoneTutorial == 0)
		{
			GUIManager.instance.ShowTutorialPanel();
		}
	}

	public void SkipTutorial()
	{
		upgrades.DoneTutorial = 1;
		GUIManager.instance.HideTutorialPanel();
	}

	public void NextLevel()
	{
		GUIManager.instance.RestartLoadScreen (tips[Random.Range(0, tips.Length)]);
		GUIManager.instance.RestartWaveInformation ();
		switch (Application.loadedLevel + 1)
		{
		case 2:
			GUIManager.instance.LevelTwoDescription();
			break;
		case 3:
			GUIManager.instance.LevelThreeDescription();
			break;
		case 4:
			GUIManager.instance.LevelFourDescription();
			break;
		case 5:
			GUIManager.instance.LevelFiveDescription();
			break;
		case 6:
			GUIManager.instance.LevelEndlessDescription();
			break;
		}
		Application.LoadLevel (Application.loadedLevel + 1);
	}

	public void StartLevel()
	{

		GUIManager.instance.LeaveLoadScreen ();
		State = GameState.Playing;
	}
	public void BackToMenu()
	{
		State = GameState.Menu;
		GUIManager.instance.RestartLoadScreen (tips[Random.Range(0, tips.Length)]);
		GUIManager.instance.BeforeLoadMenu ();
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
		GUIManager.instance.RestartLoadScreen (tips[Random.Range(0, tips.Length)]);
		GUIManager.instance.RestartWaveInformation ();
		Application.LoadLevel(Application.loadedLevel);
	}


	public void Defeat ()
	{
		State = GameState.Paused;
		GUIManager.instance.ShowDefeatScreen ();
		upgrades.Cash = (upgrades.Cash / 4) * 3;
	}

	public void Victory (int reward)
	{
		if(State == GameState.Playing)
		{
			upgrades.Cash += reward;
			State = GameState.Paused;
			GUIManager.instance.ShowVictoryScreen (reward);
			GUIManager.instance.InitializeLevels ();
		}
		
	}

 	public void EscPause()
	{
		UIToggle pause =  GameObject.Find("Pause Button").GetComponent<UIToggle> ();

		if(pause.value == true)
		{
			pause.value = false;
		} else
		{
			if(State != GameState.Menu)
			{
				pause.value = true;
			}
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
			} else 
			{
				GUIManager.instance.ChangeCursor(true);
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
