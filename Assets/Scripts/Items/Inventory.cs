using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour, IInventory
{
	// Get max slots amount and fresh dictionary
	[Header("Interface Requirements")]
	public int maxSlots = 4;
	public int maxItemAmount = 99;
	private Dictionary<string, int> items = new Dictionary<string, int>();

	// Implement above as per interface requirement
	public int MaxSlots => maxSlots;
	public int MaxItemAmount => maxItemAmount;
	public Dictionary<string, int> InventoryItems => items;

	// public enum ItemType { Food, Part, Rag, Ration }
	public TextMeshProUGUI[] pickupText;

	// add items + check in case of avoid overflow of items
	public void AddItem(string itemName, int itemAmount)
	{
		// if inventory does NOT contain key already, make one
		if (!InventoryItems.ContainsKey(itemName))
		{
			InventoryItems.Add(itemName, itemAmount);
		}
		// otherwise increment value of key already made
		else if (InventoryItems.TryGetValue(itemName, out var currentCount))
		{
			InventoryItems[itemName] += itemAmount;

			// cap item capacity
			if (InventoryItems[itemName] >= maxItemAmount)
				InventoryItems[itemName] = maxItemAmount;
		}

		// after calculations done (if any) start assigning text to UI
		UpdateInventoryUi(itemName, InventoryItems[itemName]);
	}

	// add items + check in case of avoid overflow of items
	public void RemoveItem(string itemName, int itemAmount)
	{
		// decrement value of key/value pair
		if (InventoryItems.TryGetValue(itemName, out var currentCount))
		{
			InventoryItems[itemName] -= itemAmount;

			// cap item capacity
			if (InventoryItems[itemName] <= 0)
				InventoryItems[itemName] = 0;
		}

		// after calculations done (if any) start assigning text to UI
		UpdateInventoryUi(itemName, InventoryItems[itemName]);
	}

	void UpdateInventoryUi(string name, int amount)
	{
		// TODO: Somehow make names more dynamic, cos this method SUCKS!
		switch (name)
		{
			case "Food":
				pickupText[0].text = name;
				pickupText[0].GetComponent<TextMeshProUGUI>().text = amount.ToString();
				break;
			case "Part":
				pickupText[1].text = name;
				pickupText[1].GetComponent<TextMeshProUGUI>().text = amount.ToString();
				break;
			case "Rag":
				pickupText[2].text = name;
				pickupText[2].GetComponent<TextMeshProUGUI>().text = amount.ToString();
				break;
			case "Ration":
				pickupText[3].text = name;
				pickupText[3].GetComponent<TextMeshProUGUI>().text = amount.ToString();
				break;
		}
	}
}
