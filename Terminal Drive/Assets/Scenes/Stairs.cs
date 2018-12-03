using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour {

	[SerializeField] GameObject floor;
	[SerializeField] bool open;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == ("Player")) {
			floor.SetActive(!open);
		}
	}
	
}