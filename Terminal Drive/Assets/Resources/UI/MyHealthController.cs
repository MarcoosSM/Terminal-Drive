using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyHealthController : MonoBehaviour {

	InventoryControl playerhealt ;
	Image myImage;
	[SerializeField]Sprite[] sprites;

	// Use this for initialization
	void Awake () {
		 playerhealt = GameObject.FindWithTag("Player").GetComponent<InventoryControl>();
		 myImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

		float percentOfLife = playerhealt.Health/playerhealt.MaxHealth;
		
		int index = (int)((sprites.Length-1)*(1-percentOfLife));
		if(index>=0 & index<sprites.Length){
			Debug.Log(index);
			myImage.sprite = sprites[index];
		}
	
		
	}
}
