﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerManager : MonoBehaviour {

	public int maxChargerTime = 10;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>();
		StartCoroutine(DestroyTimeOut());
		checkFlip();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col) {
		GameObject hitObj = col.gameObject;

		if(hitObj.tag.Equals("Suelo")) {
			// Cuando el cargador toca el suelo, se queda en la posición en la que haya caído.
			gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
		}
	}

	IEnumerator DestroyTimeOut(){
		yield return new WaitForSeconds(maxChargerTime);
		Destroy(gameObject);
 	}

	void checkFlip() {
		if(GameObject.Find("Player").GetComponent<Animator>().GetBool("isRight")) {
			GetComponent<SpriteRenderer>().sortingOrder = 12;
			GetComponent<SpriteRenderer>().flipY = true;
		} else {
			GetComponent<SpriteRenderer>().sortingOrder = 4;
			GetComponent<SpriteRenderer>().flipY = false;
		}
	}
}
