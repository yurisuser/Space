using System;
using System.Collections.Generic;
using UnityEngine;
using settings = Settings.Galaxy;

public static class GalaxyCreator
{
	public static void CreateGalaxy()
	{
		Galaxy.StarSystemsArr = new StarSystem[settings.STARS_AMMOUNT];
		for (int i = 0; i < settings.STARS_AMMOUNT; i++)
		{
			Galaxy.StarSystemsArr[i] = StarSystemCreator.GetRandomStarSystem(i);
		}
		Galaxy.Distances = CalculateStarsDistances();
		AddShips();
	}

	private static void AddShips()
	{
		//for (int i = 0; i < Galaxy.StarSystemsArr.Length; i++)
		//{
			Galaxy.StarSystemsArr[11].shipsList = new List<Ship>(ShipsCreator.CreateRandomShips(Settings.TEST.TEST_SHIPS_IN_SYSTEM, 11));
		//}
	}

	private static StarDistance[][] CalculateStarsDistances()
	{
		StarDistance[][] result = new StarDistance[settings.STARS_AMMOUNT][];
		for (int i = 0; i < settings.STARS_AMMOUNT; i++)
		{
			var subArr = new StarDistance[settings.STARS_AMMOUNT];
			for (int u = 0; u < settings.STARS_AMMOUNT; u++)
			{
				subArr[u] = new StarDistance
				{
					id = u,
					distance = Vector3.Distance(Galaxy.StarSystemsArr[i].position, Galaxy.StarSystemsArr[u].position),
					direction = (Galaxy.StarSystemsArr[i].position - Galaxy.StarSystemsArr[u].position).normalized
				};
			}
			Array.Sort(subArr, (x, y) => {
				return x.distance >= y.distance ? 1 : -1;
				});
			result[i] = subArr;
		}
		return result;
	}
}
