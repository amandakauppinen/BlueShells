using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
	public void StartGame()
	{
		//Script attached to play button in MainMenu for starting the game at Level 1
		Application.LoadLevel("Level 1");
	}
}