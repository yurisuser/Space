﻿public static class StationManager
{
	public static void Tick()
	{
		for (int i = 0; i < Galaxy.StarSystemsArr.Length; i++)
		{
			for (int j = 0; j < Galaxy.StarSystemsArr[i].StationArr.Length; j++)
			{
				StationProducer.Tick(i, j);
			}
		}
	}
}