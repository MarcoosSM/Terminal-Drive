﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class WeaponController : MonoBehaviour {

	//Audio
	protected AudioSource SourceAudio;
	[SerializeField]protected BulletPanelController bulletPanelController;

	//Variables para cosas visuales
	protected bool fliped = false;
	protected Animator animator;
	protected SpriteRenderer spriteRenderer;

	//Variables para con respecto la municion
	[SerializeField] public int currentMagazineAmmo;
	[SerializeField] public int magazineSize;
	[SerializeField] protected float RecharingTime; //en segundos
	public int reserveAmmo; //Catidad de balas total
	public int maxReserveAmmo; // Cantidad máxima de balas total

	//Boleanos para control de disparo
	protected bool readyToFire;
	protected bool recharging;
	protected bool empty;
	protected Transform weaponTransform;
	protected Transform barrelEnd;

	//Posicionamiento de distintos componentes
	protected Vector2 ejectorEndPos;
	protected Vector2 rawEjectorPos;

	//Parametros de la bala
	[SerializeField] protected int PPM; // projectiles por minuto
	[SerializeField] protected int ProjDamage;
	[SerializeField] protected int ProjSpeed;

	//Posicionamiento de el cargador
	protected Vector2 RawchargerPos;
	protected Vector2 FinalchargerPos;
	protected Vector2 barrelEndPos;
	protected Vector2 rawBarrelPos;
	[SerializeField] protected string bulletTargetTag;
	[SerializeField] protected GameObject bullet;
	[SerializeField] protected GameObject cap;
	[SerializeField] protected GameObject charger;

	void Update() {	
		//Comprobar si el que tiene el arma es el jugador o no
		if (gameObject.transform.parent.parent.tag.Equals("Player")) {
			if (Input.GetButtonDown("Fire1")) {
				fire();
				bulletPanelController.checkCurrentBullets();	
			}
			if (Input.GetButtonDown("Reload")) {
				reload();
			}
			checkFlip();
		}
	}

	public abstract void fire();

	protected abstract void reload();

	protected IEnumerator FireDelay() {
		bulletPanelController.checkCurrentBullets();
		readyToFire = false;
		yield return new WaitForSeconds(60 / PPM);
		readyToFire = true;
	}

	protected abstract IEnumerator rechargingDelay();

	//Este metodo compurbea la posicion del arma para evitar que este hacia abajo 
	//true si esta hacia la izquierda
	//false si esta hacia la derecha
	protected void checkFlip() {

		float rot = transform.rotation.eulerAngles.z;

		if (rot < 270 & rot > 90) {
			// right
			spriteRenderer.flipY = true;
			spriteRenderer.sortingOrder = 13;
			if (gameObject.CompareTag("Handgun")) {
				weaponTransform.position = gameObject.transform.parent.TransformPoint(-0.289f, -0.025f, 0);
			}
			if (gameObject.CompareTag("SawedOffShotgun")) {
				weaponTransform.position = gameObject.transform.parent.TransformPoint(-0.362f, -0.021f, 0);
			}
			if (gameObject.CompareTag("SubmachineGun")) {
				weaponTransform.position = gameObject.transform.parent.TransformPoint(-0.384f, -0.0215f, 0);
			}
			fliped = true;

		} else {
			// left
			spriteRenderer.flipY = false;
			spriteRenderer.sortingOrder = 6;
			if (gameObject.CompareTag("Handgun")) {
				weaponTransform.position = gameObject.transform.parent.TransformPoint(-0.289f, 0.025f, 0);
			}
			if (gameObject.CompareTag("SawedOffShotgun")) {
				weaponTransform.position = gameObject.transform.parent.TransformPoint(-0.362f, 0.021f, 0);
			}
			if (gameObject.CompareTag("SubmachineGun")) {
				weaponTransform.position = gameObject.transform.parent.TransformPoint(-0.384f, 0.0215f, 0);
			}
			fliped = false;

		}

		if (fliped) {
			float temp = Mathf.Abs(barrelEnd.localPosition.y) * -1;
			barrelEnd.localPosition = new Vector3(barrelEnd.localPosition.x, temp, 0);
		} else {
			float temp = Mathf.Abs(barrelEnd.localPosition.y);
			barrelEnd.localPosition = new Vector3(barrelEnd.localPosition.x, temp, 0);
		}
	}

	public void CalcBarrelEndPos() {

		Vector2 pos = rawBarrelPos;

		if (fliped) {
			pos.y *= -1;
		}

		//Para calcular la posicion del final de cañon hay que sumar la posicion de la pistola mas un vector que indique la posicion del cañon
		//El problema es que cuando la pistota gira , ese vector debe de rotar por la misma cantidad (y rotar un vector no es facil)

		//https://matthew-brett.github.io/teaching/rotation_2d.html

		float angle = transform.rotation.eulerAngles.z;

		//Se convierte a radianes ya que la funcion Mathf.Cos() y Mathf.Sin() tiene como entrada radianes
		angle *= Mathf.Deg2Rad;

		//valores del vector de la  posicion del final del cañon
		float x1 = pos.x;
		float y1 = pos.y;

		//x2=cosβx1−sinβy1
		//y2=sinβx1+cosβy1

		float x2 = Mathf.Cos(angle) * x1 - Mathf.Sin(angle) * y1;
		float y2 = Mathf.Sin(angle) * x1 + Mathf.Cos(angle) * y1;

		barrelEndPos = transform.position;

		barrelEndPos.x += 0;
		barrelEndPos.y += 0;

	}
	protected void CalcEjectorEndPos() {

		Vector2 pos = rawEjectorPos;
		if (fliped) {
			pos.y *= -1;
		}

		//Para calcular la posicion del final de cañon hay que sumar la posicion de la pistola mas un vector que indique la posicion del cañon
		//El problema es que cuando la pistota gira , ese vector debe de rotar por la misma cantidad (y rotar un vector no es facil)

		//https://matthew-brett.github.io/teaching/rotation_2d.html

		float angle = transform.rotation.eulerAngles.z;

		//Se convierte a radianes ya que la funcion Mathf.Cos() y Mathf.Sin() tiene como entrada radianes
		angle *= Mathf.Deg2Rad;

		//valores del vector de la  posicion del final del cañon
		float x1 = pos.x;
		float y1 = pos.y;

		//x2=cosβx1−sinβy1
		//y2=sinβx1+cosβy1

		float x2 = Mathf.Cos(angle) * x1 - Mathf.Sin(angle) * y1;
		float y2 = Mathf.Sin(angle) * x1 + Mathf.Cos(angle) * y1;

		ejectorEndPos = transform.position;

		ejectorEndPos.x += x2;
		ejectorEndPos.y += y2;

	}

	/*PRUEBA*/
	protected void CalcChargerPos() { /*PRUEBA*/

		Vector2 pos = RawchargerPos;
		if (fliped) {
			pos.y *= -1;
		}

		//Para calcular la posicion del final de cañon hay que sumar la posicion de la pistola mas un vector que indique la posicion del cañon
		//El problema es que cuando la pistota gira , ese vector debe de rotar por la misma cantidad (y rotar un vector no es facil)

		//https://matthew-brett.github.io/teaching/rotation_2d.html

		float angle = transform.rotation.eulerAngles.z;

		//Se convierte a radianes ya que la funcion Mathf.Cos() y Mathf.Sin() tiene como entrada radianes
		angle *= Mathf.Deg2Rad;

		//valores del vector de la  posicion del final del cañon
		float x1 = pos.x;
		float y1 = pos.y;

		//x2=cosβx1−sinβy1
		//y2=sinβx1+cosβy1

		float x2 = Mathf.Cos(angle) * x1 - Mathf.Sin(angle) * y1;
		float y2 = Mathf.Sin(angle) * x1 + Mathf.Cos(angle) * y1;

		FinalchargerPos = transform.position;

		FinalchargerPos.x += x2;
		FinalchargerPos.y += y2;

	}

	public Vector2 BarrelEndPos {
		get {
			return barrelEndPos;
		}
	}
	public Vector2 RawBarrelEndPos {
		get {
			return rawBarrelPos;
		}
	}

	public string BulletTargetTag {
		get {
			return bulletTargetTag;
		}
		set {
			bulletTargetTag = value;
		}
	}

	protected void getAllComponents() {

		bulletPanelController = GameObject.FindGameObjectWithTag("BulletPanel").GetComponent<BulletPanelController>();

		spriteRenderer = GetComponent<SpriteRenderer>();
		weaponTransform = GetComponent<Transform>();
		SourceAudio = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();

		barrelEnd = barrelEnd = gameObject.transform.Find("BarrelEnd");

	}

	void OnEnable() {
		checkEnableAmunition();
	}

	protected void checkEnableAmunition() {
		// Para evitar que el arma se bloquee si se desactiva mientras recarga.
		recharging = false;
		animator.SetBool("reloading", false);
		if (currentMagazineAmmo == 0) {
			reload();
		}
	}
	
	void OnDisable() {
		SourceAudio.Stop();
	}
}