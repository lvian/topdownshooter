﻿using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour, IMoveBehaviour {
	private int hitPoints;
	private int armor;
	private float speed;
	public BaseWeapon[] weapons;
	public BaseWeapon currentWeapon;
	public GameObject leftArm, rightArm;

	protected virtual void Start(){
		hitPoints = 4;
		armor = 4;
		speed = 1f;
	}

	#region IMoveBehaviour implementation
	public virtual void Move (Transform t)
	{
		Move();
	}
	#endregion

	public abstract void Move();

	public virtual int HitPoints {
		get {
			return hitPoints;
		}
		set {
			hitPoints = value;
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
