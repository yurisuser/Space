using System;

public static class StarCreator
{
	private static int subStarClass = 0; //подкласс
	private static int ranges = 10; //количество подклассов
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

		subStarClass = Rnd.Next(1, ranges);

		Star star = new Star();
		star.id = id;
		star.indexSystem = indexStarSystem;
		star.starClass = starClass;
		star.subStarClass = subStarClass;
		star.colorName = Data.starsArr[numInDataStarArr].colorName;
		star.mass = ClarificationRnd(Data.starsArr[numInDataStarArr].mass_min, Data.starsArr[numInDataStarArr].mass_max);
		star.temperature = RoundingTemperature(ClarificationRnd(Data.starsArr[numInDataStarArr].temperature_min, Data.starsArr[numInDataStarArr].temperature_max), 2);
		star.radius = ClarificationRnd(Data.starsArr[numInDataStarArr].radius_min, Data.starsArr[numInDataStarArr].radius_max);
		star.luminosity = ClarificationRnd(Data.starsArr[numInDataStarArr].luminosity_min, Data.starsArr[numInDataStarArr].luminosity_max);
		star.name = GetName(star);

		return star;
	}

	private static int RoundingTemperature(float value, int ranks)
	{
		var r = Math.Pow(10, ranks);
		return (int)(((int)(value / r)) * r);
	}

	private static float ClarificationRnd(float min, float max)
	{
		//уточненное значение, исходя из поддиапазона основного диапазона значений
		float range = max - min;
		float subRange = range / (ranges + 1);
		float subRangeStart = (min + subRange * (subStarClass));
		float subRangeEnd = (min + subRange * subStarClass + 1);
		var result = Rnd.Next(subRangeStart, subRangeEnd);
		return result;
	}

	private static string GetName(Star star)
	{
		return $"Unknown star";
	}
}
