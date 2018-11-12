using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapManager : MonoBehaviour {

	public int maxCapTime = 3;
	private AudioSource audioSource;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-20,20),Random.Range(60,80)));
		StartCoroutine(DestroyTimeOut());
		checkFlip();

		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col) {
		GameObject hitObj = col.gameObject;

		if(hitObj.tag.Equals("Suelo")) {
			// Cuando el casquillo toca el suelo, se queda en la posición en la que haya caído.
			gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

			audioSource.Play();
		}
	}

	IEnumerator DestroyTimeOut(){
		yield return new WaitForSeconds(maxCapTime);
		Destroy(gameObject);
 	}

	void checkFlip() {
		if(GameObject.Find("Player").GetComponent<Animator>().GetBool("isRight")) {
			GetComponent<SpriteRenderer>().sortingOrder = 14;
		} else {
			GetComponent<SpriteRenderer>().sortingOrder = 4;
		}
	}
}
