using System.IO;
using Newtonsoft.Json;

public static partial class Data
{
	public static ShipRapam[] ships;
	public static void Init()
	{
		GetShipsExternData();
	}
	private static void GetShipsExternData()
	{
		var json = File.ReadAllText(Settings.ExternData.SHIP_DATA_FILE);
		ships = JsonConvert.DeserializeObject<ShipRapam[]>(json);
	}
}
