﻿using UnityEngine;
using System.Collections;

public class LevelOneWaves : WaveScript {
	public GameObject[] allEnemies;
	
	// Use this for initialization
	public new IEnumerator Start () {
		Init();

		//public WaveUnit(GameObject unit, float delayEntreUnidades, float startDaWave, int lane, int maxamount){

		//waveManager = waves.GetComponentInChildren<WaveManager>();
		GameObject wave1 = new GameObject("wave1");
		wave1.transform.parent = waves.transform;
		wave1.AddComponent<Wave>();
		Wave wave1Script = wave1.GetComponent<Wave>();
		wave1Script.units = new WaveUnit[] {
			new WaveUnit(allEnemies[0], 0, 0, 0, 1),
		};
		wave1Script.nextWaveDelay = 10f;
		waveManager.waves.Add(wave1Script);

		GameObject wave2 = new GameObject("wave2");
		wave2.transform.parent = waves.transform;
		wave2.AddComponent<Wave>();
		Wave wave2Script = wave2.GetComponent<Wave>();
		wave2Script.units = new WaveUnit[] {
			new WaveUnit(allEnemies[0],  0, 0, 1, 1),
		};
		wave2Script.nextWaveDelay = 10f;
		waveManager.waves.Add(wave2Script);
		
		GameObject wave3 = new GameObject("wave3");
		wave3.transform.parent = waves.transform;
		wave3.AddComponent<Wave>();
		Wave wave3Script = wave3.GetComponent<Wave>();
		wave3Script.units = new WaveUnit[] {
			new WaveUnit(allEnemies[0],  0, 0, 0, 1),
			new WaveUnit(allEnemies[0],  5, 0, 1, 1),
		};
		wave3Script.nextWaveDelay = 10f;
		waveManager.waves.Add(wave3Script);
		
		GameObject wave4 = new GameObject("wave4");
		wave4.transform.parent = waves.transform;
		wave4.AddComponent<Wave>();
		Wave wave4Script = wave4.GetComponent<Wave>();
		wave4Script.units = new WaveUnit[] {
			new WaveUnit(allEnemies[0], 4, 3),
			new WaveUnit(allEnemies[1], 0, 4, 0, 1),
		};
		wave4Script.nextWaveDelay = 15f;
		waveManager.waves.Add(wave4Script);
		
		
		GameObject wave5 = new GameObject("wave5");
		wave5.transform.parent = waves.transform;
		wave5.AddComponent<Wave>();
		Wave wave5Script = wave5.GetComponent<Wave>();
		wave5Script.units = new WaveUnit[] {
			new WaveUnit(allEnemies[0], 4, 2),
			new WaveUnit(allEnemies[1], 2, 2),
		};
		wave5Script.nextWaveDelay = 13f;
		waveManager.waves.Add(wave5Script);
		
		GameObject bossWave = new GameObject("bossWave");
		bossWave.transform.parent = waves.transform;
		bossWave.AddComponent<Wave>();
		Wave bossWaveScript = bossWave.GetComponent<Wave>();
		bossWaveScript.units = new WaveUnit[] {
			new WaveUnit(allEnemies[1], 4, 1),
			new WaveUnit(allEnemies[0], 2, 5, 0, 4),
		};
		bossWaveScript.nextWaveDelay = 15f;
		waveManager.waves.Add(bossWaveScript);

		yield return new WaitForSeconds(1f);
		waveManager.state = WavesState.Setup;
	}


	#region implemented abstract members of WaveScript
	public override void LevelComplete ()
	{

		if(finishCalled == false)
		{
			base.findBounty ();
			finishCalled = true;
			if(GameManager.instance.Upgrades.DynamiteUnlocked == 0)
			{
				GameManager.instance.Upgrades.DynamiteUnlocked = 1;
				GUIManager.instance.ActivateDynamite ();
				GameManager.instance.Upgrades.levelsUnlocked += 1;
				GUIManager.instance.ShowUnlockMessage (Localization.Get("VictoryUnlockLevelOne"));
			} else{
				GUIManager.instance.HideUnlockMessage();
			}
		}
	}
	#endregion
}