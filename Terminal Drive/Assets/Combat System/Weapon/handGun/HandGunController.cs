using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGunController : WeaponController {

	bool readyToFire;
	void Start () {
		power=1;
		maxAmunition = 10;
		currentAmunition=maxAmunition;
		readyToFire=true;

		
	}
	
	// Update is called once per frame
	void Update () {
		  if (Input.GetMouseButtonDown(0))
            fire();
	}

	protected override void  fire(){
		if(currentAmunition>0){
			Vector2 barrelEnd = new Vector2(transform.rotation.x,transform.rotation.y);

			GameObject tempbullet = Instantiate(bullet,transform.position,transform.parent.localRotation);
			--currentAmunition;

		}else{
			currentAmunition=maxAmunition;
			Debug.Log("recargando");
		}
	}
}
