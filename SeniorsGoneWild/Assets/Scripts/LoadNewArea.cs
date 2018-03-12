using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNewArea : MonoBehaviour {

	public string levelToLoad;

	void Start () 
	{

	}

	void Update () 
	{
		
	}

	/// <summary>
	/// This is the trigger for the door in between Levels 1 and 2
	/// It requires the object passing through to be the player and 
	/// the player must have collected all 4 items to move onto the
	/// next scene by making the trigger active
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.name == "Player" && PlayerController.itemCount == 4) 
		{
			Application.LoadLevel (levelToLoad);
		}

	}
}
