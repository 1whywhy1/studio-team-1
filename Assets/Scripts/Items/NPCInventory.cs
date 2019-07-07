using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// derives from INVENTORY class!
public class NPCInventory : Inventory
{
	// initialise items to start with
	[Header("Starting Items")]
	public int food;
	public int part;
	public int rag;
	public int ration;

	void Start()
	{
//		InventoryItems.Add(name, food);
//		InventoryItems.Add(name, part);
//		InventoryItems.Add(name, rag);
//		InventoryItems.Add(name, ration);
	}
}
