using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	public GameObject textBox;

	public Text theText;

	public TextAsset textFile;
	public string [] textLines;

	public int currentLine;
	public int endAtLine;

	public PlayerController player;

	public bool isActive;

	public bool stopPlayerMovement;


	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();

		//checks for existence of text
		if (textFile != null) {
			
			textLines = (textFile.text.Split ('\n'));

		}

		if (endAtLine == 0) {

			endAtLine = textLines.Length - 1;
		
		}

		if (isActive) {
			
			EnableTextBox ();

		} else {
			
			DisableTextBox ();

		}

	}
	
	// Update is called once per frame
	void Update () {

		if (!isActive) {
			return;
		}

		theText.text = textLines [currentLine];

		//Continues through the text when pressing Space
		if (Input.GetKeyDown (KeyCode.Space)) {
			
			currentLine += 1;
		}

		//Deactivates text box once limit of array is reached
		if (currentLine > endAtLine) {
			
			DisableTextBox ();
		}
	}

	public void EnableTextBox()
	{
		textBox.SetActive (true);

		if (stopPlayerMovement) {

			player.canMove = false;
		
		}
	}

	public void DisableTextBox()
	{
		textBox.SetActive (false);

		player.canMove = true;
	}
}
