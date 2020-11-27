using System;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using Newtonsoft.Json;
using UnityEngine;

public partial struct Data
{
	public struct DBReader
	{

		private static readonly string spaceDBName = "space.db";
		private static readonly string starTableName = "star";

		private static IDbCommand dbcmd;
		private static IDataReader reader;
		public static void Read()
		{
			string conn = $"URI=file:{Application.dataPath}/{spaceDBName}";
			IDbConnection dbconn;
			dbconn = (IDbConnection)new SqliteConnection(conn);
			dbconn.Open();
			dbcmd = dbconn.CreateCommand();

			stars = ReadStar();
			Debug.Log(JsonConvert.SerializeObject(stars));
			reader.Close();
			dbcmd.Dispose();
			dbconn.Close();
		}

		private static Star[] ReadStar()
		{
			List<Star> result = new List<Star>();
			string sqlQuery = "SELECT id, type, name, probability, prefab_system_map,"
				+ $"prefab_galaxy_map FROM {starTableName}";
			dbcmd.CommandText = sqlQuery;
			reader = dbcmd.ExecuteReader();

			while (reader.Read())
			{
				Star star;
				star.id = reader.GetInt32(0);
				star.type = reader.GetInt32(1);
				star.name = reader.GetString(2);
				star.probability = reader.GetInt32(3);
				star.prefabSystemMap = reader.GetString(4);
				star.prefabGalaxyMap = reader.GetString(5);
				result.Add(star);
			}
			return result.ToArray();
		}
	}
}
