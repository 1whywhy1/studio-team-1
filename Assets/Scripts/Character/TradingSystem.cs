using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingSystem : MonoBehaviour
{
	public bool inTradingRange = false;
	private Collider npcToTradeWith;
	private GameObject tradingUI;

	[SerializeField] private TradeProgress tradeProgress = TradeProgress.Init;
	private Inventory playerInventory;
	private NPCInventory npcInventory;
	private ItemType playerSwapType, npcSwapType;
	private int playerSwapAmount, npcSwapAmount;
	private bool tradeValid;

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

	/// <summary>
	/// Occurs when player enters NPC vicinity
	/// </summary>
	/// <param name="other">NPC to trade with</param>

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
		if (npcToTradeWith != null)
		{
			// if item exists in inventory
			if (playerInventory.InventoryItems.TryGetValue(item, out int value))
			{
				// if either person does not have enough materials
				if (value < amount)
				{
					Debug.Log("YOU DON'T HAVE HAVE ENOUGH");
					tradeValid = false;
					tradeProgress = TradeProgress.Init;
				}
				else
				{
					tradeProgress = TradeProgress.NPCTurn;

					playerSwapType = item;
					playerSwapAmount = amount;
				}
			}
		}
	}

	public void TakeItem(ItemType item, int amount)
	{
		if (npcToTradeWith != null)
		{
			// if item exists in inventory
			if (npcInventory.InventoryItems.TryGetValue(item, out int value))
			{
				// if either person does not have enough materials
				if (value < amount)
				{
					Debug.Log("THEY DON'T HAVE HAVE ENOUGH");
					tradeValid = false;
					tradeProgress = TradeProgress.Init;
				}
				else
				{
					tradeProgress = TradeProgress.MakeTrade;
					tradeValid = true;

					npcSwapType = item;
					npcSwapAmount = amount;
				}
			}
		}
	}

	void ExecuteTrade()
	{
		if (tradeValid)
		{
			// takes item from player
			playerInventory.RemoveItem(playerSwapType, playerSwapAmount);
			// gives item to NPC
			npcInventory.AddItem(playerSwapType, playerSwapAmount);

			// gives item from player
			playerInventory.AddItem(npcSwapType, npcSwapAmount);
			// takes item to NPC
			npcInventory.RemoveItem(npcSwapType, npcSwapAmount);
		}
	}

	/// <summary>
	/// Trade assigned item for assigned item.
	/// </summary>
	/// <param name="itemOne">The first item.</param>
	/// <param name="itemTwo">The second item.</param>
	public void TradeItemsSetup(int tradeType)
	{
		switch (tradeType)
		{
			case 1:
				tradeProgress = TradeProgress.PlayerTurn;
				GiveItem(ItemType.Meds, Mathf.RoundToInt(npcInventory.npcTradeRatios[0].x));
				TakeItem(ItemType.Food, Mathf.RoundToInt(npcInventory.npcTradeRatios[0].y));
				ExecuteTrade();
				break;
			case 2:
				tradeProgress = TradeProgress.PlayerTurn;
				GiveItem(ItemType.Meds, Mathf.RoundToInt(npcInventory.npcTradeRatios[1].x));
				TakeItem(ItemType.Parts, Mathf.RoundToInt(npcInventory.npcTradeRatios[1].y));
				ExecuteTrade();
				break;
			case 3:
				tradeProgress = TradeProgress.PlayerTurn;
				GiveItem(ItemType.Meds, Mathf.RoundToInt(npcInventory.npcTradeRatios[2].x));
				TakeItem(ItemType.Rags, Mathf.RoundToInt(npcInventory.npcTradeRatios[2].y));
				ExecuteTrade();
				break;
			case 4:
				tradeProgress = TradeProgress.PlayerTurn;
				GiveItem(ItemType.Food, Mathf.RoundToInt(npcInventory.npcTradeRatios[3].x));
				TakeItem(ItemType.Parts, Mathf.RoundToInt(npcInventory.npcTradeRatios[3].y));
				ExecuteTrade();
				break;
			case 5:
				tradeProgress = TradeProgress.PlayerTurn;
				GiveItem(ItemType.Food, Mathf.RoundToInt(npcInventory.npcTradeRatios[4].x));
				TakeItem(ItemType.Rags, Mathf.RoundToInt(npcInventory.npcTradeRatios[4].y));
				ExecuteTrade();
				break;
			case 6:
				tradeProgress = TradeProgress.PlayerTurn;
				GiveItem(ItemType.Rags, Mathf.RoundToInt(npcInventory.npcTradeRatios[5].x));
				TakeItem(ItemType.Parts, Mathf.RoundToInt(npcInventory.npcTradeRatios[5].y));
				ExecuteTrade();
				break;
			case 7:
				tradeProgress = TradeProgress.PlayerTurn;
				GiveItem(ItemType.Food, Mathf.RoundToInt(npcInventory.npcTradeRatios[0].y));
				TakeItem(ItemType.Meds, Mathf.RoundToInt(npcInventory.npcTradeRatios[0].x));
				ExecuteTrade();
				break;
			case 8:
				tradeProgress = TradeProgress.PlayerTurn;
				GiveItem(ItemType.Parts, Mathf.RoundToInt(npcInventory.npcTradeRatios[1].y));
				TakeItem(ItemType.Meds, Mathf.RoundToInt(npcInventory.npcTradeRatios[1].x));
				ExecuteTrade();
				break;
			case 9:
				tradeProgress = TradeProgress.PlayerTurn;
				GiveItem(ItemType.Rags, Mathf.RoundToInt(npcInventory.npcTradeRatios[2].y));
				TakeItem(ItemType.Meds, Mathf.RoundToInt(npcInventory.npcTradeRatios[2].x));
				ExecuteTrade();
				break;
			case 10:
				tradeProgress = TradeProgress.PlayerTurn;
				GiveItem(ItemType.Parts, Mathf.RoundToInt(npcInventory.npcTradeRatios[3].y));
				TakeItem(ItemType.Food, Mathf.RoundToInt(npcInventory.npcTradeRatios[3].x));
				ExecuteTrade();
				break;
			case 11:
				tradeProgress = TradeProgress.PlayerTurn;
				GiveItem(ItemType.Rags, Mathf.RoundToInt(npcInventory.npcTradeRatios[4].y));
				TakeItem(ItemType.Food, Mathf.RoundToInt(npcInventory.npcTradeRatios[4].x));
				ExecuteTrade();
				break;
			case 12:
				tradeProgress = TradeProgress.PlayerTurn;
				GiveItem(ItemType.Parts, Mathf.RoundToInt(npcInventory.npcTradeRatios[5].y));
				TakeItem(ItemType.Rags, Mathf.RoundToInt(npcInventory.npcTradeRatios[5].x));
				ExecuteTrade();
				break;
			default:
				Debug.LogError("You've used the wrong TradeType. Found: " + tradeType);
				break;
		}

		tradeProgress = TradeProgress.Init;
	}
}
