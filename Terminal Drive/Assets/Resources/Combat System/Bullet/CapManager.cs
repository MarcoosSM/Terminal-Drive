using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapManager : MonoBehaviour {

	public int maxCapTime = 3;

	// Use this for initialization
	void Start () {
		StartCoroutine(DestroyTimeOut());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision) {
		GameObject hitObj = collision.gameObject;

		if(hitObj.tag.Equals("Suelo")) {
			// Cuando el casquillo toca el suelo, se desactiva la colisión y se queda en la posición en la que haya caído
			gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
			gameObject.GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	IEnumerator DestroyTimeOut(){
		yield return new WaitForSeconds(maxCapTime);
		Destroy(gameObject);
 	}
}
