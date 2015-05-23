using UnityEngine;
using System.Collections;

public class TrainingLevel : MonoBehaviour {

	private bool firstMessage,secondMessage, thirdMessage,fourthMessage, dodgeCooldown;
	private Timer interval;
	protected int bottlesDestroyed, dodgesPerformed;
	protected Player player;
	// Use this for initialization
	void Start () {
		firstMessage = false;
		secondMessage = false;
		thirdMessage = false;
		fourthMessage = false;
		bottlesDestroyed = 0;
		dodgesPerformed = 0;
		dodgeCooldown = false;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		interval = new Timer (6);
	}
	
	// Update is called once per frame
	void Update () {
		if(CustomTimer.instance.GameTimer >= 1 && firstMessage == false)
		{
			GUIManager.instance.ShowMessage("Let's start with some target practice. Shoot the bottles on top of the barrels, don't forget to reload!", 5);
			firstMessage = true;
			
		}
		if( bottlesDestroyed >= 10 )
		{

			if(secondMessage == false)
			{
				GUIManager.instance.ShowMessage("Nice shooting, those bottles didn't stand a chance! Now move around and practice your dodge, don't forget to build momentum!", 5);
				secondMessage = true;
			}
			if(player.GetPlayerState == Player.PlayerState.Dodging && dodgeCooldown == false)
			{
				dodgesPerformed ++;
				dodgeCooldown = true;
			}else if(player.GetPlayerState != Player.PlayerState.Dodging && dodgeCooldown == true)
			{
				dodgeCooldown = false;
			}
		}
		if( dodgesPerformed >= 3 )
		{
			if(thirdMessage == false)
			{
				GUIManager.instance.ShowMessage("Good, your skill in dodging air bullets is unsurpassed.", 5);
				thirdMessage = true;
				interval.Reset();
			}
		}

		if(thirdMessage && interval.RemainingTime <= 0)
		{
			if(fourthMessage == false)
			{
				GUIManager.instance.ShowMessage("You seem prepared enough, when your are ready, press the \"Leave\" button to go back to the main menu.", 4);
				GUIManager.instance.ShowLeaveButton();
				GameManager.instance.Upgrades.DoneTutorial = 1;
				fourthMessage = true;
			}

		}
	}

	public int BottlesDestroyed {
		get {
			return bottlesDestroyed;
		}
		set {
			bottlesDestroyed = value;
		}
	}

}
