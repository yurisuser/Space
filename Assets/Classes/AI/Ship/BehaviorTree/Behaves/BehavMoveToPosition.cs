namespace AIShip
{
	public class BehavMoveToPosition : ShipBehaviour
	{
		public BehavMoveToPosition()
		{
			behav = new Sequence(
				new StartStepInit(),
				new Invertor(new IsDesignedDestinationOrder()),
				new CalculateDestinationStep()
			);
		}

		public override EStateNode Tick(Ship ship)
		{
			if (behav.Tick(ship) == EStateNode.FAILURE) return EStateNode.FAILURE;
			return EStateNode.SUCCESS;
		}
	}
}