namespace AI.AIShip
{
	class BehaveDockingTest : AIBehaviour<Ship>
	{
		public BehaveDockingTest()
		{
			behav = new Selector<Ship>(
				new BehaveDocking(),
				new Selector<Ship>(
					new Sequence<Ship>(
						new IsShipState(EShipState.undocking),
						new SetShipState(EShipState.undocked)
						),
					new Sequence<Ship>(
						new IsShipState(EShipState.undocked),
						new SetShipState(EShipState.inSpace),
						new CreateNewOrder(EOrders.DockingTest)
						)
					)
				); ;
		}
	}
}
