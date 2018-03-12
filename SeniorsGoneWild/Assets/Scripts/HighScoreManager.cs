using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour {

	/// <summary>
	/// All variables are elements used for the format of the high score appearance
	/// </summary>
	private string connectionString;
	private List<HighScore> highScores = new List<HighScore> ();


	public GameObject scorePrefab;
	public GameObject nameDialog;

	public Transform scoreParent;

	public int topRanks;
	public int saveScores;

	public InputField enterName;


	/// <summary>
	/// The start contains all of the functions we will use to update the high score list
	/// This sets a path that goes directly to the database created for the game
	/// </summary>
	void Start () 
	{
		connectionString = "URI=file:" + Application.dataPath + "/HighScoreDB.db";

		CreateTable ();
		DeleteExtraScore();
		ShowScores();
	}

	/// <summary>
	/// This is used for the "Insert Name" box
	/// It is activated upon using the escape key
	/// </summary>
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			nameDialog.SetActive(!nameDialog.activeSelf);
		}
	}
		
	/// <summary>
	/// This function creates the database table inside the C# and unity files
	/// </summary>
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

		}
	}
	
	/// <summary>
	/// This function checks if the player has entered a name or not
	/// It sets the player's score, taken from the NurseController
	/// It will then enter the name, and show the previous scores
	/// </summary>
	public void EnterName()
	{
		if (enterName.text != string.Empty)
		{
			int score = NurseController.scoreCount;
			InsertScore(enterName.text, score);
			enterName.text = string.Empty;

			ShowScores();
		}
	}

	/// <summary>
	/// This function compares the scores and adjusts the rankings.
	/// Our scoreboard shows the top 10, so it removes the lowest score
	/// instead of the highest
	/// It opens and closes the connection to the database via SQL queries
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="newScore">New score.</param>
	private void InsertScore(string name, int newScore)
	{
		GetScores ();
		int hsCount = highScores.Count;
		if (highScores.Count > 0) 
		{
			HighScore lowestScore = highScores [highScores.Count - 1];
			if (lowestScore != null && saveScores > 0 && highScores.Count >= saveScores && newScore > lowestScore.Score) 
			{
				DeleteScore (lowestScore.ID);
				hsCount--;
			}
		}

		if (hsCount < saveScores)
		{

		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) 
		{
			dbConnection.Open ();
				using (IDbCommand dbCmd = dbConnection.CreateCommand ()) 
				{
					string sqlQuery = String.Format ("INSERT INTO HighScores (Name, Score) values (\"{0}\",\"{1}\")", name, newScore);

					dbCmd.CommandText = sqlQuery;
					dbCmd.ExecuteScalar ();
					dbConnection.Close ();
				}

			}
		}

	}

	/// <summary>
	/// This function retrieves the scores from the database and inserts them into the unity scoreboard
	/// and then sorts them in highest-lowest order
	/// </summary>
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

	/// <summary>
	/// This function is called upon to delete a score when a newer, higher score is
	/// inserted into the database, thus taking it off the high score list
	/// </summary>
	/// <param name="id">Identifier.</param>
	private void DeleteScore(int id)
	{
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

	/// <summary>
	/// This function is used to destroy all of the scores, in order to reinsert
	/// them into the database to ensure that no repeats are made
	/// It changes the scoreboard information, and inserts a "#" before the rank
	/// It also includes "i + 1" which starts the ranks from 1 instead of 0
	/// It includes organizational elements such as the prefab and parent items
	/// which is where the scores are inserted
	/// </summary>
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
			tmpObject.transform.SetParent(scoreParent);
			tmpObject.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
			}
		}
	}

	/// <summary>
	/// Deletes extra scores from the database based on our limit
	/// </summary>
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