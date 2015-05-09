using UnityEngine;
using System.Collections;

public abstract class WaveScript : MonoBehaviour {
	public    GameObject[] spawnPoints;
	public int levelReward;
	protected bool finishCalled;
	protected WaveManager  waveManager;
	protected GameObject   waves;

	public virtual IEnumerator Start() {
		finishCalled = false;
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

	public bool FinishCalled {
		get {
			return finishCalled;
		}
	}

	public void findBounty()
	{
		GameObject[] bounties = GameObject.FindGameObjectsWithTag ("Bounty");
		foreach(GameObject b in bounties)
		{
			b.GetComponent<Bounty>().Finished = true;
		}
	}

	public abstract void LevelComplete();
}
