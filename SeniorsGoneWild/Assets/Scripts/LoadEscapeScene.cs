using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadEscapeScene : MonoBehaviour {

	public string levelToLoad;

	void Start () 
	{

	}

	void Update () 
	{
		
	}

	/// <summary>
	/// This function allows the player to go to the Escape
	/// Scene after exiting the final door on the third level
	/// The second loop makes sure that the itemCount resets
	/// for the next player
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.name == "Player") 
		{
			PlayerController.itemCount = 0;
		}

		if (other.gameObject.name == "Player") 
		{
			Application.LoadLevel (levelToLoad);
		}

	}
}
