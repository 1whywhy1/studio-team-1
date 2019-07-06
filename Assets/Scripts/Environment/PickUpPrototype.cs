using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickUpPrototype : MonoBehaviour
{
    public Text pickUpText;             //resource value text box
    public AudioSource sxf;             // player for SFX
    public AudioClip pickUp;            // a sound to play on pick up

    private void Start()
    {
        sxf = GameObject.Find("EGO SFX").GetComponent<AudioSource>();
    }

    //checks if it player colliding with a pick up and increments an amount of this resource that player posseses.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")                               //checks the tag of the GameObject of the collision detected
        {
            sxf.PlayOneShot(pickUp);
            pickUpText.text = (int.Parse(pickUpText.text) + 1).ToString();      //converts str into int adds 1 and converts it back to str
            gameObject.SetActive(false);
        }
    }
}
