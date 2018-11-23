using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	[SerializeField] Transform dest;
	[SerializeField] Sprite open;
	[SerializeField] Sprite close;
	SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Player" && Input.GetKeyDown("e")){
			sr.sprite = open;
			StartCoroutine(pause(other));
		}
	}

	IEnumerator pause(Collider2D other){
		yield return new WaitForSeconds(1);
		sr.sprite = close;
		other.transform.position = dest.position;
		
	}

}
