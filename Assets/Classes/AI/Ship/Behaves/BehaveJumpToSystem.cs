using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.AIShip
{
	public class BehaveJumpToSystem : AIBehaviour<Ship>
	{
		public BehaveJumpToSystem()
		{
			behav = 
				new Selector<Ship>(
					new Sequence<Ship>(
					"if inSpace",
					new IsShipState(EShipState.inSpace),
					new Selector<Ship>(
						"Fly or stop",
						new Sequence<Ship>(
							"hypering if arrived",
							new IsWayPointsEmpty(),
							new SetShipState(EShipState.hypering),
							new StopMove()
							),
						new NextStep()
						)
					),
					new Sequence<Ship>(
						"if hypering",
						new IsShipState(EShipState.hypering),
						new ToHyper()
					),
					new Sequence<Ship>(
						"repeat",
						new IsShipState(EShipState.unHypering),
						new SetShipState(EShipState.inSpace),
						new CreateNewOrder(EOrders.JumpToSystem)
						)
				);
		}
	}
}
