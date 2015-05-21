using UnityEngine;
using System.Collections;

public class BossIndicator : MonoBehaviour {
	public GameObject boss;
	public GameObject player;
	public Camera cCamera;
	public Camera GUICamera;
	private GameObject sprite;

	// Use this for initialization
	void Start () {
		sprite = transform.GetChild(0).gameObject;
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
				NGUITools.SetActive (sprite, false);
			}
			else{
				NGUITools.SetActive (sprite, true);
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

				Vector3 bossPos = new Vector3(boss.transform.position.x, boss.transform.position.y, 0f);

				Vector3 newPos = new Vector3(
					Mathf.Clamp(bossPos.x, cameraRect.xMin, cameraRect.xMax),
					Mathf.Clamp(bossPos.y, cameraRect.yMin, cameraRect.yMax),
					bossPos.z);

				Vector3 guiPos = cCamera.WorldToViewportPoint(newPos);

				float sizeX = sprite.GetComponent<UISprite>().localSize.x;
				float sizeY = sprite.GetComponent<UISprite>().localSize.y;
				guiPos = GUICamera.ViewportToWorldPoint (guiPos);
				transform.localPosition = transform.parent.InverseTransformPoint(guiPos);

				newPos = transform.localPosition;
				if(transform.localPosition.y >= (NGUITools.screenSize.y/2 - (sizeY/4))){
					newPos.y -= (sizeY/2);
				}
				else if((int)(transform.localPosition.y) <= -(NGUITools.screenSize.y/2 - (sizeY/4))){
					newPos.y += (sizeY/2);
				}
				if(transform.localPosition.x >= (NGUITools.screenSize.x/2 - (sizeX/4))){
					newPos.x -= (sizeX/2);
				}
				else if(transform.localPosition.x <= -(NGUITools.screenSize.x/2 - (sizeX/4))){
					newPos.x += (sizeX/2);
				}
				//Debug.Log(newPos);
				transform.localPosition = newPos;
			}
		}
		else{
			NGUITools.SetActive (sprite, false);
			player = GameObject.Find("PlayerBase");
			GameObject[] gos = GameObject.FindGameObjectsWithTag("Boss");
			if(gos.Length > 0){
				boss = gos[0];
			}
			UISprite bossIndicatorSprite;
			bossIndicatorSprite = sprite.GetComponent<UISprite>();
			bossIndicatorSprite.topAnchor.absolute = 16;
			bossIndicatorSprite.bottomAnchor.absolute = 16;
			bossIndicatorSprite.rightAnchor.absolute = 16;
			bossIndicatorSprite.leftAnchor.absolute = 16;
		}
	}
}
