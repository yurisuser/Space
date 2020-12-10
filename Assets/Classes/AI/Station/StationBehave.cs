namespace AI.AIStation
{
	public class StationBehave : AIBehaviour<Station>
	{
		
		public StationBehave()
		{
			behav = new Selector<Station>(
				new WorkProducingModules()
				);
		}

		public override EStateNode Tick(Station station)
		{
			return behav.Tick(station);
		}
	}
}
