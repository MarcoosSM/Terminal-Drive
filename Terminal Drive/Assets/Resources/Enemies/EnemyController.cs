using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	[SerializeField] float health;
	[SerializeField] float speed = 0;
	public float rangeStartMovement = 0.3f;
	private Animator animator;
	private Transform target;
	private bool skipMove;

	void Awake() {
		animator = GetComponent<Animator>();
	}

	void Update() {
		if (health <= 0) {
			Destroy(gameObject);
		}
	}

	void FixedUpdate() {
		move();
	}

	public float Health {
		get {
			return health;
		}
		set {
			health = value;
		}
	}

	public float Speed {
		get {
			return speed;
		}
		set {
			speed = value;
		}
	}

	private void move() {
		if (speed > rangeStartMovement) {
			animator.SetFloat("Speed", speed);
		} else {
			animator.SetFloat("Speed", 0);
		}

	}

	public void takeDamage(float damageAmount) {
		health -= damageAmount;
	}
}