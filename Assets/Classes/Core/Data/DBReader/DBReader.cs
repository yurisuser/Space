using System;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

public partial struct Data
{
	public partial struct DBReader
	{
		private static readonly string spaceDBName = "space.db";
		private static readonly string starTableName = "star";
		private static readonly string planetTableName = "planet";
		private static readonly string planetOfStarProbabilityTableName = "planet_of_star_probability";
		private static readonly string goodsTableName = "goods";

		private static IDbCommand dbcmd;
		private static IDataReader reader;
		public static void Read()
		{
			string conn = $"URI=file:{Application.dataPath}/{spaceDBName}";
			IDbConnection dbconn;
			dbconn = (IDbConnection)new SqliteConnection(conn);
			dbconn.Open();
			dbcmd = dbconn.CreateCommand();

			stars = ReadStar(starTableName);
			planets = ReadPlanet(planetTableName);
			planetsOfStarProbability = ReadPlanetOfStarProbability(planetOfStarProbabilityTableName);
			goods = ReadGoods(goodsTableName);

			reader.Close();
			dbcmd.Dispose();
			dbconn.Close();
		}

		private static int GetTableRows(string tableName)
		{
			string sqlQuery = $"SELECT COUNT(*) FROM {tableName}";
			dbcmd.CommandText = sqlQuery;
			dbcmd.CommandType = CommandType.Text;
			int resultRows = 0;
			resultRows = Convert.ToInt32(dbcmd.ExecuteScalar());
			reader = dbcmd.ExecuteReader();
			reader.Close();
			return resultRows;
		}

		private static int GetTableColumn(string tableName)
		{
			dbcmd.CommandText = $"SELECT * FROM {tableName}";
			reader = dbcmd.ExecuteReader();
			int resultColumn = reader.FieldCount;
			reader.Close();
			return resultColumn;
		}

		private static Star[] ReadStar(string tableName)
		{
			List<Star> result = new List<Star>();
			string sqlQuery = $"SELECT * FROM {tableName}";
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
			reader.Close();
			return result.ToArray();
		}

		private static Planet[] ReadPlanet(string tableName)
		{
			List<Planet> result = new List<Planet>();
			string sqlQuery = $"SELECT * FROM {tableName}";
			dbcmd.CommandText = sqlQuery;
			reader = dbcmd.ExecuteReader();

			while (reader.Read())
			{
				Planet planet;
				planet.id = reader.GetInt32(0);
				planet.type = reader.GetString(1);
				planet.prefabSystemMap = reader.GetString(2);
				result.Add(planet);
			}
			reader.Close();
			return result.ToArray();
		}

		private static PlanetOfStarProbability[] ReadPlanetOfStarProbability(string tableName)
		{
			List<PlanetOfStarProbability> result = new List<PlanetOfStarProbability>();
			int rowsCount = GetTableRows(tableName);
			int columnCount = GetTableColumn(tableName);
			string sqlQuery = $"SELECT * FROM {tableName}";
			dbcmd.CommandText = sqlQuery;
			reader = dbcmd.ExecuteReader();

			while (reader.Read())
			{
				int skipField = 2;
				PlanetOfStarProbability prob;
				prob.id = reader.GetInt32(0);
				prob.type = reader.GetString(1);
				prob.probabilityOfStar = new int[columnCount - skipField];
				for (int i = skipField; i < columnCount; i++)
				{
					prob.probabilityOfStar[i - skipField] = reader.GetInt32(i);
				}
				result.Add(prob);
			}
			reader.Close();
			return result.ToArray();
		}
	}
}
