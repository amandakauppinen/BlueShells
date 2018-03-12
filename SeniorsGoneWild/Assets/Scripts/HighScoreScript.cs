using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script controls the scoreboard and adds information to it. (Like Player name, rank and score)
/// </summary>
public class HighScoreScript : MonoBehaviour {

	/// <summary>
	/// Creates score, name and rank variables for the High Score Database
	/// </summary>
	public GameObject score;
	public GameObject scoreName;
	public GameObject rank;

	/// <summary>
	/// This fucntion modifies the text component and adds text into it
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="score">Score.</param>
	/// <param name="rank">Rank.</param>
	public void SetScore(string name, string score, string rank)
	{
		this.score.GetComponent<Text>().text = score;
		this.scoreName.GetComponent<Text>().text = name;
		this.rank.GetComponent<Text>().text = rank;
	}
}
