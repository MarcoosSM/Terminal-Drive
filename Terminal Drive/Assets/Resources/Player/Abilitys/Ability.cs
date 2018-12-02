using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class Ability : MonoBehaviour {

	[SerializeField]protected float powerConsuptionPerSeg;

	public abstract void activate();
	public abstract void desactivate();

	public float PowerConsuptionPerSeg{
		get{
			return powerConsuptionPerSeg;
		}
		set{
			powerConsuptionPerSeg=value;
		}
	}
}
