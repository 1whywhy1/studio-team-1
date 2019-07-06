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
	public int maxSlots = 12;
	private Dictionary<string, int> items = new Dictionary<string, int>();

	// Implement above as per interface requirement
	public int MaxSlots => maxSlots;
	public Dictionary<string, int> InventoryItems => items;

	public enum ItemType { Food, Part, Rag, Ration }
	public TextMeshProUGUI[] pickupText;

	// add items + check in case of avoid overflow of items
	public void AddItem(string itemName)
	{
		// if inventory does NOT contain key already, make one
		if (!InventoryItems.ContainsKey(itemName))
		{
			InventoryItems.Add(itemName, 1);
		}
		// otherwise increment value of key already made
		else if (InventoryItems.TryGetValue(itemName, out var currentCount))
		{
			InventoryItems[itemName]++;
		}

		string amount = InventoryItems[itemName].ToString();
		switch (itemName)
		{
			case "Pickup1":
				pickupText[0].text = itemName;
				pickupText[0].GetComponent<TextMeshProUGUI>().text = amount;
				break;
			case "Pickup2":
				pickupText[1].text = itemName;
				pickupText[1].GetComponent<TextMeshProUGUI>().text = amount;
				break;
			case "Pickup3":
				pickupText[2].text = itemName;
				pickupText[2].GetComponent<TextMeshProUGUI>().text = amount;
				break;
			case "Pickup4":
				pickupText[3].text = itemName;
				pickupText[3].GetComponent<TextMeshProUGUI>().text = amount;
				break;
		}
	}

	// used when things go in/out of inventory
	void RearrangeInventory() {}
}
