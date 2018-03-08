using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour {

	//uses the player and camera controllers
	private PlayerController thePlayer;
	private CameraController theCamera;

	//creates a starting direction
	public Vector2 startDirection;

	void Start () 
	{
		//This finds the player, and uses the last move to
		//set the new direction in the next level
		thePlayer = FindObjectOfType<PlayerController> ();
		thePlayer.transform.position = transform.position;
		thePlayer.lastMove = startDirection;

		//This finds the camera, and sets it to be in the same position as before
		theCamera = FindObjectOfType<CameraController> ();
		theCamera.transform.position = new Vector3 (transform.position.x, transform.position.y, theCamera.transform.position.z);
	}
	

	void Update () 
	{
		
	}
}
