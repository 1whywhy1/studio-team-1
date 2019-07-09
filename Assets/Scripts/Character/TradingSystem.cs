using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingSystem : Item
{
	public bool inTradingRange = false;
	private Collider npcToTradeWith;
	private GameObject tradingUI;

	private Inventory playerInventory, npcInventory;
	private int amountToSwap;

	void Start()
	{
		playerInventory = GameObject.Find("EGOPlayerInventory").GetComponent<Inventory>();
		tradingUI = GameObject.Find("TradingUI");
		tradingUI.SetActive(false);
	}

	void Update()
	{
		// only only action when close (in range of collider)
		if (inTradingRange)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				// toggle trading UI
				tradingUI.SetActive(!tradingUI.activeSelf);

				// freeze time when trading
				if (tradingUI.activeSelf)
					Time.timeScale = 0;
				else
					Time.timeScale = 1;
			}
		}

//		Debug.Log("Test:" + ItemType.Food.ToString());
//		Debug.Log("ItemType as int: [" + (int)ItemType.Food + "].");
	}

	private void OnTriggerEnter(Collider other)
	{
		// assign designated NPC to variables
		npcInventory = other.GetComponent<Inventory>();
		npcToTradeWith = other;

		// only make actions when true (in range)
		if (other.CompareTag("NPC"))
			inTradingRange = true;
	}

	/// <summary>
	/// Occurs when player leaves NPC vicinity
	/// </summary>
	/// <param name="other">NPC to trade with</param>
	private void OnTriggerExit(Collider other)
	{
		// empty npc variable's when vicinity left
		Inventory npcInventory = null;
		npcToTradeWith = null;

		// only make actions when true (in range)
		if (other.CompareTag("NPC"))
			inTradingRange = false;
	}

	public void GiveItem()
	{
		if (npcToTradeWith != null && inTradingRange)
		{
			// takes number from player
			if (playerInventory.InventoryItems.TryGetValue(itemType, out int value))
			{
				amountToSwap = value;
				playerInventory.RemoveItem(itemType, value);

				// gives number to NPC
				npcToTradeWith.GetComponent<Inventory>().AddItem(itemType, amountToSwap);
			}
		}
	}

	public void TakeItem()
	{
		if (npcToTradeWith != null && inTradingRange)
		{
			// takes number from player
			if (npcToTradeWith.GetComponent<Inventory>().InventoryItems.TryGetValue(itemType, out int value))
			{
				amountToSwap = value;
				playerInventory.AddItem(itemType, value);
				// takes number to NPC
				npcToTradeWith.GetComponent<Inventory>().RemoveItem(itemType, amountToSwap);
			}
		}
	}

	/// <summary>
	/// Trade assigned item for assigned item.
	/// </summary>
	/// <param name="itemOne">The first item.</param>
	/// <param name="itemTwo">The second item.</param>
	void TradeItemForItem(string itemOne, string itemTwo)
	{
		// take item 1 from player
		TakeItem();

		// give item 1 to player
		GiveItem();
	}
}
