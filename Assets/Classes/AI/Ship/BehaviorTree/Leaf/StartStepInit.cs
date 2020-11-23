namespace AIShip
{
	public class StartStepInit : Leaf
	{
		public override EStateNode Tick(Ship ship)
		{
			ship.order.attribute.currentPosition = ship.order.attribute.destinationStep;
			return EStateNode.SUCCESS;
		}
	}
}