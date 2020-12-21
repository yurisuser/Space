using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			mass = Random.Range(.05f, planet.mass / 2),
			type = GetTypeMoon(),
		};
		moon.resources = GetResourcePoints(moon);
		return moon;
	}

	private static int GetTypeMoon()
	{
		return Random.Range(Data.moonsArr[0].id, Data.moonsArr[Data.moonsArr.Length - 1].id);
	}

	private static ResourcePoint[] GetResourcePoints(Moon moon)
	{
		ResourcePoint[] result = new ResourcePoint[(int)(moon.mass * 10)];

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
			int rnd = Random.Range(0, rangeProbab[rangeProbab.Length - 1]);
			for (int j = 0; j < rangeProbab.Length; j++)
			{
				if (rnd < rangeProbab[j])
				{
					PlanetResources planetRes = new PlanetResources
					{
						idResource = idsRes[j],
						extraction = Random.Range(20, 80),
					};
					ResourcePoint resPoint = new ResourcePoint { resource = planetRes };
					result[i] = resPoint;
					break;
				}
			}
		}
		return result;
	}
}
