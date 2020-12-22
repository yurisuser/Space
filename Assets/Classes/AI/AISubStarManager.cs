namespace AI.AISubStar
{
	public static class AISubStarManager
	{
		private static SubStarBehave subStarBehave = new SubStarBehave();

		public static void Tick()
		{
			for (int i = 0; i < Galaxy.StarSystemsArr.Length; i++)
			{
				for (int s = 0; s < Galaxy.StarSystemsArr[i].StationArr.Length; s++)
				{
					subStarBehave.Tick(Galaxy.StarSystemsArr[i].StationArr[s]);
				}
			}
		}
	}
}
