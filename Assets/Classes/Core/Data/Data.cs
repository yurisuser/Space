using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using static Settings.ExternData;

public partial struct Data
{
	public static Star[] starsArr;
	public static Planet[] planetsArr;
	public static Moon[] moonsArr;
	public static Probability[] planetsOfStarProbabilityArr;
	public static Probability[] moonOfPlanetProbabilityArr;
	public static PlanetaryResourcesProbability[] planetaryResourcesProbabilityArr;
	
	public static ShipRapam[] shipsArr;

	public static Goods[] goodsArr;
	public static ProductRecipe[] productRecipeArr;
	public static MiningRecipe[] miningRecipesArr;

	public static void Init()
	{
		DBReader.Read();
		shipsArr = ReadSimple<ShipRapam>(SHIP_DATA_FILE);
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
