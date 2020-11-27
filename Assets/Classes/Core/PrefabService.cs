using System.Collections.Generic;
using UnityEngine;

public static class PrefabService
{
	private static readonly string path = "Prefabs/";
	private static readonly string pathStarsGalaxyMap = "Prefabs/StarsGalaxyMap/";
	private static readonly string pathStarsSystemMap = "Prefabs/StarsSystemMap/";

	public static Dictionary<int, GameObject> StarsGalaxyMap = new Dictionary<int, GameObject>();
	public static Dictionary<int, GameObject> StarsSystemMap = new Dictionary<int, GameObject>();
	public static Dictionary<EPlanetTypes, GameObject> PlanetSystemMap = new Dictionary<EPlanetTypes, GameObject>();
	public static Dictionary<EMoonTypes, GameObject> MoonSystemMap = new Dictionary<EMoonTypes, GameObject>();
	public static UIPrefab UI = new UIPrefab();
	public class UIPrefab
	{
		private string path = "Prefabs/UI/";
		public GameObject ShowMePanel;

		public UIPrefab()
		{
			ShowMePanel = Resources.Load<GameObject>(path + "ShowMePanel");
		}


	} 
	public static void Init()
	{
		LoadStarsPrefabs();
		LoadPlanetSystemMap();
		LoadMoonSystemMap();
	}

	private static void LoadStarsPrefabs()
	{
		for (int i = 0; i < Data.stars.Length; i++)
		{
			StarsGalaxyMap.Add(i, Resources.Load<GameObject>(pathStarsGalaxyMap + Data.stars[i].prefabGalaxyMap));
			StarsSystemMap.Add(i, Resources.Load<GameObject>(pathStarsSystemMap + Data.stars[i].prefabSystemMap));
		}
	}
	private static void LoadPlanetSystemMap()
	{
		PlanetSystemMap.Add(EPlanetTypes.Continental, Resources.Load<GameObject>(path + "PlanetsSystemMap/pfPlanetContinental"));
		PlanetSystemMap.Add(EPlanetTypes.GasGiant, Resources.Load<GameObject>(path + "PlanetsSystemMap/pfPlanetGasGiant"));
		PlanetSystemMap.Add(EPlanetTypes.Ice, Resources.Load<GameObject>(path + "PlanetsSystemMap/pfPlanetIce"));
		PlanetSystemMap.Add(EPlanetTypes.Lava, Resources.Load<GameObject>(path + "PlanetsSystemMap/pfPlanetLava"));
		PlanetSystemMap.Add(EPlanetTypes.Ocean, Resources.Load<GameObject>(path + "PlanetsSystemMap/pfPlanetOcean"));
		PlanetSystemMap.Add(EPlanetTypes.Rock, Resources.Load<GameObject>(path + "PlanetsSystemMap/pfPlanetRock"));
	}

	private static void LoadMoonSystemMap()
	{
		MoonSystemMap.Add(EMoonTypes.Ice, Resources.Load<GameObject>(path + "PlanetsSystemMap/pfPlanetIce"));
		MoonSystemMap.Add(EMoonTypes.Lava, Resources.Load<GameObject>(path + "PlanetsSystemMap/pfPlanetLava"));
		MoonSystemMap.Add(EMoonTypes.Rock, Resources.Load<GameObject>(path + "PlanetsSystemMap/pfPlanetRock"));
	}
}
