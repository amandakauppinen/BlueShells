using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;

	private Animator anim;
	private Rigidbody2D myRigidbody;

	private bool playerMoving;
	public Vector2 lastMove;

	private static bool playerExists;

	public static int count;
	public Text countText;
	public Text winText;

	// Use this for initialization
	void Start () {

		count = 0;
		winText.text = "";
		SetCountText ();

		anim = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D> ();


		if (!playerExists) {
			playerExists = true;
			DontDestroyOnLoad (transform.gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	// Update is called once per frame
	void Update () {

		playerMoving = false;

		if (Input.GetAxisRaw ("Horizontal") > 0.5f || Input.GetAxisRaw ("Horizontal") < -0.5f) 
		{
			//transform.Translate (new Vector3 (Input.GetAxisRaw ("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f ));
			myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myRigidbody.velocity.y);
			playerMoving = true;
			lastMove = new Vector2 (Input.GetAxisRaw ("Horizontal"), 0f);
		} 

		if (Input.GetAxisRaw ("Vertical") > 0.5f || Input.GetAxisRaw ("Vertical") < -0.5f) 
		{
			//transform.Translate (new Vector3 (0f, Input.GetAxisRaw ("Vertical") * moveSpeed * Time.deltaTime, 0f ));
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
		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		if (other.gameObject.CompareTag("Items"))
		{
			other.gameObject.SetActive(false);
			count = count + 1;
			SetCountText ();
		}
	}

	void SetCountText()
	{
		countText.text = "Item Count: " + count.ToString ();
		if (count >= 4)
			winText.text = "You've collected all your items! Now hurry and escape!";
		
	}
}
