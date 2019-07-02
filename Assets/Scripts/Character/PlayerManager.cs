using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	[Header("Player Movement Specs")]
	public float speed = 1;
	public float rotationSpeed = 80;
	public float rotation = 0f;
	public float gravity = 8;

	private Animator anim;
	private CharacterController charController;
	private Vector3 moveDir = Vector3.zero;

	[SerializeField] private AnimationCurve jumpFallOff;
	[SerializeField] private float jumpMultiplier;
	[SerializeField] private KeyCode jumpKey;
	[SerializeField] private float movementSpeed;

	[Header("Inventory")]
	public Inventory inventory;
	public GameObject inventoryScreen;
	private bool inventoryShowing = false;

	[Header("Misc")]
	private Camera mainCam;

	// Start is called before the first frame update
	void Start()
	{
		charController = GetComponent<CharacterController>();
		anim = GetComponent<Animator>();
		inventoryScreen.SetActive(inventoryShowing);
		mainCam = Camera.main;
	}

	// Update is called once per frame
	void Update()
	{
		float horizInput = Input.GetAxis("Horizontal") * movementSpeed;
		float vertInput = Input.GetAxis("Vertical") * movementSpeed;

		// If character is touching the ground
		if (charController.isGrounded)
		{
			moveDir = new Vector3(0, 0, 1);
			moveDir *= speed;
		}

		// While [W] key button is pressed
		if (Input.GetKey(KeyCode.W))
		{
			// anim.SetBool("walking", true);
			anim.SetInteger("condition", 1);
			moveDir = new Vector3(0, 0, 1);
			moveDir *= speed;
			moveDir = transform.TransformDirection(moveDir);

			Vector3 rightMovement = transform.right * horizInput;
			Vector3 forwardMovement = transform.forward * vertInput;
			charController.SimpleMove(forwardMovement + rightMovement);
		}

		if (Input.GetKeyUp(KeyCode.W))
		{
			moveDir = new Vector3(0, 0, 0);
			anim.SetInteger("condition", 0);
		}

		// While [S] key button is pressed
		if (Input.GetKey(KeyCode.S))
		{
			//anim.SetBool("walking", true);
			anim.SetInteger("condition", 2);
			moveDir = new Vector3(0, 0, -1);
			moveDir *= speed;
			moveDir = transform.TransformDirection(moveDir);

			Vector3 rightMovement = transform.right * horizInput;
			Vector3 forwardMovement = transform.forward * vertInput;
			charController.SimpleMove(forwardMovement + rightMovement);
		}

		if (Input.GetKeyUp(KeyCode.S))
		{
			moveDir = new Vector3(0, 0, 0);
			anim.SetInteger("condition", 0);
		}

		if (Input.GetKeyDown(KeyCode.I))
		{
			ToggleInventory();
		}

		rotation += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

		transform.eulerAngles = new Vector3(0, rotation, 0);

		moveDir.y -= gravity * Time.deltaTime;
		charController.Move(moveDir * Time.deltaTime);
	}

	public void ToggleInventory()
	{
		// Show/hide menu
		if (!inventoryShowing)
		{
			inventoryScreen.SetActive(true);
			inventoryShowing = inventoryScreen.activeSelf;
		}
		else
		{
			inventoryScreen.SetActive(false);
			inventoryShowing = inventoryScreen.activeSelf;
		}
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		IInventoryItem item = hit.collider.GetComponent<IInventoryItem>();
		if (item != null)
		{
			inventory.AddItem(item);
		}
	}
}
