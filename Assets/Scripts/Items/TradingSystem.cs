using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingSystem : MonoBehaviour
{
	public GameObject tradingUI;
	private Collider npcToTradeWith;

	[SerializeField] private TradeProgress tradeProgress;
	private Inventory playerInventory;
	[SerializeField] private NPCInventory npcInventory;
	private ItemType playerSwapType, npcSwapType;
	private int playerSwapAmount, npcSwapAmount;
    private int giveToPiorityGroup, takeFromPriorityGroup;
	private bool tradeValid;

	void Start()
	{
		tradeProgress = TradeProgress.Init;
		playerInventory = GameObject.Find("EGOPlayerInventory").GetComponent<Inventory>();
		tradingUI.SetActive(false);
	}

	/// <summary>
	/// Gets triggered by character (via SendMessage())
	/// </summary>
	/// <param name="npcToBarter">Pass through NPC via OnTriggerEnter()</param>
	void ToggleBarterUi(Collider npcToBarter)
	{
		// toggle trading UI
		tradingUI.SetActive(!tradingUI.activeSelf);

		// freeze time when trading
		if (tradingUI.activeSelf)
		{
			// assign NPC inventory WHEN this method is triggered (avoids 'NullReferenceException')
			npcToTradeWith = npcToBarter;
			npcInventory = npcToTradeWith.GetComponent<NPCInventory>();

			Time.timeScale = 0;
		}
		else
		{
			// kill references when you're done
			npcToTradeWith = null;
			npcInventory = null;

			Time.timeScale = 1;
		}
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
					Debug.Log("YOU DON'T HAVE ENOUGH");
					InvalidateTrade();
				}
				else
				{
					tradeValid = true;
					tradeProgress = TradeProgress.NPCTurn;

					playerSwapType = item;
					playerSwapAmount = amount;
				}
			}
		}
	}

	public void TakeItem(ItemType item, int amount)
	{
		if (npcToTradeWith != null && tradeValid != false)
		{
			// if item exists in inventory
			if (npcInventory.InventoryItems.TryGetValue(item, out int value))
			{
				// if either person does not have enough materials
				if (value < amount)
				{
					Debug.Log("THEY DON'T HAVE HAVE ENOUGH");
					InvalidateTrade();
				}
				else
				{
					tradeValid = true;
					tradeProgress = TradeProgress.MakeTrade;

					npcSwapType = item;
					npcSwapAmount = amount;
				}
			}
		}
	}

	/// <summary>
	/// If trade shouldn't happen - it get's cancelled here
	/// </summary>
	void InvalidateTrade()
	{
		tradeValid = false;
		tradeProgress = TradeProgress.Init;
	}

	// If trade successful - it carries out here
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
	/// Trade assigned item for assigned item. Ratios are accessed via Vector2 (x and y values)
	/// </summary>
	/// <param name="itemOne">The first item.</param>
	/// <param name="itemTwo">The second item.</param>
	public void TradeItemsSetup(int tradeType)
	{
		switch (tradeType)
		{
			case 1:
				tradeProgress = TradeProgress.PlayerTurn;
                // AG: Here is where you want to check the ratio.
                // Find out how many of the specific item an NPC has and calculate the ratio for trading.
                // Eg: Player has 100 Meds and NPC has 100 Parts and 0 Meds. The player wants to trade Meds for Parts.
                //     There should be UI where the player can adjust the amount of items traded...
                //     ...so the player can say "I want to give you 1 Meds and I want Parts." and the NPC will reply "Cool! I will give you 4 parts for 1 meds."
                //     This NPC would offer 4:1 ratio because it has no Meds.
                //     If the player then offered parts, the NPC would take the ratio 1:1.
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
