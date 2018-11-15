using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryContol : MonoBehaviour {

	[SerializeField] List<GameObject> objects ;
	GameObject currentGun;
	GameObject hand;
	HandControl handControl;
	
	void Start () {
		objects = new List<GameObject>();

		//Se cargargan las armas desde los prefab
		objects.Add(Resources.Load<GameObject>("Combat System/Weapon/handGun/Handgun"));
		objects.Add(Resources.Load<GameObject>("Combat System/Weapon/SawedOffShotGun/SawedOffShotGun"));	

		hand =  transform.GetChild(0).gameObject;
		handControl = hand.GetComponent<HandControl>();

		currentGun =  Instantiate(objects[0],hand.transform);
		
	}
	void Update () {
		if (Input.GetKeyDown("1"))
        {
            changeWeapon(objects[0]);

        }else if(Input.GetKeyDown("2")){

			changeWeapon(objects[1]);

		}
	}

	void changeWeapon (GameObject NewWeapon) {
		if(!(NewWeapon.GetComponent<WeaponController>()==currentGun.GetComponent<WeaponController>())){
			Destroy(currentGun);
			currentGun = Instantiate(NewWeapon,hand.transform);
			handControl.resetWeaponReference(NewWeapon);
			
		}
	}
}
