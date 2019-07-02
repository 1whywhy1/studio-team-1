using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	public GameObject inventoryScreen;
	private bool inventoryShowing = false;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
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
}
