using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QuitButton : MonoBehaviour
{
	public void ExitGame()
	{
		//Script attached to quit button in MainMenu for exiting the game
		Application.Quit();
	}
}