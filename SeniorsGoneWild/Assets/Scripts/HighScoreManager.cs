using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;

public class HighScoreManager : MonoBehaviour {

	private string connectionString;
	private List<HighScore> highScores = new List<HighScore> ();

	public GameObject scorePrefab; 

	public Transform scoreParent;


	// Use this for initialization
	void Start () {
		connectionString = "URI=file:" + Application.dataPath + "/HighScoreDB.sqlite";
		InsertScore ("Kenneth", 10);

		//GetScores ();
		ShowScores();
	}

	// Update is called once per frame
	void Update () {

	}
	private void InsertScore(string name, int newScore)
	{
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) 
		{
			dbConnection.Open ();

			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) 
			{
				string sqlQuery = String.Format("INSERT INTO HighScores (Name, Score) values (\"{0}\",\"{1}\")",name,newScore);

				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar();
				dbConnection.Close ();


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
		for (int i = 0; i < highScores.Count; i++)
		{
			GameObject tmpObject = Instantiate(scorePrefab);
			HighScore tmpScore = highScores[i];

			tmpObject.GetComponent<HighScoreScript> ().SetScore(tmpScore.Name, tmpScore.Score.ToString(), "#" + (i + 1).ToString ());
			//changes scoreboard information. Puts # Before the rank number. 
			//i +1 means 0+1, so it's gonna start ranks from 1 instead of 0 and it will keep increasing it by 1.


			tmpObject.transform.SetParent(scoreParent);

			tmpObject.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
		
		}
	}
}



