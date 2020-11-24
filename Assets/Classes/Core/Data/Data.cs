﻿using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using static Data.GalaxyParam;
using static Settings.ExternData;

public partial struct Data
{
	public static ShipRapam[] ships;
	public static Goods[] goods;
	public static StarChance[] starChance;
	public static PlanetChance[] planetChance;
	public static StarToPlanetResources[] starToPlanetResources;
	public static void Init()
	{
		ships = ReadSimple<ShipRapam>(SHIP_DATA_FILE);
		goods = ReadSimple<Goods>(GOODS_DATA_FILE);
		starChance = ReadSimple<StarChance>(STAR_CHANSE_FILE);
		planetChance = ReadSimple<PlanetChance>(PLANET_CHANSE_FILE);
		ReadTable<StarToPlanetResources>(STAR_TO_PLANET_RESOURCES_FILE);

	}

	private static T[] ReadSimple<T>(string fileName)
	{
		T[] result;
		string json;
		try
		{
			json = File.ReadAllText(fileName);
			//Debug.Log(fileName + json);
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

	private static void ReadTable<T>(string fileName)
	{
		System.Object result;
		string json;
		try
		{
			json = File.ReadAllText(fileName);
			Debug.Log(fileName + json);
		}
		catch (System.Exception err)
		{
			Debug.LogError($"Cant read {fileName} : {err.Message}");
			throw;
		}
		result = JsonConvert.DeserializeObject(json);
		Debug.Log(fileName + JsonConvert.SerializeObject(result));
		//return result;
	}
}
