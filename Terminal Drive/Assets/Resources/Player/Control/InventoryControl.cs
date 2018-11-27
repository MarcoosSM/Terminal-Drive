using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryControl : MonoBehaviour {

	public List<GameObject> guns;
	GameObject currentGun;
	GameObject hand;
	HandControl handControl;
	
	void Awake () {
		guns = new List<GameObject>();
		
		hand =  transform.GetChild(0).gameObject;
		
		//Se cargargan las armas desde los prefab
		guns.Add(Instantiate(Resources.Load<GameObject>("Combat System/Weapon/handGun/Handgun"),hand.transform));
		guns.Add(Instantiate(Resources.Load<GameObject>("Combat System/Weapon/SawedOffShotGun/SawedOffShotGun"),hand.transform));	
		guns.Add(Instantiate(Resources.Load<GameObject>("Combat System/Weapon/SubmachineGun/SubmachineGun"),hand.transform));

		Debug.Log(guns.Capacity);

		handControl = hand.GetComponent<HandControl>();

		currentGun =  guns[0];

		foreach (GameObject weapon in guns)
		{
			weapon.SetActive(false);
		}
		currentGun.SetActive(true);
				
	}
	void Update () {
		if (Input.GetKeyDown("1"))
        {
            changeWeapon(guns[0]);

        }else if(Input.GetKeyDown("2")){

			changeWeapon(guns[1]);

		}else if(Input.GetKeyDown("3")){

			changeWeapon(guns[2]);

		}
	}

	void changeWeapon (GameObject NewWeapon) {
		if(!(NewWeapon.GetComponent<WeaponController>()==currentGun.GetComponent<WeaponController>())){
			currentGun.SetActive(false);			
			currentGun = NewWeapon;
			currentGun.SetActive(true);
			handControl.resetWeaponReference(NewWeapon);	
		}
	}
}
