using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent: MonoBehaviour
{
    public GameObject destination;
    public GameObject Hazmat;

    CharacterController Ccontroller;

    public bool isRange;
    
    // Start is called before the first frame update
    void Start()
    {
        isRange = false;
        Ccontroller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isRange == true)
        {
            Hazmat.GetComponent<CharacterController>().enabled = false;
            Hazmat.transform.position = destination.transform.position;
            Hazmat.GetComponent<CharacterController>().enabled = true;
            isRange = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "HazMad")
        {
            isRange = true;
        }
        else
        {
            isRange = false;
        }
    }
}
