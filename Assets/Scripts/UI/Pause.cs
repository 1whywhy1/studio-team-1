using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
	public static bool GameIsPaused = false;
	public GameObject settingsUI;
	public GameObject pauseMenuUI;

	[Header("Audio SFX")]
	public AudioClip menuOpen;
	public AudioClip menuClose;
	[Range(0f, 3f)] public float menuSoundLevel;
	private AudioSource audioSource;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameIsPaused)
			{
				audioSource.PlayOneShot(menuClose, menuSoundLevel);
				Resume();
			}
			else
			{
				audioSource.PlayOneShot(menuOpen, menuSoundLevel);
				Paused();
			}
		}
	}

	public void Resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	public void Paused()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void Settings()
	{
		pauseMenuUI.SetActive(false);
		settingsUI.SetActive(true);
	}

	public void Back()
	{
		settingsUI.SetActive(false);
		pauseMenuUI.SetActive(true);
	}
}