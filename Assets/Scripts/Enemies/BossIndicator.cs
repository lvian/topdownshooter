using UnityEngine;
using System.Collections;

public class BossIndicator : MonoBehaviour {
	public GameObject boss;
	public GameObject player;
	private Camera cCamera;
	private Camera GUICamera;
	private UISprite sprite;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<UISprite>();
		player = GameObject.Find("PlayerBase");
		cCamera = Camera.current;
		GUICamera =(Camera) GameObject.FindGameObjectWithTag ("GUI Camera").GetComponent<Camera>();
		boss = null;
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.instance.State != GameManager.GameState.Playing)
			return;
		if(boss != null && player != null){
			if(boss.GetComponentInChildren<SpriteRenderer>().isVisible){
				sprite.enabled = false;
			}
			else{
				sprite.enabled = true;
				if(cCamera == null){
					cCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
				}
				Vector3 bottomLeft = cCamera.ScreenToWorldPoint(Vector3.zero);
				Vector3 topRight = cCamera.ScreenToWorldPoint(new Vector3(
					cCamera.pixelWidth, cCamera.pixelHeight));
				
				Rect cameraRect = new Rect(
					bottomLeft.x,
					bottomLeft.y,
					topRight.x - bottomLeft.x,
					topRight.y - bottomLeft.y);

				Vector3 bossPos = new Vector3(boss.transform.position.x, boss.transform.position.y, 0);

				Vector3 newPos = new Vector3(
					Mathf.Clamp(bossPos.x, cameraRect.xMin, cameraRect.xMax),
					Mathf.Clamp(bossPos.y, cameraRect.yMin, cameraRect.yMax),
					bossPos.z);

				Vector3 guiPos = cCamera.WorldToViewportPoint(newPos);


				guiPos = GUICamera.ViewportToWorldPoint (guiPos);
				transform.localPosition = transform.parent.InverseTransformPoint(guiPos);
			}
		}
		else{
			player = GameObject.Find("PlayerBase");
			boss = GameObject.FindGameObjectsWithTag("Enemy")[0];
		}
	}
}
