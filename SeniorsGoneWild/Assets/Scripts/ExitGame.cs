using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour {

	public string levelToLoad;

	void Start () 
	{

	}

	void Update () 
	{

	}

	void OnTriggerEnter2D (Collider2D other)
	{
		//Script used for the final game exit, attached to the final door in the second level
		//If the player reaches the final door, it will load EndScene
		if (other.gameObject.name == "Player") 
		{
			Application.LoadLevel ("EndScene");
		}

	}
}
