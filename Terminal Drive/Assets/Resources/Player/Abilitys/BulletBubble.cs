using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBubble : Ability {

	Collider2D shield;
	List<Rigidbody2D> projectilesRB;
	// Use this for initialization
	void Awake () {
		shield  = GetComponent<Collider2D>();
		shield.enabled=false;
		projectilesRB = new List<Rigidbody2D>();
	}

	public override void activate(){
		shield.enabled=true;	
	}
	public override void desactivate(){
		Debug.Log(projectilesRB.Count);
		foreach (Rigidbody2D ProjRB in projectilesRB)
		{
			if(ProjRB!=null){
				ProjRB.gravityScale=1;
			}
			
			
		}
		projectilesRB.Clear();
		shield.enabled=false;

	}

	  void OnTriggerEnter2D(Collider2D col)
    {

		if(col.gameObject.CompareTag("Projectile")){
			Rigidbody2D projRB = col.gameObject.GetComponent<Rigidbody2D>(); 
			projRB.velocity=Vector2.zero;
			//se añader al array para uso mas tarde
			projectilesRB.Add(projRB);
		}
    }
}
