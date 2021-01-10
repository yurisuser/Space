namespace AI.AISubStar
{
	public class SubStarBehave : AIBehaviour<SubStarBody>
	{
		
		public SubStarBehave()
		{
			behav = new Sequence<SubStarBody>(
				new WorkProducingConstruction(),
				new WorkDock()
				);
		}

		public override EStateNode Tick(SubStarBody body)
		{
			return behav.Tick(body);
		}
	}
}
