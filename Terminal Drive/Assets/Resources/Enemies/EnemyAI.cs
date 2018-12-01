using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
public class EnemyAI : MonoBehaviour {
	
	//attributes
	private GameObject player;
	private Animator animator;
	[SerializeField]float health;
	[SerializeField]float speed;
	GameObject arm;
	[SerializeField]WeaponController weapon;

	private float rangeStartMovement = 0.3f;
	//Distancia a la que el enemigo para de perseguir al jugador(de cerca)
	[SerializeField]float stopRange;
	[SerializeField]private float VisionRange;
	private bool playerSeen;
	private bool folowingplayer;

	void Awake() {
		arm = transform.GetChild(0).gameObject;
		playerSeen=false;
		folowingplayer=false;
		animator = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");
		weapon.BulletTargetTag = "Player";
	}

	// Update is called once per frame
	void Update () {
		if(health<=0){
			Destroy(gameObject);
		}
	}

	void FixedUpdate() {
		move();
	}

	public float Health{
		get{
			return health;
		}
		set{
			health = value;
		}
	}

	public float Speed{
		get{
			return speed;
		}
		set{
			speed = value;
		}
	}

 	 private void move() {
        if(folowingplayer) {
			animator.SetFloat("Speed", speed);
        } else {
            animator.SetFloat("Speed", 0);
        }

    }

	public void takeDamage(float damageAmount) {
		health -= damageAmount;
	}


	//tasks

	/*
	* Move to the player position at the current speed.
	*/
	[Task]
	void MoveToPlayer()
	{
		float armOffset = 180;	
		playerSeen=false;
  		RaycastHit2D[] hits;
		Vector3 destination = player.transform.position;
		Vector3 delta = (destination - transform.position);
		hits=Physics2D.RaycastAll(transform.position,delta,VisionRange);
		foreach (RaycastHit2D hit in hits)
		{
				if(hit.collider.gameObject.Equals(player)){
					playerSeen=true;
				}
		}
		
	   	
		
		if(playerSeen){	
			if(delta.magnitude>stopRange){
				folowingplayer=true;

				Vector3 velocity = speed*delta.normalized;

				transform.position = transform.position + velocity * Time.deltaTime;

				Vector3 newDelta = (destination - transform.position);
				float d = newDelta.magnitude;

				if (Task.isInspected)
					Task.current.debugInfo = string.Format("d={0:0.000}", d);

				if ( Vector3.Dot(delta, newDelta) <= 0.0f || d < 1e-3)
				{
					transform.position = destination;
					Task.current.Succeed();
					d = 0.0f;
					Task.current.debugInfo = "d=0.000";
				}
				
			}else
			{
				folowingplayer=false;
			}

			Vector2 dir = (Vector2)(transform.position-player.transform.position);
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			angle-=armOffset;
			arm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

			weapon.fire();
				
		}else{
			folowingplayer=false;
		}
	}
}
