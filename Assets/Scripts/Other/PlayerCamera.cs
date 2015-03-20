using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	public GameObject player;
	private Camera camera;
	private bool isShaking;
	private float shakeValue, shakeTime;
	private Vector2 pos;
	private Vector2 mousePos;
	private float newX, newY;
	private Vector3 newPosCamera, newPosPlayer;
	// Use this for initialization
	void Start () {
		isShaking = false;
		camera = GetComponent<Camera> ();
		camera.transform.position = new Vector3(player.transform.position.x,player.transform.position.y, camera.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {

		mousePos = Camera.main.ScreenToViewportPoint (Input.mousePosition);

		newPosCamera.x = (mousePos.x - 0.5f)*2;
		newPosCamera.y = (mousePos.y - 0.5f)*2 ;
		newPosCamera.z = camera.transform.position.z;
		if(shakeTime > 0)
		{
			pos = Random.insideUnitCircle * shakeValue;
			camera.transform.position = new Vector3( camera.transform.position.x - pos.x , camera.transform.position.y - pos.y , transform.localPosition.z);
			shakeTime -= Time.deltaTime;
		} 
		//Debug.Log (Vector3.Distance(newPosCamera , new Vector3(mousePos.x , mousePos.y ,  newPosCamera.z)));
		//if(Vector3.Distance(newPosCamera , new Vector3(mousePos.x , mousePos.y ,  newPosCamera.z))
		camera.transform.position = Vector3.MoveTowards(camera.transform.position, player.transform.position + newPosCamera  , 3f * Time.deltaTime);
	}

	public void shakeCamera(float value, float time)
	{
		shakeTime = time;
		shakeValue = value;
		isShaking = true;

	}
}
