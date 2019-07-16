using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CameraExtensions;                          // Using static class CameraExtensions to manage Layers that are visible in the Camera View

public class FoxyVision : MonoBehaviour
{
    public Camera cam;
    private bool disableE = false;                      // To restrict player from spamming E
    public LayerMask secretLayer;                       // Select a layer mask to be toggled for the camera


    // Update is called once per frame
    void Update()
    {
        if (!disableE) {
            if (Input.GetMouseButtonDown(0))
            {
                disableE = true;                        // Disables the E button check for the player input
                cam.LayerCullingShow(secretLayer);      // Adds a layer mask to camera rendering
                StartCoroutine(CoWait());
            }
        }
    }

    IEnumerator CoWait()
    {
        yield return new WaitForSeconds(10);
        cam.LayerCullingHide(secretLayer);              // Hides a layer mask from the camera
        disableE = false;                               // Enables E button check for the player input
    }
}
