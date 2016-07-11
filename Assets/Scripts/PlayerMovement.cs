using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{	
	public GameObject Shot1;
	public GameObject Wave;
	public Camera cam;
	public CharacterController controller;
	public GameObject GateEffect;

	public float teleportTime;
	private float teleportTimeLeft;
	private bool IsTeleporting = false;
	private Vector3 teleportPosition;
	private Vector3 moveDirection = Vector3.zero;


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
		if (Input.GetMouseButtonUp (0))
		{
			Teleport ();
		}
		if(Input.GetMouseButtonDown(1))
		{
			transform.LookAt(GetHitPoint());
			GameObject Bullet;

			Bullet = Shot1;
			//Fire
			GameObject s1 = (GameObject)Instantiate(Bullet, this.transform.position + new Vector3(0,5f,0), this.transform.rotation);
			s1.GetComponent<BeamParam>().SetBeamParam(this.GetComponent<BeamParam>());

			GameObject wav = (GameObject)Instantiate(Wave, this.transform.position + new Vector3(0,5f,0), this.transform.rotation);
			wav.transform.localScale *= 0.25f;
			wav.transform.Rotate(Vector3.left, 90.0f);
			wav.GetComponent<BeamWave>().col = this.GetComponent<BeamParam>().BeamColor;
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
		Ray ray =  cam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, Mathf.Infinity))
		{
			IsTeleporting = true;
			teleportPosition = hit.point;
			SpawnEffect (GateEffect, hit.point + new Vector3(0, controller.bounds.size.y / 2));
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
}
