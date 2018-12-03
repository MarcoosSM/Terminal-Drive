using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	[SerializeField] Sprite sp;
	SpriteRenderer sr;
	AudioSource audioSource;
	bool opened;
	// Use this for initialization
	void Start () {
		opened=false;
		sr = GetComponent<SpriteRenderer>();
		audioSource= gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == ("Player")){
			sr.sprite = sp;
			if(!opened){
				audioSource.Play();
				opened=true;
			}
			
		}
	}
	public bool Opened{
		get{
			return opened;
		}
	}
}
