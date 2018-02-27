using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseController : MonoBehaviour {

	//private Animator anim;

	public float moveSpeed;

	private Rigidbody2D myRigidBody;

	private bool moving;

	public float timeBetweenMove;
	private float timeBewtweenMoveCounter;

	public float timeToMove;
	private float timeToMoveCounter;

	private Vector3 moveDirection;


	// Use this for initialization
	void Start () {
		//anim = GetComponent<Animator> ();

		myRigidBody = GetComponent<Rigidbody2D> ();

		timeBewtweenMoveCounter = timeBetweenMove;
		timeToMoveCounter = timeToMove;


	}
	
	// Update is called once per frame
	void Update () {

		if (moving) {

			timeToMoveCounter -= Time.deltaTime;
			myRigidBody.velocity = moveDirection;

			if (timeToMoveCounter < 0f) {
				
				moving = false;
				timeBewtweenMoveCounter = timeBetweenMove;
			}
			
		} else {
			
			timeBewtweenMoveCounter -= Time.deltaTime;
			myRigidBody.velocity = Vector2.zero;

			if (timeBewtweenMoveCounter < 0f) {

				moving = true;
				timeToMoveCounter = timeToMove;

				moveDirection = new Vector3 (Random.Range (-1f, 1f) * moveSpeed, Random.Range(-1f, 1f)* moveSpeed, 0f) ;
				}
			}
	
		//anim.SetFloat (
	}

}
