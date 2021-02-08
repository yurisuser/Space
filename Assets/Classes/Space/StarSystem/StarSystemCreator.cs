using System;
using UnityEngine;
using settings = Settings.Galaxy;

public static class StarSystemCreator
{
	private static float oldX;
	private static float oldY;

	public static StarSystem GetRandomStarSystem(int indexStarSystem)
	{
		Star star = StarCreator.CreateStar(indexStarSystem);
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

	private static PlanetSystem[] CreatePlanetSystems(Star star)
	{
		int planetCount = GetPlanetsAmaunt(star);
		if (planetCount == 0) return null;
		PlanetSystem[] arr = new PlanetSystem[planetCount];
		for (int i = 0; i < planetCount; i++)
		{
			arr[i] = PlanetSystemCreator.GetRandomPlanetSystem(i, star);
		}
		return arr;
	}

	private static int GetPlanetsAmaunt(Star star)
	{
		return star.subStarClass;
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

