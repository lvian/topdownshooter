using UnityEngine;
using System.Collections;

public class LevelOneWaves : WaveScript {
	public GameObject[] allEnemies;
	
	// Use this for initialization
	public new IEnumerator Start () {
		Init();

		//public WaveUnit(GameObject unit, float delay, float cooldown, int lane, int maxamount){

		//waveManager = waves.GetComponentInChildren<WaveManager>();
		GameObject wave1 = new GameObject("wave1");
		wave1.transform.parent = waves.transform;
		wave1.AddComponent<Wave>();
		Wave wave1Script = wave1.GetComponent<Wave>();
		wave1Script.units = new WaveUnit[] {
			new WaveUnit(allEnemies[0], 0, 0, 0, 1),
			//new WaveUnit(allEnemies[1], 2f, 10, 1, 1),
			//new WaveUnit(allEnemies[2], 2f, 20, 2, 1),
		};
		wave1Script.nextWaveDelay = 10f;
		waveManager.waves.Add(wave1Script);



		yield return new WaitForSeconds(1f);
		waveManager.state = WavesState.Setup;
	}


	#region implemented abstract members of WaveScript
	public override void LevelComplete ()
	{
		GameManager.instance.Upgrades.DynamiteUnlocked = 1;
		GUIManager.instance.ActivateDynamite ();
		GameManager.instance.Upgrades.levelsUnlocked += 1;
	}
	#endregion
}