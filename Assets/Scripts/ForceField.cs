using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
	// Singleton Sound Manager instance
	private static ForceField _instance;
	public static ForceField Instance { get { return _instance; } }

	[Header("Effects Area")]
	[Range(0f, 1f)] public float exitSoundLevel;
	private AudioSource audioSource;
	
	private void Awake()
	{
		if (_instance != null && _instance != this)
			Destroy(this.gameObject);
		else
			_instance = this;

		audioSource = GetComponent<AudioSource>();
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
			audioSource.PlayOneShot(audioSource.clip, exitSoundLevel);
	}
}
