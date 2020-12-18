﻿using System.Collections.Generic;
using UnityEngine;

public static class PrefabService
{
	private static readonly string path = "Prefabs/";
	private static readonly string pathStarsGalaxyMap = "Prefabs/StarsGalaxyMap/";
	private static readonly string pathStarsSystemMap = "Prefabs/StarsSystemMap/";
	private static readonly string pathPlanetsSystemMap = "Prefabs/PlanetsSystemMap/";

	public static Dictionary<int, GameObject> StarsGalaxyMap = new Dictionary<int, GameObject>();
	public static Dictionary<int, GameObject> StarsSystemMap = new Dictionary<int, GameObject>();
	public static Dictionary<int, GameObject> PlanetSystemMap = new Dictionary<int, GameObject>();
	public static Dictionary<EMoonTypes, GameObject> MoonSystemMap = new Dictionary<EMoonTypes, GameObject>();
	public static UIPrefab UI = new UIPrefab();
	public static Sprite[] goodsImages;
	public class UIPrefab
	{
		private string path = "Prefabs/UI/";
		public GameObject ShowMePanel;
		public GameObject PlanetPanel;
		public GameObject PlanetResourcePreview;

		public UIPrefab()
		{
			ShowMePanel = Resources.Load<GameObject>(path + "ShowMePanel");
			PlanetPanel = Resources.Load<GameObject>(path + "PlanetPanel/PlanetPanel");
			PlanetResourcePreview = Resources.Load<GameObject>(path + "PlanetPanel/ResourcePreview");
		}


	} 
	public static void Init()
	{
		LoadStarsPrefabs();
		LoadPlanetSystemMap();
		LoadMoonSystemMap();
		LoadGoodsSprites();
	}

	private static void LoadStarsPrefabs()
	{
		for (int i = 0; i < Data.starsArr.Length; i++)
		{
			StarsGalaxyMap.Add(i, Resources.Load<GameObject>(pathStarsGalaxyMap + Data.starsArr[i].prefabGalaxyMap));
			StarsSystemMap.Add(i, Resources.Load<GameObject>(pathStarsSystemMap + Data.starsArr[i].prefabSystemMap));
		}
	}
	private static void LoadPlanetSystemMap()
	{
		for (int i = 0; i < Data.starsArr.Length; i++)
		{
			PlanetSystemMap.Add(i, Resources.Load<GameObject>(pathPlanetsSystemMap + Data.planetsArr[i].prefabSystemMap));
		}
	}

	private static void LoadMoonSystemMap()
	{
		MoonSystemMap.Add(EMoonTypes.Ice, Resources.Load<GameObject>(path + "PlanetsSystemMap/pfPlanetIce"));
		MoonSystemMap.Add(EMoonTypes.Lava, Resources.Load<GameObject>(path + "PlanetsSystemMap/pfPlanetLava"));
		MoonSystemMap.Add(EMoonTypes.Rock, Resources.Load<GameObject>(path + "PlanetsSystemMap/pfPlanetRock"));
	}

	private static void LoadGoodsSprites()
	{
		goodsImages = Resources.LoadAll<Sprite>("Textures/goods64x64");
	}
}
