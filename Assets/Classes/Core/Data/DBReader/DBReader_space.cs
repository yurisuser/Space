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
				Star star = new Star();
				star.id = reader.GetInt32(0);
				star.starClass = reader.GetString(1);
				star.colorName = reader.GetString(2);
				star.temperature_min = reader.GetInt32(3);
				star.temperature_max = reader.GetInt32(4);
				star.mass_min = reader.GetFloat(5);
				star.mass_max = reader.GetFloat(6);
				star.radius_min = reader.GetFloat(7);
				star.radius_max = reader.GetFloat(8);
				star.luminosity_min = reader.GetFloat(9);
				star.luminosity_max = reader.GetFloat(10);
				star.probability = reader.GetInt32(11);

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

		private static string[] ReadConstellationsNames(string tableName)
		{
			List<string> list = new List<string>();
			string sqlQuery = $"SELECT name FROM {tableName}";
			dbcmd.CommandText = sqlQuery;
			reader = dbcmd.ExecuteReader();
			while (reader.Read())
			{
				string name = reader.GetString(0);
				list.Add(name);
			}
			reader.Close();
			return list.ToArray();
		}
	}
}
