using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using static Settings.ExternData;

public partial struct Data
{
	public static Star[] stars;
	public static Planet[] planets;
	public static PlanetOfStarProbability[] planetsOfStarProbability;
	public static ShipRapam[] ships;
	public static Goods[] goods;
	public static PlanetaryResourcesProbability[] planetaryResourcesProbability;
	public static void Init()
	{
		DBReader.Read();
		ships = ReadSimple<ShipRapam>(SHIP_DATA_FILE);
	}

	private static T[] ReadSimple<T>(string fileName)
	{
		T[] result;
		string json;
		try
		{
			json = File.ReadAllText(fileName);
		}
		catch (System.Exception err)
		{
			Debug.LogError($"Cant read {fileName} : {err.Message}");
			throw;
		}
		result = JsonConvert.DeserializeObject<T[]>(json);
		//Debug.Log(fileName + JsonConvert.SerializeObject(result));
		return result;
	}
}
