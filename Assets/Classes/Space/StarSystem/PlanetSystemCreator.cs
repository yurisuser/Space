public class PlanetSystemCreator
{
	public static PlanetSystem GetRandomPlanetSystem(int numberPlanet, Star motherStar)
	{
		Planet planet = PlanetCreator.CreatePlanet(numberPlanet, motherStar);
		PlanetSystem planetSystem = new PlanetSystem
		{
			planet = planet,
			moonsArray = MoonCreator.CreateMoonsArray(planet),
		};
		return planetSystem;
	}
}
