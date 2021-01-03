using System.Collections.Generic;

partial struct Data
{
	partial struct DBReader
	{
		private static Goods[] ReadGoods(string tableName)
		{
			List<Goods> result = new List<Goods>();
			string sqlQuery = $"SELECT id, name, origin, price FROM {tableName}";
			dbcmd.CommandText = sqlQuery;
			reader = dbcmd.ExecuteReader();

			while (reader.Read())
			{
				Goods goods;
				goods.id = reader.GetInt32(0);
				goods.name = reader.GetString(1);
				goods.origin = reader.GetInt32(2);
				goods.price = reader.GetInt32(3);
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
			int[] productionRange = {4, 13};
			int[] resourceRange = {14, 23 };
			List<ProductRecipe> result = new List<ProductRecipe>();

			string sqlQuery = "SELECT id, name, description, duration, product_1, product_1_count, product_2, product_2_count, " +
				$" product_3, product_3_count, product_4, product_4_count, product_5, product_5_count, " +
				$"source_1, source_1_count, source_2, source_2_count, source_3, source_3_count, source_4, source_4_count," +
				$"source_5, source_5_count FROM {tableName}";

			dbcmd.CommandText = sqlQuery;
			reader = dbcmd.ExecuteReader();

			while (reader.Read())
			{


				List<GoodsStack> prod = new List<GoodsStack>();
				List<GoodsStack> resources = new List<GoodsStack>();
				for (int i = productionRange[0]; i < productionRange[1]; i += 2)
				{
					if (!reader.IsDBNull(i) && !reader.IsDBNull(i + 1))
					{
						int goodsId = reader.GetInt32(i);
						int amount = reader.GetInt32(i + 1);
						prod.Add(new GoodsStack()
						{
							id = goodsId,
							amount = amount
						});
					}

				}

				for (int i = resourceRange[0]; i < resourceRange[1]; i += 2)
				{
					if (!reader.IsDBNull(i) && !reader.IsDBNull(i + 1))
					{
						int goodId = reader.GetInt32(i);
						int amount = reader.GetInt32(i + 1);
						resources.Add(new GoodsStack()
						{
							id = goodId,
							amount = amount
						});
					}
				}
				ProductRecipe recipe = new ProductRecipe(
					id: reader.GetInt32(0),
					name: reader.GetString(1),
					description: reader.GetString(2),
					duration: reader.GetInt32(3),
					resources: resources.ToArray(),
					production: prod.ToArray()
				);
				result.Add(recipe);
			}
			reader.Close();
			return result.ToArray();
		}

		private static MiningRecipe[] ReadMiningRecipes(string tableName)
		{
			List<MiningRecipe> result = new List<MiningRecipe>();

			string query = $"SELECT id, name, description, duration, product_1, product_1_count FROM {tableName}";
			dbcmd.CommandText = query;
			reader = dbcmd.ExecuteReader();

			while (reader.Read())
			{
				MiningRecipe recipe = new MiningRecipe(
					id: reader.GetInt32(0),
					name: reader.GetString(1),
					description: reader.GetString(2),
					duration: reader.GetInt32(3),
					resources: new GoodsStack[0],
					production: new GoodsStack[1] { new GoodsStack { id = reader.GetInt32(4), amount = reader.GetInt32(5) } }
					);
				result.Add(recipe);
			}
			reader.Close();
			return result.ToArray();
		}
	}
}
