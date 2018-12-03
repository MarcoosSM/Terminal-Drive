using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControl : MonoBehaviour {

	GameObject Cursor;
	GameObject weapon;
	WeaponController weaponController;
	private Animator animator;

	void Start() {
		weapon = transform.GetChild(0).gameObject;
		weaponController = weapon.GetComponent<WeaponController>();;
		animator = transform.parent.GetComponent<Animator>();
		Cursor = GameObject.FindGameObjectWithTag("Pointer");
	}

	public void resetWeaponReference(GameObject newGun) {
		weapon = newGun;
		weaponController = weapon.GetComponent<WeaponController>();;
	}

	// Update is called once per frame
	void FixedUpdate() {

		//El offset es para que el arma apunte al cursor y el brazo no
		Vector2 cursorPos = Cursor.transform.position;
		weaponController.CalcBarrelEndPos();
		Vector2 WeaponTrueOffset = (Vector2) weaponController.gameObject.transform.position - weaponController.BarrelEndPos;
		Vector2 cursorOffset = cursorPos + WeaponTrueOffset;

		Vector2 playerPos = GameObject.Find("Player").transform.position;

		// if = death zone
		if ((playerPos - cursorPos).magnitude > (playerPos - (Vector2) weapon.transform.position).magnitude) {
			//Calcular el angulo del brazo
			Vector2 dir = ((Vector2) transform.position) - cursorOffset;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

			// Comprueba si hace falta hacer flip al personaje dependiendo de dónde esté apuntando
			animator.SetBool("isRight", (angle < -90 || angle > 90));
		}

		//Para ver si el brazo se encuetra detras o delante del jugador
		if (animator.GetBool("isRight")) {
			GetComponent<SpriteRenderer>().sortingOrder = 14;
		} else {
			GetComponent<SpriteRenderer>().sortingOrder = 5;
		}

	}

}