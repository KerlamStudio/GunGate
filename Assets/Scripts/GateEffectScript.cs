using UnityEngine;
using System.Collections;

public class GateEffectScript : MonoBehaviour {

	void Awake()
	{
		Destroy (gameObject, GetComponentInChildren<ParticleSystem>().duration);
	}
}
