using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
	/// <summary>
	/// Script attached to teh play button in MainMenu for starting the game at Level 1
	/// </summary>
	public void StartGame()
	{
		Application.LoadLevel("Level 1");
	}
}