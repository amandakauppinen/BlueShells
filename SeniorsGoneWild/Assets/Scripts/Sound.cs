using UnityEngine.Audio;  /*This is necessary for getting access to audio settings like volume and pitch */
using UnityEngine;

[System.Serializable] /* this will give this class a permission to appear in the inspector*/
public class Sound {

	public string name;

	public AudioClip clip;

	[Range(0f, 2f)] /*these are minimum and maximum values*/
	public float volume;
	[Range(.1f, 3f)]
	public float pitch;

	public bool loop;

	[HideInInspector] /*this will hide the variable from the Inspector. We do this because this is a value 
	that we publish automatically in the awake method*/
	public AudioSource source;

}
