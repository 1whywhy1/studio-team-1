using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
	string Name { get; }
	Sprite Sprite { get; }
}

public interface IInventory
{
	int MaxSlots { get; }
	Dictionary<string, int> InventoryItems { get; }
}
