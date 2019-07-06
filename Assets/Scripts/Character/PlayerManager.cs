using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	[Header("Inventory")]
	// public Inventory inventory;
	public GameObject inventoryScreen;
	private bool inventoryShowing = false;

	void Start()
	{
		inventoryScreen.SetActive(inventoryShowing);
	}

	public void ToggleInventory()
	{
		// Show/hide menu
		if (!inventoryShowing)
		{
			inventoryScreen.SetActive(true);
			inventoryShowing = inventoryScreen.activeSelf;
		}
		else
		{
			inventoryScreen.SetActive(false);
			inventoryShowing = inventoryScreen.activeSelf;
		}
	}
}
