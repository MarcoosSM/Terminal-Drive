using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletPanelController : MonoBehaviour {

	[SerializeField]GameObject HandGunBulletsPanel;
	[SerializeField]Sprite HandGunBullet;
	[SerializeField]Sprite HandGunCassing;

	[SerializeField]GameObject ShotGunBulletsPanel;
	[SerializeField]Sprite ShotGunBullet;
	[SerializeField]Sprite ShotGunCassing;

	[SerializeField]GameObject MachineGunBulletsPanel;
	[SerializeField]Sprite MachineGunBullet;
	[SerializeField]Sprite MachineGunCassing;

	WeaponController currentWeaponController;
	public void checkCurrentWeapon(WeaponController weaponController){

		currentWeaponController = weaponController;

		disableAllPanels();

		if(currentWeaponController is HandGunController){
			HandGunBulletsPanel.SetActive(true);

		}else if(currentWeaponController is SawdOffShotGunController){
			ShotGunBulletsPanel.SetActive(true);

		}else if(currentWeaponController is SubmachineGunController){
			MachineGunBulletsPanel.SetActive(true);

		}

		checkCurrentBullets();
	}
	public void checkCurrentBullets(){

		int currentAmmo = currentWeaponController.currentMagazineAmmo;
		int MaxAmmo = currentWeaponController.magazineSize;
		int empyMagazine = MaxAmmo-currentAmmo;

		
		if(currentWeaponController is HandGunController){
			
			foreach(Transform children in HandGunBulletsPanel.transform){
				if(!(empyMagazine==0)){
					children.GetComponent<Image>().sprite =HandGunCassing;
					--empyMagazine;
				}else{
					children.GetComponent<Image>().sprite = HandGunBullet;
				}
			}

		}else if(currentWeaponController is SawdOffShotGunController){
			
			foreach(Transform children in ShotGunBulletsPanel.transform){
				if(!(empyMagazine==0)){
					children.GetComponent<Image>().sprite = ShotGunCassing;
					--empyMagazine;
				}else{
					children.GetComponent<Image>().sprite = ShotGunBullet;
				}
			}

		}else if(currentWeaponController is SubmachineGunController){
			foreach(Transform children in MachineGunBulletsPanel.transform){
				if(!(empyMagazine==0)){
					children.GetComponent<Image>().sprite = MachineGunCassing;
					--empyMagazine;
				}else{
					children.GetComponent<Image>().sprite = MachineGunBullet;
				}
			}
		}
	}
	private void disableAllPanels(){
		HandGunBulletsPanel.SetActive(false);
		MachineGunBulletsPanel.SetActive(false); 
		ShotGunBulletsPanel.SetActive(false);
	}

}
