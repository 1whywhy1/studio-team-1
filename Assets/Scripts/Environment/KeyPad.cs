using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPad : MonoBehaviour
{
    [SerializeField] string curPassword = "4826";   //pasword

    public string input = "";
    public AudioClip woodenDoor;
    public Transform doorleft;            //left door of the locked door
   // public Transform doorright;           //right door of the locked door
    public Text errorText;
    public AudioSource effects;

    private bool onTrigger;                //check in the player is near to keypad lock
    private bool doorOpened = false;       //opens the door
    private bool keypadShow;               //shows keypad
    private string errorMsg = "Wrong password";

    bool doorSoundPlayed = false;

    void Update()
    {
        if (input == curPassword)
        {
            doorOpened = true;
        }
        else if (input.Length > 3)
        {
            //reset the lock
            onTrigger = false;
            keypadShow = false;
            input = "";
            errorText.text = errorMsg;
            StartCoroutine("CoWaitForMessage");
        }

        if (doorOpened)
        {
            if (!doorSoundPlayed)
            {
                effects.PlayOneShot(woodenDoor);
                doorSoundPlayed = !doorSoundPlayed;
            }
           
            //opens both doors
            var rotLeft = Quaternion.RotateTowards(doorleft.rotation, Quaternion.Euler(0.0f, -90.0f, 0.0f), Time.deltaTime * 250);
            doorleft.rotation = rotLeft;
           // var rotRight = Quaternion.RotateTowards(doorright.rotation, Quaternion.Euler(0.0f, 90.0f, 0.0f), Time.deltaTime * 250);
          //  doorright.rotation = rotRight;
        }
        keyPress();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            input = "";
            onTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onTrigger = false;
            input = "";
            keypadShow = false;
        }
    }

    private void OnGUI()
    {
        if (!doorOpened)
        {
            if (onTrigger)
            {
                GUI.Box(new Rect(500, 500, 200, 25), "Press E");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    keypadShow = true;
                    onTrigger = false;
                }
            }
            if (input.Length >= 4)
            {
                keypadShow = false;
            }

            if (keypadShow)
            {
                GUI.Box(new Rect(0, 0, 320, 455), "");
                GUI.Box(new Rect(5, 5, 310, 25), input);
                if (GUI.Button(new Rect(5, 35, 100, 100), "1"))
                {
                    input = input + "1";
                }
                if (GUI.Button(new Rect(110, 35, 100, 100), "2"))
                {
                    input = input + "2";
                }
                if (GUI.Button(new Rect(215, 35, 100, 100), "3"))
                {
                    input = input + "3";
                }
                if (GUI.Button(new Rect(5, 140, 100, 100), "4"))
                {
                    input = input + "4";
                }
                if (GUI.Button(new Rect(110, 140, 100, 100), "5"))
                {
                    input = input + "5";
                }
                if (GUI.Button(new Rect(215, 140, 100, 100), "6"))
                {
                    input = input + "6";
                }
                if (GUI.Button(new Rect(5, 245, 100, 100), "7"))
                {
                    input = input + "7";
                }
                if (GUI.Button(new Rect(110, 245, 100, 100), "8"))
                {
                    input = input + "8";
                }
                if (GUI.Button(new Rect(215, 245, 100, 100), "9"))
                {
                    input = input + "9";
                }
                if (GUI.Button(new Rect(5, 350, 100, 100), "Cancel"))
                {
                    input = "";
                    keypadShow = false;
                    Cursor.visible = false;
                }
                if (GUI.Button(new Rect(110, 350, 100, 100), "0"))
                {
                    input = input + "0";
                }
            }
        }
    }

    void keyPress()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            input += "1";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            input = input + "2";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            input += "3";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            input += "4";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            input += "5";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            input += "6";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            input += "7";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            input += "8";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            input += "9";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            input += "0";
        }
    }

    IEnumerator CoWaitForMessage()
    {
        yield return new WaitForSeconds(3.0f);
        errorText.text = "";
    }
}
