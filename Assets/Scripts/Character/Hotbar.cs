using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
	public Inventory inventory;

	void Start()
	{
		Inventory.ItemAdded += InventoryScript_ItemAdded;
	}

	private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
	{
		Transform inventoryPanel = transform.Find("PlayerHotbar");
		foreach (Transform slot in inventoryPanel)
		{
			Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();

			// When found the empty slot
			if (!image.enabled)
			{
				image.enabled = true;
				image.sprite = e.Item.ItemImage;

				// TODO: Store a reference to the item

				break;
			}
		}
	}
}
