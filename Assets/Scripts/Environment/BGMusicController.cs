using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicController : MonoBehaviour
{ 
    private AudioSource musicPlayer;
    public AudioClip inGameBG;

    // Start is called before the first frame update
    void Start()
    {
        musicPlayer = this.GetComponent<AudioSource>();
        StartCoroutine(CoWait());
      
    }

    // Waits for 5 second and starts playing an audio clip
    IEnumerator CoWait()
    {
        yield return new WaitForSeconds(5);
        musicPlayer.Play();
    }
}
