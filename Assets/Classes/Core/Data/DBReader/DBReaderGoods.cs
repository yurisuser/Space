using System.Collections.Generic;

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
	}
}
