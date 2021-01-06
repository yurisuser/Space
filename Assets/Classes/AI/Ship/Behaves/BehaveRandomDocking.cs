using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.AIShip
{
	class BehaveRandomDocking : AIBehaviour<Ship>
	{
		public BehaveRandomDocking()
		{
			behav = new Sequence<Ship>(
				new Selector<Ship>(
					new Invertor<Ship> ( new isDockDesigned()),
					new Docking()
					),
				new Selector<Ship> (
					new IsValidOrder(),
					new CreateNewOrderDocking()					
					)
				);
		}
	}
}
