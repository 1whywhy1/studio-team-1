using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickUpPrototype : MonoBehaviour
{
    public Text pickUpText;
    public AudioSource sxf;
    public AudioClip pickUp;

    private void Start()
    {
        sxf = GameObject.Find("EGO SFX").GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sxf.PlayOneShot(pickUp);
            gameObject.SetActive(false);
        }
    }
}
