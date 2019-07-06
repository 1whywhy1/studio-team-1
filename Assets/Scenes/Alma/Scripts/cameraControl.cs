using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    public GameObject targetPlayer;
    public GameObject targetPlayer2;
    public GameObject targetPlayer3;
    public GameObject targetPlayer4;

    public Move player;
    public Move player2;
    public Move player3;
    public Move player4;

    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;
    public GameObject cam4;

    public GameObject trueTarget;


    public GameObject targetZoom;

    float desiredDistance = 3f;

    float pitch = 0f; // controls up and down
    float pitchMin = -10f;
    float pitchMax = 60f;
    float yaw = 0f; // controls side to side
    float roll = 0f; // controls camera rotation

    float sensitivity = 15f; // create the sensitivity of the mouse

    void Start()
    {
        trueTarget = targetPlayer;
        cam1.SetActive(true);
        cam2.SetActive(false);
        cam3.SetActive(false);
        cam4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        pitch -= sensitivity * Input.GetAxis("Mouse Y");
        yaw += sensitivity * Input.GetAxis("Mouse X");
        // multiplying these movements by the sensitivity which let's us control how much moving to the mouse effects the camera angle.

        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
        // Clamp prevents a value from going below a minimum or above a maximum value.

        transform.localEulerAngles = new Vector3(pitch, yaw, roll);

        // Camera's position
        transform.position = trueTarget.transform.position - desiredDistance * transform.forward + Vector3.up * 1.5f;
        // Start from the player's position and go backwards a distance desiredDistance from the player.

        if (Input.GetMouseButton(1))
        {
            transform.position = targetZoom.transform.position;

        }
        if (Input.GetKeyDown("1"))
        {
            trueTarget = targetPlayer;

            cam1.SetActive(true);
            cam2.SetActive(false);
            cam3.SetActive(false);
            cam4.SetActive(false);


            player.isControlling = true;
            player2.isControlling = false;
            player3.isControlling = false;
            player4.isControlling = false;



        }
        if (Input.GetKeyDown("2"))
        {
            trueTarget = targetPlayer2;

            cam2.SetActive(true);
            cam1.SetActive(false);
            cam3.SetActive(false);
            cam4.SetActive(false);


            player2.isControlling = true;
            player.isControlling = false;
            player3.isControlling = false;
            player4.isControlling = false;



        }
        if (Input.GetKeyDown("3"))
        {
            trueTarget = targetPlayer3;

            cam3.SetActive(true);
            cam1.SetActive(false);
            cam2.SetActive(false);
            cam4.SetActive(false);


            player3.isControlling = true;
            player.isControlling = false;
            player2.isControlling = false;
            player4.isControlling = false;

        }
        if (Input.GetKeyDown("4"))
        {
            trueTarget = targetPlayer4;

            cam4.SetActive(true);
            cam1.SetActive(false);
            cam2.SetActive(false);
            cam3.SetActive(false);


            player4.isControlling = true;
            player.isControlling = false;
            player2.isControlling = false;
            player3.isControlling = false;



        }

    }


}
