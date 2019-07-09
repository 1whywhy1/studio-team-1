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
	}

	void OnTriggerEnter(Collider other)
	{
		// assign designated NPC to variables
		npcInventory = other.GetComponent<NPCInventory>();
		npcToTradeWith = other;

		// only make actions when true (in range)
		if (other.CompareTag("NPC"))
			inTradingRange = true;
	}

	/// <summary>
	/// Occurs when player leaves NPC vicinity
	/// </summary>
	/// <param name="other">NPC to trade with</param>
	void OnTriggerExit(Collider other)
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
				// takes item from player
				playerInventory.RemoveItem(item, amount);

				// gives item to NPC
				npcInventory.AddItem(item, amount);
			}
		}
	}

	public void TakeItem(ItemType item, int amount)
	{
		if (npcToTradeWith != null && inTradingRange)
		{
			// if item exists in inventory
			if (npcInventory.InventoryItems.TryGetValue(item, out int value))
			{
				// gives item from player
				playerInventory.AddItem(item, amount);
				// takes item to NPC
				npcInventory.RemoveItem(item, amount);
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
				GiveItem(ItemType.Meds, 1);
				TakeItem(ItemType.Food, 3);
				break;
			case 2:
				GiveItem(ItemType.Meds, 2);
				TakeItem(ItemType.Parts, 3);
				break;
			case 3:
				GiveItem(ItemType.Meds, 2);
				TakeItem(ItemType.Rags, 1);
				break;
			case 4:
				GiveItem(ItemType.Food, 3);
				TakeItem(ItemType.Parts, 1);
				break;
			case 5:
				GiveItem(ItemType.Food, 3);
				TakeItem(ItemType.Rags, 1);
				break;
			case 6:
				GiveItem(ItemType.Rags, 2);
				TakeItem(ItemType.Parts, 1);
				break;
			case 7:
				GiveItem(ItemType.Food, 3);
				TakeItem(ItemType.Meds, 1);
				break;
			case 8:
				GiveItem(ItemType.Parts, 3);
				TakeItem(ItemType.Meds, 2);
				break;
			case 9:
				GiveItem(ItemType.Rags, 1);
				TakeItem(ItemType.Meds, 2);
				break;
			case 10:
				GiveItem(ItemType.Parts, 1);
				TakeItem(ItemType.Food, 3);
				break;
			case 11:
				GiveItem(ItemType.Rags, 1);
				TakeItem(ItemType.Food, 3);
				break;
			case 12:
				GiveItem(ItemType.Parts, 1);
				TakeItem(ItemType.Rags, 2);
				break;
			default:
				Debug.LogError("You've used the wrong TradeType. Found: " + tradeType);
				break;
		}
	}
}
