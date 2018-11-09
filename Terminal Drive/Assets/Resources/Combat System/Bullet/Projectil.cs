using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectil : MonoBehaviour {

	private float damage;
	private float speed;
	private int maxBulletTime = 5; //segs
	private Rigidbody2D rigidbody2D;

	private int numHits = 0;
	void Start () {

		//Velocidad inicial
		rigidbody2D= GetComponent<Rigidbody2D>();

		rigidbody2D.AddForce(transform.right*-speed);

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
			++numHits;
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
