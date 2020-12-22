namespace AI.AISubStar
{
	public class SubStarBehave : AIBehaviour<SubStarBody>
	{
		
		public SubStarBehave()
		{
			behav = new Selector<SubStarBody>(
				new WorkProducingModules()
				);
		}

		public override EStateNode Tick(SubStarBody station)
		{
			return behav.Tick(station);
		}
	}
}
