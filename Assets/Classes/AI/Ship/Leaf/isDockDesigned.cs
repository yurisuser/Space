using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.AIShip { 
	class isDockDesigned : Condition<Ship>
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
