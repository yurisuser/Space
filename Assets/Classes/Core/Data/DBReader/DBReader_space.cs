using System.Collections.Generic;

public partial struct Data
{
	public partial struct DBReader
	{
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
