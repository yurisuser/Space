using System.Collections.Generic;

public partial struct Data
{
	public partial struct DBReader
	{
		private static ShipRapam[] ReadShipsParam(string tableName)
		{
			List<ShipRapam> result = new List<ShipRapam>();
			string sqlQuery = $"SELECT id, model_name, max_speed, base_cargo_size FROM {tableName}";
			dbcmd.CommandText = sqlQuery;
			reader = dbcmd.ExecuteReader();

			while (reader.Read())
			{
				ShipRapam param = new ShipRapam()
				{
					id = reader.GetInt32(0),
					modelName = reader.GetString(1),
					maxSpeed = reader.GetInt32(2),
					baseCargoSize = reader.GetInt32(3)
				};
				result.Add(param);
			}
			reader.Close();
			return result.ToArray();
		}
	}
}
