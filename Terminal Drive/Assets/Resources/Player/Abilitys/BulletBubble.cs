using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBubble : Ability {

	Collider2D shield;
	// Use this for initialization
	void Start () {
		shield  = GetComponent<Collider2D>();
		shield.enabled=false;
	}

	public override void activate(){
		shield.enabled=true;
	}
	public override void desactivate(){
		shield.enabled=false;
	}

	  void OnTriggerEnter2D(Collider2D col)
    {

		if(col.gameObject.CompareTag("Projectile")){
			Rigidbody2D projRB = col.gameObject.GetComponent<Rigidbody2D>(); 
			projRB.velocity=Vector2.zero;
			//projRB.gravityScale=1;
		}
    }
}
