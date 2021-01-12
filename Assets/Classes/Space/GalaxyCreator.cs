﻿using System;
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
		Galaxy.DistancesSortedNear = SortDistanceArr(Galaxy.Distances);
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

	private static StarDistance[][] SortDistanceArr(StarDistance[][] arr)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			Array.Sort(arr[i], (x, y) => {
				return x.distance >= y.distance ? 1 : -1;
			});
		}
		return arr;
	}
}
