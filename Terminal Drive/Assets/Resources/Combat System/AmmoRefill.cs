using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoRefill : MonoBehaviour {

	AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource>();
	}

	void OnTriggerStay2D(Collider2D other) {
		if (Input.GetButtonDown("Use") && other.gameObject.tag == "Player") {
			List<GameObject> guns = other.GetComponent<InventoryControl>().guns;
			// Pone la munición de reserva de cada arma del jugador a la cantidad máxima de balas
			foreach (GameObject weapon in guns) {
				WeaponController wController = weapon.GetComponent<WeaponController>();
				if (wController.reserveAmmo != wController.maxReserveAmmo) {
					wController.reserveAmmo = wController.maxReserveAmmo;
					audioSource.Play();
				}

			}
		}
	}

}