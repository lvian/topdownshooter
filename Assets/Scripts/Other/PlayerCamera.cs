using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	public GameObject player;
	private Camera camera;
	private bool isShaking;
	private float shakeValue, shakeTime;
	private Vector2 pos;
	// Use this for initialization
	void Start () {
		isShaking = false;
		camera = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(shakeTime > 0)
		{
			pos = Random.insideUnitCircle * shakeValue;
			camera.transform.position = new Vector3( camera.transform.position.x - pos.x , camera.transform.position.y - pos.y , transform.localPosition.z);
			shakeTime -= Time.deltaTime;
		} 

		camera.transform.position = Vector3.MoveTowards(new Vector3(camera.transform.position.x , camera.transform.position.y , camera.transform.position.z), new Vector3(player.transform.position.x , player.transform.position.y, camera.transform.position.z) , 2 * Time.deltaTime);
	
	}

	public void shakeCamera(float value, float time)
	{
		shakeTime = time;
		shakeValue = value;
		isShaking = true;

	}
}
