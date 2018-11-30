using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	[SerializeField] Transform dest;
	[SerializeField] Sprite open;
	[SerializeField] Sprite close;
	SpriteRenderer sr;
	AudioSource audioSource;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		audioSource =  GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Player" && Input.GetKeyDown("e")){
			sr.sprite = open;
			audioSource.Play();
			StartCoroutine(pause(other));
			
		}
	}

	IEnumerator pause(Collider2D other){
		yield return new WaitForSeconds(0.5f);
		sr.sprite = close;
		other.transform.position = dest.position;
		
	}

}
