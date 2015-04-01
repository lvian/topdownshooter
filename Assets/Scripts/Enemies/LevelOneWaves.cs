using UnityEngine;
using System.Collections;

public class LevelOneWaves : WaveScript {
	public GameObject[] allEnemies;
	
	// Use this for initialization
	public new IEnumerator Start () {
		Init();

		//waveManager = waves.GetComponentInChildren<WaveManager>();
		GameObject wave1 = new GameObject("wave1");
		wave1.transform.parent = waves.transform;
		wave1.AddComponent<Wave>();
		Wave wave1Script = wave1.GetComponent<Wave>();
		wave1Script.units = new WaveUnit[] {
			new WaveUnit(allEnemies[0], 2f, 0, 0, 3),
			new WaveUnit(allEnemies[1], 2f, 10, 1, 1),
			new WaveUnit(allEnemies[2], 2f, 20, 2, 1),
		};
		waveManager.waves.Add(wave1Script);

		GameObject wave2 = new GameObject("wave2");
		wave2.transform.parent = waves.transform;
		wave2.AddComponent<Wave>();
		Wave wave2Script = wave2.GetComponent<Wave>();
		wave2Script.units = new WaveUnit[] {
			new WaveUnit(allEnemies[1], 2f, 0, 0, 3),
		};
		waveManager.waves.Add(wave2Script);

		yield return new WaitForSeconds(1f);
		waveManager.state = WavesState.Setup;
	}
}