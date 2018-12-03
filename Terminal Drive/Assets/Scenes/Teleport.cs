using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	[SerializeField] Transform dest;
	[SerializeField] Sprite open;
	[SerializeField] Sprite close;
	SpriteRenderer sr;
	AudioSource audioSource;
	
	void Start() {
		sr = GetComponent<SpriteRenderer>();
		audioSource = GetComponent<AudioSource>();
	}

	void OnTriggerStay2D(Collider2D other) {
		if (Input.GetButtonDown("Use") && other.gameObject.tag == "Player") {
			sr.sprite = open;
			audioSource.Play();
			StartCoroutine(pause(other));
		}
	}

	IEnumerator pause(Collider2D other) {
		yield return new WaitForSeconds(0.5f);
		sr.sprite = close;
		other.transform.position = dest.position;
	}

}