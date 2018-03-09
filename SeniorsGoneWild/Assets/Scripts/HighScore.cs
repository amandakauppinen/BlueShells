using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HighScore : IComparable<HighScore>
//the IComparable command is for sorting the scores
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
	public int CompareTo(HighScore other)
	{
		//first > second return -1
		//first < second return 1
		//first == second returrn 0

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


