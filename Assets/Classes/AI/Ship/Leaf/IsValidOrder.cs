namespace AI.AIShip
{
	class IsValidOrder : Condition<Ship>
	{
		public override EStateNode Tick(Ship ship)
		{
			if (ship.order == null || ship.order.wayPoints == null || ship.order.wayPoints.Count == 0)
			{
				return EStateNode.FAILURE;
			}
			return EStateNode.SUCCESS;
		}
	}
}
