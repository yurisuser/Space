namespace AI.AIShip
{
	class BehaveDocking : AIBehaviour<Ship>
	{
		public BehaveDocking()
		{
			behav =
			new Selector<Ship>(
				new Sequence<Ship>(
					"if inSpace",
					new IsShipState(EShipState.inSpace),
					new Selector<Ship>(
						"Fly or stop",
						new Sequence<Ship>(
							"docking if arrived",
							new IsWayPointsEmpty(),
							new SetShipState(EShipState.docking),
							new StopMove()
							),
						new NextStep()
						)
					),
				new Sequence<Ship>(
					"if docking",
					new IsShipState(EShipState.docking),
					new ToDock()
					)
				);
		}
	}
}
