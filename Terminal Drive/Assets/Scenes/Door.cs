using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	AudioSource audioSource;
	[SerializeField] Sprite sp;
	SpriteRenderer sr;

	bool opened;
	// Use this for initialization
	void Start () {
		opened=false;
		sr = GetComponent<SpriteRenderer>();
		audioSource=gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == ("Player")){
			sr.sprite = sp;
			if(!audioSource.isPlaying && !opened){
				audioSource.Play();
			}
			opened=true;
		}
	}
}
