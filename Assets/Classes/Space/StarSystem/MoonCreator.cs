using System.Collections.Generic;
using UnityEngine;

public static class MoonCreator
{
	private static readonly int moonsCount = 10;
	public static Moon[] CreateMoonsArray(Planet planet)
	{
		Moon[] arr = new Moon[moonsCount];
		for (int i = 0; i < moonsCount; i++)
		{
			arr[i] = CreateMoon(i, planet);
		}
		return arr;
	}

	private static Moon CreateMoon(int numberMoon, Planet planet)
	{
		Moon moon = new Moon
		{
			id = Galaxy.GetNextId(),
			motherPlanet = planet,
			angleOnOrbit = 15 * numberMoon,
			name = planet.name + numberMoon,
			orbitNumber = numberMoon,
			orbitSpeed = 50,
			rotateSpeed = 5,
			mass = Rnd.Next(.05f, planet.mass / 2),
			type = GetMoonType(planet.type),
			parent = planet,
			subStarType = ESubStarType.moon
		};
		moon.resourcer = GetResourcer(moon);
		moon.controlCentre = new Hub(moon);
		return moon;
	}

	private static int GetMoonType(int planetType)
	{
		int[] probabilityesForPlanetType = new int[Data.moonsArr.Length];
		int[] rangeProbabilityes = new int[probabilityesForPlanetType.Length];

		for (int i = 0; i < probabilityesForPlanetType.Length; i++)
		{
			probabilityesForPlanetType[i] = Data.moonOfPlanetProbabilityArr[i].probability[planetType];
		}

		rangeProbabilityes[0] = probabilityesForPlanetType[0];
		for (int i = 1; i < probabilityesForPlanetType.Length; i++)
		{
			rangeProbabilityes[i] = rangeProbabilityes[i - 1] + probabilityesForPlanetType[i];
		}

		int type = 0;
		int rnd = Rnd.Next(0, rangeProbabilityes[rangeProbabilityes.Length - 1]);

		for (int i = 0; i < rangeProbabilityes.Length; i++)
		{
			if (rnd <= rangeProbabilityes[i])
			{
				type = Data.moonsArr[i].id;
				break;
			}
		}
		return type;
	}

	private static Resourcer GetResourcer(Moon moon)
	{
		var result = new ResourceDeposit[Rnd.Next(0, (int)(moon.mass * 10))];
		List<int> idsRes = new List<int>();
		List<int> probabRes = new List<int>();
		for (int i = 0; i < Data.planetaryResourcesProbabilityArr.Length; i++)
		{
			if (Data.planetaryResourcesProbabilityArr[i].probabilityOfPlanet[moon.type] == 0) continue;
			idsRes.Add(Data.planetaryResourcesProbabilityArr[i].resurceId);
			probabRes.Add(Data.planetaryResourcesProbabilityArr[i].probabilityOfPlanet[moon.type]);
		}

		int[] rangeProbab = new int[probabRes.Count];
		rangeProbab[0] = probabRes[0];

		for (int i = 1; i < rangeProbab.Length; i++)
		{
			rangeProbab[i] = rangeProbab[i - 1] + probabRes[i];
		}

		for (int i = 0; i < result.Length; i++)
		{
			int rnd = Rnd.Next(0, rangeProbab[rangeProbab.Length - 1]);
			for (int j = 0; j < rangeProbab.Length; j++)
			{
				if (rnd < rangeProbab[j])
				{
					ResourceDeposit res = new ResourceDeposit
					{
						idResource = idsRes[j],
						extraction = Rnd.Next(10, 50),
					};
					result[i] = res;
					break;
				}
			}
		}
		return new Resourcer(result, moon);
	}
}
