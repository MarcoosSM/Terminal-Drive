using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawdOffShotGunController : WeaponController {

	[SerializeField] int Dispersion = 1; //
	[SerializeField] int NumProjectil = 5; //

	void Awake() {
		getAllComponents();
		magazineSize = 2;
		currentMagazineAmmo=magazineSize;
		readyToFire=true;
		recharging=false;
	}

	void Start () {
		rawBarrelPos=new Vector2(-0.25f,0.12f);
		rawEjectorPos=new Vector2(0,0.15f);
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

		if(reserveAmmo>0) {
			for (int i = 0; i < magazineSize; ++i) {
				//Casquillo
				Instantiate(cap, ejectorEndPos ,transform.parent.localRotation);
			}
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
				for (int i = 0; i < NumProjectil; ++i) {
					float angle = Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
					float spread = Random.Range(-Dispersion, Dispersion);
					Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle + spread));
	
					// Instantiate the bullet using our new rotation
					GameObject tempbullet = Instantiate(bullet, barrelEndPos, bulletRotation);
					//GameObject tempbullet = Instantiate(bullet,barrelEndPos ,transform.parent.localRotation);
					Projectil project = tempbullet.GetComponent<Projectil>();
					project.Damage=ProjDamage;
					project.Speed=ProjSpeed;
				}
				//Sonido
				SourceAudio.Play();

				--currentMagazineAmmo;

				if(currentMagazineAmmo == 0) {
					if(!recharging){
						reload();
					}
				}
			}else{
				if(!recharging){
					reload();
				}
			}
			StartCoroutine(FireDelay());
		}
	}

	override protected IEnumerator rechargingDelay(){
		//animacion recargando
		animator.SetBool("reloading",true);
		recharging=true;

		yield return new WaitForSeconds(RecharingTime);

		int neededAmmo = magazineSize - currentMagazineAmmo;

		if(reserveAmmo >= neededAmmo) {
			currentMagazineAmmo = magazineSize;
			reserveAmmo -= neededAmmo;
		} else {
			currentMagazineAmmo += reserveAmmo;
			reserveAmmo = 0;
		}

		//animacion recargado
		animator.SetBool("reloading",false);
		recharging=false;
 	}
}
