﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectil : MonoBehaviour {

	float damage;
	
	void Start () {

		//Daño que hace a los enemigos

		damage=10;
			//Velocidad inicial
		Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();

		rigidbody2D.AddForce(transform.right*-200);

		
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

	

}
