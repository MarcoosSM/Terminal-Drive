using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour {

	
	protected float power ;
	protected int currentAmunition;

	protected int maxAmunition;

	[SerializeField] protected  GameObject bullet;

	
	
	protected abstract void fire();



}
