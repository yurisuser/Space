using AI;

namespace AI
{
	public class StartStepInit : Leaf<Ship>
	{
		public override EStateNode Tick(Ship ship)
		{
			ship.order.attribute.currentPosition = ship.order.attribute.destinationStep;
			return EStateNode.SUCCESS;
		}
	}
}