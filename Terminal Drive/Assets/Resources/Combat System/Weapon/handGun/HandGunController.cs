using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGunController : WeaponController {

	void Awake() {
		getAllComponents();
		maxAmunition = 10;
		currentAmunition=maxAmunition;

		readyToFire=true;
		recharging=false;
	}

	void Start () {

		rawBarrelPos=new Vector2(-0.25f,0.12f);
		rawEjectorPos=new Vector2(0,0.15f);
		RawchargerPos=new Vector2(0.16f,-0.1f);
	}

	protected override void reload() {
		if(recharging) {
			return;
			// No se hace nada si ya está en proceso de recarga
		}

		if(currentAmunition == maxAmunition) {
			return;
			// No se hace nada si el cargador está lleno
		}

		if(TotalBullets>0){
			//Cargador
			CalcChargerPos();
			Instantiate(charger, FinalchargerPos,transform.parent.localRotation);
			StartCoroutine(rechargingDelay());
			Debug.Log("recargado");
		} else {
			// Si no tiene balas y se ha intentado recargar, se queda en un estado de recarga constante (sin cargador y en rojo)
			recharging = true;
			animator.SetBool("reloading", true);
		}
	}

	protected override void fire(){
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
					reload();
				}

			}else{
				reload();
			}
			StartCoroutine(FireDelay());
		}


	}
	
	override protected IEnumerator rechargingDelay() {
		recharging=true;
		animator.SetBool("reloading", true);
		yield return new WaitForSeconds(RecharingTime);

		int neededBullets = maxAmunition - currentAmunition;

		if(TotalBullets >= neededBullets) {
			currentAmunition = maxAmunition;
			TotalBullets -= neededBullets;
		} else {
			currentAmunition += TotalBullets;
			TotalBullets = 0;
		}
		recharging=false;
		animator.SetBool("reloading", false);
 	}
	 
}
