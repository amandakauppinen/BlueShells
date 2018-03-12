using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadHighScores : MonoBehaviour {

	/// <summary>
	/// This function loads the EndScene
	/// from the credits page
	/// </summary>
	public void LoadEndScene()
	{
		Application.LoadLevel("EndScene");
	}
}