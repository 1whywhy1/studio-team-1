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
			if (inventoryShowing)
			{
				inventoryScreen.SetActive(true);
			}
			else
			{
				inventoryScreen.SetActive(false);
			}
		}
	}
}
