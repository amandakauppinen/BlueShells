using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour {


	public string dialogue;
	private DialogueManager dMan;
	// Use this for initialization
	void Start () {
		dMan = FindObjectOfType<DialogueManager> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D other){
		
		if (other.gameObject.name == "Player")
		{
			
			if (Input.GetKeyUp(KeyCode.Space))
			{
				dMan.ShowBox(dialogue);
			}
		}
	}

}
