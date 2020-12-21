using System;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using Newtonsoft.Json;
using UnityEngine;

public partial struct Data
{
	public partial struct DBReader
	{
		private static readonly string spaceDBName = "space.db";
		private static readonly string starTableName = "star";
		private static readonly string planetTableName = "planet";
		private static readonly string moonTableName = "moons";

		private static readonly string planetOfStarProbabilityTableName = "planet_of_star_probability";
		private static readonly string planetaryResourcesProbabilityTableName = "planet_resources_probability";
		private static readonly string moonProbability = "moon_of_planet_probability";

		private static readonly string goodsTableName = "goods";
		private static readonly string recipeTableName = "product_recipe";
		private static readonly string miningRecipeTableName = "mining_recipe";


		private static IDbCommand dbcmd;
		private static IDataReader reader;
		public static void Read()
		{
			string conn = $"URI=file:{Application.dataPath}/{spaceDBName}";
			IDbConnection dbconn;
			dbconn = (IDbConnection)new SqliteConnection(conn);
			dbconn.Open();
			dbcmd = dbconn.CreateCommand();

			starsArr = ReadStar(starTableName);
			planetsArr = ReadPlanetOrMoon(planetTableName);
			moonsArr = ReadPlanetOrMoon(moonTableName);

			planetsOfStarProbabilityArr = ReadProbability(planetOfStarProbabilityTableName);
			planetaryResourcesProbabilityArr = ReadPlanetaryResourcesProbability(planetaryResourcesProbabilityTableName);
			moonOfPlanetProbabilityArr = ReadProbability(moonProbability);

			goodsArr = ReadGoods(goodsTableName);
			productRecipeArr = ReadProductRecipes(recipeTableName);
			miningRecipesArr = ReadMiningRecipes(miningRecipeTableName);

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

		private static Moon[] ReadPlanetOrMoon(string tableName)
		{
			List<Moon> result = new List<Moon>();
			string sqlQuery = $"SELECT id, name, prefab_system_map FROM {tableName}";
			dbcmd.CommandText = sqlQuery;
			reader = dbcmd.ExecuteReader();

			while (reader.Read())
			{
				Moon planet = new Moon();
				planet.id = reader.GetInt32(0);
				planet.name = reader.GetString(1);
				planet.prefabSystemMap = reader.GetString(2);
				result.Add(planet);
			}
			reader.Close();
			return result.ToArray();
		}

		private static Probability[] ReadProbability(string tableName)
		{
			List<Probability> result = new List<Probability>();
			int rowsCount = GetTableRows(tableName);
			int columnCount = GetTableColumn(tableName);
			string sqlQuery = $"SELECT * FROM {tableName}";
			dbcmd.CommandText = sqlQuery;
			reader = dbcmd.ExecuteReader();

			while (reader.Read())
			{
				int skipField = 2;
				Probability prob = new Probability();
				prob.id = reader.GetInt32(0);
				prob.type = reader.GetInt32(1);
				prob.probability = new int[columnCount - skipField];
				for (int i = skipField; i < columnCount; i++)
				{
					prob.probability[i - skipField] = reader.GetInt32(i);
				}
				result.Add(prob);
			}
			reader.Close();
			return result.ToArray();
		}
	}
}
