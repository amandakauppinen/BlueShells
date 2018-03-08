using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	//Creates move speed for player
	public float moveSpeed;

	//Includes the animations for the player and creates a rigidbody
	private Animator anim;
	private Rigidbody2D myRigidbody;

	//Player is either moving or not
	private bool playerMoving;
	public Vector2 lastMove;

	//Player either exists or does not
	private static bool playerExists;

	//Count, counText and winText are used for to keep track of item collection
	public static int count;
	public Text countText;
	public Text winText;

	void Start () 
	{
		//Count is set to zero and will display nothing until first item is collected
		count = 0;
		winText.text = "";
		SetCountText ();

		//retrieves animator and rigidbody for Player
		anim = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D> ();

		//If the player doesn't exist, it creates a player
		//Otherwise, if there is a player, the player is destroyed and replaced with a new one
		//This is for movement to the next level (scene)
		if (!playerExists) 
		{
			playerExists = true;
			DontDestroyOnLoad (transform.gameObject);
		} 
		else 
		{
			Destroy (gameObject);
		}
	}
		
	void Update () 
	{
		//By default, Player is static until given input
		playerMoving = false;

		//If input keys are left or right, the player will move horizontally and
		//hold it's last position until affected by movement again
		if (Input.GetAxisRaw ("Horizontal") > 0.5f || Input.GetAxisRaw ("Horizontal") < -0.5f) 
		{
			myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myRigidbody.velocity.y);
			playerMoving = true;
			lastMove = new Vector2 (Input.GetAxisRaw ("Horizontal"), 0f);
		} 

		//If input keys are up or down, the player will move vertically and
		//hold it's last position until affected by movement again
		if (Input.GetAxisRaw ("Vertical") > 0.5f || Input.GetAxisRaw ("Vertical") < -0.5f) 
		{
			myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
			playerMoving = true;
			lastMove = new Vector2 (0f, Input.GetAxisRaw ("Vertical"));
		}

		if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
		{ 
			myRigidbody.velocity = new Vector2 (0f, myRigidbody.velocity.y);
		}

		if (Input.GetAxisRaw ("Vertical") < 0.5f && Input.GetAxisRaw ("Vertical") > -0.5f)
		{ 
			myRigidbody.velocity = new Vector2 ( myRigidbody.velocity.x, 0f);
		}
			
		anim.SetFloat ("MoveX", Input.GetAxisRaw("Horizontal"));
		anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));

		anim.SetBool ("PlayerMoving", playerMoving);
		anim.SetFloat("LastMoveX", lastMove.x);
		anim.SetFloat("LastMoveY", lastMove.y);
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		//If items are collected, add 1 to the count
		if (other.gameObject.CompareTag("Items"))
		{
			other.gameObject.SetActive(false);
			count = count + 1;
			SetCountText ();
		}
	}

	void SetCountText()
	{
		//Display a text that shows the amount of items collected
		//If the item amount is equal to 4 or greater, display "win and escape" text
		countText.text = "Item Count: " + count.ToString ();
		if (count >= 4)
			winText.text = "You've collected all your items! Now hurry and escape!";
	}
}
