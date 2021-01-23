using System.Collections.Generic;
using UnityEngine;

public static class PlanetCreator
{
	public static Planet CreatePlanet(int numberPlanet, Star motherStar)
	{
		Planet planet = new Planet
		{
			id = Galaxy.GetNextId(),
			motherStar = motherStar,
			type = GetPlanetType(motherStar.type),
			name = motherStar.name + " " + (char)(numberPlanet + 65),
			rotateSpeed = 10,
			orbitSpeed = 100,
			mass = 1,
			orbitNumber = numberPlanet,
			angleOnOrbit = 15 * numberPlanet
		};
		planet.resourcer = GetResourcer(planet);
		planet.controlCentre = new Hub(planet);
		return planet;
	}

	private static int GetPlanetType(int starType)
	{
		int[] probabilityesForStarType = new int[Data.planetsArr.Length];
		int[] rangeProbabilityes = new int[probabilityesForStarType.Length];
		for (int i = 0; i < probabilityesForStarType.Length; i++)
		{
			probabilityesForStarType[i] = Data.planetsOfStarProbabilityArr[i].probability[starType];
		}
		rangeProbabilityes[0] = probabilityesForStarType[0];
		for (int i = 1; i < probabilityesForStarType.Length; i++)
		{
			rangeProbabilityes[i] = rangeProbabilityes[i - 1] + probabilityesForStarType[i];
		}
		int type = 0;
		int rnd = Rnd.Next(0, rangeProbabilityes[rangeProbabilityes.Length - 1]);
		for (int i = 0; i < rangeProbabilityes.Length; i++)
		{
			if (rnd <= rangeProbabilityes[i])
			{
				type = i;
				break;
			}
		}
		return type;
	}

	private static Resourcer GetResourcer(Planet planet)
	{
		var result = new ResourceDeposit[Rnd.Next(0, (int)(planet.mass * 10))];
		List<int> idsRes = new List<int>();
		List<int> probabRes = new List<int>();
		for (int i = 0; i < Data.planetaryResourcesProbabilityArr.Length; i++)
		{
			if (Data.planetaryResourcesProbabilityArr[i].probabilityOfPlanet[planet.type] == 0) continue;
			idsRes.Add(Data.planetaryResourcesProbabilityArr[i].resurceId);
			probabRes.Add(Data.planetaryResourcesProbabilityArr[i].probabilityOfPlanet[planet.type]);
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
		return new Resourcer(result, planet);
	}
}
