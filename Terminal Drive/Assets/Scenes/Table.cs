using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {

	[SerializeField] Collider2D collider;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Player" && Input.GetKeyDown("e")){
			collider.enabled = true;
			//transform.Rotate(0,90,0);
		}		
	}
}
