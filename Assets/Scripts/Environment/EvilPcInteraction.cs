using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EvilPcInteraction : MonoBehaviour
{
	public TextMeshProUGUI info;		  // Information thatwas given to the Player in the beginning of the level
	public GameObject interactionMessage; // For showing a message in the middle of the screen telling the player to Press E for interaction
	public GameObject submitMenu;		  // Evil PC Submition menu

	[Header("Audio section")]
	public AudioClip computerError;
	public AudioClip computerSuccess;
	private AudioSource sfxSource;

	void Awake()
	{
		sfxSource = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && this.enabled)
		{
			interactionMessage.gameObject.SetActive(true);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player") && this.enabled)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				interactionMessage.SetActive(false);
				info.gameObject.SetActive(false);
				submitMenu.SetActive(true);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player") && this.enabled)
		{
			submitMenu.gameObject.SetActive(false);
			interactionMessage.gameObject.SetActive(false);
			info.gameObject.SetActive(true);
		}
	}

	public void SubmissionFailure()
	{
		sfxSource.clip = computerError;
		
		if (sfxSource.clip == computerError)
			sfxSource.PlayOneShot(sfxSource.clip);
		else
			Debug.LogWarning("Incorrect audio clip selected, played nothing.");
	}
	
	public void SubmissionSuccess(bool didSucceed)
	{
		if (didSucceed)
		{
			sfxSource.clip = computerSuccess;

			if (sfxSource.clip == computerSuccess)
				sfxSource.PlayOneShot(sfxSource.clip);
			else
				Debug.LogWarning("Incorrect audio clip selected, played nothing.");
		}
		else
		{
			SubmissionFailure();
		}
	}
}