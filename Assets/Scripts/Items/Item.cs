using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInventoryItem
{
	// Get item name & inventory UI sprite
	[Header("Interface Requirements")]
	public int itemValue = 1;
	public Sprite itemSprite;

	// Implement above as per interface requirement
	// Only has 'get', so variable is read only
	public string Name => itemName;
	public int Amount => itemValue;
	public Sprite Sprite => itemSprite;

	public ItemType itemType = ItemType.Unassigned;
	private Inventory inventory;
	private string itemName;

	private void Awake()
	{
		inventory = GameObject.Find("EGOPlayerInventory").GetComponent<Inventory>();

		itemName = itemType.ToString();
	}

	/// <summary>
	/// Where pickup adds to player inventory
	/// </summary>
	/// <param name="other"></param>
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			inventory.AddItem(itemType, itemValue);

			Destroy(gameObject);
		}
	}
}
