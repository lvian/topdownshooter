using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public UIPanel levelsPanel, upgradesPanel, creditsPanel, inGameGUI;

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
	}
	
	// Update is called once per frame
	void Update () {
	
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
		NGUITools.SetActive (inGameGUI.gameObject, true);
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

	public void ShotgunBullets(float bulletsLeft, float bulletsTotal)
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

	public void RifleBullets(float bulletsLeft, float bulletsTotal)
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
}
