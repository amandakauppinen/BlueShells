using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseController : MonoBehaviour {

	//Declares a moving speed for the nurse
	public float moveSpeed;

	//Declares a rigid body for the nurse
	private Rigidbody2D myRigidBody;

	//The nurse is either moving or is not
	private bool moving;

	//Because the nurse movement is random, the code 
	//needs a counter in between the randomized moves
	//before the nurse will be set to move again
	public float timeBetweenMove;
	private float timeBetweenMoveCounter;

	//These set how long the nurse will be moving
	public float timeToMove;
	private float timeToMoveCounter;

	//Sets a moving direction for the nurse
	private Vector3 moveDirection;

	//This is for the character interation when caught by a nurse
	//The character will reload, and there is a timer for that
	public float waitToBeload;
	private bool reloading;

	//Includes the Player object
	private GameObject thePlayer;

	void Start () 
	{
		//Gives a rigid body to the nurse
		myRigidBody = GetComponent<Rigidbody2D> ();

		//Sets a random time between nurse movements
		timeBetweenMoveCounter = Random.Range (timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
		timeToMoveCounter = Random.Range (timeToMove * 0.75f, timeBetweenMove * 1.25f);
	}

	void Update () 
	{
		if (moving) 
		{
			timeToMoveCounter -= Time.deltaTime;
			myRigidBody.velocity = moveDirection;

			//If timeToMoveCounter goes below zero, the nurse will 
			//stop moving for a random amount of time
			if (timeToMoveCounter < 0f) 
			{
				moving = false;
				timeBetweenMoveCounter = Random.Range (timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
			}
			
		} 
		else 
		{
			timeBetweenMoveCounter -= Time.deltaTime;
			myRigidBody.velocity = Vector2.zero;

			if (timeBetweenMoveCounter < 0f)
			{
				moving = true;
				timeToMoveCounter = Random.Range (timeToMove * 0.75f, timeBetweenMove * 1.25f);
				moveDirection = new Vector3 (Random.Range (-1f, 1f) * moveSpeed, Random.Range(-1f, 1f)* moveSpeed, 0f) ;
			}
		}
		if (reloading) 
		{
			//If the player is caught (waitToBeload <0), player will be reset to the 
			//beginning of the level and become active again
			waitToBeload -= Time.deltaTime;
			if (waitToBeload < 0) 
			{
				Application.LoadLevel (Application.loadedLevel);
				thePlayer.SetActive (true);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		//If the player is caught, he will become inactive and reload
		if (other.gameObject.name == "Player")
		{
			other.gameObject.SetActive(false);
			reloading = true;
			thePlayer = other.gameObject;
		}
	}
}