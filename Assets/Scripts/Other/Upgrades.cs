using UnityEngine;
using System.Collections;

[System.Serializable]
public class Upgrades {

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

	public int ArmorUpgrade1 {
		get {
			return PlayerPrefs.GetInt("armorUpgrade1");
		}
		set {
			PlayerPrefs.SetInt("amorUpgrade1", value);
		}
	}

	public int ArmorUpgrade2 {
		get {
			return PlayerPrefs.GetInt("armorUpgrade2");
		}
		set {
			PlayerPrefs.SetInt("amorUpgrade2", value);
		}
	}

	public int ArmorUpgrade3 {
		get {
			return PlayerPrefs.GetInt("armorUpgrade3");
		}
		set {
			PlayerPrefs.SetInt("amorUpgrade3", value);
		}
	}

	public int ArmorUpgrade4 {
		get {
			return PlayerPrefs.GetInt("armorUpgrade4");
		}
		set {
			PlayerPrefs.SetInt("amorUpgrade4", value);
		}
	}

	public int Cash {
		get {
			return cash;
		}
		set {
			cash = value;
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

	public int RevolverDeviation {
		get {
			return PlayerPrefs.GetInt("revolverCone");
		}
		set {
			PlayerPrefs.SetInt("revolverCone", value);
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



	// Rifle Upgrades 

	public int RifleUnlocked {
		get {
			return PlayerPrefs.GetInt("RifleUnlocked");
		}
		set {
			PlayerPrefs.SetInt("RifleUnlocked", value);
		}
	}
}
