using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseController : MonoBehaviour {

	/// <summary>
	/// Declares a moving speed for the nurse
	/// </summary>
	public float moveSpeed;

	/// <summary>
	/// Declares a rigid body for the nurse
	/// </summary>
	private Rigidbody2D myRigidBody;

	/// <summary>
	/// The nurse is either moving or is not
	/// </summary>
	private bool moving;

	/// <summary>
	/// Because the nurse movement is random, the code 
	/// needs a counter in between the randomized moves
	/// before the nurse will be set to move again
	/// </summary>
	public float timeBetweenMove;
	private float timeBetweenMoveCounter;

	/// <summary>
	/// These set how long the nurse will be moving
	/// </summary>
	public float timeToMove;
	private float timeToMoveCounter;

	/// <summary>
	/// Sets a moving direction for the nurse
	/// </summary>
	private Vector3 moveDirection;

	/// <summary>
	/// This is for the character interation when caught by a nurse
	/// The character will reload, and there is a timer for that
	/// </summary>
	public float waitToBeload;
	private bool reloading;

	/// <summary>
	/// Includes the Player object
	/// </summary>
	private GameObject thePlayer;

	/// <summary>
	/// This is used to set a baseline for the scoring system
	/// </summary>
	public static int scoreCount = 500;

	/// <summary>
	/// This function gives a rigid body to nurse
	/// It also sets a random time in between nurse movements
	/// </summary>
	void Start () 
	{
		myRigidBody = GetComponent<Rigidbody2D> ();

		timeBetweenMoveCounter = Random.Range (timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
		timeToMoveCounter = Random.Range (timeToMove * 0.75f, timeBetweenMove * 1.25f);
	}

	/// <summary>
	/// If the timeToMoveCounter goes below zero, the nurse will
	/// stop moving for a random amount of time
	/// If the player is caught (waitToBeload <0), player will be reset to the 
	/// beginning of the level and become active again
	/// </summary>
	void Update () 
	{
		if (moving) 
		{
			timeToMoveCounter -= Time.deltaTime;
			myRigidBody.velocity = moveDirection;

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
			waitToBeload -= Time.deltaTime;
			if (waitToBeload < 0) 
			{
				Application.LoadLevel (Application.loadedLevel);
				thePlayer.SetActive (true);
			}
		}
	}

	/// <summary>
	/// If the player is caught by a nurse, he will become inactive
	/// and relaod. He will also have points added for every nurse
	/// contact. It also includes the sound that will be played
	/// on nurse contact.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name == "Player")
		{
			other.gameObject.SetActive(false);
			reloading = true;
			thePlayer = other.gameObject;
			scoreCount = scoreCount - 10;

			FindObjectOfType<AudioManager> ().Play ("Busted");
		}
	}
}