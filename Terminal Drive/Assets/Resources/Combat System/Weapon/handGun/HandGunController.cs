using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGunController : WeaponController {

	bool readyToFire;
	Vector2 RawBarrelPos;

	Vector2 RawEjectorPos;
	void Start () {

		power=1;
		maxAmunition = 10;
		currentAmunition=maxAmunition;

		readyToFire=true;
		spriteRenderer = GetComponent<SpriteRenderer>();

		RawBarrelPos=new Vector2(-0.25f,0.12f);
		RawEjectorPos=new Vector2(0,0.15f);
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		  if (Input.GetMouseButtonDown(0)){
 			fire();
		  }

        checkFlip();
	
	}

	protected override void  fire(){

		CalcBarrelEndPos(RawBarrelPos);
		CalcEjectorEndPos(RawEjectorPos);

		if(currentAmunition>0){
			
			//Bala
			GameObject tempbullet = Instantiate(bullet,barrelEndPos ,transform.parent.localRotation);
			Projectil project = tempbullet.AddComponent<Projectil>();
			project.Damage=10;
			project.Speed=200;

			//Casquillo
			GameObject tempCap = Instantiate(cap, ejectorEndPos ,transform.parent.localRotation);
			tempCap.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-20,20),70));

			--currentAmunition;

		}else{
			currentAmunition=maxAmunition;
			Debug.Log("recargando");
		}
	}
}
