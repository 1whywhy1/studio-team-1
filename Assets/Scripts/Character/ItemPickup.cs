using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInventoryItem
{
	public string _Name;
	public string Name => _Name;

	public Sprite _ItemImage;
	public Sprite ItemImage => _ItemImage;

	public void OnPickup()
	{
		gameObject.SetActive(false);
	}
}
