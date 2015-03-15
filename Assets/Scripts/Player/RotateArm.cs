using UnityEngine;
using System.Collections;

public class RotateArm : MonoBehaviour {

	public Animator anim;
	private AnimatorClipInfo an;
	private Quaternion lastRotation;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {

		if(anim.GetCurrentAnimatorClipInfo(0).Length > 0)
		{
			an = anim.GetCurrentAnimatorClipInfo(0)[0];
			if(anim.transform.parent.tag =="Player")
			{
				if(an.clip.name != "Reload")
				{
					//Turns the weapon towards the current mouse position
					Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
					diff.Normalize();
					float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
					transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

					if(transform.localEulerAngles.z > 13f)
					{
						transform.localRotation = lastRotation;
					} else{
						lastRotation = transform.localRotation;
					}

				}
			}
		}
	}
}
