using System;
public static class Rnd
{
	private static int seed = 123;
	private static Random rndTrue = new Random();
	private static Random rnd = new Random(seed);

	public static void SetSeed(int seedNew)
	{
		seed = seedNew;
	}
	public static int Next(int minValue, int maxValue)
	{
		return rnd.Next(minValue, maxValue);
	}

	public static float Next(float minValue, float maxValue)
	{
		return (float)(rnd.NextDouble() * (maxValue - minValue) + minValue);
	}

	public static int Next()
	{
		return rnd.Next();
	}

	public static int NextTrue(int minValue, int maxValue)
	{
		return rndTrue.Next(minValue, maxValue);
	}

	public static float NextTrue(float minValue, float maxValue)
	{
		return (float)(rndTrue.NextDouble() * (maxValue - minValue) + minValue);
	}

	public static int NextTrue()
	{
		return rndTrue.Next();
	}
}
