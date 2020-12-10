namespace AI.AIStation
{
	public static class AIStationManager
	{
		private static StationBehave stationBehave = new StationBehave();

		public static void Tick()
		{
			for (int i = 0; i < Galaxy.StarSystemsArr.Length; i++)
			{
				for (int s = 0; s < Galaxy.StarSystemsArr[i].StationArr.Length; s++)
				{
					stationBehave.Tick(Galaxy.StarSystemsArr[i].StationArr[s]);
				}
			}
		}
	}
}
