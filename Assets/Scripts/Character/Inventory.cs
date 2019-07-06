using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour, IInventory
{
	// Get max slots amount and fresh dictionary
	[Header("Interface Requirements")] public int maxSlots = 12;
	public Dictionary<string, int> items = new Dictionary<string, int>();

	// Implement above as per interface requirement
	public int MaxSlots => maxSlots;
	public Dictionary<string, int> InventoryItems => items;

	public enum ItemType { Food, Part, Rag, Ration }
	public TMP_Text[] pickupNotifications;

	[SerializeField] private int currentCount = 0;

	// add items + check in case of avoid overflow of items
	public void AddItem(string itemName)
	{
		Debug.Log(itemName);
		Debug.Log(InventoryItems[itemName] + ": " + currentCount);

		if (!InventoryItems.ContainsKey(itemName))
		{
			InventoryItems.Add(itemName, 1);
		}
		else if (InventoryItems.TryGetValue(itemName, out var currentCount))
		{
			InventoryItems[itemName] = currentCount++;
		}
	}

	// used when things go in/out of inventory
	void RearrangeInventory() {}
}
