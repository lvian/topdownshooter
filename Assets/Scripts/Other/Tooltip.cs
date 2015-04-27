using UnityEngine;
using System.Collections;

public class Tooltip : MonoBehaviour {

	public string tooltipText;
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
			CustomTooltip.Show(tooltipText);
			return;
		}
		UITooltip.Hide();
	}
}
