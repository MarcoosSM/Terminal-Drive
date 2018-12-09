using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CurrentAmunitionBoxes : MonoBehaviour {

	
	[SerializeField]Text box1; 
	[SerializeField]Text box2; 
	[SerializeField]Text box3; 

	WeaponController Weapon1;
	WeaponController Weapon2;
	WeaponController Weapon3;

	InventoryControl InventoryControl;
	void Start(){
		InventoryControl =  GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryControl>();
		Weapon1 = InventoryControl.guns[0].GetComponent<WeaponController>();
		Weapon2 = InventoryControl.guns[1].GetComponent<WeaponController>();
		Weapon3 = InventoryControl.guns[2].GetComponent<WeaponController>();

	}
	void Update () {
		box1.text = (Weapon1.reserveAmmo).ToString();
		box2.text = (Weapon2.reserveAmmo).ToString();
		box3.text = (Weapon3.reserveAmmo).ToString();

	}
}
