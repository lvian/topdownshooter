using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public UIPanel levelsPanel, upgradesPanel, creditsPanel;

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
		Debug.Log (UIEventTrigger.current.transform.GetChild(0).GetComponentInChildren<TweenColor>());
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
}
