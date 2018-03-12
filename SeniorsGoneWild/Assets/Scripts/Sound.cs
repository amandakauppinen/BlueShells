using UnityEngine.Audio;  
using UnityEngine;

/// <summary>
/// This is necessary for getting access to audio settings like volume and pitch 
/// this will give this class a permission to appear in the inspector
/// </summary>
[System.Serializable] 
public class Sound {

	/// <summary>
	/// Creates a name and a clip variables to be used in Unity
	/// </summary>
	public string name;
	public AudioClip clip;

	/// <summary>
	/// these are minimum and maximum values
	/// </summary>
	[Range(0f, 2f)] 
	public float volume;
	[Range(.1f, 3f)]
	public float pitch;

	/// <summary>
	/// this will hide the variable from the Inspector. We do this because this is a value 
	/// that we publish automatically in the awake method
	/// </summary>
	public bool loop;

	[HideInInspector]
	public AudioSource source;

}
