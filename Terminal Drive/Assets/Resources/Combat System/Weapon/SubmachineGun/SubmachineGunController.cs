using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmachineGunController : WeaponController {

	void Awake() {
		getAllComponents();
		magazineSize = 25;
		currentMagazineAmmo = magazineSize;
		readyToFire = true;
		recharging = false;
	}

	void Start() {
		rawBarrelPos = new Vector2(-0.25f, 0.12f);
		rawEjectorPos = new Vector2(0, 0.15f);
		RawchargerPos = new Vector2(0.03f, -0.1f);
	}

	// Update is called once per frame
	void Update() {
		// Override porque el Fire1 cambia
		if (gameObject.transform.parent.parent.tag.Equals("Player")) {
			if (Input.GetButton("Fire1")) {
				fire();
			}
			if (Input.GetButtonDown("Reload")) {
				reload();
			}
			checkFlip();
		}
	}

	protected override void reload() {
		
		if (empty && reserveAmmo > 0) {
			// Si el arma estaba en estado "vacío" y ahora tiene balas para recargar, deja de estar en estado vacío
			empty = false;
			recharging = false;
			animator.SetBool("reloading", false);
		}

		if (recharging) {
			return;
			// No se hace nada si ya está en proceso de recarga
		}

		if (currentMagazineAmmo == magazineSize) {
			return;
			// No se hace nada si el cargador está lleno
		}

		if (reserveAmmo > 0) {
			//Cargador
			CalcChargerPos();
			Instantiate(charger, FinalchargerPos, transform.parent.localRotation);
			StartCoroutine(rechargingDelay());
		} else {
			if (currentMagazineAmmo == 0) {
				// Si no tiene balas y se ha intentado recargar, se queda en un estado de recarga constante (sin cargador y en rojo)
				empty = true;
				recharging = true;
				animator.SetBool("reloading", true);
			}
		}

	}

	public override void fire() {
		if (readyToFire && !recharging) {
			CalcBarrelEndPos();
			CalcEjectorEndPos();

			if (currentMagazineAmmo > 0) {
				//Bala
				GameObject tempbullet = Instantiate(bullet, barrelEnd.position, barrelEnd.rotation);
				
				Projectil project = tempbullet.GetComponent<Projectil>();
				project.Damage = ProjDamage;
				project.Speed = ProjSpeed;
				project.TargerTag = bulletTargetTag;

				//Casquillo
				GameObject tempCap = Instantiate(cap, ejectorEndPos, transform.parent.localRotation);

				//Resta de la cantidad de municion
				--currentMagazineAmmo;

				//Sonido
				SourceAudio.Play();

				if (currentMagazineAmmo == 0) {
					if (!recharging) {
						reload();
					}
				}

			} else {
				if (!recharging) {
					reload();
				}
			}
			StartCoroutine(FireDelay());
			bulletPanelController.checkCurrentBullets();	
		}
	}

	override protected IEnumerator rechargingDelay() {
		recharging = true;
		animator.SetBool("reloading", true);
		yield return new WaitForSeconds(RecharingTime);

		int neededAmmo = magazineSize - currentMagazineAmmo;

		if (reserveAmmo >= neededAmmo) {
			currentMagazineAmmo = magazineSize;
			reserveAmmo -= neededAmmo;
		} else {
			currentMagazineAmmo += reserveAmmo;
			reserveAmmo = 0;
		}
		recharging = false;
		animator.SetBool("reloading", false);
		bulletPanelController.checkCurrentBullets();
	}
}