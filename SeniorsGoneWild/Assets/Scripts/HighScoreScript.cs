using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighScoreScript : MonoBehaviour {
	//This script controls the scoreboard and adds information to it. (Like Player name, rank and score)

	public GameObject score;
	public GameObject scoreName;
	public GameObject rank;

	/*this will modify the text component and add text in it*/
	public void SetScore(string name, string score, string rank)
	{
		this.score.GetComponent<Text>().text = score;
		this.scoreName.GetComponent<Text>().text = name;
		this.rank.GetComponent<Text>().text = rank;
	
	}
}
