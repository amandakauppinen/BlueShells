using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HighScore : IComparable<HighScore>

/// <summary>
/// Creates Score, Name, and ID variables for the database
/// IComparable command used for sorthing the scores
/// </summary>
/// <value>The score.</value>
{
	public int Score {get;set;}
	public string Name { get; set; }
	public int ID { get; set; }
	//public DateTime Date { get; set; }

	public HighScore (int id, int score, string name){
		//if we want date and time, then include "DateTime date" inside those brackets
		this.Score = score;
		this.Name = name;
		this.ID = id;
		//this.Date = date;

	}

	/// <summary>
	/// Function used to compare the scores in order to sort and delete later on
	/// first > second return -1
	/// first < second return 1
	/// first == second return 0
	/// </summary>
	/// <returns>The to.</returns>
	/// <param name="other">Other.</param>
	public int CompareTo(HighScore other)
	{
		if (other.Score < this.Score)
		{
			return -1;
		}

		else if (other.Score > this.Score)
		{
			return 1;
		}

		return 0;
	}

}


