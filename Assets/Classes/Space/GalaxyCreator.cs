using System;
using System.Collections.Generic;
using System.Linq;
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
		Galaxy.DistancesSortedNear = SortDistanceArr();
		Galaxy.HyperTunnels = SpaceNetworkCreator.Create();
		AddNameSpaceObjects();
		AddShips();
	}

	private static void AddShips()
	{
		for (int i = 0; i < Galaxy.StarSystemsArr.Length; i++)
		{
			Galaxy.StarSystemsArr[i].shipsList = new List<Ship>(ShipsCreator.CreateRandomShips(Settings.TEST.TEST_SHIPS_IN_SYSTEM, i));
		}
	}

	private static StarDistance[][] CalculateStarsDistances()
	{
		StarDistance[][] result = new StarDistance[settings.STARS_AMMOUNT][];
		for (int i = 0; i < settings.STARS_AMMOUNT; i++)
		{
			List<StarDistance> subList = new List<StarDistance>();
			for (int u = 0; u < settings.STARS_AMMOUNT; u++)
			{
				subList.Add(new StarDistance
				{
					index = u,
					distance = Vector3.Distance(Galaxy.StarSystemsArr[i].position, Galaxy.StarSystemsArr[u].position),
					direction = (Galaxy.StarSystemsArr[u].position - Galaxy.StarSystemsArr[i].position).normalized
				});
			}
			result[i] = subList.ToArray();
		}
		return result;
	}

	private static StarDistance[][] SortDistanceArr()
	{
		var list = new List<StarDistance[]>();
		for (int i = 0; i < Galaxy.Distances.Length; i++)
		{
			var subList = new StarDistance[Galaxy.Distances[i].Length];
			for (int j = 0; j < subList.Length; j++)
			{
				subList[j] = Galaxy.Distances[i][j];
			}

			Array.Sort(subList, (x, y) =>
			{
				return x.distance >= y.distance ? 1 : -1;
			});
			list.Add(subList);
		}

		return list.ToArray();
	}

	private static void AddNameSpaceObjects()
	{
		string[] constellNameArr = Data.constellationNames.OrderBy(x => Rnd.NextTrue()).ToArray();
		int[][] sectorIds = SpaceNetworkCreator.GetSectorIdsStar();
		for (int s = 0; s < sectorIds.Length; s++)
		{
			for (int i = 0; i < sectorIds[s].Length; i++)
			{
				int indexStar = sectorIds[s][i];
				Galaxy.StarSystemsArr[indexStar].star.name = $"{Data.greekLetters[i]} {constellNameArr[s]}";
				if (Galaxy.StarSystemsArr[indexStar].planetSystemsArray == null) return;
				for (int ps = 0; ps < Galaxy.StarSystemsArr[indexStar].planetSystemsArray.Length; ps++)
				{
					Galaxy.StarSystemsArr[indexStar].planetSystemsArray[ps].planet.name = $"{Data.greekLetters[i]} {constellNameArr[s]} {Data.smallLetters[ps + 1]}";
					if (Galaxy.StarSystemsArr[indexStar].planetSystemsArray[ps].moonsArray == null) return;
					for (int m = 0; m < Galaxy.StarSystemsArr[indexStar].planetSystemsArray[ps].moonsArray.Length; m++)
					{
						Galaxy.StarSystemsArr[indexStar].planetSystemsArray[ps].moonsArray[m].name = $"{Data.greekLetters[i]} {constellNameArr[s]} {Data.smallLetters[ps + 1]} {Data.romeIntegers[m]}";
					}
				}
			}
		}
	}
}
