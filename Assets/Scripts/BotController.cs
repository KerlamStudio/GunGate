using UnityEngine;
using System.Collections;

public class BotController : MonoBehaviour {
	public GameObject GateEffect;
	public GameObject laser;
	public GameObject target;
	public float minTeleportTime = 1;
	public float maxTeleportTime = 3;
	public float minShootTime = 1;
	public float maxShootTime = 3;

	private Transform myTransform;

	private bool isTeleporting;
	private bool isShooting;

	// Use this for initialization
	void Start () {
		myTransform = GetComponent<Transform> ();
		isTeleporting = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isTeleporting) {
			Invoke ("Teleport", Random.Range (minTeleportTime, maxTeleportTime));
			isTeleporting = true;
		}
		if (!isShooting) {
			Invoke("Shoot",Random.Range (minShootTime, maxShootTime));
			isShooting = true;
		}
	}

	void Teleport()
	{
		Vector3 teleportPosition = new Vector3(Random.Range(-50,50),3,Random.Range(-50,50));
		SpawnEffect (GateEffect, teleportPosition);
		myTransform.position = teleportPosition;
		isTeleporting = false;
	}

	void SpawnEffect(GameObject effect, Vector3 pos)
	{
		Instantiate (effect, pos, effect.transform.rotation);
	}


	void Shoot()
	{
		transform.LookAt(target.GetComponent<Transform>());
		Instantiate(laser, myTransform.position, myTransform.rotation);
		isShooting = false;
	}
}
