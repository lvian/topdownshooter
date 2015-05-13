using UnityEngine;
using System.Collections;

public class LevelEndlessWaves : WaveScript {
	public GameObject[] allEnemies;
	
	// Use this for initialization
	public new IEnumerator Start () {
		Init();

		//public WaveUnit(GameObject unit, float delay, float cooldown, int lane, int maxamount){

		//Endless will have 16 waves, last boss weats a mariarti hat
		//waveManager = waves.GetComponentInChildren<WaveManager>();
		GameObject wave1 = new GameObject("wave1");
		wave1.transform.parent = waves.transform;
		wave1.AddComponent<Wave>();
		Wave wave1Script = wave1.GetComponent<Wave>();
		wave1Script.units = new WaveUnit[] {
			new WaveUnit(allEnemies[0], 1),
		};
		wave1Script.nextWaveDelay = 10f;
		waveManager.waves.Add(wave1Script);
	



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