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

	void OnTriggerEnter2D (Collider2D other)
	{
		//This is the trigger for the final door in the first level
		//If the player has collected the required amount of items, the trigger will become active
		//Afer the trigger is active, the player can move on to the next level
		if (other.gameObject.name == "Player" && PlayerController.itemCount == 4) 
		{
			Application.LoadLevel (levelToLoad);
		}

	}
}
