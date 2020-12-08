using AI;

namespace AI
{
	public class IsDesignedDestinationOrder : Condition<Ship>
	{
		public override EStateNode Tick(Ship ship)
		{
			if (ship.position == ship.order.attribute.destinationOrder) return EStateNode.SUCCESS;
			return EStateNode.FAILURE;
		}
	}
}