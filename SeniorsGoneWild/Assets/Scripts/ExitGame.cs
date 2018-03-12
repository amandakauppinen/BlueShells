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

	/// <summary>
	/// Fucntion used for the final game exit, attached to the final door in the third level
	/// If the player reaches the final door, it will load Credits
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter2D (Collider2D other)
	{
		{
			Application.LoadLevel ("EndScene");
		}

	}
}
