﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseController : MonoBehaviour {

	//private Animator anim;

	public float moveSpeed;

	private Rigidbody2D myRigidBody;

	private bool moving;

	public float timeBetweenMove;
	private float timeBetweenMoveCounter;

	public float timeToMove;
	private float timeToMoveCounter;

	private Vector3 moveDirection;


	// Use this for initialization
	void Start () {
		//anim = GetComponent<Animator> ();

		myRigidBody = GetComponent<Rigidbody2D> ();

		//timeBetweenMoveCounter = timeBetweenMove;
		//timeToMoveCounter = timeToMove;
		timeBetweenMoveCounter = Random.Range (timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
		timeToMoveCounter = Random.Range (timeToMove * 0.75f, timeBetweenMove * 1.25f);
	}
	
	// Update is called once per frame
	void Update () {

		if (moving) {

			timeToMoveCounter -= Time.deltaTime;
			myRigidBody.velocity = moveDirection;

			if (timeToMoveCounter < 0f) {
				
				moving = false;
				//timeBetweenMoveCounter = timeBetweenMove;
				timeBetweenMoveCounter = Random.Range (timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
			}
			
		} else {
			
			timeBetweenMoveCounter -= Time.deltaTime;
			myRigidBody.velocity = Vector2.zero;

			if (timeBetweenMoveCounter < 0f) {

				moving = true;
				//timeToMoveCounter = timeToMove;
				timeToMoveCounter = Random.Range (timeToMove * 0.75f, timeBetweenMove * 1.25f);

				moveDirection = new Vector3 (Random.Range (-1f, 1f) * moveSpeed, Random.Range(-1f, 1f)* moveSpeed, 0f) ;
				}
			}
	
		//anim.SetFloat (
	}

}
