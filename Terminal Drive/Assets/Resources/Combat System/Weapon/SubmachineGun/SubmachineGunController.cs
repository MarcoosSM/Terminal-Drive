using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmachineGunController : WeaponController {

void Awake() {
		getAllComponents();
	}

	void Start () {

		reserveAmmo = 25;
		currentMagazineAmmo=reserveAmmo;

		readyToFire=true;
		recharging=false;

		rawBarrelPos=new Vector2(-0.25f,0.12f);
		rawEjectorPos=new Vector2(0,0.15f);
		RawchargerPos=new Vector2(0.03f,-0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Fire1")) {
 			fire();
		}

        checkFlip();
	}

	protected override void fire(){
		if(readyToFire){
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
	/* override protected IEnumerator FireDelay(){
		readyToFire=false;
		yield return new WaitForSeconds(60/PPM);
		readyToFire=true;
 	}*/
	override protected IEnumerator rechargingDelay(){
		Debug.Log("recargando");
		
		CalcChargerPos();
		animator.SetBool("reloading",true);
		//Cargador
		GameObject tempCharger = Instantiate(charger, FinalchargerPos,transform.parent.localRotation);
		recharging=true;

		yield return new WaitForSeconds(RecharingTime);
		currentMagazineAmmo=reserveAmmo;
		
		//animacion recargado
		animator.SetInteger("ammo", currentMagazineAmmo);
		animator.SetBool("reloading",false);
		recharging=false;

		Debug.Log("recargado");
		
 	}

    protected override void reload()
    {
        throw new System.NotImplementedException();
    }
	
}
