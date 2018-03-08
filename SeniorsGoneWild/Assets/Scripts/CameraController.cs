using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	//Camera will follow player, create a target position and correlate with a move speed
	public GameObject followTarget;
	private Vector3 targetPos;
	public float moveSpeed;

	//Camera either exists or it does not
	private static bool cameraExists;

	void Start () 
	{
		//Game will not destroy camera on start
		DontDestroyOnLoad (transform.gameObject); 

		//If no camera exists, a camera will be made
		//If camera exists, it will be destroyed and a new one will be made
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
	

	void Update () 
	{
		//Camera will follow target position in the x and y directions
		//The move speed of camera will be smoothed by deltatime
		targetPos = new Vector3 (followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
		transform.position = Vector3.Lerp (transform.position, targetPos, moveSpeed * Time.deltaTime);
	}
}
