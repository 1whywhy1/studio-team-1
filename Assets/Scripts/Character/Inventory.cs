using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public const int SLOTS = 5;
	private List<IInventoryItem> Items = new List<IInventoryItem>();
	public static event EventHandler<InventoryEventArgs> ItemAdded;

	public void AddItem(IInventoryItem item)
	{
		if (Items.Count < SLOTS)
		{
			Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
			if (collider.enabled)
			{
				collider.enabled = false;
				Items.Add(item);
				item.OnPickup();

				if (ItemAdded != null)
				{
					ItemAdded(this, new InventoryEventArgs(item));
				}
			}
		}
	}
}
