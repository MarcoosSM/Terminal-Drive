using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawdOffShotGunController : WeaponController {

	
	[SerializeField] int VelocidadBala = 200 ;
	[SerializeField] int DañoDeBala = 10;
	[SerializeField] int PPM = 30; // projectiles por minuto
	[SerializeField] int RecharingTime = 2; //en segundos
	[SerializeField] int Dispersion = 1; //
	[SerializeField] int NumProjectil = 5; //

	bool readyToFire;
	bool recharging;
	private Animator animator;

	void Awake() {
		animator = GetComponent<Animator>();
	}

	void Start () {

		power=1;
		maxAmunition = 10;
		currentAmunition=maxAmunition;

		readyToFire=true;
		recharging=false;

		spriteRenderer = GetComponent<SpriteRenderer>();

		rawBarrelPos=new Vector2(-0.25f,0.12f);
		rawEjectorPos=new Vector2(0,0.15f);
		RawchargerPos=new Vector2(0.16f,-0.1f);
		
	}
	
	// Update is called once per frame
	void Update () {
		//animacion recarga
		if (currentAmunition == 0) {
			animator.SetInteger("ammo", 0);
		}
		
		if (Input.GetMouseButtonDown(0)){
 			fire();
		}

        checkFlip();
	
	}

	protected override void  fire(){
		if(readyToFire){
			CalcBarrelEndPos();
			CalcEjectorEndPos();
			
			if(currentAmunition > 0) {

				//Bala

				for (int i = 0; i < NumProjectil; ++i)
				{
					GameObject tempbullet = Instantiate(bullet,barrelEndPos ,transform.parent.localRotation);
					Projectil project = tempbullet.AddComponent<Projectil>();
					project.Damage=DañoDeBala;
					project.Speed=VelocidadBala;
				}


				//Casquillo
				GameObject tempCap = Instantiate(cap, ejectorEndPos ,transform.parent.localRotation);

				--currentAmunition;
				Debug.Log(currentAmunition);
				
			}else{
				if(!recharging){
					StartCoroutine(rechargingDelay());
				}
			}
			StartCoroutine(FireDelay());
		}


	}
	IEnumerator FireDelay(){
		readyToFire=false;
		yield return new WaitForSeconds(60/PPM);
		readyToFire=true;
 	}
	IEnumerator rechargingDelay(){
		
		CalcChargerPos();
		animator.SetBool("reloading",true);
		//Cargador
		//GameObject tempCharger = Instantiate(charger, FinalchargerPos,transform.parent.localRotation);
		recharging=true;

		yield return new WaitForSeconds(RecharingTime);
		currentAmunition=maxAmunition;
		
		//animacion recargado
		//animator.SetInteger("ammo", currentAmunition);
		//animator.SetBool("reloading",false);
		recharging=false;

		
		
 	}
}
