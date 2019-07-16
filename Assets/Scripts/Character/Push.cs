using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public GameObject heldItem;
    public bool isHolding;
    public bool isRange;


    // Start is called before the first frame update
    void Start()
    {
        isRange = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isRange == true)
        {
            if (isHolding == false)
            {
                heldItem.transform.SetParent(this.transform);

                heldItem.transform.localPosition = new Vector3(0, 0.75f, 1);
                isHolding = true;
                return;
            }
            if(isHolding == true)
            {
                heldItem.transform.SetParent(null);
                isHolding = false;
                heldItem = null;
                return;
            }
        }
    }

    public void canPush()
    {

    }

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Pushable")
        {
            isRange = true;
            heldItem = other.gameObject;

        }
        else
        {
            isRange = false;
            heldItem = null;
        }

    }


}
