using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public abstract class WeaponController : MonoBehaviour {

	
	protected float power ;
	protected int currentAmunition;

	protected bool fliped=false;
	protected int maxAmunition;

	protected SpriteRenderer spriteRenderer;

	protected Vector2 barrelEndPos;
	[SerializeField] protected  GameObject bullet;

	
	
	protected abstract void fire();

	//Este metodo compurbea la posicion del arma para evitar que este hacia abajo 
	//true si esta hacia la izquierda
	//false si esta hacia la derecha

	protected void checkFlip(){

		float rot = transform.rotation.eulerAngles.z;

		if(rot<270 & rot>90){
			
			spriteRenderer.flipY=true;
			spriteRenderer.sortingOrder=1;

			fliped = true;

		}else{

			spriteRenderer.flipY=false;
			spriteRenderer.sortingOrder=2;
			fliped = false;

		}
	}

	protected void CalcBarrelEndPos(Vector2 pos){

		if(fliped){
			pos.y*=-1;
		}

		//Para calcular la posicion del final de cañon hay que sumar la posicion de la pistola mas un vector que indique la posicion del cañon
		//El problema es que cuando la pistota gira , ese vector debe de rotar por la misma cantidad (y rotar un vector no es facil)

		//https://matthew-brett.github.io/teaching/rotation_2d.html
		
		float angle = transform.rotation.eulerAngles.z;

		//Se convierte a radianes ya que la funcion Mathf.Cos() y Mathf.Sin() tiene como entrada radianes
		angle*= Mathf.Deg2Rad;

		//valores del vector de la  posicion del final del cañon
		float x1=pos.x;
		float y1=pos.y;
			
	 		//x2=cosβx1−sinβy1
			//y2=sinβx1+cosβy1

		float x2 = Mathf.Cos(angle) * x1 - Mathf.Sin(angle) * y1;
		float y2 = Mathf.Sin(angle) * x1 + Mathf.Cos(angle) * y1;

		barrelEndPos = transform.position;

		barrelEndPos.x += x2;
		barrelEndPos.y += y2;

	}
	



}
