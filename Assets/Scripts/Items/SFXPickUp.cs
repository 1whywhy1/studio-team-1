using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPickUp : MonoBehaviour
{
    public AudioClip pickUpSFX;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<AudioSource>().PlayOneShot(pickUpSFX);
        StartCoroutine(CoWait());
    }

    IEnumerator CoWait()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this);
    }
}
