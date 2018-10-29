using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	Rigidbody2D RB2d;

	// Use this for initialization
	void Start () {
		RB2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

	}
	void FixedUpdate(){

		if(Input.GetKey("d")){
			RB2d.AddForce(new Vector2(10,0));
		}
		if(Input.GetKey("a")){
			RB2d.AddForce(new Vector2(-10,0));
		}
		if(Input.GetKeyDown("w")){
			RB2d.AddForce(new Vector2(0,400));
		}
		if(Input.GetKey("s")){

		}
	}
}
