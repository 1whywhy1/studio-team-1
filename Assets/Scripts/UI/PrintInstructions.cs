using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintInstructions : MonoBehaviour
{
    public Text textDisplay;                //text box to diplay
    public string sentence;
    private int index;
    public float typingSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        sentence = "You are in a correction facility\nWork as a team and bring the required\nresources to the computer in front of you or prepare to be corrected...";
        StartCoroutine(CoType());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Displays a str one letter at a time every typingSpeed seconds
    IEnumerator CoType()
    {
        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

}
