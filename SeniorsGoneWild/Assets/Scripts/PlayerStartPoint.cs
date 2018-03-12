using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour {

	/// <summary>
	/// Uses the player and camera controllers
	/// </summary>
	private PlayerController thePlayer;
	private CameraController theCamera;

	/// <summary>
	/// Creates the starting direction
	/// </summary>
	public Vector2 startDirection;

	/// <summary>
	/// This function finds the player, and uses the last move to
	/// set the new direction in the next level
	/// It also sets the camera in the same position post-level transition
	/// </summary>
	void Start () 
	{
		thePlayer = FindObjectOfType<PlayerController> ();
		thePlayer.transform.position = transform.position;
		thePlayer.lastMove = startDirection;

		theCamera = FindObjectOfType<CameraController> ();
		theCamera.transform.position = new Vector3 (transform.position.x, transform.position.y, theCamera.transform.position.z);
	}

	void Update () 
	{
		
	}
}
