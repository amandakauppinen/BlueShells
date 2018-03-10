﻿using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour {

	private string connectionString;
	private List<HighScore> highScores = new List<HighScore> ();

	public GameObject scorePrefab; 

	public Transform scoreParent;

	public int topRanks;

	public int saveScores;

	public InputField enterName;

	public GameObject nameDialog;


	// Use this for initialization
	void Start () {
		connectionString = "URI=file:" + Application.dataPath + "/HighScoreDB.db";

		CreateTable ();
		//GetScores ();
		DeleteExtraScore();
		ShowScores();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			nameDialog.SetActive(!nameDialog.activeSelf);
		}
	}


	private void CreateTable()
	{
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) 
		{
			dbConnection.Open ();

			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {
				string sqlQuery = String.Format ("CREATE TABLE if not exists HighScores (PlayerID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name INTEGER NOT NULL, Score INTEGER )");

				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar ();
				dbConnection.Close ();
			}

		}}
	

	public void EnterName()
	{
		if (enterName.text != string.Empty)
			//this will check if the player entered a name or not
		{
			//int score = UnityEngine.Random.Range(1,500);
			int score = NurseController.scoreCount;
			InsertScore(enterName.text, score);
			enterName.text = string.Empty;


			ShowScores();
		}
	}


	private void InsertScore(string name, int newScore)
	{
		GetScores ();
		int hsCount = highScores.Count;

		if (highScores.Count > 0) {
			HighScore lowestScore = highScores [highScores.Count - 1];
			if (lowestScore != null && saveScores > 0 && highScores.Count >= saveScores && newScore > lowestScore.Score) {
				DeleteScore (lowestScore.ID);
				hsCount--;
			}
		}

		if (hsCount < saveScores)
		{

		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) 
		{
			dbConnection.Open ();

				using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {
					string sqlQuery = String.Format ("INSERT INTO HighScores (Name, Score) values (\"{0}\",\"{1}\")", name, newScore);

					dbCmd.CommandText = sqlQuery;
					dbCmd.ExecuteScalar ();
					dbConnection.Close ();
				}

			}
		}

	}


	private void GetScores()
	{
		highScores.Clear ();

		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) 
		{
			dbConnection.Open ();

			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) 
			{
				string sqlQuery = "SELECT * FROM HighScores";

				dbCmd.CommandText = sqlQuery;

				using (IDataReader reader = dbCmd.ExecuteReader())
				{
					while (reader.Read ()) 
					{
						highScores.Add(new HighScore(reader.GetInt32(0),reader.GetInt32(2),reader.GetString(1)));
							//with date and time add to the end: reader.GetDateTime(3);
					}

					dbConnection.Close ();
					reader.Close ();
				}
			}
		}

		highScores.Sort ();

	}
	private void DeleteScore(int id)
	{
		//DELETE FROM HighScores WHERE PlayerID = "4"
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) 
		{
			dbConnection.Open ();

			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) 
			{
				string sqlQuery = String.Format("DELETE FROM HighScores WHERE PlayerID = \"{0}\"",id);

				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar();
				dbConnection.Close ();

			}
		}
	}

	private void ShowScores() 
	{
		GetScores ();

		foreach (GameObject score in GameObject.FindGameObjectsWithTag("Score")) 
		{
			Destroy (score);
		}

		for (int i = 0; i < topRanks; i++)
		{
			if (i <= highScores.Count -1)
			{
				
			GameObject tmpObject = Instantiate(scorePrefab);
			HighScore tmpScore = highScores[i];

			tmpObject.GetComponent<HighScoreScript> ().SetScore(tmpScore.Name, tmpScore.Score.ToString(), "#" + (i + 1).ToString ());
			//changes scoreboard information. Puts # Before the rank number. 
			//i +1 means 0+1, so it's gonna start ranks from 1 instead of 0 and it will keep increasing it by 1.

			tmpObject.transform.SetParent(scoreParent);
			//tmpObject.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
			}
		}
	}
	private void DeleteExtraScore()
	{
		GetScores ();
		if (saveScores <= highScores.Count) 
		{
			int deleteCount = highScores.Count - saveScores;
			highScores.Reverse ();

			using (IDbConnection dbConnection = new SqliteConnection (connectionString)) 
			{
				dbConnection.Open ();

				using (IDbCommand dbCmd = dbConnection.CreateCommand ()) 
				{
					for (int i = 0; i < deleteCount; i++) 
					{
						string sqlQuery = String.Format ("DELETE FROM HighScores WHERE PlayerID = \"{0}\"", highScores [i].ID);

						dbCmd.CommandText = sqlQuery;
						dbCmd.ExecuteScalar ();
				

					}
					dbConnection.Close ();
				}
			}
		}
	}
}



