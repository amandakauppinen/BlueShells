using UnityEngine.Audio;
using UnityEngine;
using System;

/// <summary>
/// This script is for controlling the game audio
/// </summary>
public class AudioManager : MonoBehaviour {

	/// <summary>
	/// Variables for managing audio
	/// </summary>
	public Sound[] sounds;
	public static AudioManager instance;

	/// <summary>
	/// This is almost same as the start exept it's called right before. This let's us to play sounds in the start
	/// The foreach loop will loop through our sounds. Small "s" is the sound that we are currently looking for
	/// </summary>
	void Awake () 
	{
		if (instance == null)
		instance = this;
		
		else
		{
			Destroy (gameObject);
			return;
		}

		DontDestroyOnLoad (gameObject);

		foreach (Sound s in sounds) 
		{
			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	/// <summary>
	/// Plays first theme
	/// </summary>
	void Start ()
	{
		Play ("Theme1");
	}
		
	/// <summary>
	/// This is for other classes to find their own sound from the audio manager
	/// </summary>
	/// <param name="name">Name.</param>
	public void Play (string name)
	{
		Sound s = Array.Find (sounds, sound => sound.name == name);
		s.source.Play ();
	}

}
