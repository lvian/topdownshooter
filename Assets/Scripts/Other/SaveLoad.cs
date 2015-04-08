using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public static class SaveLoad {

	public static List<Upgrades> savedCharacters = new List<Upgrades>();


	public static void Save()
	{
		savedCharacters.Add (GameManager.instance.Upgrades);
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath);
		bf.Serialize (file, SaveLoad.savedCharacters);
		file.Close ();
	}

	public static void Load() {
		if(File.Exists(Application.persistentDataPath + "/savedGames.gd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
			SaveLoad.savedCharacters = (List<Upgrades>)bf.Deserialize(file);
			file.Close();
		}
	}
}
