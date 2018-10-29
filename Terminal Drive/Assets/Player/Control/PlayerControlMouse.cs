using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlMouse : MonoBehaviour {

	[SerializeField] GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector3 springPlace = gameObject.GetComponent<Camera>().ScreenToWorldPoint
            (new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
 		
		  target.transform.position = new Vector3(springPlace.x, springPlace.y, 0);
            
        }
		
	
}
