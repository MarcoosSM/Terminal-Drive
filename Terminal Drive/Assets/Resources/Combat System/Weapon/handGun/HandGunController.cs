using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGunController : WeaponController {

	void Awake() {
		getAllComponents();
	}

	void Start () {

		maxAmunition = 10;
		currentAmunition=maxAmunition;

		readyToFire=true;
		recharging=false;

		rawBarrelPos=new Vector2(-0.25f,0.12f);
		rawEjectorPos=new Vector2(0,0.15f);
		RawchargerPos=new Vector2(0.16f,-0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		//animacion recarga
		if (currentAmunition == 0) {
			animator.SetInteger("ammo", 0);
		}
		
		if(Input.GetButtonDown("Fire1")) {
 			fire();
		}

        checkFlip();
	}

	protected override void  fire(){
		if(readyToFire){
			CalcBarrelEndPos();
			CalcEjectorEndPos();
			
			if(currentAmunition > 0) {

				//Bala
				GameObject tempbullet = Instantiate(bullet,barrelEndPos ,transform.parent.localRotation);
				Projectil project = tempbullet.GetComponent<Projectil>();
				project.Damage=ProjDamage;
				project.Speed=ProjSpeed;

				//Casquillo
				GameObject tempCap = Instantiate(cap, ejectorEndPos ,transform.parent.localRotation);

				//Resta de la cantidad de municion
				--currentAmunition;
				
				//Sonido
				SourceAudio.Play();
				
				if(currentAmunition == 0) {
					if(!recharging) {
						StartCoroutine(rechargingDelay());
					}
				}

			}else{
				if(!recharging){
					StartCoroutine(rechargingDelay());
				}
			}
			StartCoroutine(FireDelay());
		}


	}
	override protected IEnumerator FireDelay(){
		readyToFire=false;
		yield return new WaitForSeconds(60/PPM);
		readyToFire=true;
 	}
	override protected IEnumerator rechargingDelay(){
		Debug.Log("recargando");
		
		CalcChargerPos();
		animator.SetBool("reloading",true);
		//Cargador
		GameObject tempCharger = Instantiate(charger, FinalchargerPos,transform.parent.localRotation);
		recharging=true;

		yield return new WaitForSeconds(RecharingTime);
		currentAmunition=maxAmunition;
		
		//animacion recargado
		animator.SetInteger("ammo", currentAmunition);
		animator.SetBool("reloading",false);
		recharging=false;

		Debug.Log("recargado");
		
 	}

	 
}
