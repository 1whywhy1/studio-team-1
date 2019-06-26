using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float speed = 1;
    float rotSpeed = 80;
    float rot = 0f;
    float gravity = 8;

    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private float movementSpeed;

    public CharacterController charController;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;


    Vector3 moveDir = Vector3.zero;

    CharacterController controller;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // When is 
        if (controller.isGrounded)
        {
            moveDir = new Vector3(0, 0, 1);
            moveDir *= speed;
        }
        if (Input.GetKey(KeyCode.W))
        {
           // anim.SetBool("walking", true);
            anim.SetInteger("condition", 1);
            moveDir = new Vector3(0, 0, 1);
            moveDir *= speed;
            moveDir = transform.TransformDirection(moveDir);
            float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;
            float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;

            Vector3 rightMovement = transform.right * horizInput;
            Vector3 forwardMovement = transform.forward * vertInput;
            charController.SimpleMove(forwardMovement + rightMovement);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            moveDir = new Vector3(0, 0, 0);
            anim.SetInteger("condition", 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //anim.SetBool("walking", true);
            anim.SetInteger("condition", 2);
            moveDir = new Vector3(0, 0, -1);
            moveDir *= speed;
            moveDir = transform.TransformDirection(moveDir);
            float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;
            float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;

            Vector3 rightMovement = transform.right * horizInput;
            Vector3 forwardMovement = transform.forward * vertInput;
            charController.SimpleMove(forwardMovement + rightMovement);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            moveDir = new Vector3(0, 0, 0);
            anim.SetInteger("condition", 0);
        }
    
        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, rot, 0);

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);

    }
   
}
