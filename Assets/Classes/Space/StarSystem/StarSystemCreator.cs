using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using settings = Settings.Galaxy;

public static class StarSystemCreator
{
	private static float currentGalaxyHandDistance;

	public static StarSystem GetRandomStarSystem(int numberStar)
	{
		Star star = CreateStar();
		return new StarSystem
		{
			id = Galaxy.GetNextId(),
			star = star,
			position = GetStarSistemPosition(numberStar),
			planetSystemsArray = CreatePlanetSystems(star),
			galaxyHandDistance = currentGalaxyHandDistance,

			ShipsArr = ShipsCreator.CreateRandomShips(Settings.TEST.TEST_SHIPS_IN_SYSTEM),
			StationArr = StationCreator.CreateTestStation(Settings.TEST.TEST_STATIONS_IN_SYSTEM)
		};
	}

	private static Star CreateStar()
	{
		int[] probabilitysStarsArr = new int[Data.starsArr.Length];
		probabilitysStarsArr[0] = Data.starsArr[0].probability;
		for (int i = 1; i < Data.starsArr.Length; i++)
		{
			probabilitysStarsArr[i] = probabilitysStarsArr[i - 1] + Data.starsArr[i].probability;
		}
		int rnd = Random.Range(0, probabilitysStarsArr[probabilitysStarsArr.Length - 1]);
		int id = Galaxy.GetNextId();
		int type = 0;
		for (int i = 0; i < probabilitysStarsArr.Length; i++)
		{
			if ( rnd <= probabilitysStarsArr[i])
			{
				type = i;
				break;
			}
		}
		string name = "St" + id.ToString();
		Star star = new Star(id, type, name);
		return star;
	}

	private static PlanetSystem[] CreatePlanetSystems(Star star)
	{
		int planetCount = Random.Range(2, 10);
		PlanetSystem[] arr = new PlanetSystem[10];
		for (int i = 0; i < 10; i++)
		{
			arr[i] = PlanetSystemCreator.GetRandomPlanetSystem(i, star);
		}
		return arr;
	}

	private static Vector3 GetStarSistemPosition(int numberStar)
	{
		Vector3 coord = GenerateStarsNoGausssianDistr();
		for (int i = 0; i < numberStar; i++)
		{
			if (Vector3.Distance(coord, Galaxy.StarSystemsArr[i].position) < settings.MIN_STAR_INTERVAL)
			{
				coord = GetStarSistemPosition(numberStar);
			}
		}
		return coord;
	}

	private static Vector3 GenerateStarsNoGausssianDistr()
	{
		float x = Random.Range(-1f, 1f);
		if (x == 0) x = 0.0001f;
		float y = Random.Range(-settings.GALAXY_RADIUS, settings.GALAXY_RADIUS);
		x = (float)Math.Pow(x, settings.DENSITY_ARMS) 
			* settings.GALAXY_RADIUS 
			+ Random.Range(-settings.WIDTH_ARMS, settings.WIDTH_ARMS);
		x *= Random.Range(0f, 1f) > .5 ? 1 : -1;
		float z = settings.GALAXY_STAR_LAYER;
		var rrr = new Vector3(x, y, z);
		currentGalaxyHandDistance = x;
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

