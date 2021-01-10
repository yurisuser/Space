namespace AI.AIShip
{
	public class IsWayPointsEmpty : Leaf<Ship>
	{
		public override EStateNode Tick(Ship ship)
		{
			if (ship.order.wayPoints.Count == 0)
			{
				return EStateNode.SUCCESS;
			}
			return EStateNode.FAILURE;
		}
	}
}
