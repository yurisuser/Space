namespace AI.AIShip
{
	class StopMove : Leaf<Ship>
	{
		public override EStateNode Tick(Ship ship)
		{
			ship.order.currentPosition = ship.order.destinationStep;
			return EStateNode.SUCCESS;
		}
	}
}
