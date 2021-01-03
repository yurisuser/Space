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
			ship.order.attribute.currentPosition = ship.order.attribute.destinationStep;
			ship.order.attribute.destinationStep = ship.order.attribute.wayPoints.Dequeue();
			return EStateNode.SUCCESS;
		}
	}
}
