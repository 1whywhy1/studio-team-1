using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    float speed = 1;
    float rotationSpeed = 80;
    float rotation = 0f;
    float gravity = 8;

    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private float movementSpeed;

    public CharacterController charController;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;

    private Camera mainCam;

    Vector3 moveDir = Vector3.zero;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
        mainCam = Camera.main;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;
        float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;

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

        rotation += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, rotation, 0);

        moveDir.y -= gravity * Time.deltaTime;
        charController.Move(moveDir * Time.deltaTime);
    }
}
