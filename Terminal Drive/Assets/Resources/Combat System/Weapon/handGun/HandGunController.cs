﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGunController : WeaponController {

	void Awake() {
		getAllComponents();
		magazineSize = 10;
		currentMagazineAmmo=magazineSize;

		readyToFire=true;
		recharging=false;

		SourceAudio.clip = FireSound;
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

		if(currentMagazineAmmo == magazineSize) {
			return;
			// No se hace nada si el cargador está lleno
		}

		if(reserveAmmo > 0){
			//Cargador
			CalcChargerPos();
			Instantiate(charger, FinalchargerPos,transform.parent.localRotation);
			StartCoroutine(rechargingDelay());
			Debug.Log("recargado");
		} else {
			if(currentMagazineAmmo == 0) {
				// Si no tiene balas y se ha intentado recargar, se queda en un estado de recarga constante (sin cargador y en rojo)
				recharging = true;
				animator.SetBool("reloading", true);
			}	
		}
	}

	protected override void fire(){
		if(readyToFire && !recharging){
			CalcBarrelEndPos();
			CalcEjectorEndPos();
			
			if(currentMagazineAmmo > 0) {

				//Bala
				GameObject tempbullet = Instantiate(bullet,barrelEndPos ,transform.parent.localRotation);
				Projectil project = tempbullet.GetComponent<Projectil>();
				project.Damage=ProjDamage;
				project.Speed=ProjSpeed;

				//Casquillo
				GameObject tempCap = Instantiate(cap, ejectorEndPos ,transform.parent.localRotation);

				//Resta de la cantidad de municion
				--currentMagazineAmmo;
				
				//Sonido
				
				SourceAudio.Play();
				
				if(currentMagazineAmmo == 0) {
					reload();
				}

			}else{
				reload();
			}
			StartCoroutine(FireDelay());
		}


	}
	
	override protected IEnumerator rechargingDelay() {
		//Sonido
		SourceAudio.clip = ReloadSound;
		SourceAudio.Play();

		recharging=true;
		animator.SetBool("reloading", true);
		yield return new WaitForSeconds(RecharingTime);

		int neededAmmo = magazineSize - currentMagazineAmmo;

		if(reserveAmmo >= neededAmmo) {
			currentMagazineAmmo = magazineSize;
			reserveAmmo -= neededAmmo;
		} else {
			currentMagazineAmmo += reserveAmmo;
			reserveAmmo = 0;
		}
		recharging=false;
		animator.SetBool("reloading", false);

			SourceAudio.clip = FireSound;
 	}
    void OnDisable()
    {
      	SourceAudio.Stop();
		SourceAudio.clip=FireSound;	  
    }

	 
}
