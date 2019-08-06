using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ava : MonoBehaviour
{
   

    CharacterController Ccontroller;

    public bool isRange;

    public Transform spawnLocation;

    public GameObject[] pieces;

    public GameObject foods;
    public GameObject meds;
    public GameObject rags;
    public GameObject parts;

    // Start is called before the first frame update
    void Start()
    {
        isRange = false;
        Ccontroller = GetComponent<CharacterController>();

        pieces = new GameObject[4];
        pieces[0] = meds;
        pieces[1] = rags;
        pieces[2] = foods;
        pieces[3] = parts;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)&& isRange == true)
        {
            Instantiate(pieces[Random.Range(0, pieces.Length)], spawnLocation.position, spawnLocation.rotation);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ava Variant")
        {
            isRange = true;
        }
        else
        {
            isRange = false;
        }
    }

}
