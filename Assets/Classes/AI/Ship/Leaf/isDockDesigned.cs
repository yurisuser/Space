namespace AI.AIShip { 
	class IsDockDesigned : Condition<Ship>
	{
		public override EStateNode Tick(Ship ship)
		{
			if (ship.order.dock.parent.position == ship.order.destinationStep)
			{
				return EStateNode.SUCCESS;
			}
			return EStateNode.FAILURE;
		}
	}
}
