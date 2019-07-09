using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingSystem : MonoBehaviour
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

	public void GiveItem(ItemType item, int amount)
	{
		if (npcToTradeWith != null && inTradingRange)
		{
			// if item exists in inventory
			if (playerInventory.InventoryItems.TryGetValue(item, out int value))
			{
//				amountToSwap = value;

				// takes item from player
				playerInventory.RemoveItem(item, amount);

				// gives item to NPC
				npcToTradeWith.GetComponent<Inventory>().AddItem(item, amount);
			}
		}
	}

	public void TakeItem(ItemType item, int amount)
	{
		if (npcToTradeWith != null && inTradingRange)
		{
			// if item exists in inventory
			if (npcToTradeWith.GetComponent<Inventory>().InventoryItems.TryGetValue(item, out int value))
			{
//				amountToSwap = value;

				// gives item from player
				playerInventory.AddItem(item, amount);
				// takes item to NPC
				npcToTradeWith.GetComponent<Inventory>().RemoveItem(item, amount);
			}
		}
	}

	/// <summary>
	/// Trade assigned item for assigned item.
	/// </summary>
	/// <param name="itemOne">The first item.</param>
	/// <param name="itemTwo">The second item.</param>
	public void TradeItemForItem(int tradeType)
	{
		switch (tradeType)
		{
			case 1:
				GiveItem(ItemType.Food, 1);
				TakeItem(ItemType.Rags, 3);
				break;
			default:
				Debug.LogError("You've used the wrong TradeType. Found: " + tradeType);
				break;
		}
	}
}
