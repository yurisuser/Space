﻿using UnityEngine;

public class SceneStateStarSystem : SceneState
{
	private readonly string folderSystemObjectName = "StarSystemObjects";
	private readonly string folderShipName = "Ships";
	private readonly string folderStationName = "Stations";
	private readonly string folderOrbits = "Orbits";
	private StarSystem starSystem;
	private int StarSystemIndexInArr;
	private Camera cam;

	public SceneStateStarSystem(StarSystem starSystem)
	{
		this.starSystem = starSystem;
	}
	public override void DrawScene()
	{
		cam = GameObject.Find("Camera").GetComponent<Camera>();
		cam.orthographicSize = Settings.CameraStarSystem.MAX_ZOOM;
		new GameObject { name = folderOrbits };
		DrawStar();
		DrawPlanetsSystem();
		DrawShips();
		DrawStations();
	}

	private void DrawStar()
	{
		GameObject folder = new GameObject { name = folderSystemObjectName };
		GameObject star = GameObject.Instantiate(
			PrefabService.StarsSystemMap[starSystem.star.type],
			new Vector3(0, 0, Settings.StarSystem.SYSTEM_SHIPS_LAYER),
			Quaternion.identity);
		star.transform.SetParent(folder.transform);
	}

	private void DrawPlanetsSystem()
	{
		DrawPlanets();
		DrawPlanetOrbit();	
	}

	private void DrawPlanets()
	{

		GameObject folder = GameObject.Find(folderSystemObjectName);
		for (int i = 0; i < starSystem.planetSystemsArray.Length; i++)
		{
			Planet planet = starSystem.planetSystemsArray[i].planet;
			GameObject go = GameObject.Instantiate(
				PrefabService.PlanetSystemMap[planet.type],
				planet.position,
				Quaternion.identity);
			go.transform.localScale += new Vector3(
				planet.size * Settings.StarSystem.PLANET_SCALE,
				planet.size * Settings.StarSystem.PLANET_SCALE,
				planet.size * Settings.StarSystem.PLANET_SCALE);
			go.transform.SetParent(folder.transform);
			go.GetComponent<PlanetSysMap>().planet = starSystem.planetSystemsArray[i].planet;
			DrawMoons(starSystem.planetSystemsArray[i]);
			DrawMoonsOrbit(starSystem.planetSystemsArray[i]);
		}
	}

	private void DrawPlanetOrbit()
	{
		GameObject folder = GameObject.Find(folderOrbits);
		float PointStep = (Mathf.PI * 2 / Settings.LineOrbit.CIRCLES_POINS);
		float ROrbit;
		for (int pl = 0; pl < starSystem.planetSystemsArray.Length; pl++)
		{
			Planet planet = starSystem.planetSystemsArray[pl].planet;
			ROrbit = (planet.orbitNumber + Settings.LineOrbit.EMPTY_PLANET_ORBITS_COUNT) * Settings.LineOrbit.ORBITS_PLANET_SIZE;
			Vector3[] vec3arr = new Vector3[Settings.LineOrbit.CIRCLES_POINS + 1];
			GameObject lineOrbit = new GameObject();
			for (int i = 0; i < Settings.LineOrbit.CIRCLES_POINS; i++)
			{
				vec3arr[i] = new Vector3((int)ROrbit * Mathf.Cos(i * PointStep), (int)ROrbit * Mathf.Sin(i * PointStep), 0);
			}
			vec3arr[vec3arr.Length - 1] = vec3arr[0];
			lineOrbit.name = "orbit planet" + planet.name;
			lineOrbit.AddComponent<ShowOrbitScript>();
			lineOrbit.AddComponent<LineRenderer>();
			LineRenderer lineRender = lineOrbit.GetComponent<LineRenderer>();
			lineRender.positionCount = Settings.LineOrbit.CIRCLES_POINS + 1;
			lineRender.SetPositions(vec3arr);
			lineRender.startColor = lineRender.endColor = Settings.LineOrbit.COLOR;
			lineRender.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
			lineRender.transform.SetParent(folder.transform);
		}
	}

	private void DrawMoons(PlanetSystem planetSystem)
	{
		GameObject folder = GameObject.Find(folderSystemObjectName);
		GameObject obj;
		Moon moon;
		for (int i = 0; i < planetSystem.moonsArray.Length; i++)
		{
			moon = planetSystem.moonsArray[i];
			Vector3 moonposition = planetSystem.moonsArray[i].position;
			obj = Object.Instantiate(PrefabService.MoonSystemMap[(EMoonTypes)moon.type], moonposition, Quaternion.identity);
			obj.name = planetSystem.planet.name + " " + (i + 1).ToString();
			obj.transform.localScale += new Vector3(
				moon.size * Settings.StarSystem.MOON_SCALE, 
				moon.size * Settings.StarSystem.MOON_SCALE, 
				moon.size * Settings.StarSystem.MOON_SCALE);
			obj.transform.SetParent(folder.transform);
		}
	}

	private void DrawMoonsOrbit(PlanetSystem planetSystem)
	{
		GameObject folder = GameObject.Find(folderOrbits);
		float PointStep = (Mathf.PI * 2 / Settings.LineOrbit.CIRCLES_POINS);
		float ROrbit;
		for (int m = 0; m < planetSystem.moonsArray.Length; m++)
		{
			Moon moon = planetSystem.moonsArray[m];
			ROrbit = (moon.orbitNumber + Settings.LineOrbit.EMPTY_PLANET_ORBITS_COUNT) * Settings.LineOrbit.ORBIT_MOON_SIZE;
			Vector3[] vec3arr = new Vector3[Settings.LineOrbit.CIRCLES_POINS + 1];
			GameObject lineOrbit = new GameObject();
			for (int i = 0; i < Settings.LineOrbit.CIRCLES_POINS; i++)
			{
				vec3arr[i] = new Vector3((int)ROrbit * Mathf.Cos(i * PointStep), (int)ROrbit * Mathf.Sin(i * PointStep), 0) 
					+ planetSystem.planet.position;
			}
			vec3arr[vec3arr.Length - 1] = vec3arr[0];
			lineOrbit.name = "orbit moon" + moon.name;
			lineOrbit.AddComponent<ShowOrbitScript>();
			lineOrbit.AddComponent<LineRenderer>();
			LineRenderer lineRender = lineOrbit.GetComponent<LineRenderer>();
			lineRender.positionCount = Settings.LineOrbit.CIRCLES_POINS + 1;
			lineRender.SetPositions(vec3arr);
			lineRender.startColor = lineRender.endColor = Settings.LineOrbit.COLOR;
			lineRender.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
			lineRender.transform.SetParent(folder.transform);
		}
	}

	private void DrawShips()
	{
		if (starSystem.ShipsArr.Length < 1) return;
		GameObject folder = new GameObject { name = folderShipName };
		for (int i = 0; i < starSystem.ShipsArr.Length; i++)
		{
			GameObject go = GameObject.Instantiate(
				Resources.Load("Prefabs/Ships/TestShip") as GameObject,
				starSystem.ShipsArr[i].position,
				Quaternion.identity);
			go.transform.SetParent(folder.transform);
			go.GetComponent<ShipScr>().SetShip(starSystem.ShipsArr[i]);
		}
	}

	private void DrawStations()
	{
		if (starSystem.StationArr.Length < 1) return;
		GameObject folder = new GameObject { name = folderStationName };
		for (int i = 0; i < starSystem.StationArr.Length; i++)
		{
			GameObject go = GameObject.Instantiate(
				Resources.Load("Prefabs/Stations/TestStation") as GameObject,
				starSystem.StationArr[i].position,
				Quaternion.identity);
			go.transform.SetParent(folder.transform);
			go.GetComponent<StationSysMapScr>().SetIndexes(StarSystemIndexInArr, i);
		}
	}
}
