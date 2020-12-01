using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

partial struct Data
{
	partial struct DBReader
	{
		private static Goods[] ReadGoods(string tableName)
		{
			List<Goods> result = new List<Goods>();
			string sqlQuery = $"SELECT * FROM {tableName}";
			dbcmd.CommandText = sqlQuery;
			reader = dbcmd.ExecuteReader();

			while (reader.Read())
			{
				Goods goods;
				goods.id = reader.GetInt32(0);
				goods.name = reader.GetString(1);
				goods.origin = reader.GetString(2);
				result.Add(goods);
			}

			reader.Close();
			return result.ToArray();
		}

		private static PlanetaryResourcesProbability[] ReadPlanetaryResourcesProbability(string tableName)
		{
			List<PlanetaryResourcesProbability> result = new List<PlanetaryResourcesProbability>();
			string sqlQuery = $"SELECT * FROM {tableName}";
			int columnCount = GetTableColumn(tableName);
			dbcmd.CommandText = sqlQuery;
			reader = dbcmd.ExecuteReader();

			while (reader.Read())
			{
				int skipField = 2;
				PlanetaryResourcesProbability prob;
				prob.id = reader.GetInt32(0);
				prob.name = reader.GetString(1);
				prob.probabilityOfPlanet = new int[columnCount - skipField];
				for (int i = skipField; i < columnCount; i++)
				{
					prob.probabilityOfPlanet[i - skipField] = reader.GetInt32(i);
				}
				result.Add(prob);
			}
			reader.Close();
			//Debug.Log(JsonConvert.SerializeObject(result));
			return result.ToArray();

		}
	}
}
