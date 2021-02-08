namespace AI.AISubStar
{
	public static class AISubStarManager
	{
		private static SubStarBehave subStarBehave = new SubStarBehave();

		public static void Tick()
		{

			for (int i = 0; i < Galaxy.StarSystemsArr.Length; i++)
			{
				//station
				if (Galaxy.StarSystemsArr[i].StationArr == null) continue;
				for (int s = 0; s < Galaxy.StarSystemsArr[i].StationArr.Length; s++)
				{
					subStarBehave.Tick(Galaxy.StarSystemsArr[i].StationArr[s]);
				}
				//planet
				if (Galaxy.StarSystemsArr[i].planetSystemsArray == null) continue;
				for (int p = 0; p < Galaxy.StarSystemsArr[i].planetSystemsArray.Length; p++)
				{
					subStarBehave.Tick(Galaxy.StarSystemsArr[i].planetSystemsArray[p].planet);
					//moon
					if (Galaxy.StarSystemsArr[i].planetSystemsArray[p].moonsArray == null) continue;
					for (int m = 0; m < Galaxy.StarSystemsArr[i].planetSystemsArray[p].moonsArray.Length; m++)
					{
						subStarBehave.Tick(Galaxy.StarSystemsArr[i].planetSystemsArray[p].moonsArray[m]);
					}
				}
			}
		}
	}
}
