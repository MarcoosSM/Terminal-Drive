using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour {

	[SerializeField]float currentEnergy;
	[SerializeField]float EnergyRegen;
	[SerializeField]float MaxEnergy;
	private Ability currentAbility;
	private bool AbilityActivated;
	// Use this for initialization
	void Start () {
		currentAbility=GameObject.FindGameObjectWithTag("Power").GetComponent<Ability>();
		AbilityActivated=false;
		StartCoroutine(EnergyControl());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetButton("Ability") &&  currentEnergy>currentAbility.PowerConsuptionPerSeg){
			//Para que solo se active una vez
			if(!AbilityActivated){
				currentAbility.activate();
				AbilityActivated=true;
			}
			
		}else{
			if(AbilityActivated){
				currentAbility.desactivate();
				AbilityActivated=false;
			}
			
		}
	}
	IEnumerator EnergyControl() 
	{	
		//Precision de la energia (Menos segundos mas precision)
		float segs  = 0.25f;
		//For ever
		while(true){
			if(AbilityActivated){
				currentEnergy -= currentAbility.PowerConsuptionPerSeg*segs;
			}else{
				if(currentEnergy>=MaxEnergy){
					currentEnergy=MaxEnergy;
					
				}else{
					currentEnergy += EnergyRegen*segs;
					
				}
			}
			 yield return new WaitForSeconds(segs);
		}
		
	}
}
