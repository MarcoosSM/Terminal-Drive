using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawdOffShotGunController : WeaponController {

	[SerializeField] AudioClip startReloadSound;
	[SerializeField] AudioClip endReloadSound;
	[SerializeField] AudioClip fireSound;
	[SerializeField] int Dispersion = 1;
	[SerializeField] int NumProjectil = 5;

	void Awake() {
		getAllComponents();
		magazineSize = 2;
		currentMagazineAmmo = magazineSize;
		readyToFire = true;
		recharging = false;
	}

	void Start() {
		rawBarrelPos = new Vector2(-0.25f, 0.12f);
		rawEjectorPos = new Vector2(0, 0.15f);
	}

	protected override void reload() {
		if (empty && reserveAmmo > 0) {
			// Si el arma estaba en estado "vacío" y ahora tiene balas, deja de estar en estado vacío
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
			for (int i = 0; i < magazineSize; ++i) {
				//Casquillo
				Instantiate(cap, ejectorEndPos, transform.parent.localRotation);
			}
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
				for (int i = 0; i < NumProjectil; ++i) {
					float angle = Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
					float spread = Random.Range(-Dispersion, Dispersion);
					Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle + spread));

					// Instantiate the bullet using our new rotation
					GameObject tempbullet = Instantiate(bullet, barrelEnd.position, bulletRotation);
					//GameObject tempbullet = Instantiate(bullet,barrelEndPos ,transform.parent.localRotation);
					Projectil project = tempbullet.GetComponent<Projectil>();
					project.Damage = ProjDamage;
					project.Speed = ProjSpeed;
					project.TargerTag = bulletTargetTag;
				}
				//Sonido
				SourceAudio.Play();

				--currentMagazineAmmo;

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
		}
	}

	override protected IEnumerator rechargingDelay() {

		recharging = true;

		yield return new WaitForSeconds(0.25f);

		SourceAudio.clip = startReloadSound;
		SourceAudio.Play();

		//animacion recargando
		animator.SetBool("reloading", true);

		float endRecharingDelay = 0.15f;
		yield return new WaitForSeconds(RecharingTime - endRecharingDelay);

		if (reserveAmmo > 0) {
			SourceAudio.clip = endReloadSound;
			SourceAudio.Play();
		}
		yield return new WaitForSeconds(endRecharingDelay);

		int neededAmmo = magazineSize - currentMagazineAmmo;

		if (reserveAmmo >= neededAmmo) {
			currentMagazineAmmo = magazineSize;
			reserveAmmo -= neededAmmo;
		} else {
			currentMagazineAmmo += reserveAmmo;
			reserveAmmo = 0;
		}

		SourceAudio.clip = fireSound;

		//animacion recargado
		animator.SetBool("reloading", false);
		recharging = false;
	}
}