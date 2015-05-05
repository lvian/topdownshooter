using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour{
	public WavesState 		state;
	public List<GameObject> spawnPoints;
	public List<Wave> 	    waves;

	private int 		_index;
	private bool		_autoStartNextWave;
	private WaveScript	waveScript;
	
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
		//Begin();
		state = WavesState.NotStarted;
	}
	
	private void Setup(){
		//Debug.Log("Setup() called");
		_index++;
		if(_index >= waves.Count){
			bool allDead = true;
			foreach(Wave w in waves){
				allDead = allDead && w.AllDead;
			}
			if(allDead)
				state = WavesState.Finished;
			return;	
		}
		else{
			Wave current = waves[_index];
			current.initUnits();
			current.started = true;
			if(_index + 1 == waves.Count)
				current.last = true;
			state = WavesState.Idle;
		}
	}
	
	private void Wait(){
		Debug.Log("Wait() called");
		if (!waves[_index].last) {
			GUIManager.instance.UpdateWaveCounter (_index + 1, WavesCount);
		} else {
			GUIManager.instance.UpdateWaveCounter (_index + 1, WavesCount, true);
		}
		if(waves[_index].Done){
			state = WavesState.Setup;
		}
	}
	
	private void Finish(){
		//Get value from level wave
		waveScript.LevelComplete ();
		GameManager.instance.Victory (waveScript.levelReward);


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

	public WaveScript WaveScript {
		get {
			return waveScript;
		}
		set {
			waveScript = value;
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