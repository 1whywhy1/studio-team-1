using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
	[Header("Effects Area")]
	[Range(0f, 1f)] public float exitSoundLevel;
	private AudioSource audioSource;
	
	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
			audioSource.PlayOneShot(audioSource.clip, exitSoundLevel);
	}
}
