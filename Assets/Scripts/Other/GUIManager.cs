using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public UIPanel levelsPanel, upgradesPanel, creditsPanel, inGameGUI, loadScreen, mainMenu;
	public AudioClip cashSound;
	private GameObject revolverBullets, shotgunBullets, rifleBullets, messageWindow;
	public static GUIManager instance = null;

	void Awake () {
		if(instance == null)
			instance = this;
		else if(instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	void Start () {
		//Uncomment to delete upgrades and level
		PlayerPrefs.DeleteAll ();
		GameManager.instance.Upgrades.Cash += 1500;
		//GameManager.instance.Upgrades.ShotgunUnlocked = 1;
		//GameManager.instance.Upgrades.RifleUnlocked = 1;
		messageWindow = transform.Find("Message Panel/Message").gameObject;
		revolverBullets = transform.Find("InGame/Weapon Information/Revolver Bullets").gameObject;
		shotgunBullets = transform.Find("InGame/Weapon Information/Shotgun Bullets").gameObject;
		rifleBullets = transform.Find("InGame/Weapon Information/Rifle Bullets").gameObject;
		InitializeGUI ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void InitializeGUI () 
	{
		InitializeLevels ();
		InitializeUpgrades ();

	}

	public void InitializeLevels () 
	{
		GameObject levels = transform.Find("Levels/Board").gameObject;
		for(int x =  GameManager.instance.Upgrades.levelsUnlocked ;  x >= 0 ; x--)
		{
			levels.transform.GetChild(x).GetComponent<UIButton>().isEnabled = true;
		}
	}

	public void InitializeUpgrades () 
	{
		if(GameManager.instance.Upgrades.RevolverCone == 0)
		{
			GameObject revolver1 = transform.Find("Upgrades/Board/Revolver1").gameObject;
			revolver1.GetComponent<UIButton>().isEnabled = true;
			revolver1.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = GameManager.instance.Upgrades.revolver1.ToString();
		}

		if(GameManager.instance.Upgrades.RevolverReload == 0)
		{
			GameObject revolver2 = transform.Find("Upgrades/Board/Revolver2").gameObject;
			revolver2.GetComponent<UIButton>().isEnabled = true;
			revolver2.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = GameManager.instance.Upgrades.revolver2.ToString();
		}

		if(GameManager.instance.Upgrades.ShotgunCone == 0)
		{
			GameObject shotgun1 = transform.Find("Upgrades/Board/Shotgun1").gameObject;
			shotgun1.GetComponent<UIButton>().isEnabled = true;
			shotgun1.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = GameManager.instance.Upgrades.shotgun1.ToString();
		}
		
		if(GameManager.instance.Upgrades.ShotgunPellets == 0)
		{
			GameObject shotgun2 = transform.Find("Upgrades/Board/Shotgun2").gameObject;
			shotgun2.GetComponent<UIButton>().isEnabled = true;
			shotgun2.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = GameManager.instance.Upgrades.shotgun2.ToString();
		}

		if(GameManager.instance.Upgrades.RifleCone == 0)
		{
			GameObject rifle1 = transform.Find("Upgrades/Board/Rifle1").gameObject;
			rifle1.GetComponent<UIButton>().isEnabled = true;
			rifle1.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = GameManager.instance.Upgrades.rifle1.ToString();
		}
		
		if(GameManager.instance.Upgrades.RifleMobility == 0)
		{
			GameObject rifle2 = transform.Find("Upgrades/Board/Rifle2").gameObject;
			rifle2.GetComponent<UIButton>().isEnabled = true;
			rifle2.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = GameManager.instance.Upgrades.rifle2.ToString();
		}

		if(GameManager.instance.Upgrades.Armor1 == 0)
		{
			GameObject Armor1 = transform.Find("Upgrades/Board/Armor1").gameObject;
			Armor1.GetComponent<UIButton>().isEnabled = true;
			Armor1.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = GameManager.instance.Upgrades.armor1.ToString();
		}
		
		if(GameManager.instance.Upgrades.Armor2 == 0)
		{
			GameObject Armor2 = transform.Find("Upgrades/Board/Armor2").gameObject;
			Armor2.GetComponent<UIButton>().isEnabled = true;
			Armor2.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = GameManager.instance.Upgrades.armor2.ToString();
		}

		if(GameManager.instance.Upgrades.Armor3 == 0)
		{
			GameObject Armor3 = transform.Find("Upgrades/Board/Armor3").gameObject;
			Armor3.GetComponent<UIButton>().isEnabled = true;
			Armor3.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = GameManager.instance.Upgrades.armor3.ToString();
		}
		
		if(GameManager.instance.Upgrades.Armor4 == 0)
		{
			GameObject Armor4 = transform.Find("Upgrades/Board/Armor4").gameObject;
			Armor4.GetComponent<UIButton>().isEnabled = true;
			Armor4.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = GameManager.instance.Upgrades.armor4.ToString();
		}

		if(GameManager.instance.Upgrades.Dodge == 0)
		{
			GameObject dodge = transform.Find("Upgrades/Board/Dodge").gameObject;
			dodge.GetComponent<UIButton>().isEnabled = true;
			dodge.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = GameManager.instance.Upgrades.dodge.ToString();
		}
		
		if(GameManager.instance.Upgrades.Money == 0)
		{
			GameObject money = transform.Find("Upgrades/Board/Money").gameObject;
			money.GetComponent<UIButton>().isEnabled = true;
			money.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = GameManager.instance.Upgrades.money.ToString();
		}


		GameObject upgradeCash = transform.Find("Upgrades/Board/Player Cash/Value").gameObject;
		upgradeCash.GetComponent<UILabel>().text = "$ "+ GameManager.instance.Upgrades.Cash.ToString();


	}


	public void UpdateCash(int value)
	{
		GameObject inGameCash = transform.Find("Upgrades/Board/Player Cash/Value").gameObject;
		inGameCash.GetComponent<UILabel>().text = "$ "+value.ToString();

		GameObject upgradesCash = transform.Find("InGame/Player Information/Cash/Cash Value").gameObject;
		upgradesCash.GetComponent<UILabel>().text = "$ "+value.ToString();
	}

	public void UpdateDynamite(int value)
	{
		GameObject inGameCash = transform.Find("InGame/Weapon Information/Dynamite/Dynamite Number").gameObject;
		inGameCash.GetComponent<UILabel>().text = value.ToString();
		
	}

	public void UpdateWaveCounter(int current, int total, bool bossWave = false)
	{
		if(!bossWave)
		{
			GameObject waveCurrent = transform.Find("InGame/Wave Information/Waves/Wave Label").gameObject;
			waveCurrent.GetComponent<UILabel>().text = "Wave "+current.ToString()+" of "+ total.ToString();

		} else
		{
			GameObject waveCurrent = transform.Find("InGame/Wave Information/Waves/Wave Label").gameObject;
			waveCurrent.GetComponent<UILabel>().text = "Boss Wave!";

		}
		
	}

	public void UpdateWaveTimer(int time , bool bossWave = false)
	{
		if(!bossWave)
		{
			GameObject inGameCash = transform.Find("InGame/Wave Information/Timer/Wave Timer Number").gameObject;
			inGameCash.GetComponent<UILabel>().text = time.ToString();
		} else
		{
			GameObject inGameCash = transform.Find("InGame/Wave Information/Timer/Wave Timer Number").gameObject;
			inGameCash.GetComponent<UILabel>().text = " - ";
		}
	}
	public void RestartWaveInformation()
	{
		UpdateWaveCounter (0,0);
		UpdateWaveTimer(0,true);
	}
	public void UpdateDodgeCooldown(float value)
	{
		GameObject dodgeCooldown = transform.Find("InGame/Player Information/Dodge Background/Dodge").gameObject;

		dodgeCooldown.GetComponent<UISlider>().value = value;

	}

	public void disableTweenColor()
	{
		UIEventTrigger.current.transform.GetChild(0).GetComponentInChildren<TweenColor>().ResetToBeginning();
		UIEventTrigger.current.transform.GetChild(0).GetComponentInChildren<TweenColor>().enabled = false;
	}

	public void ShowPanel(GameObject panel)
	{
		NGUITools.SetActive (panel, true);
	}

	public void DisablePanel(GameObject panel)
	{
		NGUITools.SetActive (panel, false);
	}


	public void StartLevel()
	{
		NGUITools.SetActive (levelsPanel.gameObject, false);
		NGUITools.SetActive (upgradesPanel.gameObject, false);
		NGUITools.SetActive (creditsPanel.gameObject, false);
		NGUITools.SetActive (mainMenu.gameObject, false);
		loadScreen.GetComponent<TweenAlpha> ().ResetToBeginning ();
		NGUITools.SetActive (loadScreen.gameObject, true);
		NGUITools.SetActive (inGameGUI.gameObject, true);
	}

	public void BackToMenu()
	{
		NGUITools.SetActive (mainMenu.gameObject, true);
		NGUITools.SetActive (inGameGUI.gameObject, false);
	}


	public void RestartLoadScreen()
	{
		GameObject defeatScreen = transform.Find("InGame/Defeat Panel").gameObject;
		NGUITools.SetActive (defeatScreen , false);
		GameObject victoryScreen = transform.Find("InGame/Victory Panel").gameObject;
		NGUITools.SetActive (victoryScreen , false);
		UpdateDodgeCooldown (0);
		loadScreen.GetComponent<TweenAlpha> ().PlayReverse ();

	}

	public void ShowDefeatScreen()
	{
		GameObject defeatScreen = transform.Find("InGame/Defeat Panel").gameObject;
		NGUITools.SetActive (defeatScreen , true);
		
	}

	public void ShowVictoryScreen()
	{
		GameObject victoryScreen = transform.Find("InGame/Victory Panel").gameObject;
		NGUITools.SetActive (victoryScreen , true);
		GameObject victoryText = transform.Find("InGame/Victory Panel/Sprite/Poster/Win Text 2").gameObject;
		victoryText.GetComponent<UILabel> ().text = "$ xxxx Cash Reward." ;
		
	}


	void OnLevelWasLoaded(int level) {

		loadScreen.GetComponent<TweenAlpha> ().PlayForward ();
		
	}

	public void RevolverBullets(float bulletsLeft, float bulletsTotal)
	{
		GameObject revolverBullets = transform.Find("InGame/Weapon Information/Revolver Bullets").gameObject;

		for(int x = (int)bulletsLeft; x < bulletsTotal ; x++)
		{
			NGUITools.SetActive(revolverBullets.transform.GetChild(x).gameObject, false);
		}

		for(int x = (int)bulletsLeft -  1; x >= 0 ; x--)
		{
			NGUITools.SetActive(revolverBullets.transform.GetChild(x).gameObject, true);
		}
	}

	public void ShowRevolverBullets()
	{
		NGUITools.SetActive (revolverBullets, true);
		NGUITools.SetActive (shotgunBullets, false);
		NGUITools.SetActive (rifleBullets, false);

	}

	public void ShotgunBullets(float bulletsLeft, float bulletsTotal)
	{
		GameObject shotgunBullets = transform.Find("InGame/Weapon Information/Shotgun Bullets").gameObject;
		
		for(int x = (int)bulletsLeft; x < bulletsTotal ; x++)
		{
			NGUITools.SetActive(shotgunBullets.transform.GetChild(x).gameObject, false);
		}
		
		for(int x = (int)bulletsLeft -  1; x >= 0 ; x--)
		{
			NGUITools.SetActive(shotgunBullets.transform.GetChild(x).gameObject, true);
		}
	}

	public void ShowShotgunBullets()
	{
		NGUITools.SetActive (revolverBullets, false);
		NGUITools.SetActive (shotgunBullets, true);
		NGUITools.SetActive (rifleBullets, false);
		
	}

	public void RifleBullets(float bulletsLeft, float bulletsTotal)
	{
		GameObject rifleBullets = transform.Find("InGame/Weapon Information/Rifle Bullets").gameObject;
		
		for(int x = (int)bulletsLeft; x < bulletsTotal ; x++)
		{
			NGUITools.SetActive(rifleBullets.transform.GetChild(x).gameObject, false);
		}
		
		for(int x = (int)bulletsLeft -  1; x >= 0 ; x--)
		{
			NGUITools.SetActive(rifleBullets.transform.GetChild(x).gameObject, true);
		}
	}

	public void ShowRifleBullets()
	{
		NGUITools.SetActive (revolverBullets, false);
		NGUITools.SetActive (shotgunBullets, false);
		NGUITools.SetActive (rifleBullets, true);
		
	}

	public void HealthGUI(float healthLeft, float healthTotal)
	{
		GameObject healthIcons = transform.Find("InGame/Player Information/Health/Health Value").gameObject;
		
		for(int x = (int)healthLeft; x < healthTotal ; x++)
		{
			NGUITools.SetActive(healthIcons.transform.GetChild(x).gameObject, false);
		}
		
		for(int x = (int)healthLeft -  1; x >= 0 ; x--)
		{
			NGUITools.SetActive(healthIcons.transform.GetChild(x).gameObject, true);
		}

	}

	public void ArmorGUI(float armorLeft , float armorTotal)
	{
		GameObject armorIcons = transform.Find("InGame/Player Information/Health/Armor Value").gameObject;
		
		for(int x = (int)armorLeft; x < armorTotal ; x++)
		{
			NGUITools.SetActive(armorIcons.transform.GetChild(x).gameObject, false);
		}
		
		for(int x = (int)armorLeft -  1; x >= 0 ; x--)
		{
			NGUITools.SetActive(armorIcons.transform.GetChild(x).gameObject, true);
		}
		
	}
	public void ReloadBarActive(bool active)
	{
		GameObject healthbar = transform.Find("InGame/ReloadBar").gameObject;
		healthbar.GetComponent<UISlider> ().value = 0;
		NGUITools.SetActive(healthbar, active);
		
	}

	public void ReloadBarUpdate(float barValue)
	{
		GameObject healthbar = transform.Find("InGame/ReloadBar").gameObject;
		healthbar.GetComponent<UISlider> ().value = barValue;
		
	}


	// Buying Upgrades

	public void BuyRevolverCone()
	{
		if(GameManager.instance.Upgrades.Cash >= GameManager.instance.Upgrades.revolver1)
		{
			GameManager.instance.Upgrades.Cash -= GameManager.instance.Upgrades.revolver1;
			GameManager.instance.Upgrades.RevolverCone = 1;
			SoundManager.instance.clipOneShot(cashSound);

			GameObject revolver1 = transform.Find("Upgrades/Board/Revolver1").gameObject;
			revolver1.GetComponent<UIButton>().isEnabled = false;
			revolver1.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = "Sold";
		} else
		{
			notEnoughMoney();
		}
		
	}

	public void BuyRevolverReload()
	{
		if(GameManager.instance.Upgrades.Cash >= GameManager.instance.Upgrades.revolver2)
		{
			GameManager.instance.Upgrades.Cash -= GameManager.instance.Upgrades.revolver2;
			GameManager.instance.Upgrades.RevolverReload = 1;
			SoundManager.instance.clipOneShot(cashSound);
			GameObject revolver2 = transform.Find("Upgrades/Board/Revolver2").gameObject;
			revolver2.GetComponent<UIButton>().isEnabled = false;
			revolver2.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = "Sold";
		} else
		{
			notEnoughMoney();
		}
	}


	public void BuyShotgunCone()
	{
		if(GameManager.instance.Upgrades.Cash >= GameManager.instance.Upgrades.shotgun1)
		{
			GameManager.instance.Upgrades.Cash -= GameManager.instance.Upgrades.shotgun1;
			GameManager.instance.Upgrades.ShotgunCone = 1;
			SoundManager.instance.clipOneShot(cashSound);
			GameObject shotgun1 = transform.Find("Upgrades/Board/Shotgun1").gameObject;
			shotgun1.GetComponent<UIButton>().isEnabled = false;
			shotgun1.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = "Sold";
		} else
		{
			notEnoughMoney();
		}
		
	}
	
	public void BuyShotgunPellet()
	{
		if(GameManager.instance.Upgrades.Cash >= GameManager.instance.Upgrades.revolver2)
		{
			GameManager.instance.Upgrades.Cash -= GameManager.instance.Upgrades.shotgun2;
			GameManager.instance.Upgrades.ShotgunPellets = 1;
			SoundManager.instance.clipOneShot(cashSound);
			GameObject shotgun2 = transform.Find("Upgrades/Board/Shotgun2").gameObject;
			shotgun2.GetComponent<UIButton>().isEnabled = false;
			shotgun2.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = "Sold";
		} else
		{
			notEnoughMoney();
		}
	}

	public void BuyRifleCone()
	{
		if(GameManager.instance.Upgrades.Cash >= GameManager.instance.Upgrades.rifle1)
		{
			GameManager.instance.Upgrades.Cash -= GameManager.instance.Upgrades.rifle1;
			GameManager.instance.Upgrades.RifleCone = 1;
			SoundManager.instance.clipOneShot(cashSound);
			GameObject rifle1 = transform.Find("Upgrades/Board/Rifle1").gameObject;
			rifle1.GetComponent<UIButton>().isEnabled = false;
			rifle1.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = "Sold";
		} else
		{
			notEnoughMoney();
		}
		
	}
	
	public void BuyRifleMobility()
	{
		if(GameManager.instance.Upgrades.Cash >= GameManager.instance.Upgrades.rifle2)
		{
			GameManager.instance.Upgrades.Cash -= GameManager.instance.Upgrades.rifle2;
			GameManager.instance.Upgrades.RifleMobility = 1;
			SoundManager.instance.clipOneShot(cashSound);
			GameObject rifle2 = transform.Find("Upgrades/Board/Rifle2").gameObject;
			rifle2.GetComponent<UIButton>().isEnabled = false;
			rifle2.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = "Sold";
		} else
		{
			notEnoughMoney();
		}
	}

	public void BuyDodge()
	{
		if(GameManager.instance.Upgrades.Cash >= GameManager.instance.Upgrades.dodge)
		{
			GameManager.instance.Upgrades.Cash -= GameManager.instance.Upgrades.dodge;
			GameManager.instance.Upgrades.Dodge = 1;
			SoundManager.instance.clipOneShot(cashSound);
			GameObject dodge = transform.Find("Upgrades/Board/Dodge").gameObject;
			dodge.GetComponent<UIButton>().isEnabled = false;
			dodge.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = "Sold";
		} else
		{
			notEnoughMoney();
		}
		
	}
	
	public void BuyMoney()
	{
		if(GameManager.instance.Upgrades.Cash >= GameManager.instance.Upgrades.money)
		{
			GameManager.instance.Upgrades.Cash -= GameManager.instance.Upgrades.money;
			GameManager.instance.Upgrades.Money = 1;
			SoundManager.instance.clipOneShot(cashSound);
			GameObject money = transform.Find("Upgrades/Board/Money").gameObject;
			money.GetComponent<UIButton>().isEnabled = false;
			money.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = "Sold";
		} else
		{
			notEnoughMoney();
		}
	}

	public void BuyArmor1()
	{
		if(GameManager.instance.Upgrades.Cash >= GameManager.instance.Upgrades.armor1)
		{
			GameManager.instance.Upgrades.Cash -= GameManager.instance.Upgrades.armor1;
			GameManager.instance.Upgrades.Armor1 = 1;
			SoundManager.instance.clipOneShot(cashSound);
			GameObject armor1 = transform.Find("Upgrades/Board/Armor1").gameObject;
			armor1.GetComponent<UIButton>().isEnabled = false;
			armor1.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = "Sold";
		} else
		{
			notEnoughMoney();
		}
	}

	public void BuyArmor2()
	{
		if(GameManager.instance.Upgrades.Cash >= GameManager.instance.Upgrades.armor2)
		{
			GameManager.instance.Upgrades.Cash -= GameManager.instance.Upgrades.armor2;
			GameManager.instance.Upgrades.Armor2 = 1;
			SoundManager.instance.clipOneShot(cashSound);
			GameObject armor2 = transform.Find("Upgrades/Board/Armor2").gameObject;
			armor2.GetComponent<UIButton>().isEnabled = false;
			armor2.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = "Sold";
		} else
		{
			notEnoughMoney();
		}
	}

	public void BuyArmor3()
	{
		if(GameManager.instance.Upgrades.Cash >= GameManager.instance.Upgrades.armor3)
		{
			GameManager.instance.Upgrades.Cash -= GameManager.instance.Upgrades.armor3;
			GameManager.instance.Upgrades.Armor3 = 1;
			SoundManager.instance.clipOneShot(cashSound);
			GameObject armor3 = transform.Find("Upgrades/Board/Armor3").gameObject;
			armor3.GetComponent<UIButton>().isEnabled = false;
			armor3.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = "Sold";
		} else
		{
			notEnoughMoney();
		}
	}

	public void BuyArmor4()
	{
		if(GameManager.instance.Upgrades.Cash >= GameManager.instance.Upgrades.armor4)
		{
			GameManager.instance.Upgrades.Cash -= GameManager.instance.Upgrades.armor4;
			GameManager.instance.Upgrades.Armor4 = 1;
			SoundManager.instance.clipOneShot(cashSound);
			GameObject armor4 = transform.Find("Upgrades/Board/Armor4").gameObject;
			armor4.GetComponent<UIButton>().isEnabled = false;
			armor4.transform.FindChild("Value Background/Value").GetComponent<UILabel>().text = "Sold";
		} else
		{
			notEnoughMoney();
		}
	}
	public void notEnoughMoney()
	{
		GameObject noMoney = transform.Find("Upgrades/Board/No Money").gameObject;
		noMoney.GetComponent<TweenAlpha> ().ResetToBeginning();
		noMoney.GetComponent<TweenAlpha> ().PlayForward ();

	}

	public void ShowMessage(string message, float duration)
	{
		messageWindow.transform.FindChild ("Text").GetComponent<UILabel> ().text = message;
		messageWindow.GetComponent<TweenAlpha> ().ResetToBeginning ();
		messageWindow.GetComponent<TweenAlpha> ().duration = duration;
		messageWindow.GetComponent<TweenAlpha> ().PlayForward();

	}
}
