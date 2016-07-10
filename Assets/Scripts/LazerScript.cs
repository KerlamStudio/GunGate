using UnityEngine;
using System.Collections;

public class LazerScript : MonoBehaviour {
	
	public float speed = 20;
	public float timeLeft = 30;
	void Start()
	{
	}

	void Update()
	{
		transform.position += transform.forward * speed * Time.deltaTime;

		timeLeft -= Time.deltaTime;
		if(timeLeft <= 0)
			Destroy(gameObject);
	}
}
