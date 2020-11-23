using System.Collections.Generic;
using UnityEngine;

public static class PrefabService
{
	private static readonly string path = "Prefabs/";
	public static Dictionary<EStarTypes, GameObject> StarsGalaxyMap = new Dictionary<EStarTypes, GameObject>();
	public static Dictionary<EStarTypes, GameObject> StarsSystemMap = new Dictionary<EStarTypes, GameObject>();
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
		LoadStarsGalaxyMap();
		LoadStarSystemMap();
		LoadPlanetSystemMap();
		LoadMoonSystemMap();
	}

	private static void LoadStarsGalaxyMap()
	{
		StarsGalaxyMap.Add(EStarTypes.Black, Resources.Load<GameObject>(path + "StarsGalaxyMap/pfBlackStarGal"));
		StarsGalaxyMap.Add(EStarTypes.Blue, Resources.Load<GameObject>(path + "StarsGalaxyMap/pfBlueStarGal"));
		StarsGalaxyMap.Add(EStarTypes.Neutron, Resources.Load<GameObject>(path + "StarsGalaxyMap/pfNeutronStarGal"));
		StarsGalaxyMap.Add(EStarTypes.Red, Resources.Load<GameObject>(path + "StarsGalaxyMap/pfRedStarGal"));
		StarsGalaxyMap.Add(EStarTypes.White, Resources.Load<GameObject>(path + "StarsGalaxyMap/pfWhiteStarGal"));
		StarsGalaxyMap.Add(EStarTypes.Yello, Resources.Load<GameObject>(path + "StarsGalaxyMap/pfYelloStarGal"));
	}

	private static void LoadStarSystemMap()
	{
		StarsSystemMap.Add(EStarTypes.Black, Resources.Load<GameObject>(path + "StarsSystemMap/pfBlackStarSys"));
		StarsSystemMap.Add(EStarTypes.Blue, Resources.Load<GameObject>(path + "StarsSystemMap/pfBlueStarSys"));
		StarsSystemMap.Add(EStarTypes.Neutron, Resources.Load<GameObject>(path + "StarsSystemMap/pfNeutronStarSys"));
		StarsSystemMap.Add(EStarTypes.Red, Resources.Load<GameObject>(path + "StarsSystemMap/pfRedStarSys"));
		StarsSystemMap.Add(EStarTypes.White, Resources.Load<GameObject>(path + "StarsSystemMap/pfWhiteStarSys"));
		StarsSystemMap.Add(EStarTypes.Yello, Resources.Load<GameObject>(path + "StarsSystemMap/pfYelloStarSys"));
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
