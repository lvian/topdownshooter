using UnityEngine;
using System.Collections;

public class Tooltip : MonoBehaviour {

	public string tooltipText;
	public string localizeKey;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTooltip (bool show)
	{
		if (show == true)
		{
			//CustomTooltip.Show(tooltipText);
			CustomTooltip.Show(localizeKey, true);
			return;
		}
		UITooltip.Hide();
	}
}
