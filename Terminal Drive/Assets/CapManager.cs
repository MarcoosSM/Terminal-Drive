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

	IEnumerator DestroyTimeOut(){
		
		yield return new WaitForSeconds(maxCapTime);

		Destroy(gameObject);
 	}
}
