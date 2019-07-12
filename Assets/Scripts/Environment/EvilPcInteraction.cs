using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EvilPcInteraction : MonoBehaviour
{
    public TextMeshProUGUI interact;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interact.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown("E"))
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interact.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
