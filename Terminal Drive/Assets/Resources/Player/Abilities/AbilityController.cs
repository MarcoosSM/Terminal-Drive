using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour {

	[SerializeField] float currentEnergy;
	[SerializeField] float EnergyRegen;
	[SerializeField] float maxEnergy;
	private Ability currentAbility;
	private bool AbilityActivated;

	void Start() {
		currentAbility = GameObject.FindGameObjectWithTag("Power").GetComponent<Ability>();
		AbilityActivated = false;
		StartCoroutine(EnergyControl());
	}

	void FixedUpdate() {
		if (Input.GetButton("Ability") && currentEnergy >= 0) {
			//Para que solo se active una vez
			if (!AbilityActivated) {
				currentAbility.activate();
				AbilityActivated = true;
			}

		} else {
			if (AbilityActivated) {
				currentAbility.desactivate();
				AbilityActivated = false;
			}

		}
	}
	IEnumerator EnergyControl() {
		//Precision de la energia (Menos segundos mas precision)
		float segs = 0.01f;
		//For ever
		while (true) {
			if (AbilityActivated) {
				currentEnergy -= currentAbility.PowerConsuptionPerSeg * segs;
			} else {
				if (currentEnergy >= maxEnergy) {
					currentEnergy = maxEnergy;

				} else {
					currentEnergy += EnergyRegen * segs;

				}
			}
			yield return new WaitForSeconds(segs);
		}

	}

	public float CurrentEnergy{
		get{
			return currentEnergy;
		}
		set{
			currentEnergy=value;
		}
	}
		public float MaxEnergy{
		get{
			return  maxEnergy;
		}
		set{
			 maxEnergy=value;
		}
	}
}