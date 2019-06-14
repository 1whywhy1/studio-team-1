using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpPrototype : MonoBehaviour
{
    public TextMeshProUGUI pickUpText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            pickUpText.enabled = true;
            if(Input.GetButtonDown("E"))
            {
                Destroy(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            pickUpText.enabled = false;
        }
    }
}
