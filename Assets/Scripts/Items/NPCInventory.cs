using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPC has full Inventory functionality plus ability to start off with 'X' amount of items
/// </summary>
public class NPCInventory : Inventory
{
	// initialise items to start with
	[Header("Starting Items")]
	public int food;
	public int meds;
	public int parts;
	public int rags;

	void Start()
	{
		// TODO: Somehow make names more dynamic, cos this method SUCKS!
		AddItem(ItemType.Food, food);
		AddItem(ItemType.Meds, meds);
		AddItem(ItemType.Parts, parts);
		AddItem(ItemType.Rags, rags);
	}
}
