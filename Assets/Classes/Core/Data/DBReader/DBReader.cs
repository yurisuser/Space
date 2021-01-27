using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

public partial struct Data
{
	public partial struct DBReader
	{
		private static readonly string spaceDBName = "space.db";
		//Space
		private static readonly string starTableName = "stars";
		private static readonly string planetTableName = "planet";
		private static readonly string moonTableName = "moons";
		private static readonly string planetOfStarProbabilityTableName = "planet_of_star_probability";
		private static readonly string planetaryResourcesProbabilityTableName = "planet_resources_probability";
		private static readonly string moonProbability = "moon_of_planet_probability";
		//Goods
		private static readonly string goodsTableName = "goods";
		private static readonly string recipeTableName = "product_recipe";
		private static readonly string miningRecipeTableName = "mining_recipe";
		//Ships
		private static readonly string shipsParamTableName = "ship_param";


		private static IDbCommand dbcmd;
		private static IDataReader reader;
		public static void Read()
		{
			string conn = $"URI=file:{Application.dataPath}/{spaceDBName}";
			IDbConnection dbconn;
			dbconn = (IDbConnection)new SqliteConnection(conn);
			dbconn.Open();
			dbcmd = dbconn.CreateCommand();
			//Space
			starsArr = ReadStar(starTableName);
			planetsArr = ReadPlanetOrMoon(planetTableName);
			moonsArr = ReadPlanetOrMoon(moonTableName);
			planetsOfStarProbabilityArr = ReadProbability(planetOfStarProbabilityTableName);
			planetaryResourcesProbabilityArr = ReadPlanetaryResourcesProbability(planetaryResourcesProbabilityTableName);
			moonOfPlanetProbabilityArr = ReadProbability(moonProbability);
			//Goods
			goodsArr = ReadGoods(goodsTableName);
			productRecipeArr = ReadProductRecipes(recipeTableName);
			miningRecipesArr = ReadMiningRecipes(miningRecipeTableName);
			//Ship
			shipsParamArr = ReadShipsParam(shipsParamTableName);

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

		
	}
}
