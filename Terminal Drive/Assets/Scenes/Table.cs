using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {

	[SerializeField] Collider2D collider;
	Animator animator;
	GameObject player;

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		animator = player.GetComponent<Animator>();
	}

	void OnTriggerStay2D(Collider2D other) {
		if (Input.GetButtonDown("Use") && other.gameObject.tag == "Player") {
			collider.enabled = true;
			GameObject player = other.gameObject;

			if (animator.GetBool("isRight")) {
				transform.localEulerAngles = new Vector3(0, 0, -90);
			} else {
				transform.localEulerAngles = new Vector3(0, 0, 90);
			}
		}
	}

}