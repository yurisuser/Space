namespace AIShip
{
	public class BehavPatrol : ShipBehaviour
	{
		public BehavPatrol()
		{
			behav =	new Repeater(
				new Sequence(
					new StartStepInit(),
					new Invertor(new BehavMoveToPosition()),
					new GetRandomPosition()
					)
				);
		}
	}
}