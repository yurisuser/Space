using System.IO;
using Newtonsoft.Json;

public static partial class Data
{
	public static ShipRapam[] ships;
	public static Goods[] goods;
	public static void Init()
	{
		GetShipsExternData();
		GetGoodsExternData();
	}
	private static void GetShipsExternData()
	{
		var json = File.ReadAllText(Settings.ExternData.SHIP_DATA_FILE);
		ships = JsonConvert.DeserializeObject<ShipRapam[]>(json);
	}

	private static void GetGoodsExternData()
	{
		var json = File.ReadAllText(Settings.ExternData.GOODS_DATA_FILE);
		goods = JsonConvert.DeserializeObject<Goods[]>(json);
	}
}
