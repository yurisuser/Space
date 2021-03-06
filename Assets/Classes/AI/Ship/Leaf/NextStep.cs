﻿namespace AI.AIShip
{
	public class NextStep : Leaf<Ship>
	{
		public override EStateNode Tick(Ship ship)
		{
			ship.order.currentPosition = ship.order.destinationStep;
			ship.order.destinationStep = ship.order.wayPoints.Dequeue();
			return EStateNode.SUCCESS;
		}
	}
}
