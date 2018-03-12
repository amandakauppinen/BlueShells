using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	/// <summary>
	/// Creates move speed for player
	/// </summary>
	public float moveSpeed;

	/// <summary>
	/// Inlcudes the animations for the player and creates a rigidbody
	/// </summary>
	private Animator anim;
	private Rigidbody2D myRigidbody;

	/// <summary>
	/// Player is either moving or is not
	/// </summary>
	private bool playerMoving;
	public Vector2 lastMove;

	/// <summary>
	/// Player either exists or does not
	/// </summary>
	private static bool playerExists;

	/// <summary>
	/// These variables are used to keep track of item collection
	/// </summary>
	public static int itemCount;
	public Text countText;
	public Text winText;

	/// <summary>
	/// Variable for pausing movement during dialogue
	/// </summary>
	public bool canMove;

	/// <summary>
	/// Item count is set to zero when the game starts and will
	/// display no win text until later
	/// If the player doesn't exist, it creates a player
	/// Otherwise, if there is a player, the player is destroyed and replaced
	/// This is used for movement to the next scene
	/// </summary>
	void Start () 
	{
		itemCount = 0;
		winText.text = "";
		SetCountText ();

		anim = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D> ();

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

	/// <summary>
	/// This function is used to pause the character's movement
	/// while dialogue is open
	/// By default, the player is static until given input
	/// Based on the keys pressed, the player will either move horizontally
	/// or vertically and will retain the direction of it's last move until
	/// a directional button is pushed again
	/// </summary>
	void Update () 
	{

		if (!canMove)
		{
		return;
		}
			
		playerMoving = false;

		if (Input.GetAxisRaw ("Horizontal") > 0.5f || Input.GetAxisRaw ("Horizontal") < -0.5f) 
		{
			myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myRigidbody.velocity.y);
			playerMoving = true;
			lastMove = new Vector2 (Input.GetAxisRaw ("Horizontal"), 0f);
		} 
			
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

	/// <summary>
	/// If items are collected, 1 is added to the count
	/// A sound will be played upon item collection
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag("Items"))
		{
			other.gameObject.SetActive(false);
			itemCount = itemCount + 1;
			SetCountText ();

			FindObjectOfType<AudioManager> ().Play ("Item");
		}
	}

	/// <summary>
	/// This function displays a text that shows the amount of iems collected
	/// If the item amount is equal to 4 or greater, display "win and escape" text
	/// </summary>
	void SetCountText()
	{
		//countText.text = "Item Count: " + count.ToString ();
		if (itemCount >= 4)
			winText.text = "You've collected all your items! Now hurry and escape!";
	}
}
