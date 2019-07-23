using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CameraExtensions;                          // Using static class CameraExtensions to manage Layers that are visible in the Camera View
using UnityEngine.Rendering.PostProcessing;

public class FoxyVision : MonoBehaviour
{
    public Camera cam;
    private bool disablePower = false;                      // To restrict player from spamming E
    public LayerMask secretLayer;                       // Select a layer mask to be toggled for the camera
    public Move rox;
    public PostProcessVolume foxEffect;

    // Update is called once per frame
    void Update()
    {
        if (rox.isControlling)
        {
            if (!disablePower)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    foxEffect.enabled = !foxEffect.enabled; // Switch on the post processing
                    disablePower = true;                        // Disables the E button check for the player input
                    cam.LayerCullingShow(secretLayer);      // Adds a layer mask to camera rendering
                    StartCoroutine(CoWait());
                }
            }
        }
    }

    IEnumerator CoWait()
    {
        yield return new WaitForSeconds(10);
        cam.LayerCullingHide(secretLayer);              // Hides a layer mask from the camera
        foxEffect.enabled = !foxEffect.enabled;         // Switch off the post processing
        disablePower = false;                               // Enables E button check for the player input
    }
}
