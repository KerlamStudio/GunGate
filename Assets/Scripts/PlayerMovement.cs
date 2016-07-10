using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{	
	public Camera cam;
	public CharacterController controller;
	public GameObject GateEffect;
	public GameObject laser;


	public float teleportTime;
	private float teleportTimeLeft;
	private bool IsTeleporting = false;
	private Vector3 teleportPosition;


	void Start()
	{
		teleportTimeLeft = teleportTime;
	}

	void FixedUpdate()
	{
	}

	void Update()
	{
		CheckInputs ();

		if (IsTeleporting)
		{
			teleportTimeLeft -= Time.deltaTime;
			if (teleportTimeLeft <= 0)
			{
				Teleport (teleportPosition);
				IsTeleporting = false;
				teleportTimeLeft = teleportTime;

			}
		}

	}

	void CheckInputs()
	{
		// Teleportation
		if (Input.GetMouseButtonDown (0))
		{
			Teleport();
		}

		if(Input.GetMouseButtonDown(1))
		{
			Shoot();
		}
	}

	Vector3 GetHitPoint()
	{

		Ray ray =  cam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, Mathf.Infinity))
		{
			return hit.point;
		}
		return Vector3.zero;
	}

	void Teleport()
	{
		Vector3 hit = GetHitPoint();
		if (hit != Vector3.zero)
		{
			IsTeleporting = true;
			teleportPosition = hit;
			SpawnEffect (GateEffect, hit + new Vector3(0, controller.bounds.size.y / 2));
		}
	}

	void Teleport(Vector3 position)
	{
		controller.transform.position = position + new Vector3(0, controller.bounds.size.y / 2, 0);
	}

	void SpawnEffect(GameObject effect, Vector3 pos)
	{
		Instantiate (effect, pos, effect.transform.rotation);
	}

	void Shoot()
	{
		Shoot(controller.transform.position);
	}

	void Shoot(Vector3 origin)
	{
		transform.LookAt(GetHitPoint());
		Instantiate(laser, origin, controller.transform.rotation);
	}
}
