using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }

        PickUpItem(other.transform);
    }

    public virtual void OnPickup(Transform item)
    {
        //emty
    }

    void PickUpItem(Transform item)
    {

    }
}
