using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    public GameObject targetPlayer;

    float desiredDistance = 3f;

    float pitch = 0f; // controls up and down
    float pitchMin = -10f; 
    float pitchMax = 60f;
    float yaw = 0f; // controls side to side
    float roll = 0f; // controls camera rotation

    float sensitivity = 15f; // create the sensitivity of the mouse

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
        transform.position = targetPlayer.transform.position - desiredDistance * transform.forward + Vector3.up * 1.5f;   
        // Start from the player's position and go backwards a distance desiredDistance from the player.
    }   
}
