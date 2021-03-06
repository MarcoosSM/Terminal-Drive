﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBubble : Ability {

	Collider2D shield;
	List<Rigidbody2D> projectilesRB;
    private Animator animator;
	GameObject player;

	void Awake() {
		shield = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
		shield.enabled = false;
		projectilesRB = new List<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	public override void activate() {
		Debug.Log("activated");
		animator.SetBool("usingShield", true);
		shield.enabled = true;
		player.GetComponent<PlayerControl>().canMove = false;
		audioRuniteStart();
	}

	public override void desactivate() {
		Debug.Log("deactivated");
		foreach (Rigidbody2D ProjRB in projectilesRB) {
			if (ProjRB != null) {
				ProjRB.gravityScale = 1;
			}
		}
		projectilesRB.Clear();
		animator.SetBool("usingShield", false);
		shield.enabled = false;
		player.GetComponent<PlayerControl>().canMove = true;
		audioRuniteStop();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("Projectile")) {
			col.gameObject.GetComponent<Projectil>().Damage = 0;
			Rigidbody2D projRB = col.gameObject.GetComponent<Rigidbody2D>();
			projRB.velocity = Vector2.zero;
			//se añade al array para uso mas tarde
			projectilesRB.Add(projRB);
		}
	}

}