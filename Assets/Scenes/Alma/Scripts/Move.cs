using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
	public float moveSpeed = 10f;
	public float jumpHeight = 10f;
	public float jumpCount = 1f;
	public float jumpMax = 1f;
	public bool isControlling;

	private CharacterController Ccontroller;
	private Animator anim;
	private Camera cam;

	private float gravity = 0f;
	private float jumpVelocity = 0;

	private string state = "Movement";

	/// <summary>
	/// Trading stuff
	/// </summary>
	public PlayerInControl inControl;
	public TradingSystem tradingScript;
	private Collider npcToTradeWith;
	private NPCInventory npcInventory;
	[SerializeField] private bool inTradingRange;

	void Start()
	{
		Ccontroller = GetComponent<CharacterController>();
		anim = GetComponent<Animator>();
		cam = Camera.main;
	}

	void LateUpdate()
	{
		// what to activate/deactivate when character is being controlled
		if (isControlling)
		{
			Movement();
			tradingScript.enabled = true;
		}
		else
		{
			tradingScript.enabled = false;
		}

		// only only action when close (in range of collider)
		if (inTradingRange)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				tradingScript.SendMessage("ToggleBarterUi");
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		// assign designated NPC to variables
		npcInventory = other.GetComponent<NPCInventory>();
		npcToTradeWith = other;

		// only make actions when true (in range)
		if (other.CompareTag("NPC"))
			inTradingRange = true;
	}

	void OnTriggerExit(Collider other)
	{
		// empty npc variable's when vicinity left
		npcInventory = null;
		npcToTradeWith = null;

		// only make actions when true (in range)
		if (other.CompareTag("NPC"))
			inTradingRange = false;
	}

	void Movement()
	{
		float x = Input.GetAxisRaw("Horizontal");
		float z = Input.GetAxisRaw("Vertical");

		Vector3 direction = new Vector3(x, 0, z).normalized;
		float cameraDirection = cam.transform.localEulerAngles.y;
		direction = Quaternion.AngleAxis(cameraDirection, Vector3.up) * direction;

		Vector3 velocity = direction * moveSpeed * Time.deltaTime;
		float percentSpeed = velocity.magnitude / (moveSpeed * Time.deltaTime);

		anim.SetFloat("movePercent", percentSpeed);

		if (Ccontroller.isGrounded)
		{
			jumpCount = jumpMax;
			gravity = 1f;
			anim.SetBool("isGrounded", true);
			state = "Movement";
		}
		else
		{
			gravity += 0.25f;
			gravity = Mathf.Clamp(gravity, -20f, 20f);
		}

		if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
		{
			Jump();
		}

		Vector3 gravityVector = -Vector3.up * gravity * Time.deltaTime;

		Ccontroller.Move(velocity + gravityVector);

		if (velocity.magnitude > 0)
		{
			float yAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
			transform.localEulerAngles = new Vector3(0, yAngle, 0);
		}


		if (Input.GetKeyDown(KeyCode.LeftShift) && Ccontroller.isGrounded)
		{
			Jump(); // this is why it doesnt run on shift but plays jump animations
		}
	}

	void ReturnToMovement() {}

	void Jump()
	{
		gravity = -jumpHeight;
		jumpCount--;
		jumpVelocity -= jumpHeight;
		state = "Jump";
		anim.SetTrigger("Jump");
		anim.SetBool("isGrounded", false);
	}
}
