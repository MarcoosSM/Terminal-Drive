using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControl : MonoBehaviour {

	GameObject Cursor ;
	GameObject weapon;
	private Animator animator;

	
	
	// Use this for initialization
	void Start () {
		weapon = transform.GetChild(0).gameObject;
		animator = transform.parent.GetComponent<Animator>();
		Cursor = GameObject.FindGameObjectWithTag("Pointer");
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Cursor para mover el brazo
		var dir = transform.position - Cursor.transform.position;
 		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
 		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		// Comprueba si hace falta hacer flip al personaje dependiendo de dónde esté apuntando
		if(angle > -90 && angle < 90) {
			animator.SetBool("isRight", false);
		} else {
			animator.SetBool("isRight", true);
		}

		//Para ver si el brazo se encuetra detras o delante del jugador
		if(animator.GetBool("isRight")){
			GetComponent<SpriteRenderer>().sortingOrder=13;
		}else{
			GetComponent<SpriteRenderer>().sortingOrder=5;
		}
		
	}
}