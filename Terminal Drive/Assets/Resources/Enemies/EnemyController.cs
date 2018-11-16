using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	[SerializeField]float health;
	private Animator animator;
	private Transform target;
	private bool skipMove;

	void Start () {
		health=100;
	}
	
	// Update is called once per frame
	void Update () {
		if(health<=0){
			Destroy(gameObject);
		}
	}

	public float Health{
		get{
			return health;
		}
		set{
			health = value;
		}
	}

	public void takeDamage(float damageAmount) {
		health -= damageAmount;
	}
}
