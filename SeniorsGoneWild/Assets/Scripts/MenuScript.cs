using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Title screen script
/// </summary>
public class MenuScript : MonoBehaviour
{
	public void StartGame()
	{
		Application.LoadLevel("Level 1");
	}
}