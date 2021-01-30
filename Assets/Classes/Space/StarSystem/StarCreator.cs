using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public static class StarCreator
{
	public static Star CreateStar(int indexStarSystem)
	{
		int[] probabilitysStarsArr = new int[Data.starsArr.Length];
		probabilitysStarsArr[0] = Data.starsArr[0].probability;
		for (int i = 1; i < Data.starsArr.Length; i++)
		{
			probabilitysStarsArr[i] = probabilitysStarsArr[i - 1] + Data.starsArr[i].probability;
		}
		int rnd = Rnd.Next(0, probabilitysStarsArr[probabilitysStarsArr.Length - 1]);
		int id = Galaxy.GetNextId();
		string starClass = null;
		int numInDataStarArr = 0;

		for (int i = 0; i < probabilitysStarsArr.Length; i++)
		{
			if (rnd <= probabilitysStarsArr[i])
			{
				starClass = Data.starsArr[i].starClass;
				numInDataStarArr = i;
				break;
			}
		}

		if (indexStarSystem == 0)//Central black hole
		{
			starClass = Array.Find(Data.starsArr, x => x.starClass == "BH").starClass;
		}

		Star star = new Star();

		star.id = id;
		star.indexSystem = indexStarSystem;
		star.starClass = starClass;
		star.colorName = Data.starsArr[numInDataStarArr].colorName;
		star.temperature = Rnd.Next(Data.starsArr[numInDataStarArr].temperature_min, Data.starsArr[numInDataStarArr].temperature_max);
		star.mass = Rnd.Next(Data.starsArr[numInDataStarArr].mass_min, Data.starsArr[numInDataStarArr].mass_max);
		star.radius = Rnd.Next(Data.starsArr[numInDataStarArr].radius_min, Data.starsArr[numInDataStarArr].radius_max);
		star.luminosity = Rnd.Next(Data.starsArr[numInDataStarArr].luminosity_min, Data.starsArr[numInDataStarArr].luminosity_max);
		star.name = GetName(star);

		return star;
	}

	private static string GetName(Star star)
	{
		return $"St {star.id}";
	}
}
