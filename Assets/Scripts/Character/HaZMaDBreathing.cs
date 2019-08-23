using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaZMaDBreathing : MonoBehaviour
{
    public AudioClip breatheIn, breatheOut;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void BreatheIn()
    {
        audioSource.PlayOneShot(breatheIn);
    }

    private void BreatheOut()
    {
        audioSource.PlayOneShot(breatheOut);
    }
}
