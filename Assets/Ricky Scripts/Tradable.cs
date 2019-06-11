using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Tradable", menuName = "Items/Tradable")]
public class Tradable : Item
{


    public override void Use()
    {
        GameObject player = Inventory.instance.player;
        //here should be the script for trading
        //Add a line of code after combining
        //Inventory.instance.Remove(this);
    }

}
