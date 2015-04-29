using UnityEngine;
using System.Collections;




public class Upgrades {

	
	public  int revolver1 = 500;
	public  int revolver2 = 1000;
	public  int shotgun1 = 1000;
	public  int shotgun2 = 1500;
	public  int rifle1 = 2000;
	public  int rifle2 = 2500;
	public  int dodge = 3000;
	public  int money = 2000;
	public  int armor1 = 1000;
	public  int armor2 = 2500;
	public  int armor3 = 4000;
	public  int armor4 = 5500;
	private int cash;

	public int HealthUpgrade1 {
		get {
			return PlayerPrefs.GetInt("healthUpgrade1");
		}
		set {
			PlayerPrefs.SetInt("healthUpgrade1", value);
		}
	}

	public int HealthUpgrade2 {
		get {
			return PlayerPrefs.GetInt("healthUpgrade2");
		}
		set {
			PlayerPrefs.SetInt("healthUpgrade2", value);
		}
	}

	public int HealthUpgrade3 {
		get {
			return PlayerPrefs.GetInt("healthUpgrade3");
		}
		set {
			PlayerPrefs.SetInt("healthUpgrade3", value);
		}
	}

	public int HealthUpgrade4 {
		get {
			return PlayerPrefs.GetInt("healthUpgrade4");
		}
		set {
			PlayerPrefs.SetInt("healthUpgrade4", value);
		}
	}

	public int Armor1 {
		get {
			return PlayerPrefs.GetInt("armor1");
		}
		set {
			PlayerPrefs.SetInt("armor1", value);
		}
	}

	public int Armor2 {
		get {
			return PlayerPrefs.GetInt("armor2");
		}
		set {
			PlayerPrefs.SetInt("armor2", value);
		}
	}

	public int Armor3 {
		get {
			return PlayerPrefs.GetInt("armor3");
		}
		set {
			PlayerPrefs.SetInt("armor3", value);
		}
	}

	public int Armor4 {
		get {
			return PlayerPrefs.GetInt("armor4");
		}
		set {
			PlayerPrefs.SetInt("armor4", value);
		}
	}

	public int Dodge {
		get {
			return PlayerPrefs.GetInt("dodge");
		}
		set {
			PlayerPrefs.SetInt("dodge", value);
		}
	}

	public int Money {
		get {
			return PlayerPrefs.GetInt("money");
		}
		set {
			PlayerPrefs.SetInt("money", value);
		}
	}

	public int Cash {
		get {
			return cash;
		}
		set {
			cash = value;
			GUIManager.instance.UpdateCash(cash);
		}
	}

	public int CashToPrefs {
		get {
			return PlayerPrefs.GetInt("cash");
		}
		set {
			PlayerPrefs.SetInt("cash", value);
		}
	}



	// Revolver Upgrades 

	public int RevolverCone {
		get {
			return PlayerPrefs.GetInt("revolverCone");
		}
		set {
			PlayerPrefs.SetInt("revolverCone", value);
		}
	}

	public int RevolverReload {
		get {
			return PlayerPrefs.GetInt("revolverReload");
		}
		set {
			PlayerPrefs.SetInt("revolverReload", value);
		}
	}

	//Shotgun Upgrades 
	
	public int DynamiteUnlocked {
		get {
			return PlayerPrefs.GetInt("DynamiteUnlocked");
		}
		set {
			PlayerPrefs.SetInt("DynamiteUnlocked", value);
		}
	}


	//Shotgun Upgrades 

	public int ShotgunUnlocked {
		get {
			return PlayerPrefs.GetInt("ShotgunUnlocked");
		}
		set {
			PlayerPrefs.SetInt("ShotgunUnlocked", value);
		}
	}

	public int ShotgunCone {
		get {
			return PlayerPrefs.GetInt("ShotgunCone");
		}
		set {
			PlayerPrefs.SetInt("ShotgunCone", value);
		}
	}
	
	public int ShotgunPellets {
		get {
			return PlayerPrefs.GetInt("ShotgunPellet");
		}
		set {
			PlayerPrefs.SetInt("ShotgunPellet", value);
		}
	}


	// Rifle Upgrades 

	public int RifleUnlocked {
		get {
			return PlayerPrefs.GetInt("RifleUnlocked");
		}
		set {
			PlayerPrefs.SetInt("RifleUnlocked", value);
		}
	}

	public int RifleCone {
		get {
			return PlayerPrefs.GetInt("RifleCone");
		}
		set {
			PlayerPrefs.SetInt("RifleCone", value);
		}
	}

	public int RifleMobility {
		get {
			return PlayerPrefs.GetInt("RifleMobility");
		}
		set {
			PlayerPrefs.SetInt("RifleMobility", value);
		}
	}

	// Levels Unlocked
	public int levelsUnlocked {
		get {
			return PlayerPrefs.GetInt("levelsUnlocked");
		}
		set {
			PlayerPrefs.SetInt("levelsUnlocked", value);
		}
	}

}
