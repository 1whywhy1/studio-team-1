using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float moveSpeed = 10f;
	public float jumpHeight = 10f;

	private CharacterController controller;
	private Animator anim;
	private Camera mainCam;
	private float gravity = 0f;
	private float jumpVelocity = 0f;
	private string state = "Movement";

	// Start is called before the first frame update
	void Start()
	{
		controller = GetComponent<CharacterController>();
		anim = GetComponent<Animator>();
		mainCam = Camera.main;
	}

	// Update is called once per frame
	void Update()
	{
		Movement();
		Jump();
		Animation();

		if (state == "Jump")
		{
			Jump();
			Movement();
		}
	}

	void Movement()
	{
		float x = Input.GetAxisRaw("Horizontal");
		float z = Input.GetAxisRaw("Vertical");

		Vector3 direction = new Vector3(x, 0, z);
		float cameraDirection = mainCam.transform.localEulerAngles.y;
		direction = Quaternion.AngleAxis(cameraDirection, Vector3.up) * direction;
		Vector3 velocity = direction * moveSpeed * Time.deltaTime;

		float percentSpeed = velocity.magnitude / (moveSpeed * Time.deltaTime);
		// anim.SetFloat("movePercent", percentSpeed);

		if (controller.isGrounded)
		{
			gravity = 0;
		}
		else
		{
			gravity += 0.25f;
			gravity = Mathf.Clamp(gravity, 1f, 20f);
		}

		Vector3 gravityVector = -Vector3.up * gravity * Time.deltaTime;
		controller.Move(velocity + gravityVector);

		if (velocity.magnitude > 0)
		{
			float yAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
			transform.localEulerAngles = new Vector3(0, yAngle, 0);
		}

		if (Input.GetKeyDown(KeyCode.LeftShift) && controller.isGrounded) {}
	}

	void ReturnToMovement()
	{
	}

	void Jump()
	{
		if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
		{
			jumpVelocity = jumpHeight;
			ChangeState("Jump");
		}

		void ChangeState(string stateName)
		{
			state = "Jump";
			anim.SetTrigger("Jump");
		}

		if (Input.GetKeyUp(KeyCode.Space) && controller.isGrounded)
		{
			ChangeState("Movement");
		}
	}

	void Animation()
	{
		// While [W] or [S] key button is pressed
		if (Input.GetKey(KeyCode.W))
		{
			anim.SetFloat("MoveFrontBack", 1f);

			if (Input.GetKey(KeyCode.A))
			{
				anim.SetFloat("MoveLeftRight", -1f);
			}
			else if (Input.GetKey(KeyCode.D))
			{
				anim.SetFloat("MoveLeftRight", 1f);
			}
			else
			{
				anim.SetFloat("MoveLeftRight", 0f);
			}
		}
		else if (Input.GetKey(KeyCode.S))
		{
			anim.SetFloat("MoveFrontBack", -1f);
		}
		else
		{
			anim.SetFloat("MoveFrontBack", 0f);
		}
	}
}
