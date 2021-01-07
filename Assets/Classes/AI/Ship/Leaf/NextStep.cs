using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.AIShip
{
	public class NextStep : Leaf<Ship>
	{
		public override EStateNode Tick(Ship ship)
		{
			ship.order.currentPosition = ship.order.destinationStep;
			if (ship.order.wayPoints.Count == 0) 
			{
				return EStateNode.SUCCESS;
			}
			ship.order.destinationStep = ship.order.wayPoints.Dequeue();
			return EStateNode.SUCCESS;
		}
	}
}
