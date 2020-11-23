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
	}
}
