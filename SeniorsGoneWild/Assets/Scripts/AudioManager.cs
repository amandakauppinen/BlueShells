using UnityEngine.Audio; /*This is necessary for getting access to audio settings like volume and pitch */
using UnityEngine;
using System;

/*this whole thing is for controlling the game sounds*/

public class AudioManager : MonoBehaviour {

	public Sound[] sounds;

	public static AudioManager instance;


	/*This is almost same as the start exept it's called right before. This let's us to play sounds in the start*/
	void Awake () {

		if (instance == null)
		instance = this;
		
		else
		{
			Destroy (gameObject);
			return;
		}

		DontDestroyOnLoad (gameObject);
		/*This foreach loop will loop through our sounds. Small "s" is the sound that we are currently looking for */
		foreach (Sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	void Start ()
	{
		Play ("Theme1");
	}


	/*This is for other classes to find their own sound from the audio manager*/
	public void Play (string name)
	{
		Sound s = Array.Find (sounds, sound => sound.name == name);
		s.source.Play ();
	}

}
