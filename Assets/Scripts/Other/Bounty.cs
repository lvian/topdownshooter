using UnityEngine;
using System.Collections;

public class Bounty : MonoBehaviour {

	public int bountyAmount;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		
		//Will be used in the future ... I'll  be back!!!!
		if (other.tag == "Player")
		{

			GameManager.instance.PlayerCash += bountyAmount;
			GameObject.Find("Cash Value").GetComponent<UILabel>().text = "$ " +GameManager.instance.PlayerCash.ToString();
			GameObject.Destroy(transform.gameObject);
		}
	}

}
