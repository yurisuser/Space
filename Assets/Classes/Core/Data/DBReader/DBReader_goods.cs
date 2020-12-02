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
				goods.origin = reader.GetInt32(2);
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
				prob.resurceId = reader.GetInt32(1);
				prob.probabilityOfPlanet = new int[columnCount - skipField];
				for (int i = skipField; i < columnCount; i++)
				{
					prob.probabilityOfPlanet[i - skipField] = reader.GetInt32(i);
				}
				result.Add(prob);
			}
			reader.Close();
			return result.ToArray();

		}

		private static ProductRecipe[] ReadProductRecipes(string tableName)
		{
			int[] prodRange = {4, 13};
			int[] srcRange = {14, 23 };
			List<ProductRecipe> result = new List<ProductRecipe>();

			string sqlQuery = "SELECT id, name, description, duration, product_1, product_1_count, product_2, product_2_count, " +
				$" product_3, product_3_count, product_4, product_4_count, product_5, product_5_count, " +
				$"source_1, source_1_count, source_2, source_2_count, source_3, source_3_count, source_4, source_4_count," +
				$"source_5, source_5_count FROM {tableName}";

			dbcmd.CommandText = sqlQuery;
			reader = dbcmd.ExecuteReader();

			while (reader.Read())
			{
				ProductRecipe recipe;
				recipe.id = reader.GetInt32(0);
				recipe.name = reader.GetString(1);
				recipe.description = reader.GetString(2);
				recipe.duration = reader.GetInt32(3);
				List<GoodsStack> prod = new List<GoodsStack>();
				List<GoodsStack> src = new List<GoodsStack>();
				for (int i = prodRange[0]; i < prodRange[1]; i += 2)
				{
					if (!reader.IsDBNull(i) && !reader.IsDBNull(i + 1))
					{
						int g = reader.GetInt32(i);
						int c = reader.GetInt32(i + 1);
						prod.Add(new GoodsStack()
						{
							id = g,
							quantity = c
						});
					}

				}

				for (int i = srcRange[0]; i < srcRange[1]; i += 2)
				{
					if (!reader.IsDBNull(i) && !reader.IsDBNull(i + 1))
					{
						int g = reader.GetInt32(i);
						int c = reader.GetInt32(i + 1);
						src.Add(new GoodsStack()
						{
							id = g,
							quantity = c
						});
					}
				}
				recipe.production = prod.ToArray();
				recipe.resources = src.ToArray();
				result.Add(recipe);
			}
			return result.ToArray();
		}
	}
}
