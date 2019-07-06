using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUp : MonoBehaviour
{
    RaycastHit hit;
    private Camera playerCam;
    private bool inPoV;
    private bool inRange;
    int layerMask = 8;
    private float pickUpRange = 0;

    public TextMeshProUGUI useText;

    private void Start()
    {
        playerCam = Camera.main;
    }

    private void Update()
    {
        InSight();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }

        inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }

        inRange = false;
    }

    private void InSight()
    {
        if (inRange)
        {
            Ray playerAim = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(playerAim, out hit, pickUpRange, layerMask))
            {
                if (hit.collider.tag == "pickUps")
                {
                    useText.enabled = true;
                }
                else
                {
                    useText.enabled = false;
                }
            }
        }
    }

    public virtual void OnPickup(Transform item)
    {
        //emty
    }
}
