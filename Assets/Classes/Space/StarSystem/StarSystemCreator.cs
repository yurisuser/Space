using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using settings = Settings.Galaxy;

public static class StarSystemCreator
{
	private static float oldX;
	private static float oldY;

	public static StarSystem GetRandomStarSystem(int indexStarSystem)
	{
		Star star = CreateStar(indexStarSystem);
		return new StarSystem
		{
			id = indexStarSystem,
			indexSystem = indexStarSystem,
			star = star,
			position = GetStarSistemPosition(indexStarSystem),
			planetSystemsArray = CreatePlanetSystems(star),
			oldX = oldX,
			oldY = oldY,

			StationArr = StationCreator.CreateTestStation(Settings.TEST.TEST_STATIONS_IN_SYSTEM)
		};
	}

	private static Star CreateStar(int index)
	{
		int[] probabilitysStarsArr = new int[Data.starsArr.Length];
		probabilitysStarsArr[0] = Data.starsArr[0].probability;
		for (int i = 1; i < Data.starsArr.Length; i++)
		{
			probabilitysStarsArr[i] = probabilitysStarsArr[i - 1] + Data.starsArr[i].probability;
		}
		int rnd = Rnd.Next(0, probabilitysStarsArr[probabilitysStarsArr.Length - 1]);
		int id = Galaxy.GetNextId();
		string starClass = null;
		int numInDataStarArr = 0;
		for (int i = 0; i < probabilitysStarsArr.Length; i++)
		{
			if ( rnd <= probabilitysStarsArr[i])
			{
				starClass = Data.starsArr[i].starClass;
				numInDataStarArr = i;
				break;
			}
		}
		string name = "St" + id.ToString();

		if (index == 0)         //Cental black hole
		{
			starClass = Array.Find(Data.starsArr, x => x.starClass == "BH").starClass;
		}

		Star star = new Star(
			id: id, 
			name: name, 
			indexSystem: index,
			starClass: starClass,
			colorName: Data.starsArr[numInDataStarArr].colorName,
			temperature: Rnd.Next(Data.starsArr[numInDataStarArr].temperature_min, Data.starsArr[numInDataStarArr].temperature_max),
			mass: Rnd.Next(Data.starsArr[numInDataStarArr].mass_min, Data.starsArr[numInDataStarArr].mass_max),
			radius: Rnd.Next(Data.starsArr[numInDataStarArr].radius_min, Data.starsArr[numInDataStarArr].radius_max),
			luminosity: Rnd.Next(Data.starsArr[numInDataStarArr].luminosity_min, Data.starsArr[numInDataStarArr].luminosity_max)
			);
		return star;
	}

	private static PlanetSystem[] CreatePlanetSystems(Star star)
	{
		int planetCount = Rnd.Next(2, 10);
		PlanetSystem[] arr = new PlanetSystem[10];
		for (int i = 0; i < 10; i++)
		{
			arr[i] = PlanetSystemCreator.GetRandomPlanetSystem(i, star);
		}
		return arr;
	}

	private static Vector3 GetStarSistemPosition(int indexStar)
	{
		Vector3 coord = GenerateStarsNoGausssianDistr();
		float k = 1;
		if (indexStar == 0)
		{
			return new Vector3(0, 0, settings.GALAXY_STAR_LAYER);
		}
		for (int i = 0; i < indexStar; i++)
		{
			k = i == 0 ? settings.CENTRAL_BLACK_HOLE_INTERVAL_K : 1;
			if (Vector3.Distance(coord, Galaxy.StarSystemsArr[i].position) < (settings.MIN_STAR_INTERVAL * k))
			{
				coord = GetStarSistemPosition(indexStar);
			}
		}
		return coord;
	}

	private static Vector3 GenerateStarsNoGausssianDistr()
	{
		float x = Rnd.Next(-1f, 1f);
		if (x == 0) x = 0.0001f;
		float y = Rnd.Next(-settings.GALAXY_RADIUS, settings.GALAXY_RADIUS);
		x = (float)Math.Pow(x, settings.DENSITY_ARMS) 
			* settings.GALAXY_RADIUS 
			+ Rnd.Next(-settings.WIDTH_ARMS, settings.WIDTH_ARMS);
		x *= Rnd.Next(0f, 1f) > .5 ? 1 : -1;
		float z = settings.GALAXY_STAR_LAYER;
		var rrr = new Vector3(x, y, z);
		oldX = x;
		oldY = y;
		return TwistCoordinates(rrr);
	}

	private static Vector3 TwistCoordinates(Vector3 vec)
	{
		float angle = Mathf.Atan(vec.y / vec.x);
		float radius = Mathf.Sqrt(vec.x * vec.x + vec.y * vec.y);
		radius *= (vec.x - vec.y) < 0 ? -1 : 1;
		float normalizedRadius = Math.Abs(radius) / settings.GALAXY_RADIUS;
		float newAngle = angle + normalizedRadius * normalizedRadius * 4f;
		var _x = (float)(Mathf.Cos(newAngle) * radius);
		var _y = (float)(Mathf.Sin(newAngle) * radius);
		return new Vector3(_x, _y, settings.GALAXY_STAR_LAYER);
	}


}

