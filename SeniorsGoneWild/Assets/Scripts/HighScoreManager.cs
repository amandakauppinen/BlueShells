using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class HighScoreManager : MonoBehaviour {

	private string connectionString;

	// Use this for initialization
	void Start () {
		connectionString = "URI=file:" + Application.dataPath + "/HighScoreDB.sqlite";
		//InsertScore ("Kenneth", 10);
		DeleteScore(2);
		GetScores ();
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
				string sqlQuery = String.Format("insert into HighScores (Name, Score) values (\"{0}\",\"{1}\")",name,newScore);

				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar();
				dbConnection.Close ();


			}
		}
	}

	private void GetScores()
	{
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
						Debug.Log (reader.GetString (1)+ "- " + reader.GetInt32(2));
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
}



