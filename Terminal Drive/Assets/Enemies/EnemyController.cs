using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	[SerializeField]float health;
	void Start () {
		Debug.Log("star");
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
}
