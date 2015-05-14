using UnityEngine;
using System.Collections;

public class LevelEndlessWaves : WaveScript {
	public GameObject[] allEnemies;
	
	// Use this for initialization
	public new IEnumerator Start () {
		Init();

		//public WaveUnit(GameObject unit, float delay, float cooldown, int lane, int maxamount){

		//Endless will have 11 waves, last boss weats a mariarti hat

		//easy
		GameObject wave1 = new GameObject("wave1");
		wave1.transform.parent = waves.transform;
		wave1.AddComponent<Wave>();
		Wave wave1Script = wave1.GetComponent<Wave>();
		wave1Script.units = new WaveUnit[] {
			new WaveUnit(allEnemies[0], 3,   3),
		};
		wave1Script.nextWaveDelay = 15f;
		waveManager.waves.Add(wave1Script);

		GameObject wave2 = new GameObject("wave2");
		wave2.transform.parent = waves.transform;
		wave2.AddComponent<Wave>();
		Wave wave2Script = wave2.GetComponent<Wave>();
		wave2Script.units = new WaveUnit[] {
			new WaveUnit(allEnemies[0], 3,  2),
			new WaveUnit(allEnemies[1], 3, 2),
		};
		
		wave2Script.nextWaveDelay = 20f;
		waveManager.waves.Add(wave2Script);
		
		GameObject wave3 = new GameObject("wave3");
		wave3.transform.parent = waves.transform;
		wave3.AddComponent<Wave>();
		Wave wave3Script = wave3.GetComponent<Wave>();
		wave3Script.units = new WaveUnit[] {
			new WaveUnit(allEnemies[1], 3, 2),
			new WaveUnit(allEnemies[0],  3,2),
			new WaveUnit(allEnemies[2], 3, 1),
		};
		wave3Script.nextWaveDelay = 25f;
		waveManager.waves.Add(wave3Script);
		
		GameObject wave4 = new GameObject("wave4");
		wave4.transform.parent = waves.transform;
		wave4.AddComponent<Wave>();
		Wave wave4Script = wave4.GetComponent<Wave>();
		wave4Script.units = new WaveUnit[] {
			new WaveUnit(allEnemies[1], 3, 2),
			new WaveUnit(allEnemies[0], 3, 1),
			new WaveUnit(allEnemies[2],  3,1),
			new WaveUnit(allEnemies[3],  3,1),
		};
		wave4Script.nextWaveDelay = 30f;
		waveManager.waves.Add(wave4Script);
		
		
		
		GameObject wave5 = new GameObject("wave5");
		wave5.transform.parent = waves.transform;
		wave5.AddComponent<Wave>();
		Wave wave5Script = wave5.GetComponent<Wave>();
		wave5Script.units = new WaveUnit[] {
			new WaveUnit(allEnemies[1], 3, 1),
			new WaveUnit(allEnemies[0], 3, 1),
			new WaveUnit(allEnemies[2], 3, 1),
			new WaveUnit(allEnemies[3], 3, 2),
		};
		wave5Script.nextWaveDelay = 30f;
		waveManager.waves.Add(wave5Script);


		//medium
		GameObject wave6 = new GameObject("wave6");
		wave6.transform.parent = waves.transform;
		wave6.AddComponent<Wave>();
		Wave wave6Script = wave6.GetComponent<Wave>();
		wave6Script.units = new WaveUnit[] {
			new WaveUnit(allEnemies[4], 3),
		};
		wave6Script.nextWaveDelay = 15f;
		waveManager.waves.Add(wave6Script);
		
		GameObject wave7 = new GameObject("wave7");
		wave7.transform.parent = waves.transform;
		wave7.AddComponent<Wave>();
		Wave wave7Script = wave7.GetComponent<Wave>();
		wave7Script.units = new WaveUnit[] {
			new WaveUnit(allEnemies[4], 3,  2),
			new WaveUnit(allEnemies[5], 3, 2),
		};
		
		wave7Script.nextWaveDelay = 20f;
		waveManager.waves.Add(wave7Script);
		
		GameObject wave8 = new GameObject("wave8");
		wave8.transform.parent = waves.transform;
		wave8.AddComponent<Wave>();
		Wave wave8Script = wave8.GetComponent<Wave>();
		wave8Script.units = new WaveUnit[] {
			new WaveUnit(allEnemies[5], 3, 2),
			new WaveUnit(allEnemies[4],  3,2),
			new WaveUnit(allEnemies[6], 3, 1),
		};
		wave8Script.nextWaveDelay = 25f;
		waveManager.waves.Add(wave8Script);
		
		GameObject wave9 = new GameObject("wave9");
		wave9.transform.parent = waves.transform;
		wave9.AddComponent<Wave>();
		Wave wave9Script = wave9.GetComponent<Wave>();
		wave9Script.units = new WaveUnit[] {
			new WaveUnit(allEnemies[5], 3, 2),
			new WaveUnit(allEnemies[4], 3, 1),
			new WaveUnit(allEnemies[6], 3, 1),
			new WaveUnit(allEnemies[7],  3,1),
		};
		wave9Script.nextWaveDelay = 30f;
		waveManager.waves.Add(wave9Script);
		
		
		
		GameObject wave10 = new GameObject("wave10");
		wave10.transform.parent = waves.transform;
		wave10.AddComponent<Wave>();
		Wave wave10Script = wave10.GetComponent<Wave>();
		wave5Script.units = new WaveUnit[] {
			new WaveUnit(allEnemies[5], 3, 1),
			new WaveUnit(allEnemies[4], 3, 1),
			new WaveUnit(allEnemies[6], 3, 1),
			new WaveUnit(allEnemies[7], 3, 2),
		};
		wave10Script.nextWaveDelay = 30f;
		waveManager.waves.Add(wave10Script);




		//bosses
		GameObject bossWave1 = new GameObject("bossWave1");
		bossWave1.transform.parent = waves.transform;
		bossWave1.AddComponent<Wave>();
		Wave bossWave1Script = bossWave1.GetComponent<Wave>();
		bossWave1Script.units = new WaveUnit[] {
			new WaveUnit(allEnemies[8], 3,  1),
			new WaveUnit(allEnemies[9], 3,  1),
			new WaveUnit(allEnemies[10], 3,  1),
			new WaveUnit(allEnemies[11],  3, 1),
		};
		bossWave1Script.nextWaveDelay = 30f;
		waveManager.waves.Add(bossWave1Script);
	



		yield return new WaitForSeconds(1f);
		waveManager.state = WavesState.Setup;
	}

	
	#region implemented abstract members of WaveScript
	public override void LevelComplete ()
	{
		base.findBounty ();
		if (finishCalled == false) {
			finishCalled = true;
			//unlocks mariarchi hat
			GUIManager.instance.HideNextLevelButton();
		}


	}
	#endregion
}