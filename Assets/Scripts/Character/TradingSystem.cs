using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingSystem : MonoBehaviour
{
	public bool inTradingRange = false;
	private GameObject tradingUI;

	void Start()
	{
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
		// only make actions when true (in range)
		if (other.CompareTag("NPC"))
			inTradingRange = true;
	}

	private void OnTriggerExit(Collider other)
	{
		// only make actions when true (in range)
		if (other.CompareTag("NPC"))
			inTradingRange = false;
	}
}
