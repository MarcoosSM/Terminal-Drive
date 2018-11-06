using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapManager : MonoBehaviour {

	public int maxCapTime = 3;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-20,20),70));
		StartCoroutine(DestroyTimeOut());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col) {
		GameObject hitObj = col.gameObject;

		if(hitObj.tag.Equals("Suelo")) {
			// Cuando el casquillo toca el suelo, se queda en la posición en la que haya caído.
			gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
		}
	}

	IEnumerator DestroyTimeOut(){
		yield return new WaitForSeconds(maxCapTime);
		Destroy(gameObject);
 	}
}
