using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadToMain : MonoBehaviour {

	/// <summary>
	/// This function loads the menu from the End Scene
	/// </summary>
	public void LoadMenu()
	{
		Application.LoadLevel("Menu");
	}
}