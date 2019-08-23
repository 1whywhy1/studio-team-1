using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips;
    public AudioClip jumpUp, jumpLand;

    private AudioSource audioSource; 

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //selects a random of footspets 1 and 2 and plays it once
    private void Step()
    {
        audioSource.PlayOneShot(clips[Random.Range(0, 1)]);
    }

    private void JumpUp()
    {
        audioSource.PlayOneShot(jumpUp);
    }
    private void JumpLand()
    {
        audioSource.PlayOneShot(jumpLand);
    }

}
