using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPointerControl : MonoBehaviour {

	[SerializeField] GameObject target;
	[SerializeField]GameObject hand;
	Camera mainCamera;
	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
		Debug.Log(mainCamera);

		foreach (Transform child in transform){
			if(child.name=="Hand"){
				
				hand = child.gameObject;
				Debug.Log(hand);
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector3 pointer = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
 		
		target.transform.position = new Vector3(pointer.x, pointer.y, 0);

		var dir = transform.position - target.transform.position;
 		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
 		hand.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


    }

		
		
	
}
