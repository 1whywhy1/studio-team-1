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
		// TODO: Somehow make names more dynamic, cos this method SUCKS!
		AddItem("Food", food);
		AddItem("Part", part);
		AddItem("Rag", rag);
		AddItem("Ration", ration);
	}
}
