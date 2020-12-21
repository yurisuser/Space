using System;
using Random = UnityEngine.Random;

public class PlanetSystemCreator
{
	public static PlanetSystem GetRandomPlanetSystem(int numberPlanet, Star motherStar)
	{
		Planet planet = PlanetCreator.CreatePlanet(numberPlanet, motherStar);
		PlanetSystem planetSystem = new PlanetSystem
		{
			planet = planet,
			moonsArray = CreateMoonsArray(7, planet),
		};
		return planetSystem;
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
			mass = Random.Range(.1f, planet.mass / 2),
			type = 1
		};
		return moon;
	}
}