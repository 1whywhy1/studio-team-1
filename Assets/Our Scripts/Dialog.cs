using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentenses;
    private int index;
    public float typingSpeed = 1f;
    public AudioSource source;

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
        if(textDisplay.text == sentenses[index])
        {
            continueButton.SetActive(true);
        }
    }

    public void NextSentence()
    {
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

    IEnumerator CoType ()
    {
        foreach(char letter in sentenses[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
