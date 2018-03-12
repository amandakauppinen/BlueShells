using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QuitButton : MonoBehaviour
{
	/// <summary>
	/// This function attaches to the quit button in the MainMenu
	/// used for exiting the game entirely
	/// </summary>
	public void ExitGame()
	{
		Application.Quit();
	}
}