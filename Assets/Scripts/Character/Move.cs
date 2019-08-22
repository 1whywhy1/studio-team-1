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
    public bool isGrounded;

    public AudioClip pickUpSFX, jumpUp, jumpLand;

    private CharacterController Ccontroller;
	private Animator anim;
	private Camera cam;

	private float gravity = 0f;
	private float jumpVelocity = 0f;

	private string state = "Movement";

	/// <summary>
	/// Trading stuff
	/// </summary>
	public PlayerInControl inControl;
	private TradingSystem tradingScript;
	private Collider npcToTradeWith;
	private NPCInventory npcInventory;
	[SerializeField] private bool inTradingRange;

	void Start()
	{
		tradingScript = GameObject.Find("EGOPlayerInventory").GetComponent<TradingSystem>();
		Ccontroller = GetComponent<CharacterController>();
		anim = GetComponent<Animator>();
		cam = Camera.main;
	}

	private void Update()
	{
		// only only action when close (in range of collider)
		if (inTradingRange && tradingScript)
		{
			if (Input.GetKeyDown(KeyCode.E) && npcToTradeWith != null)
			{
				// Debug.Log(npcToTradeWith);
				tradingScript.SendMessage("ToggleBarterUi", npcToTradeWith);
			}
		}
	}

	void LateUpdate()
	{
		// what to activate/deactivate when character is being controlled
		if (isControlling)
		{
			Movement();

		}
	}

	void OnTriggerEnter(Collider other)
	{
		// assign designated NPC to variables
		npcToTradeWith = other;
		npcInventory = npcToTradeWith.GetComponent<NPCInventory>();

        // assign 'in range' to true
        if (other.CompareTag("NPC"))
        {
            inTradingRange = true;
        }

        // play pick up sound
        if (other.CompareTag("Pick Up"))
        {
            AudioSource.PlayClipAtPoint(pickUpSFX, other.transform.position);
        }
    }

	void OnTriggerExit(Collider other)
	{
		// empty npc variable's when vicinity left
		npcInventory = null;
		npcToTradeWith = null;

		// only make actions when true (in range)
		if (other.CompareTag("NPC"))
		{
			inTradingRange = false;
		}
	}

	void Movement()
	{
        

		float x = Input.GetAxisRaw("Horizontal");
		float z = Input.GetAxisRaw("Vertical");

		Vector3 direction = new Vector3(x, 0, z).normalized;
		float cameraDirection = cam.transform.localEulerAngles.y;
		direction = Quaternion.AngleAxis(cameraDirection, Vector3.up) * direction;

		Vector3 velocity = direction * (moveSpeed * Time.deltaTime);
		float percentSpeed = velocity.magnitude / (moveSpeed * Time.deltaTime);

		anim.SetFloat("movePercent", percentSpeed);

		if (Ccontroller.isGrounded)
		{
			jumpCount = jumpMax;
			//gravity = 1f;
            isGrounded = true;
			anim.SetBool("isGrounded", true);
			state = "Movement";
		}
		else
		{
            isGrounded = false;
            anim.SetBool("isGrounded", false);
            gravity += 0.2f;
			gravity = Mathf.Clamp(gravity, -20f, 20f);
		}

		Vector3 gravityVector = -Vector3.up * gravity * Time.deltaTime;
		Ccontroller.Move(velocity + gravityVector);

		if (velocity.magnitude > 0)
		{
			float yAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
			transform.localEulerAngles = new Vector3(0, yAngle, 0);
		}

		if (Input.GetKeyDown(KeyCode.Space)&& jumpCount > 0)
		{
			Jump();
		}
	}

	void Jump()
	{
        print(Vector3.up);
        isGrounded = false;
		gravity = -jumpHeight;
		jumpCount--;
		jumpVelocity -= jumpHeight;
		state = "Jump";
		anim.SetTrigger("Jump");
		anim.SetBool("isGrounded", false);
	}
}
