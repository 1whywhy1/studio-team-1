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
        for (int i= 0; i < 4; ++i){
            int playerInt = (int.Parse(player[i].text));
            int requirementInt = (int.Parse(requirement[i].text));
            if (playerInt >= requirementInt)
            {
                playerInt -= requirementInt;
                player[i].text = playerInt.ToString();
            }
            else
            {
                // Display error here
                break;
            }
        }

    }

}
