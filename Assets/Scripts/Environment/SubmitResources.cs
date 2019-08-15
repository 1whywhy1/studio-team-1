﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubmitResources : MonoBehaviour
{
    public TextMeshProUGUI[] player;            //Player inventory
    public TextMeshProUGUI[] requirement;       //Requirement values
    public TextMeshProUGUI errorMessage,successMessage;        //To display an event message

    public AudioClip success,failure;
    public AudioSource audioSource;


    public void CheckTheItems()
    {
        bool hasEnough = true;
        int[] playerInt;                    //array to store player's resources in INT
        int[] requirementInt;               //array to store required from the player resources in INT

        playerInt = new int[4];
        requirementInt = new int[4];

        //Populate arrays
        for (int i = 0; i < 4; ++i)
        {
            playerInt[i] = (int.Parse(player[i].text));
            requirementInt[i] = (int.Parse(requirement[i].text));
        }

        //Check if player has enough of each resource
        for (int i= 0; i < 4; ++i)
        {
            if (playerInt[i] < requirementInt[i])
            {
                hasEnough = false;
                break;
            }
        }

        
        if (hasEnough)
        {
            //Takes the resources from the player
            for(int i = 0; i < 4; ++i)
            {
                playerInt[i] -= requirementInt[i];
                player[i].text = playerInt[i].ToString();
            }
            //displays an success message, plays a success sound and starts a coroutine to switch the message off
            successMessage.gameObject.SetActive(true);
            audioSource.PlayOneShot(success);
            StartCoroutine(CoWaitForMessage(successMessage));
        }
        else
        {
            //displays an error message, plays a failure sound and starts a coroutine to switch the message off
            errorMessage.gameObject.SetActive(true);
            audioSource.PlayOneShot(failure);
            StartCoroutine(CoWaitForMessage(errorMessage));
        }
    }

    IEnumerator CoWaitForMessage(TextMeshProUGUI eventMessage)
    {
        yield return new WaitForSeconds(3.0f);
        eventMessage.gameObject.SetActive(false);
    }
}
