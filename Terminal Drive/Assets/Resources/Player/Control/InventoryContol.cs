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
		
		hand =  transform.GetChild(0).gameObject;
		
		//Se cargargan las armas desde los prefab
		objects.Add(Instantiate(Resources.Load<GameObject>("Combat System/Weapon/handGun/Handgun"),hand.transform));
		objects.Add(Instantiate(Resources.Load<GameObject>("Combat System/Weapon/SawedOffShotGun/SawedOffShotGun"),hand.transform));	
		objects.Add(Instantiate(Resources.Load<GameObject>("Combat System/Weapon/SubmachineGun/SubmachineGun"),hand.transform));

		Debug.Log(objects.Capacity);

		handControl = hand.GetComponent<HandControl>();

		currentGun =  objects[0];

		foreach (GameObject weapon in objects)
		{
			weapon.SetActive(false);
		}
		currentGun.SetActive(true);
				
	}
	void Update () {
		if (Input.GetKeyDown("1"))
        {
            changeWeapon(objects[0]);

        }else if(Input.GetKeyDown("2")){

			changeWeapon(objects[1]);

		}else if(Input.GetKeyDown("3")){

			changeWeapon(objects[2]);

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
