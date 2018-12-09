using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {

	[SerializeField] protected float powerConsuptionPerSeg;
	[SerializeField] protected AudioClip StartClip;
	[SerializeField] protected AudioClip MidClip;
	[SerializeField] protected AudioClip EndClip;
	AudioSource audioSource;

	void Start(){
		audioSource = GetComponent<AudioSource>();
	}
	public abstract void activate();
	public abstract void desactivate();

	public float PowerConsuptionPerSeg {
		get {
			return powerConsuptionPerSeg;
		}
		set {
			powerConsuptionPerSeg = value;
		}
	}

	


	protected void audioRuniteStart(){
		
		Debug.Log("started");
		audioSource.clip = StartClip;
		audioSource.Play();
		StartCoroutine(MidAudioRutine());
		

	}
	protected IEnumerator MidAudioRutine(){
		yield return new WaitForSeconds(StartClip.length);
		if(!audioSource.clip.Equals(EndClip)){
			audioSource.clip = MidClip;
			audioSource.Play();
			Debug.Log("Mid");
		}
		
	}

	protected void audioRuniteStop(){
		audioSource.clip = EndClip;
		audioSource.Play();
		Debug.Log("Ended");
	}
}