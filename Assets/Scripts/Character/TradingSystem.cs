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

	private void OnTriggerEnter(Collider other)
	{
		// assign designated NPC to variables
		npcInventory = other.GetComponent<Inventory>();
		npcToTradeWith = other;

		// only make actions when true (in range)
		if (other.CompareTag("NPC"))
			inTradingRange = true;
	}

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
			if (playerInventory.InventoryItems.TryGetValue("Food", out int value))
			{
				amountToSwap = value;
				playerInventory.RemoveItem("Food", value);

				// gives number to NPC
				npcToTradeWith.GetComponent<Inventory>().AddItem("Food", amountToSwap);
			}
		}
	}

	public void TakeItem()
	{
		if (npcToTradeWith != null && inTradingRange)
		{
			// takes number from player
			if (npcToTradeWith.GetComponent<Inventory>().InventoryItems.TryGetValue("Food", out int value))
			{
				amountToSwap = value;
				playerInventory.AddItem("Food", value);
				// takes number to NPC
				npcToTradeWith.GetComponent<Inventory>().RemoveItem("Food", amountToSwap);
			}
		}
	}
}
