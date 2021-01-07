using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.AIShip
{
	class BehaveDockingTest : AIBehaviour<Ship>
	{
		public BehaveDockingTest()
		{
			behav = new Sequence<Ship>(
				new Selector<Ship>(
					new Invertor<Ship>(new isDockDesigned()),
					new Docking()
					),
				new Selector<Ship>(
					new IsValidOrder(),
					new CreateNewOrder(EOrders.DockingTest)
					),
				new NextStep()

				);
		}
	}
}
