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

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator CoWait()
    {
     
        yield return new WaitForSeconds(5);
        musicPlayer.Play();
    }
}
