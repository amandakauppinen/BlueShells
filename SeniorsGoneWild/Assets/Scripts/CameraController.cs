using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	/// <summary>
	/// Camera will follow player, create a target position and correlate with a move speed
	/// </summary>
	public GameObject followTarget;
	private Vector3 targetPos;
	public float moveSpeed;

	/// <summary>
	/// Camera either exists or it does not
	/// </summary>
	private static bool cameraExists;


	/// <summary>
	/// Game will not destroy the camera on start
	/// If no camer exists, a camera will be made
	/// If camera exists, it will be destroyed and a new one will be made
	/// </summary>
	void Start () 
	{
		DontDestroyOnLoad (transform.gameObject); 

		if (!cameraExists) 
		{
			cameraExists = true;
			DontDestroyOnLoad (transform.gameObject);
		} 
		else 
		{
			Destroy (gameObject);
		}


	}
	
	/// <summary>
	/// Camera will follow target position in the x and y directions
	/// The move speed of the camera will be smoothed by deltatime
	/// </summary>
	void Update () 
	{
		targetPos = new Vector3 (followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
		transform.position = Vector3.Lerp (transform.position, targetPos, moveSpeed * Time.deltaTime);
	}
}
