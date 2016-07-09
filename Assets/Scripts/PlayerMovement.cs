using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{	
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 9.82F;
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
		// If player is on the ground
		if (controller.isGrounded)
		{
			// Get the input (ZQSD)
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;

			/* JUMP
			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
			*/
		}
	}

	void Update()
	{
		CheckInputs ();

		// Move character
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);

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
