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
	private Dictionary<ItemType, int> items = new Dictionary<ItemType, int>();

	// Implement above as per interface requirement
	public int MaxSlots => maxSlots;
	public int MaxItemAmount => maxItemAmount;
	public Dictionary<ItemType, int> InventoryItems => items;

	// public enum ItemType { Food, Part, Rag, Ration }
	public TextMeshProUGUI[] pickupText;

	// add items + check in case of avoid overflow of items
	public void AddItem(ItemType itemName, int itemAmount)
	{
		ItemType currentItem = ItemType.Unassigned;

		// if inventory does NOT contain key already, make one
		if (!items.ContainsKey(itemName))
		{
			items.Add(itemName, itemAmount);
		}
		// otherwise increment value of key already made
		else if (items.TryGetValue(itemName, out var currentCount))
		{
			items[itemName] += itemAmount;

			// cap item capacity
			if (items[itemName] >= maxItemAmount)
				items[itemName] = maxItemAmount;
		}

		// after calculations done (if any) start assigning text to UI
		UpdateInventoryUi(itemName, items[itemName]);
	}

	// add items + check in case of avoid overflow of items
	public void RemoveItem(ItemType itemName, int itemAmount)
	{
		ItemType currentItem = ItemType.Unassigned;

		// decrement value of key/value pair
		if (items.TryGetValue(itemName, out var currentCount))
		{
			items[itemName] -= itemAmount;

			// cap item capacity
			if (items[itemName] <= 0)
				items[itemName] = 0;
		}

		// after calculations done (if any) start assigning text to UI
		UpdateInventoryUi(itemName, items[itemName]);
	}

	void UpdateInventoryUi(ItemType name, int amount)
	{
		switch (name)
		{
			case ItemType.Food:
				pickupText[(int)name - 1].text = name.ToString();
				pickupText[(int)name - 1].GetComponent<TextMeshProUGUI>().text = amount.ToString();
				break;
			case ItemType.Meds:
				pickupText[(int)name - 1].text = name.ToString();
				pickupText[(int)name - 1].GetComponent<TextMeshProUGUI>().text = amount.ToString();
				break;
			case ItemType.Parts:
				pickupText[(int)name - 1].text = name.ToString();
				pickupText[(int)name - 1].GetComponent<TextMeshProUGUI>().text = amount.ToString();
				break;
			case ItemType.Rags:
				pickupText[(int)name - 1].text = name.ToString();
				pickupText[(int)name - 1].GetComponent<TextMeshProUGUI>().text = amount.ToString();
				break;
		}
	}
}
