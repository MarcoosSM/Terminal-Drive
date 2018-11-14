using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectil : MonoBehaviour {

	private float damage;
	private float speed;
	private int maxBulletTime = 5; //segs
	private Rigidbody2D rigidbody2d;

	private int numHits = 0;
	void Start () {

		//Velocidad inicial
		rigidbody2d= GetComponent<Rigidbody2D>();

		rigidbody2d.AddForce(transform.right*-speed);

		//DestroyTimeOut();

		StartCoroutine(DestroyTimeOut());
	}
	

	public float Damage {
		get{
			return damage;
		}
		set{
			damage=value;
		}
	}
	public float Speed {
		get{
			return speed;
		}
		set{
			speed=value;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		GameObject hittedObj = collision.gameObject;

		if(hittedObj.tag.Equals("Enemy")) {
			EnemyController ec = (EnemyController) hittedObj.GetComponent("EnemyController");
			ec.takeDamage(damage);
			Destroy(gameObject); // Destruye el proyectil
		} else {
			// Cuando el proyectil toca a otro proyectil, no se cuenta como hit
			if(!hittedObj.tag.Equals("Projectile")) {
				++numHits;
			}
			
			// Cuando rebota por segunda vez, se destruye
			if(numHits >= 2) {
				Destroy(gameObject);
			}
		}

    }

	IEnumerator DestroyTimeOut() {
		yield return new WaitForSeconds(maxBulletTime);
		Destroy(gameObject);
 	}

}
