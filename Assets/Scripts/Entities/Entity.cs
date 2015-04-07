using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour, IMoveBehaviour {
	public int hitPoints;
	public int armor;
	public float speed;

	protected Transform valhalla;
	public BaseWeapon[] weapons;
	public BaseWeapon currentWeapon;

	public void Start(){
	}

	protected void InitEntity() {
		//Debug.Log("Starting entity!!");
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
