using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    CharacterController  Ccontroller;
    Animator anim;
    Camera cam;

    public float moveSpeed = 10f;
    public float jumpHeight = 10f;

    float gravity = 0f;
    float jumpVelocity = 0;

    string state = "Movement";

    // Start is called before the first frame update
    void Start()
    {
        Ccontroller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
        if(state == "Jump")
        {
            Jump();
            Movement();
        }
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
            gravity = 0;
        }
        else
        {
            gravity += 0.25f;
            gravity = Mathf.Clamp(gravity, 1f, 20f);
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

        }
        

    }
    void ReturnToMovement()
    {
        
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Ccontroller.isGrounded)
        {
            jumpVelocity = jumpHeight;
            ChangeState("Jump");
        }

        void ChangeState(string stateName)
        {
            state = "Jump";
            anim.SetTrigger("Jump");
        }

        if(Input.GetKeyUp(KeyCode.Space) && Ccontroller.isGrounded)
        {
            ChangeState("Movement");
        }
    }
}
