﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectil : MonoBehaviour {

	float damage;
	int maxBulletTime = 5; //segs
	
	void Start () {

		//Daño que hace a los enemigos

		damage=10;
			//Velocidad inicial
		Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();

		rigidbody2D.AddForce(transform.right*-200);

		//DestroyTimeOut();

		StartCoroutine(DestroyTimeOut());
		
	}
	

	public float Damage {
		get{
			return damage;
		}
	}

		void OnCollisionEnter2D(Collision2D collision)
    {
		GameObject hittedObj = collision.gameObject;
		EnemyController enemy;
		//Se comprueba si el objeto con el que ha colisionado tiene el componenete projectil(Solo asignado a balas) y se lo asigna a una variable
		if((enemy = (EnemyController)hittedObj.GetComponent("EnemyController"))!=null){
			
			
			enemy.Health-=damage;

			Destroy(gameObject);
		}

		//Destroy(gameObject);
    }

	IEnumerator DestroyTimeOut(){

		Debug.Log("starttimeout");
		yield return new WaitForSeconds(maxBulletTime);
		Debug.Log("endtimeout");
		Destroy(gameObject);
 	}

	

}