using UnityEngine;
using System.Collections;

public class GateEffectScript : MonoBehaviour {

	void Awake()
	{
		Debug.Log("AWAKE");
		Destroy (gameObject, GetComponentInChildren<ParticleSystem>().duration);
	}
}
