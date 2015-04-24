using UnityEngine;
using System.Collections;

public class LabelLocalization : MonoBehaviour {
	public string key;
	private UILabel label;

	// Use this for initialization
	void Awake () {
		label = GetComponent<UILabel>();

	}
	
	// Update is called once per frame
	void Update () {
		label.text = L18n.instance.GetText(key);
	}
}
