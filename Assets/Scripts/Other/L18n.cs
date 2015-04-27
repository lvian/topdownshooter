using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;

public class L18n : MonoBehaviour {
	public static L18n instance;
	public string currentLanguage;
	public Dictionary<string,FileInfo> languages;
	public Dictionary<string,string> database;

	void Awake(){
		if(instance == null)
			instance = this;
		else if(instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad (this);
		database = new Dictionary<string, string>();
		languages = new Dictionary<string, FileInfo>();
	}

	// Use this for initialization
	void Start(){
		/*
		DirectoryInfo info = new DirectoryInfo("Assets/l18n/");
		FileInfo[] f = info.GetFiles("*.xml");
		//Debug.Log(f.Length);
		for(int i = 0; i < f.Length; i++){
			AddLanguage(f[i]);
		}

		//LoadDatabase(languages["American English"].FullName);
		//LoadDatabase(languages["Brazilian Portuguese"].FullName);
		LoadDatabase(languages[currentLanguage].FullName);

		foreach( string key in database.Keys ){
			Debug.Log("<entry key=\""+ key +">"+database[key]+"</entry>");
		}
		*/
	}

	void LoadDatabase(string fileFullPath){
		StreamReader streamReader = new StreamReader(fileFullPath);
		string text = streamReader.ReadToEnd();
		streamReader.Close();
		using (XmlReader reader = XmlReader.Create(new StringReader(text)))
		{
			bool key = true;
			string sKey = "";
			while (reader.Read())
			{
				switch (reader.NodeType)
				{
				case XmlNodeType.Element:
					if(reader.Name.Equals("entry")){
						if(key){
							sKey = reader.GetAttribute("key");
							key = false;
						}
					}
					if(reader.Name.Equals("language")){
						if(key)
							currentLanguage = reader.GetAttribute("name");
					}
					break;
				case XmlNodeType.Text:
					if(!key){
						key = true;
						database.Add(sKey, reader.Value);
					}
					break;
				}
			}
		}
	}

	void AddLanguage(FileInfo fileInfo){
		//Debug.Log(fileInfo.FullName);
		StreamReader streamReader = new StreamReader(fileInfo.FullName);
		string text = streamReader.ReadToEnd();
		streamReader.Close();
		using (XmlReader reader = XmlReader.Create(new StringReader(text)))
		{
			reader.ReadToFollowing("language");
			//Debug.Log(" n " + reader.Name);
			string key = reader.GetAttribute("name");
			//Debug.Log(key);
			if(key != null)
				languages.Add(key, fileInfo);
		}
	}

	public string GetText(string key){
		return database[key];
	}
}
