using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour{
	public WavesState 		state;
	public List<GameObject> spawnPoints;
	public List<Wave> 	    waves;

	private int 		_index;
	private bool		_autoStartNextWave;
	private UILabel		totalWave;
	private UILabel		currentWave;
	
	IEnumerator Start (){
		state = WavesState.Initialize;
		while(true){
			switch(state){
			case WavesState.Initialize:
				Initialize();
				break;
			case WavesState.NotStarted:
				break;
			case WavesState.Setup:
				Setup();
				break;
			case WavesState.Idle:
				Wait();
				break;
			case WavesState.Finished:
				Finish();
				break;
			default:
				break;
			}
			yield return 0;
		}
	}
	/*
	public void Begin(){
		_waves = new List<Wave>( GetComponentsInChildren<Wave>());
		_waves.Sort(delegate(Wave x, Wave y) {
			return x.name.CompareTo(y.name);
		});
	}*/

	public void Begin(){
		//Debug.Log("Begin() called");
	}
	
	private void Initialize(){
		//Debug.Log("Initialize() called");
		_index = -1;
		_autoStartNextWave = true;
		totalWave = GameObject.Find("Total Wave").GetComponent<UILabel>();
		currentWave = GameObject.Find("Current Wave").GetComponent<UILabel>();
		//Begin();
		state = WavesState.NotStarted;
	}
	
	private void Setup(){
		//Debug.Log("Setup() called");
		_index++;
		if(_index >= waves.Count){
			bool allDead = true;
			foreach(Wave w in waves){
				Debug.Log(w.transform.name + " has " + w.transform.childCount + " children!");
				if(w.transform.childCount > 0){
					allDead = false;
				}
			}
			//Debug.Log("All dead? " + allDead);
			if(allDead)
				state = WavesState.Finished;
			return;	
		}
		else{
			//Debug.Log(_index + " XXX " + waves.Count + "!");
			Wave current = waves[_index];
			current.initUnits();
			current.started = true;
			if(_index + 1 == waves.Count)
				current.last = true;
			state = WavesState.Idle;
		}
	}
	
	private void Wait(){
		//Debug.Log("Wait() called");
		currentWave.text = "Wave " +(_index + 1) ;
		totalWave.text = "of " + WavesCount;
		if(waves[_index].Done){
			state = WavesState.Setup;
		}
	}
	
	private void Finish(){
		//Debug.Log("Finish() called");
		//Messenger<bool>.Broadcast("PlayerVictory", true);
	}
	
	public int WavesCount{
		get {
			return waves.Count;
		}
	}
	
	public int CurrentWave{
		get {
			return _index + 1;
		}
	}
	
	public bool Rushing {
		get {
			return _autoStartNextWave;	
		}
		set {
			_autoStartNextWave = value;	
		}
	}
}

public enum WavesState{
	Idle,
	Initialize,
	NotStarted,
	Setup,
	Finished
}