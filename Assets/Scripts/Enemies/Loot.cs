using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Loot : MonoBehaviour {
	public List<GameObject> item;
	public List<float> chance;
	public List<string> amount;

	private float offset = .1f;

	public void SpawnLoot(){
		//Debug.Log("Loot, bitch!");
		if(item.Count != chance.Count || chance.Count != amount.Count){
			Debug.LogWarning("All lists must have the same length");
			return;
		}
		for(int i = 0; i < item.Count; i++){
			float sortedChance = Random.Range(0f,100f);
			if(sortedChance <= chance[i]){
				int sortedAmount = GetAmount(amount[i]);
				Vector3 offsetPos = new Vector3(
					Random.Range(-offset,offset),
					Random.Range(-offset,offset),
					0
				);
				GameObject go = GameObject.Instantiate(item[i], transform.position, transform.rotation) as GameObject;
				//Debug.Log(transform.position + " " + offsetPos);
				go.transform.Translate(offsetPos);
				if(sortedAmount > 0)
				{
					go.GetComponent<Bounty>().bountyAmount = sortedAmount;
				}
			}
		}
	}

	int GetAmount(string s){
		if(s.Contains("-")){
			int i1 = int.Parse(s.Substring(0, s.IndexOf("-")));
			int i2 = int.Parse(s.Substring(s.IndexOf("-") + 1));
			return Random.Range(i1,i2);
		}
		else{
			return int.Parse(s);
		}
	}
}
