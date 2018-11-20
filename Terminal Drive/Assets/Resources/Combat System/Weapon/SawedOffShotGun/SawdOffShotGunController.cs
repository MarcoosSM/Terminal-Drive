using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawdOffShotGunController : WeaponController {

	
	[SerializeField] int Dispersion = 1; //
	[SerializeField] int NumProjectil = 5; //


	void Awake() {
		getAllComponents();
	}

	void Start () {

		maxAmunition = 2;
		currentAmunition=maxAmunition;

		readyToFire=true;
		recharging=false;

		rawBarrelPos=new Vector2(-0.25f,0.12f);
		rawEjectorPos=new Vector2(0,0.15f);
		
	}

	protected override void reload() {
		
	}
	
	protected override void  fire(){
		if(readyToFire){
			CalcBarrelEndPos();
			CalcEjectorEndPos();
			
			if(currentAmunition > 0) {
				
				//Bala
				for (int i = 0; i < NumProjectil; ++i) {
					float angle = Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
					float spread = Random.Range(-Dispersion, Dispersion);
					Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle + spread));
	
					// Instantiate the bullet using our new rotation
					GameObject tempbullet = Instantiate(bullet, barrelEndPos, bulletRotation);
					//GameObject tempbullet = Instantiate(bullet,barrelEndPos ,transform.parent.localRotation);
					Projectil project = tempbullet.GetComponent<Projectil>();
					project.Damage=ProjDamage;
					project.Speed=ProjSpeed;
				}
				//Sonido
				SourceAudio.Play();

				--currentAmunition;

				
				if(currentAmunition == 0) {
					if(!recharging){
						StartCoroutine(rechargingDelay());
					}
				}
			}else{
				if(!recharging){
					StartCoroutine(rechargingDelay());
				}
			}
			StartCoroutine(FireDelay());
		}


	}
	override protected IEnumerator FireDelay(){
		readyToFire=false;
		yield return new WaitForSeconds(60/PPM);
		readyToFire=true;
 	}
	override protected IEnumerator rechargingDelay(){
		
		//animacion recargando
		animator.SetBool("reloading",true);
		for (int i = 0; i < maxAmunition; ++i) {
			//Casquillo
			GameObject tempCap = Instantiate(cap, ejectorEndPos ,transform.parent.localRotation);
		}
		recharging=true;

		yield return new WaitForSeconds(RecharingTime);
		currentAmunition=maxAmunition;
		
		//animacion recargado
		animator.SetBool("reloading",false);
		recharging=false;

		
		
 	}
}
