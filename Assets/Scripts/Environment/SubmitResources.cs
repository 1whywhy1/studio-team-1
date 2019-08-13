using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubmitResources : MonoBehaviour
{
    public TextMeshProUGUI[] player;            //Player inventory
    public TextMeshProUGUI[] requirement;       //Requirement values
    //public TextMeshProUGUI error;

    public void CheckTheItems()
    {
        bool hasEnough = true;
        int[] playerInt;
        int[] requirementInt;

        playerInt = new int[4];
        requirementInt = new int[4];

        for (int i = 0; i < 4; ++i)
        {
            playerInt[i] = (int.Parse(player[i].text));
            requirementInt[i] = (int.Parse(requirement[i].text));
        }

        for (int i= 0; i < 4; ++i)
        {
            if (playerInt[i] < requirementInt[i])
            {
                hasEnough = false;
            }
        }

        if (hasEnough)
        {
            for(int i = 0; i < 4; ++i)
            {
                playerInt[i] -= requirementInt[i];
                player[i].text = playerInt[i].ToString();
            }
        }
        else
        {
            // Display error here
        }
    }

}
