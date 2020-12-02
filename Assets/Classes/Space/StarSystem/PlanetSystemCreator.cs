using System;
using Random = UnityEngine.Random;

public class PlanetSystemCreator
{
	public static PlanetSystem GetRandomPlanetSystem(int numberPlanet, Star motherStar)
	{
		Planet planet = CreatePlanet(numberPlanet, motherStar);
		PlanetSystem planetSystem = new PlanetSystem
		{
			planet = planet,
			moonsArray = CreateMoonsArray(7, planet),
		};
		return planetSystem;
	}

	private static Planet CreatePlanet(int numberPlanet, Star motherStar)
	{
		return new Planet
		{
			id = Galaxy.GetNextId(),
			motherStar = motherStar,
			type = GetPlanetType(motherStar.type),
			name = motherStar.name + " " + (char)(numberPlanet + 65),
			rotateSpeed = 10,
			orbitSpeed = 100,
			size = 1,
			orbitNumber = numberPlanet,
			angleOnOrbit = 15 * numberPlanet,
		};
	}

	private static int GetPlanetType(int starType)
	{
		int[] probabilityesForStarType = new int[Data.planetsArr.Length];
		int[] rangeProbabilityes = new int[probabilityesForStarType.Length];
		for (int i = 0; i < probabilityesForStarType.Length; i++)
		{
			probabilityesForStarType[i] = Data.planetsOfStarProbabilityArr[i].probabilityOfStar[starType];
		}
		rangeProbabilityes[0] = probabilityesForStarType[0];
		for (int i = 1; i < probabilityesForStarType.Length; i++)
		{
			rangeProbabilityes[i] = rangeProbabilityes[i - 1] + probabilityesForStarType[i];
		}
		int type = 0;
		int rnd = Random.Range(0, rangeProbabilityes[rangeProbabilityes.Length - 1]);
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

	private static Moon[] CreateMoonsArray(int moonsCount, Planet planet)
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
			size = Random.Range(.1f, planet.size / 2),
			type = (EMoonTypes)Random.Range(0, Enum.GetNames(typeof(EMoonTypes)).Length)
		};
		return moon;
	}
}