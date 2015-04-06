using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Wave : MonoBehaviour {
	
	public WaveUnit[] 	units;
	public WaveManager	manager;
	public float		nextWaveDelay;
	public bool 		started;		// defined by WaveManager script
	public bool			last;

	private bool		done;
	private int 		numberOfSpawns;
	private int			spawnsRemaining;
	private Timer		timer;

	private UILabel		timerNumber;
	private UILabel		timerLabel;
	
	void Awake(){
		done = started = last = false;
		timerNumber = GameObject.Find("Wave Timer Number").GetComponent<UILabel>();
		timerLabel = GameObject.Find("Wave Timer Label").GetComponent<UILabel>();
	}
	
	void Start(){
		manager = GetComponentInParent<WaveManager>();
	}
	
	void Update(){
		if(GameManager.instance.State != GameManager.GameState.Playing)
			return;
		if(timer != null){
			if(!last){
				timerNumber.text = Mathf.RoundToInt(timer.RemainingTime) + "s";
			}
			else{
				timerLabel.text = "Last Wave";
				timerNumber.text = "";
			}
		}
				
		if(!started || done)
			return;
		bool tmpDone = true;
		foreach(WaveUnit unit in units){
			if(unit.delay.IsElapsed && unit.cooldown.IsElapsed){
				if(unit.unitCount < unit.Amount){
					spawnUnit(unit.Unit, unit.SpawnPoint);
					unit.unitCount++;
					unit.delay.Reset();
				}
			}
			tmpDone = (tmpDone && unit.Done);
		}
		done = tmpDone;
		if(done){

		}
	}
	
	public void initUnits(){
		done = started = false;
		foreach(WaveUnit unit in units){
			unit.unitCount 		= 0;
			unit.cooldown.Reset();
			unit.delay.Reset();
		}
		UpdateInfo();

	}

	public void UpdateInfo(){
		numberOfSpawns = 0;
		float longest = 0f;
		for(int j = 0; j < units.Length; j++){
			numberOfSpawns += units[j].Amount;
			float duration = units[j].delay.Duration + units[j].cooldown.Duration * units[j].Amount;
			if(duration > longest)
				longest = duration;
		}
		timer = new Timer(longest + nextWaveDelay);
		spawnsRemaining = numberOfSpawns;
	}
	
	private void spawnUnit(GameObject unit, int lane){
		spawnsRemaining--;
		GameObject  newUnit = Object.Instantiate(unit, manager.spawnPoints[lane].transform.position, Quaternion.identity) as GameObject;
		newUnit.transform.parent = transform;
	}

	public int NumberOfSpawns {
		get {
			return numberOfSpawns;
		}
	}

	public int SpawnsRemaining {
		get {
			return spawnsRemaining;
		}
	}

	public float TimeRemaining {
		get {
			return timer.RemainingTime;
		}
	}

	public bool Done {
		get {
			return (done && timer.IsElapsed);
		}
	}
}