using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore 
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

}


