using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectil : MonoBehaviour {

	private float damage;
	private float speed;
	private int maxBulletTime = 5; //segs
	private Rigidbody2D rigidbody2d;

	private string targerTag;

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

	public string TargerTag{
		get{
			return targerTag;
		}
		set{
			targerTag=value;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		
		GameObject hittedObj = col.gameObject;
		Debug.Log(hittedObj.name);

		if(hittedObj.tag.Equals("Enemy")&&hittedObj.tag.Equals(targerTag)) {
			EnemyAI eAI = (EnemyAI) hittedObj.GetComponent("EnemyAI");
			eAI.takeDamage(damage);
			Destroy(gameObject);
		}
		if(hittedObj.tag.Equals("Player")&&hittedObj.tag.Equals(targerTag)) {
			InventoryControl IC = hittedObj.GetComponent<InventoryControl>();
			IC.takeDamage(damage);
			Destroy(gameObject);
		}
		
		if(!(hittedObj.tag.Equals("Projectile") || hittedObj.tag.Equals("L2") || hittedObj.tag.Equals("Enemy") || hittedObj.tag.Equals("Player"))){
			Destroy(gameObject); // Destruye el proyectil si colisiona con algo que no sea otro projectil para evitar problemas con la escopeta
		}

		

    }

	IEnumerator DestroyTimeOut() {
		yield return new WaitForSeconds(maxBulletTime);
		Destroy(gameObject);
 	}

}
