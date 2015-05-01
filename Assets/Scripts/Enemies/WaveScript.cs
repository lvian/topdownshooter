using UnityEngine;
using System.Collections;

public abstract class WaveScript : MonoBehaviour {
	public    GameObject[] spawnPoints;
	public int levelReward;
	protected WaveManager  waveManager;
	protected GameObject   waves;

	public virtual IEnumerator Start() {
		Init();
		yield return 0;
	}

	protected void Init(){
		waves = new GameObject("WaveManager");
		waves.transform.parent = transform;
		waveManager = waves.AddComponent<WaveManager>() as WaveManager;
		waveManager.Begin();
		waveManager.spawnPoints = new System.Collections.Generic.List<GameObject>(spawnPoints);
		waveManager.waves = new System.Collections.Generic.List<Wave>();
		waveManager.WaveScript = this; 
	}

	public abstract void LevelComplete();
}
