using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {

	[SerializeField] Collider2D collider;
	Animator animator;
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		animator = player.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Player" && Input.GetKeyDown("e")){
			collider.enabled = true;
			GameObject player = other.gameObject;
			
			if (animator.GetBool("isRight")){
				transform.localEulerAngles = new Vector3(0,0,-90);
			} else {
				transform.localEulerAngles = new Vector3(0,0,90);
			}
		}		
	}
}
