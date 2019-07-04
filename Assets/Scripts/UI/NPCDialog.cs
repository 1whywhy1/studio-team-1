using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCDialog : MonoBehaviour
{
    public Text textDisplay;               
    public string[] sentenses;              // Sentences to be displayed
    private int index;                      // For printing sentences
    public float typingSpeed = 0.05f;       
    public AudioSource source;              // Audio Source for button click sound
    public GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(CoType());
    }

    // Update is called once per frame
    void Update()
    {
        //activates button when the full sentence is displayed
        if(textDisplay.text == sentenses[index])
        {
            continueButton.SetActive(true);
        }
    }

    public void NextSentence()
    {
        // Button sound
        source.Play();
        continueButton.SetActive(false);

        if (index<sentenses.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(CoType());
        }
        else
        {
            textDisplay.text = "";
        }

    }

    // Prints out a sentence a letter at a time every typingSpeed seconds
    IEnumerator CoType ()
    {
        foreach(char letter in sentenses[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
