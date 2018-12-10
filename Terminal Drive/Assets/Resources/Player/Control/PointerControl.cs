using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerControl : MonoBehaviour {

	Camera mainCamera;
	GameObject player;
	Animator animator;

	void Awake() {
		Cursor.visible = false;
	}

	// Use this for initialization
	void Start() {
		mainCamera = Camera.main;
		player = GameObject.FindGameObjectWithTag("Player");
		animator = player.GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate() {
		//Codigo para mover el cursor
		Vector3 pointer = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		gameObject.transform.position = new Vector3(pointer.x, pointer.y, 0);
	}

}