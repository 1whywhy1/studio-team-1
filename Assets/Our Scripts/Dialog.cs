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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoType());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextSentence()
    {
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
