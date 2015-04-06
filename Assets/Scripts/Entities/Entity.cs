using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour, IMoveBehaviour {
	private int hitPoints;
	private int armor;
	private float speed;

	protected Transform valhalla;
	public BaseWeapon[] weapons;
	public BaseWeapon currentWeapon;

	public void Start(){
	}

	protected void InitEntity() {
		//Debug.Log("Starting entity!!");
		hitPoints = 4;
		armor = 4;
		speed = 1.5f;
		valhalla = GameObject.Find("Valhalla").transform;
	}

	#region IMoveBehaviour implementation
	public virtual void Move (Transform t)
	{
		Move();
	}
	#endregion

	public abstract void Move();
	public abstract void Died();

	public virtual int HitPoints {
		get {
			return hitPoints;
		}
		set {
			hitPoints = value;
			if(hitPoints == 0)
			{
				Died();
			}
		}
	}

	public virtual int Armor {
		get {
			return armor;
		}
		set {
			armor = value;
		}
	}

	public virtual float Speed {
		get {
			return speed;
		}
		set {
			speed = value;
		}
	}
}
