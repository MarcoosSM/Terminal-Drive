using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyController : MonoBehaviour {

	AbilityController playerEnergia ;
	Image myImage;
	[SerializeField]Sprite[] sprites;

	// Use this for initialization
	void Awake () {
		 playerEnergia = GameObject.FindWithTag("Player").GetComponent<AbilityController>();
		 myImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("asdasd");
		float percentOfEnergy = playerEnergia.CurrentEnergy/playerEnergia.MaxEnergy;
		Debug.Log("asdasd");
		Debug.Log(percentOfEnergy +"="+ playerEnergia.CurrentEnergy+"/"+playerEnergia.MaxEnergy);
		
		int index = (int)((sprites.Length-1)*(1-percentOfEnergy));
		if(index>=0 & index<sprites.Length){
			
			myImage.sprite = sprites[index];
		}
	
		
	}
}
