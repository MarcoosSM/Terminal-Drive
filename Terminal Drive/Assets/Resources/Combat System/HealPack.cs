using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : MonoBehaviour {

	AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource>();
	}

	void OnTriggerStay2D(Collider2D other) {
		if (Input.GetButtonDown("Use") && other.gameObject.tag == "Player") {
			InventoryControl IC = other.GetComponent<InventoryControl>();
			IC.Health = IC.MaxHealth;
			if(!audioSource.isPlaying){
				audioSource.Play();
			}
		}
	}
}
