using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPointerControl : MonoBehaviour {

	[SerializeField] GameObject target;
	GameObject hand;
	GameObject weapon;
	Camera mainCamera;

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		mainCamera = Camera.main;

		//Se busca la mano el jugador
		foreach (Transform child in transform){
			if(child.name=="Hand"){
				
				hand = child.gameObject;

				weapon = hand.transform.GetChild(0).gameObject;
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Codigo para mover el cursor
		Vector3 pointer = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		target.transform.position = new Vector3(pointer.x, pointer.y, 0);

		//Cursor para mover el brazo
		var dir = transform.position - target.transform.position;
 		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
 		hand.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		//Para ver si el brazo se encuetra detras o delante del jugador

		if(animator.GetBool("isRight")){

			hand.GetComponent<SpriteRenderer>().sortingOrder=2;

		}else{

			hand.GetComponent<SpriteRenderer>().sortingOrder=1;
		}

    }

		
		
	
}
