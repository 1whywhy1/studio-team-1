using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInventoryItem
{
	// Get item name & inventory UI sprite
	[Header("Interface Requirements")]
	public string itemName;
	public int itemValue = 1;
	public Sprite itemSprite;

	// Implement above as per interface requirement
	// Only has 'get', so variable is read only
	public string Name => itemName;
	public int Amount => itemValue;
	public Sprite Sprite => itemSprite;

	void Start()
	{
		itemName = gameObject.name;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Inventory inventory = GameObject.Find("PlayerInventory").GetComponent<Inventory>();
			inventory.AddItem(itemName, itemValue);

			Destroy(gameObject);
		}
	}
}
