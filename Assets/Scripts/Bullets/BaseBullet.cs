using UnityEngine;
using System.Collections;

public abstract class BaseBullet  : MonoBehaviour{
	protected string bulletName;
	public float bulletSpeed;
	public int bulletDamage;

	// Update is called once per frame
	void Update () {
		if (GameManager.instance.State == GameManager.GameState.Playing){
			transform.Translate(new Vector3(0, bulletSpeed, 0) * Time.deltaTime);
			if(Mathf.Abs(transform.position.x) > 50f || Mathf.Abs(transform.position.y) > 50)
				Destroy();
		}
	}

	public virtual void Destroy(){
		GameObject.Destroy(gameObject);
	}

	public abstract void OnTriggerEnter2D(Collider2D other);
}