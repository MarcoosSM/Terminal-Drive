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

	protected override void reload() {
		if(TotalBullets>0){
			CalcChargerPos();
			//Cargador
			GameObject tempCharger = Instantiate(charger, FinalchargerPos,transform.parent.localRotation);

			StartCoroutine(rechargingDelay());

			Debug.Log("recargado");
		}
	}

	protected override void  fire(){
		if(readyToFire && !recharging){
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
						reload();
					}
				}

			}else{
				if(!recharging) {
					reload();
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
		recharging=true;
		animator.SetBool("reloading", true);
		yield return new WaitForSeconds(RecharingTime);
		if(TotalBullets>=maxAmunition){
			currentAmunition=maxAmunition;
			TotalBullets-=maxAmunition;
		}else{
			currentAmunition=TotalBullets;
			TotalBullets=0;
		}
		recharging=false;
		animator.SetBool("reloading", false);
 	}
	void OnEnable(){
		Debug.Log("enable");
		checkEnableAmunition();
	}
	 
}
