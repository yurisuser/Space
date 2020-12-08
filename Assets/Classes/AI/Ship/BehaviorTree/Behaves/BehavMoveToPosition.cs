using AI;

namespace AI
{
	public class BehavMoveToPosition : AIBehaviour<Ship>
	{
		public BehavMoveToPosition()
		{
			behav = new Sequence<Ship>(
				new StartStepInit(),
				new Invertor<Ship>(new IsDesignedDestinationOrder()),
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