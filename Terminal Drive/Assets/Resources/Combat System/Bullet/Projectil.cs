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

	void OnTriggerEnter2D(Collider2D col) {
		
		GameObject hittedObj = col.gameObject;
		Debug.Log(hittedObj.name);

		if(hittedObj.tag.Equals("Enemy")) {
			EnemyController ec = (EnemyController) hittedObj.GetComponent("EnemyController");
			ec.takeDamage(damage);
			
		}
		
		if(!(hittedObj.tag.Equals("Projectile") || hittedObj.tag.Equals("L2"))){
			Destroy(gameObject); // Destruye el proyectil si colisiona con algo que no sea otro projectil para evitar problemas con la escopeta
		}

		

    }

	IEnumerator DestroyTimeOut() {
		yield return new WaitForSeconds(maxBulletTime);
		Destroy(gameObject);
 	}

}
