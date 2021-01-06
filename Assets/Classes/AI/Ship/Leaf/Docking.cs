using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.AIShip
{
	class Docking : Leaf<Ship>
	{
		public override EStateNode Tick(Ship ship)
		{
			ship.order.attribute.dock.ToDocking(ship);
			return EStateNode.SUCCESS;
		}
	}
}
