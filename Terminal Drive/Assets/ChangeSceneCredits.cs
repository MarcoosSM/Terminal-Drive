using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeSceneCredits : MonoBehaviour {

	[SerializeField] private Object choosenScene;
	[SerializeField] Sprite open;
	[SerializeField] Sprite close;
	SpriteRenderer sr;
	AudioSource audioSource;
	
	void Start() {
		sr = GetComponent<SpriteRenderer>();
		audioSource = GetComponent<AudioSource>();
	}

	void OnTriggerStay2D(Collider2D other) {
		if (Input.GetButtonDown("Use") && other.gameObject.tag == "Player") {
			sr.sprite = open;
			audioSource.Play();
			StartCoroutine(pause(other));
		}
	}

	IEnumerator pause(Collider2D other) {
		yield return new WaitForSeconds(1f);
		sr.sprite = close;
		ChangeScene();
	}

	public void ChangeScene() {
		SceneManager.LoadScene(choosenScene.name);
	}

	
}
