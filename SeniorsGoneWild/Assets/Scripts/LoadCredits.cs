using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCredits : MonoBehaviour {

	/// <summary>
	/// This function loads the menu from the End Scene
	/// </summary>
	public void LoadCredit()
	{
		Application.LoadLevel("Credits");
	}
}
