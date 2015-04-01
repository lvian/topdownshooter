using UnityEngine;
using System.Collections;

public class WaveUnit {
	private GameObject 	_unit;			// unit game object
	private int			_spawnPoint;	// spawn point in wich the units are spawned
	private int			_amount;		// amount of units to be spawned
	public int			unitCount;		// amount of unit already spawned
	public Timer 		delay;			// delay between each unit spawn
	public Timer		cooldown;		// star time of units spawn since the start of the wave
	
	
	public WaveUnit(GameObject unit, float delay, float cooldown, int lane, int maxamount){
		this._unit 		 = unit;
		this._spawnPoint = lane;
		this._amount 	 = maxamount;
		this.delay 		 = new Timer(delay);
		this.cooldown 	 = new Timer(cooldown);
		unitCount		 = 0;
	}

	public WaveUnit(GameObject unit, float delay, int lane){
		this._unit 		 = unit;
		this._spawnPoint = lane;
		this._amount 	 = 1;
		this.delay 		 = new Timer(delay);
		this.cooldown 	 = new Timer(0f);
		unitCount		 = 0;
	}
	
	public bool Done{
		get {
			return (unitCount == Amount);	
		}
	}
	
	public int SpawnPoint {
		get {
			return this._spawnPoint;
		}
	}
	
	public int Amount {
		get {
			return this._amount;
		}
	}
	
	public GameObject Unit {
		get {
			return this._unit;
		}
	}
}