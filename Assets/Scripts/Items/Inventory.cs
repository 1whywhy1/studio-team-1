using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditorInternal;

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
	
	/// <summary>
	/// Make inventory based off a Singleton design
	/// </summary>
	private static Inventory _instance;
	public static Inventory Instance { get { return _instance; } }

	private void Awake()
	{
		if (_instance != null && _instance != this)
			Destroy(this.gameObject);
		else
			_instance = this;
	}

	// add items + check in case of avoid overflow of items
	public void AddItem(ItemType itemName, int itemAmount)
	{
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
				pickupText[(int)name - 1].text = amount.ToString();
				break;
			case ItemType.Meds:
				pickupText[(int)name - 1].text = amount.ToString();
				break;
			case ItemType.Parts:
				pickupText[(int)name - 1].text = amount.ToString();
				break;
			case ItemType.Rags:
				pickupText[(int)name - 1].text = amount.ToString();
				break;
		}
	}
}
